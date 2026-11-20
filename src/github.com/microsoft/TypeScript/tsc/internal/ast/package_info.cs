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
global using coreꓸResolutionMode = go.github.com.microsoft.TypeScript.tsc.@internal.core_package.ModuleKind;
// </ImportedTypeAliases>

using go;
using static go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;

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
[assembly: GoTypeAlias("AbstractKeyword", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("AccessExpression", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("AccessorDeclaration", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("AccessorKeyword", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("AdditiveOperator", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("AdditiveOperatorOrHigher", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("AnyImportOrRequireStatement", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("AnyImportSyntax", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("AnyValidImportOrReExport", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ArgumentList", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList")]
[assembly: GoTypeAlias("ArrayBindingElement", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ArrayDestructuringAssignment", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ArrayLiteralExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ArrayTypeNodeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ArrowFunctionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("AsExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("AssertKeyword", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("AssertionExpression", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("AssertsKeyword", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("AssignmentOperator", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("AssignmentOperatorOrHigher", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("AssignmentOperatorToken", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("AsteriskToken", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("AsyncKeyword", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("AwaitExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("AwaitKeyword", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("BigIntLiteralNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("BinaryExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("BinaryOperator", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("BinaryOperatorToken", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("BindingElementList", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList")]
[assembly: GoTypeAlias("BindingElementNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("BindingName", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("BindingPatternNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("BitwiseOperator", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("BitwiseOperatorOrHigher", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("BlockNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("BlockOrExpression", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("BooleanLiteral", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("BreakOrContinueStatement", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("BreakStatementNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("CallExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("CallLikeExpression", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("CallOrNewExpression", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("CallSignatureDeclarationNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("CaseBlockNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("CaseClausesList", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList")]
[assembly: GoTypeAlias("CaseKeyword", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("CaseOrDefaultClauseNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("CatchClauseNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ClassDeclarationNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ClassElement", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ClassElementList", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList")]
[assembly: GoTypeAlias("ClassExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ClassLikeDeclaration", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ClassStaticBlockDeclarationNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ColonToken", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("CompoundAssignmentOperator", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("ComputedPropertyNameNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ConciseBody", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ConditionalExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ConditionalTypeNodeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ConstKeyword", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ConstructSignatureDeclarationNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ConstructorDeclarationNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ConstructorTypeNodeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ContinueStatementNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("DebuggerStatementNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("Declaration", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("DeclarationName", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("DeclareKeyword", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("DecoratorNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("DefaultKeyword", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("DeleteExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("DestructuringAssignment", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("DoStatementNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("DotDotDotToken", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("DotToken", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ElementAccessExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ElementList", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList")]
[assembly: GoTypeAlias("EmptyStatementNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("EndOfFile", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("EntityName", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("EnumDeclarationNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("EnumMemberList", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList")]
[assembly: GoTypeAlias("EnumMemberNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("EqualityOperator", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("EqualityOperatorOrHigher", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("EqualsGreaterThanToken", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("EqualsToken", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ExclamationToken", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ExponentiationOperator", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("ExportAssignmentNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ExportDeclarationNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ExportKeyword", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ExportSpecifierList", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList")]
[assembly: GoTypeAlias("ExportSpecifierNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("Expression", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ExpressionStatementNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ExpressionWithTypeArgumentsList", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList")]
[assembly: GoTypeAlias("ExpressionWithTypeArgumentsNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ExternalModuleReferenceNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("FalseLiteral", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("FlowLabel", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FlowNode")]
[assembly: GoTypeAlias("ForInOrOfStatementNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ForInitializer", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ForStatementNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("FunctionBody", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("FunctionDeclarationNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("FunctionExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("FunctionLikeDeclaration", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("FunctionTypeNodeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("GetAccessorDeclarationNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("HeritageClauseElement", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("HeritageClauseElementList", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList")]
[assembly: GoTypeAlias("HeritageClauseList", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList")]
[assembly: GoTypeAlias("HeritageClauseNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("IdentifierNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("IfStatementNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ImportAttributeList", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList")]
[assembly: GoTypeAlias("ImportAttributeName", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ImportAttributeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ImportAttributesNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ImportClauseNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ImportClauseOrBindingPattern", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ImportDeclarationNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ImportEqualsDeclarationNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ImportExpression", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ImportPhaseModifierSyntaxKind", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("ImportSpecifierList", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList")]
[assembly: GoTypeAlias("ImportSpecifierNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ImportTypeNodeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("InKeyword", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("IncrementExpression", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("IndexSignatureDeclarationNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("IndexedAccessTypeNodeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("InferTypeNodeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("InterfaceDeclarationNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("IntersectionTypeNodeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocAllTypeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocAugmentsTagNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocCallbackTagNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocComment", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocDeprecatedTagNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocFullName", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocImplementsTagNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocImportTagNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocLinkCodeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocLinkNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocLinkPlainNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocNameReferenceNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocNodeSyntaxKind", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("JSDocNonNullableTypeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocNullableTypeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocOptionalTypeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocOverloadTagNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocOverrideTagNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocParameterOrPropertyTagNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocPrivateTagNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocProtectedTagNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocPublicTagNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocReadonlyTagNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocReturnTagNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocSatisfiesTagNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocSeeTagNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocSignatureNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocTag", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocTemplateTagNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocTextNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocThisTagNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocThrowsTagNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocTypeExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocTypeLiteralNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocTypeTagNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocTypedefTagNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocUnknownTagNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JSDocVariadicTypeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JsxAttributeLike", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JsxAttributeList", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList")]
[assembly: GoTypeAlias("JsxAttributeName", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JsxAttributeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JsxAttributeValue", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JsxAttributesNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JsxChild", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JsxChildList", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList")]
[assembly: GoTypeAlias("JsxClosingElementNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JsxClosingFragmentNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JsxElementNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JsxExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JsxFragmentNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JsxNamespacedNameNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JsxOpeningElementNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JsxOpeningFragmentNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JsxOpeningLikeElement", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JsxSelfClosingElementNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JsxSpreadAttributeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JsxTagNameExpression", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JsxTextNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("JsxTokenSyntaxKind", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("KeywordExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("KeywordExpressionSyntaxKind", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("KeywordSyntaxKind", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("KeywordTypeNodeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("KeywordTypeSyntaxKind", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("LabeledStatementNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("LeftHandSideExpression", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("LiteralExpression", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("LiteralLikeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("LiteralSyntaxKind", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("LiteralToken", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("LiteralTypeNodeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("LogicalOperator", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("LogicalOperatorOrHigher", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("LogicalOrCoalescingAssignmentOperator", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("MappedTypeNodeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("MemberName", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("MetaPropertyNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("MethodDeclarationNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("MethodSignatureDeclarationNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("MinusToken", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("MissingDeclarationNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("Modifier", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ModifierLike", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ModifierSyntaxKind", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("ModuleBlockNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ModuleBody", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ModuleDeclarationNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ModuleExportName", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ModuleName", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ModuleReference", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("MultiplicativeOperator", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("MultiplicativeOperatorOrHigher", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("NamedExportBindings", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("NamedExportsNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("NamedImportBindings", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("NamedImportsNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("NamedImportsOrExports", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("NamedMember", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("NamedTupleMemberNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("NamespaceExportDeclarationNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("NamespaceExportNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("NamespaceImportNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("NewExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("NoSubstitutionTemplateLiteralNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("NodeBody", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("NonNullExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("NotEmittedStatementNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("NotEmittedTypeElementNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("NullLiteral", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("NumericLiteralNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("NumericOrStringLikeLiteral", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ObjectDestructuringAssignment", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ObjectLiteralElement", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ObjectLiteralElementLike", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ObjectLiteralExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ObjectLiteralLike", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ObjectLiteralLikeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ObjectTypeDeclaration", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("OmittedExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("OptionalTypeNodeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("OutKeyword", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("OverrideKeyword", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ParameterDeclarationNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ParameterList", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList")]
[assembly: GoTypeAlias("ParenthesizedExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ParenthesizedTypeNodeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("PartiallyEmittedExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("PlusToken", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("PostfixUnaryExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("PostfixUnaryOperator", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("PragmaKindFlags", "byte")]
[assembly: GoTypeAlias("PrefixUnaryExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("PrefixUnaryOperator", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("PrivateIdentifierNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("PrivateKeyword", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("PropertyAccessExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("PropertyAssignmentNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("PropertyDeclarationNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("PropertyDefinitionList", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList")]
[assembly: GoTypeAlias("PropertyName", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("PropertyNameLiteral", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("PropertySignatureDeclarationNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ProtectedKeyword", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("PseudoLiteralSyntaxKind", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("PseudoLiteralToken", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("PublicKeyword", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("PunctuationSyntaxKind", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("QualifiedNameNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("QuestionDotToken", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("QuestionToken", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ReadonlyKeyword", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("RegularExpressionLiteralNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("RelationalOperator", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("RelationalOperatorOrHigher", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("RestTypeNodeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ReturnStatementNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("SatisfiesExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("SemicolonClassElementNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("SetAccessorDeclarationNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ShiftOperator", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("ShiftOperatorOrHigher", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("ShorthandPropertyAssignmentNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("SignatureDeclaration", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("SourceFileNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("SpreadAssignmentNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("SpreadElementNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("Statement", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("StatementList", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList")]
[assembly: GoTypeAlias("StaticKeyword", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("StringLiteralLike", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("StringLiteralLikeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("StringLiteralNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("SuperExpression", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("SwitchStatementNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("SyntaxListNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("SyntheticExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("SyntheticReferenceExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TaggedTemplateExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TemplateExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TemplateHeadNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TemplateLiteral", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TemplateLiteralLikeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TemplateLiteralToken", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TemplateLiteralTypeNodeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TemplateLiteralTypeSpanList", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList")]
[assembly: GoTypeAlias("TemplateLiteralTypeSpanNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TemplateMiddleNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TemplateMiddleOrTail", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TemplateSpanList", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList")]
[assembly: GoTypeAlias("TemplateSpanNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TemplateTailNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ThisExpression", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ThisTypeNodeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ThrowStatementNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TokenNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TokenSyntaxKind", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("TriviaSyntaxKind", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind")]
[assembly: GoTypeAlias("TrueLiteral", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TryStatementNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TupleTypeNodeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TypeAliasDeclarationNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TypeArgumentList", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList")]
[assembly: GoTypeAlias("TypeAssertionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TypeElement", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TypeElementList", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList")]
[assembly: GoTypeAlias("TypeList", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList")]
[assembly: GoTypeAlias("TypeLiteralNodeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TypeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TypeOfExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TypeOnlyImportDeclaration", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TypeOperatorNodeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TypeParameterDeclarationNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TypeParameterList", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList")]
[assembly: GoTypeAlias("TypePredicateNodeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TypePredicateParameterName", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TypeQueryNodeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("TypeReferenceNodeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("UnionOrIntersectionTypeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("UnionTypeNodeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("ValidImportTypeNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("VariableDeclarationListNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("VariableDeclarationNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("VariableDeclarationNodeList", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList")]
[assembly: GoTypeAlias("VariableOrParameterDeclaration", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("VariableOrPropertyDeclaration", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("VariableStatementNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("VoidExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("WhileStatementNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("WithStatementNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
[assembly: GoTypeAlias("YieldExpressionNode", "go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node")]
// </ExportedTypeAliases>

// As types are cast to interfaces in Go source code, the go2cs code converter
// will generate an assembly level `GoImplement` attribute for each unique cast.
// This allows the interface to be implemented in the C# source code using source
// code generation (see go2cs-gen). Resolving each duck-typed cast at compile time
// this way is what keeps startup free of reflection.

// <InterfaceImplementations>
[assembly: GoImplement<ArrayLiteralExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<ArrayTypeNode, nodeData>(Pointer = true)]
[assembly: GoImplement<ArrowFunction, nodeData>(Pointer = true)]
[assembly: GoImplement<AsExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<AwaitExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<BigIntLiteral, nodeData>(Pointer = true)]
[assembly: GoImplement<BinaryExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<BindingElement, nodeData>(Pointer = true)]
[assembly: GoImplement<BindingPattern, nodeData>(Pointer = true)]
[assembly: GoImplement<Block, nodeData>(Pointer = true)]
[assembly: GoImplement<BreakStatement, nodeData>(Pointer = true)]
[assembly: GoImplement<CallExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<CallSignatureDeclaration, nodeData>(Pointer = true)]
[assembly: GoImplement<CaseBlock, nodeData>(Pointer = true)]
[assembly: GoImplement<CaseOrDefaultClause, nodeData>(Pointer = true)]
[assembly: GoImplement<CatchClause, nodeData>(Pointer = true)]
[assembly: GoImplement<ClassDeclaration, nodeData>(Pointer = true)]
[assembly: GoImplement<ClassExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<ClassStaticBlockDeclaration, nodeData>(Pointer = true)]
[assembly: GoImplement<ComputedPropertyName, nodeData>(Pointer = true)]
[assembly: GoImplement<ConditionalExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<ConditionalTypeNode, nodeData>(Pointer = true)]
[assembly: GoImplement<ConstructSignatureDeclaration, nodeData>(Pointer = true)]
[assembly: GoImplement<ConstructorDeclaration, nodeData>(Pointer = true)]
[assembly: GoImplement<ConstructorTypeNode, nodeData>(Pointer = true)]
[assembly: GoImplement<ContinueStatement, nodeData>(Pointer = true)]
[assembly: GoImplement<DebuggerStatement, nodeData>(Pointer = true)]
[assembly: GoImplement<Decorator, nodeData>(Pointer = true)]
[assembly: GoImplement<DeleteExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<DoStatement, nodeData>(Pointer = true)]
[assembly: GoImplement<ElementAccessExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<EmptyStatement, nodeData>(Pointer = true)]
[assembly: GoImplement<EnumDeclaration, nodeData>(Pointer = true)]
[assembly: GoImplement<EnumMember, nodeData>(Pointer = true)]
[assembly: GoImplement<ExportAssignment, nodeData>(Pointer = true)]
[assembly: GoImplement<ExportDeclaration, nodeData>(Pointer = true)]
[assembly: GoImplement<ExportSpecifier, nodeData>(Pointer = true)]
[assembly: GoImplement<ExpressionStatement, nodeData>(Pointer = true)]
[assembly: GoImplement<ExpressionWithTypeArguments, nodeData>(Pointer = true)]
[assembly: GoImplement<ExternalModuleReference, nodeData>(Pointer = true)]
[assembly: GoImplement<FlowReduceLabelData, nodeData>(Pointer = true)]
[assembly: GoImplement<FlowSwitchClauseData, nodeData>(Pointer = true)]
[assembly: GoImplement<ForInOrOfStatement, nodeData>(Pointer = true)]
[assembly: GoImplement<ForStatement, nodeData>(Pointer = true)]
[assembly: GoImplement<FunctionDeclaration, nodeData>(Pointer = true)]
[assembly: GoImplement<FunctionExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<FunctionTypeNode, nodeData>(Pointer = true)]
[assembly: GoImplement<GetAccessorDeclaration, nodeData>(Pointer = true)]
[assembly: GoImplement<HeritageClause, nodeData>(Pointer = true)]
[assembly: GoImplement<Identifier, nodeData>(Pointer = true)]
[assembly: GoImplement<IfStatement, nodeData>(Pointer = true)]
[assembly: GoImplement<ImportAttribute, nodeData>(Pointer = true)]
[assembly: GoImplement<ImportAttributes, nodeData>(Pointer = true)]
[assembly: GoImplement<ImportClause, nodeData>(Pointer = true)]
[assembly: GoImplement<ImportDeclaration, nodeData>(Pointer = true)]
[assembly: GoImplement<ImportEqualsDeclaration, nodeData>(Pointer = true)]
[assembly: GoImplement<ImportSpecifier, nodeData>(Pointer = true)]
[assembly: GoImplement<ImportTypeNode, nodeData>(Pointer = true)]
[assembly: GoImplement<IndexSignatureDeclaration, nodeData>(Pointer = true)]
[assembly: GoImplement<IndexedAccessTypeNode, nodeData>(Pointer = true)]
[assembly: GoImplement<InferTypeNode, nodeData>(Pointer = true)]
[assembly: GoImplement<InterfaceDeclaration, nodeData>(Pointer = true)]
[assembly: GoImplement<IntersectionTypeNode, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDoc, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocAllType, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocAugmentsTag, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocCallbackTag, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocDeprecatedTag, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocImplementsTag, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocImportTag, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocLink, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocLinkCode, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocLinkPlain, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocNameReference, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocNonNullableType, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocNullableType, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocOptionalType, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocOverloadTag, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocOverrideTag, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocParameterOrPropertyTag, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocPrivateTag, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocProtectedTag, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocPublicTag, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocReadonlyTag, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocReturnTag, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocSatisfiesTag, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocSeeTag, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocSignature, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocTemplateTag, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocText, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocThisTag, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocThrowsTag, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocTypeExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocTypeLiteral, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocTypeTag, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocTypedefTag, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocUnknownTag, nodeData>(Pointer = true)]
[assembly: GoImplement<JSDocVariadicType, nodeData>(Pointer = true)]
[assembly: GoImplement<JsxAttribute, nodeData>(Pointer = true)]
[assembly: GoImplement<JsxAttributes, nodeData>(Pointer = true)]
[assembly: GoImplement<JsxClosingElement, nodeData>(Pointer = true)]
[assembly: GoImplement<JsxClosingFragment, nodeData>(Pointer = true)]
[assembly: GoImplement<JsxElement, nodeData>(Pointer = true)]
[assembly: GoImplement<JsxExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<JsxFragment, nodeData>(Pointer = true)]
[assembly: GoImplement<JsxNamespacedName, nodeData>(Pointer = true)]
[assembly: GoImplement<JsxOpeningElement, nodeData>(Pointer = true)]
[assembly: GoImplement<JsxOpeningFragment, nodeData>(Pointer = true)]
[assembly: GoImplement<JsxSelfClosingElement, nodeData>(Pointer = true)]
[assembly: GoImplement<JsxSpreadAttribute, nodeData>(Pointer = true)]
[assembly: GoImplement<JsxText, nodeData>(Pointer = true)]
[assembly: GoImplement<KeywordExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<KeywordTypeNode, nodeData>(Pointer = true)]
[assembly: GoImplement<LabeledStatement, nodeData>(Pointer = true)]
[assembly: GoImplement<LiteralTypeNode, nodeData>(Pointer = true)]
[assembly: GoImplement<MappedTypeNode, nodeData>(Pointer = true)]
[assembly: GoImplement<MetaProperty, nodeData>(Pointer = true)]
[assembly: GoImplement<MethodDeclaration, nodeData>(Pointer = true)]
[assembly: GoImplement<MethodSignatureDeclaration, nodeData>(Pointer = true)]
[assembly: GoImplement<MissingDeclaration, nodeData>(Pointer = true)]
[assembly: GoImplement<ModuleBlock, nodeData>(Pointer = true)]
[assembly: GoImplement<ModuleDeclaration, nodeData>(Pointer = true)]
[assembly: GoImplement<NamedExports, nodeData>(Pointer = true)]
[assembly: GoImplement<NamedImports, nodeData>(Pointer = true)]
[assembly: GoImplement<NamedTupleMember, nodeData>(Pointer = true)]
[assembly: GoImplement<NamespaceExport, nodeData>(Pointer = true)]
[assembly: GoImplement<NamespaceExportDeclaration, nodeData>(Pointer = true)]
[assembly: GoImplement<NamespaceImport, nodeData>(Pointer = true)]
[assembly: GoImplement<NewExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<NoSubstitutionTemplateLiteral, nodeData>(Pointer = true)]
[assembly: GoImplement<Node, go.github.com.microsoft.TypeScript.tsc.@internal.debug_package.FailBadSyntaxKind_node>(Pointer = true)]
[assembly: GoImplement<NodeFactory, NodeFactoryCoercible>(Pointer = true)]
[assembly: GoImplement<NonNullExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<NotEmittedStatement, nodeData>(Pointer = true)]
[assembly: GoImplement<NotEmittedTypeElement, nodeData>(Pointer = true)]
[assembly: GoImplement<NumericLiteral, nodeData>(Pointer = true)]
[assembly: GoImplement<ObjectLiteralExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<OmittedExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<OptionalTypeNode, nodeData>(Pointer = true)]
[assembly: GoImplement<ParameterDeclaration, nodeData>(Pointer = true)]
[assembly: GoImplement<ParenthesizedExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<ParenthesizedTypeNode, nodeData>(Pointer = true)]
[assembly: GoImplement<PartiallyEmittedExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<PostfixUnaryExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<PrefixUnaryExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<PrivateIdentifier, nodeData>(Pointer = true)]
[assembly: GoImplement<PropertyAccessExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<PropertyAssignment, nodeData>(Pointer = true)]
[assembly: GoImplement<PropertyDeclaration, nodeData>(Pointer = true)]
[assembly: GoImplement<PropertySignatureDeclaration, nodeData>(Pointer = true)]
[assembly: GoImplement<QualifiedName, nodeData>(Pointer = true)]
[assembly: GoImplement<RegularExpressionLiteral, nodeData>(Pointer = true)]
[assembly: GoImplement<RestTypeNode, nodeData>(Pointer = true)]
[assembly: GoImplement<ReturnStatement, nodeData>(Pointer = true)]
[assembly: GoImplement<SatisfiesExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<SemicolonClassElement, nodeData>(Pointer = true)]
[assembly: GoImplement<SetAccessorDeclaration, nodeData>(Pointer = true)]
[assembly: GoImplement<ShorthandPropertyAssignment, nodeData>(Pointer = true)]
[assembly: GoImplement<SourceFile, HasFileName>(Pointer = true)]
[assembly: GoImplement<SourceFile, SourceFileLike>(Pointer = true)]
[assembly: GoImplement<SourceFile, nodeData>(Pointer = true)]
[assembly: GoImplement<SpreadAssignment, nodeData>(Pointer = true)]
[assembly: GoImplement<SpreadElement, nodeData>(Pointer = true)]
[assembly: GoImplement<StringLiteral, nodeData>(Pointer = true)]
[assembly: GoImplement<SwitchStatement, nodeData>(Pointer = true)]
[assembly: GoImplement<SyntaxList, nodeData>(Pointer = true)]
[assembly: GoImplement<SyntheticExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<SyntheticReferenceExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<TaggedTemplateExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<TemplateExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<TemplateHead, nodeData>(Pointer = true)]
[assembly: GoImplement<TemplateLiteralTypeNode, nodeData>(Pointer = true)]
[assembly: GoImplement<TemplateLiteralTypeSpan, nodeData>(Pointer = true)]
[assembly: GoImplement<TemplateMiddle, nodeData>(Pointer = true)]
[assembly: GoImplement<TemplateSpan, nodeData>(Pointer = true)]
[assembly: GoImplement<TemplateTail, nodeData>(Pointer = true)]
[assembly: GoImplement<ThisTypeNode, nodeData>(Pointer = true)]
[assembly: GoImplement<ThrowStatement, nodeData>(Pointer = true)]
[assembly: GoImplement<Token, nodeData>(Pointer = true)]
[assembly: GoImplement<TryStatement, nodeData>(Pointer = true)]
[assembly: GoImplement<TupleTypeNode, nodeData>(Pointer = true)]
[assembly: GoImplement<TypeAliasDeclaration, nodeData>(Pointer = true)]
[assembly: GoImplement<TypeAssertion, nodeData>(Pointer = true)]
[assembly: GoImplement<TypeLiteralNode, nodeData>(Pointer = true)]
[assembly: GoImplement<TypeOfExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<TypeOperatorNode, nodeData>(Pointer = true)]
[assembly: GoImplement<TypeParameterDeclaration, nodeData>(Pointer = true)]
[assembly: GoImplement<TypePredicateNode, nodeData>(Pointer = true)]
[assembly: GoImplement<TypeQueryNode, nodeData>(Pointer = true)]
[assembly: GoImplement<TypeReferenceNode, nodeData>(Pointer = true)]
[assembly: GoImplement<UnionTypeNode, nodeData>(Pointer = true)]
[assembly: GoImplement<VariableDeclaration, nodeData>(Pointer = true)]
[assembly: GoImplement<VariableDeclarationList, nodeData>(Pointer = true)]
[assembly: GoImplement<VariableStatement, nodeData>(Pointer = true)]
[assembly: GoImplement<VoidExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<WhileStatement, nodeData>(Pointer = true)]
[assembly: GoImplement<WithStatement, nodeData>(Pointer = true)]
[assembly: GoImplement<YieldExpression, nodeData>(Pointer = true)]
[assembly: GoImplement<hasFileNameImpl, HasFileName>(Pointer = true)]
// </InterfaceImplementations>

// <ImplicitConversions>
// </ImplicitConversions>

// The struct types whose fields this package reaches through a pointer, as Go spells it, `p.f`:
// go2cs-gen emits a field property on the pointer for each of their fields.
[assembly: GoFieldAccess(typeof(ArrayLiteralExpression))]
[assembly: GoFieldAccess(typeof(ArrayTypeNode))]
[assembly: GoFieldAccess(typeof(ArrowFunction))]
[assembly: GoFieldAccess(typeof(BigIntLiteral))]
[assembly: GoFieldAccess(typeof(BinaryExpression))]
[assembly: GoFieldAccess(typeof(BinaryOperatorToken))]
[assembly: GoFieldAccess(typeof(BindingElement))]
[assembly: GoFieldAccess(typeof(Block))]
[assembly: GoFieldAccess(typeof(BodyBase))]
[assembly: GoFieldAccess(typeof(CaseBlock))]
[assembly: GoFieldAccess(typeof(CatchClause))]
[assembly: GoFieldAccess(typeof(CheckJsDirective))]
[assembly: GoFieldAccess(typeof(ClassDeclaration))]
[assembly: GoFieldAccess(typeof(ClassExpression))]
[assembly: GoFieldAccess(typeof(ClassStaticBlockDeclaration))]
[assembly: GoFieldAccess(typeof(CompositeBase))]
[assembly: GoFieldAccess(typeof(ConditionalExpression))]
[assembly: GoFieldAccess(typeof(ConditionalTypeNode))]
[assembly: GoFieldAccess(typeof(ConstructorDeclaration))]
[assembly: GoFieldAccess(typeof(ConstructorTypeNode))]
[assembly: GoFieldAccess(typeof(ContentMapperSourceFileInfo))]
[assembly: GoFieldAccess(typeof(DeclarationBase))]
[assembly: GoFieldAccess(typeof(DeclarationName))]
[assembly: GoFieldAccess(typeof(DiagnosticsCollection))]
[assembly: GoFieldAccess(typeof(ElementAccessExpression))]
[assembly: GoFieldAccess(typeof(ElementList))]
[assembly: GoFieldAccess(typeof(EnumDeclaration))]
[assembly: GoFieldAccess(typeof(EnumMember))]
[assembly: GoFieldAccess(typeof(ExportAssignment))]
[assembly: GoFieldAccess(typeof(ExportDeclaration))]
[assembly: GoFieldAccess(typeof(ExportSpecifier))]
[assembly: GoFieldAccess(typeof(ExportableBase))]
[assembly: GoFieldAccess(typeof(Expression))]
[assembly: GoFieldAccess(typeof(FlowReduceLabelData))]
[assembly: GoFieldAccess(typeof(FlowSwitchClauseData))]
[assembly: GoFieldAccess(typeof(ForInOrOfStatement))]
[assembly: GoFieldAccess(typeof(ForInitializer))]
[assembly: GoFieldAccess(typeof(ForStatement))]
[assembly: GoFieldAccess(typeof(FunctionDeclaration))]
[assembly: GoFieldAccess(typeof(FunctionExpression))]
[assembly: GoFieldAccess(typeof(FunctionLikeBase))]
[assembly: GoFieldAccess(typeof(FunctionLikeWithBodyBase))]
[assembly: GoFieldAccess(typeof(GetAccessorDeclaration))]
[assembly: GoFieldAccess(typeof(HeritageClause))]
[assembly: GoFieldAccess(typeof(HeritageClauseElementList))]
[assembly: GoFieldAccess(typeof(IfStatement))]
[assembly: GoFieldAccess(typeof(ImportAttribute))]
[assembly: GoFieldAccess(typeof(ImportAttributeList))]
[assembly: GoFieldAccess(typeof(ImportAttributes))]
[assembly: GoFieldAccess(typeof(ImportClause))]
[assembly: GoFieldAccess(typeof(ImportDeclaration))]
[assembly: GoFieldAccess(typeof(ImportEqualsDeclaration))]
[assembly: GoFieldAccess(typeof(ImportSpecifier))]
[assembly: GoFieldAccess(typeof(ImportTypeNode))]
[assembly: GoFieldAccess(typeof(IndexSignatureDeclaration))]
[assembly: GoFieldAccess(typeof(IndexedAccessTypeNode))]
[assembly: GoFieldAccess(typeof(InferTypeNode))]
[assembly: GoFieldAccess(typeof(InterfaceDeclaration))]
[assembly: GoFieldAccess(typeof(IntersectionTypeNode))]
[assembly: GoFieldAccess(typeof(JSDoc))]
[assembly: GoFieldAccess(typeof(JSDocAugmentsTag))]
[assembly: GoFieldAccess(typeof(JSDocCallbackTag))]
[assembly: GoFieldAccess(typeof(JSDocDeprecatedTag))]
[assembly: GoFieldAccess(typeof(JSDocImplementsTag))]
[assembly: GoFieldAccess(typeof(JSDocImportTag))]
[assembly: GoFieldAccess(typeof(JSDocLink))]
[assembly: GoFieldAccess(typeof(JSDocLinkCode))]
[assembly: GoFieldAccess(typeof(JSDocLinkPlain))]
[assembly: GoFieldAccess(typeof(JSDocNameReference))]
[assembly: GoFieldAccess(typeof(JSDocOverloadTag))]
[assembly: GoFieldAccess(typeof(JSDocOverrideTag))]
[assembly: GoFieldAccess(typeof(JSDocParameterOrPropertyTag))]
[assembly: GoFieldAccess(typeof(JSDocPrivateTag))]
[assembly: GoFieldAccess(typeof(JSDocProtectedTag))]
[assembly: GoFieldAccess(typeof(JSDocPublicTag))]
[assembly: GoFieldAccess(typeof(JSDocReadonlyTag))]
[assembly: GoFieldAccess(typeof(JSDocReturnTag))]
[assembly: GoFieldAccess(typeof(JSDocSatisfiesTag))]
[assembly: GoFieldAccess(typeof(JSDocSeeTag))]
[assembly: GoFieldAccess(typeof(JSDocTemplateTag))]
[assembly: GoFieldAccess(typeof(JSDocText))]
[assembly: GoFieldAccess(typeof(JSDocThisTag))]
[assembly: GoFieldAccess(typeof(JSDocThrowsTag))]
[assembly: GoFieldAccess(typeof(JSDocTypeLiteral))]
[assembly: GoFieldAccess(typeof(JSDocTypeTag))]
[assembly: GoFieldAccess(typeof(JSDocTypedefTag))]
[assembly: GoFieldAccess(typeof(JSDocUnknownTag))]
[assembly: GoFieldAccess(typeof(JsxAttribute))]
[assembly: GoFieldAccess(typeof(JsxChild))]
[assembly: GoFieldAccess(typeof(JsxElement))]
[assembly: GoFieldAccess(typeof(JsxExpression))]
[assembly: GoFieldAccess(typeof(JsxFragment))]
[assembly: GoFieldAccess(typeof(JsxNamespacedName))]
[assembly: GoFieldAccess(typeof(JsxText))]
[assembly: GoFieldAccess(typeof(LiteralLikeNodeBase))]
[assembly: GoFieldAccess(typeof(LiteralTypeNode))]
[assembly: GoFieldAccess(typeof(LocalsContainerBase))]
[assembly: GoFieldAccess(typeof(MappedTypeNode))]
[assembly: GoFieldAccess(typeof(MetaProperty))]
[assembly: GoFieldAccess(typeof(MethodDeclaration))]
[assembly: GoFieldAccess(typeof(MethodSignatureDeclaration))]
[assembly: GoFieldAccess(typeof(MissingDeclaration))]
[assembly: GoFieldAccess(typeof(ModifierList))]
[assembly: GoFieldAccess(typeof(ModuleDeclaration))]
[assembly: GoFieldAccess(typeof(ModuleReference))]
[assembly: GoFieldAccess(typeof(NamedExportBindings))]
[assembly: GoFieldAccess(typeof(NamedImportBindings))]
[assembly: GoFieldAccess(typeof(NamedTupleMember))]
[assembly: GoFieldAccess(typeof(NamespaceExport))]
[assembly: GoFieldAccess(typeof(NamespaceExportDeclaration))]
[assembly: GoFieldAccess(typeof(NamespaceImport))]
[assembly: GoFieldAccess(typeof(NoSubstitutionTemplateLiteral))]
[assembly: GoFieldAccess(typeof(Node))]
[assembly: GoFieldAccess(typeof(NodeDefault))]
[assembly: GoFieldAccess(typeof(NodeFactory))]
[assembly: GoFieldAccess(typeof(NodeList))]
[assembly: GoFieldAccess(typeof(NodeVisitor))]
[assembly: GoFieldAccess(typeof(NumericLiteral))]
[assembly: GoFieldAccess(typeof(ObjectLiteralExpression))]
[assembly: GoFieldAccess(typeof(ParameterDeclaration))]
[assembly: GoFieldAccess(typeof(ParameterList))]
[assembly: GoFieldAccess(typeof(PositionMap))]
[assembly: GoFieldAccess(typeof(PostfixUnaryExpression))]
[assembly: GoFieldAccess(typeof(PrefixUnaryExpression))]
[assembly: GoFieldAccess(typeof(PropertyAccessExpression))]
[assembly: GoFieldAccess(typeof(PropertyAssignment))]
[assembly: GoFieldAccess(typeof(PropertyDeclaration))]
[assembly: GoFieldAccess(typeof(PropertySignatureDeclaration))]
[assembly: GoFieldAccess(typeof(QualifiedName))]
[assembly: GoFieldAccess(typeof(RegularExpressionLiteral))]
[assembly: GoFieldAccess(typeof(SetAccessorDeclaration))]
[assembly: GoFieldAccess(typeof(ShorthandPropertyAssignment))]
[assembly: GoFieldAccess(typeof(SourceFile))]
[assembly: GoFieldAccess(typeof(StringLiteral))]
[assembly: GoFieldAccess(typeof(SwitchStatement))]
[assembly: GoFieldAccess(typeof(Symbol))]
[assembly: GoFieldAccess(typeof(SyntheticExpression))]
[assembly: GoFieldAccess(typeof(SyntheticReferenceExpression))]
[assembly: GoFieldAccess(typeof(TaggedTemplateExpression))]
[assembly: GoFieldAccess(typeof(TemplateExpression))]
[assembly: GoFieldAccess(typeof(TemplateHead))]
[assembly: GoFieldAccess(typeof(TemplateLiteralLikeNodeBase))]
[assembly: GoFieldAccess(typeof(TemplateLiteralTypeNode))]
[assembly: GoFieldAccess(typeof(TemplateLiteralTypeSpan))]
[assembly: GoFieldAccess(typeof(TemplateMiddle))]
[assembly: GoFieldAccess(typeof(TemplateSpan))]
[assembly: GoFieldAccess(typeof(TemplateTail))]
[assembly: GoFieldAccess(typeof(TryStatement))]
[assembly: GoFieldAccess(typeof(TypeAliasDeclaration))]
[assembly: GoFieldAccess(typeof(TypeList))]
[assembly: GoFieldAccess(typeof(TypeOperatorNode))]
[assembly: GoFieldAccess(typeof(TypeParameterDeclaration))]
[assembly: GoFieldAccess(typeof(TypePredicateNode))]
[assembly: GoFieldAccess(typeof(TypeQueryNode))]
[assembly: GoFieldAccess(typeof(TypeReferenceNode))]
[assembly: GoFieldAccess(typeof(UnionTypeNode))]
[assembly: GoFieldAccess(typeof(VariableDeclaration))]
[assembly: GoFieldAccess(typeof(VariableDeclarationList))]
[assembly: GoFieldAccess(typeof(VariableDeclarationNodeList))]
[assembly: GoFieldAccess(typeof(VariableStatement))]
[assembly: GoFieldAccess(typeof(YieldExpression))]
[assembly: GoFieldAccess(typeof(sourceFileDataCell<>))]

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/ast/ast.go", "ast.cs", "ACAugs6CgpSmgoKCpqaigpSmooKUAAwagqaCgoKCgoKUxoKCpoKmgqaCxrKCgoKCpqaCgoKUAAkUgoKCgsaClIKClIKmgoKCAAsUgoKCgoLGooKCgoIADyiCgoKMgqqCgoKCgoKCgoKCgoKCgoKCgpSCgoKClAAICrK0goKClKaCgoKUpoKCgpSmgoKClKaClKSkpKSkpKSkpKSkpKSkpKSmgpSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKaClKSkpMaCgpS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tPiClKSkpoKCgpSmgpSkpKSkpKSkpKSmgoKClKaClKSkpKSkgoLGpoKCgpSmgpSkpKSkpKSmgoKClKaClKSkpKSmgoKClKaClKTIgoKClKaCgoKUpoKUpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpILGxqKClLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLSClAAICoKUpKSkpKSkpKSkpMaCgpS0tLS0tLS0tLS0+IKUpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpoKUpKSkpoKCgpSmgpSkpKSkpKiClKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSmgoKClKaClKSkpKaClKSkpKaClKSkpoKUpKSkpoKUpKSmgpSkpKSkpKSmgpSkpKaCgoKUpoKUpKSkpKSmgoKClKaClKSkpKSkpKSkpKaClKSkpKSCgpSmgpSkpKSkpoKUpKSkpKSkpKaClKSkqqKCgpSCgpSUqoKmggAhRIKUgoKCgoKCgoKCgoKCgoKCpoqmgqaCAAgqgqaCpqKCpqKClIKkgpSkgoKUlIKCgpSUgoSCgpSCgtqCtqaigpaCpgABMqiolKSmpKSkpKiClLS0pqjYooKUhoKYgpaCmKaigoKUlKSCgpSkgoKUpIKCgoKUpqSClKSEgpSmgpSkpIKUpMiClKSkpAAKGrSiyriSoMi0osqClLi4gqaClLi2soKUgoKCpoKUqrKClIKCgqaCgoKClMqEgoKClKaEypS0gpQAAi6kpKSkpKSkpKTGgsaCxoLKgsyExoKEgpSmgsaCgpTagsyCqILGgpSkpMiCxoKClAADEILGgsyCgpSCggADGoLKlJSCgpQABRCCxoLGgpSkpOiixoLKgoKU3oKClL6CxoKClNyixoKClNqCgpTagsaixoLOgoKU3pKgorSCgpQAAxKCxpSCgpQAAxSCyIKClIKCAAMcgsiCAAIQgsiCyoKUpKTKtILGgsaCgpTGgoyCgqamoMSCxoIAAhKCxoKCggACGILGgqaCxoKmgsaCgoKUqoLGgsaCroLGgqqCxoLGgsaCxoIABBCCxoLGgsaCyoLMhMaCpoLGgsiigqaUhoKmgoKUgoKUlIKU3IKmgsaCgpTGgoKUxoKClMaCzILIgsqCzILMgsiCxoLGgsqCxoLGgsaCzoIAKWKCpoKCkpSm0oKmgpSClIKUgoIAUsQBgoKUgoKCgoKCxoKmgqiCgpSogoKUrIKClKqCgpSqgqaCgpSmgoKUACBAgoKUqKKClKaCgpSogoKUqIKClKiCpoKClMaCgoKCnrSClILGgqaCpoKmgqaCpoKmgqaCpoKmgqaCptKCloKCgpSWgoSClIKClILWgqaCpoKmsqaCpqSClIKCgoKCgoKCgoKCgoKCpqKCgoKmgsaigoKClMbSgoKCgoKCgoKCptqigpSCkoaCgpS4goKClJSUlKaCqKKClKaCgoK87oKCgoKCgpSUgpSClIKCgoL4goKUgpSkpKSkpKSkpKSkpOjSgoKClNailJKCgriCoqSCgoKCgpaEgqampoKkgsiClLSCgoKUgpTIqIKCgoKmyIKGgpqCgoKUguyUtKSklILGgoKCgoKUgpSCpgAPHIIALl6CzIIAAhCyxoI=", "202-206:1;203-205:1.1;2224-2226:1;2406-2408:1;2675-2677:1;2684-2696:1;2853-2879:1;2857-2875:1.1;2889-2891:1;2896-2899:1;2980-2985:1;2988-3082:2", "", "2089=propagateSubtreeFacts/2/3/1,propagateSubtreeFacts/3/3/2,IfElse/1/1/3;2101=propagateSubtreeFacts/2/2/1;2118=propagateSubtreeFacts/1/1/1;2125=propagateEraseableSyntaxSubtreeFacts/1/2/1,propagateEraseableSyntaxSubtreeFacts/2/2/2,propagateSubtreeFacts/2/2/3;2131=IfElse/1/1/1;2164=propagateSubtreeFacts/1/2/1,propagateEraseableSyntaxSubtreeFacts/1/2/2,propagateEraseableSyntaxSubtreeFacts/2/2/3,propagateSubtreeFacts/2/2/4;2175=propagateSubtreeFacts/2/3/1,propagateSubtreeFacts/3/3/2,IfElse/1/1/3;2186=propagateSubtreeFacts/1/3/1,propagateSubtreeFacts/2/3/2,propagateEraseableSyntaxListSubtreeFacts/1/1/3,propagateNodeListSubtreeFacts/1/1/4,propagateEraseableSyntaxSubtreeFacts/1/2/5,propagateEraseableSyntaxSubtreeFacts/2/2/6,propagateSubtreeFacts/3/3/7,IfElse/1/2/8,IfElse/2/2/9;2204=propagateSubtreeFacts/1/1/1,propagateEraseableSyntaxListSubtreeFacts/1/1/2,propagateNodeListSubtreeFacts/1/2/3,propagateNodeListSubtreeFacts/2/2/4;2247=propagateSubtreeFacts/2/2/1;2256=propagateSubtreeFacts/1/1/1,propagateNodeListSubtreeFacts/1/1/2;2266=propagateSubtreeFacts/1/2/1,propagateSubtreeFacts/2/2/2;2280=propagateSubtreeFacts/1/2/1,propagateSubtreeFacts/2/2/2;2298=propagateSubtreeFacts/2/2/1;2308=propagateSubtreeFacts/2/2/1;2329=propagateSubtreeFacts/1/3/1,propagateSubtreeFacts/2/3/2,propagateSubtreeFacts/3/3/3,IfElse/1/1/4;2338=propagateSubtreeFacts/2/2/1;2360=propagateEraseableSyntaxListSubtreeFacts/1/1/1,propagateNodeListSubtreeFacts/1/1/2,propagateEraseableSyntaxSubtreeFacts/1/2/3,propagateEraseableSyntaxSubtreeFacts/2/2/4,propagateSubtreeFacts/1/1/5;2376=propagateSubtreeFacts/1/2/1,propagateEraseableSyntaxListSubtreeFacts/1/1/2,propagateNodeListSubtreeFacts/1/1/3,propagateEraseableSyntaxSubtreeFacts/1/2/4,propagateEraseableSyntaxSubtreeFacts/2/2/5,propagateSubtreeFacts/2/2/6;2381=propagateSubtreeFacts/1/1/1;2392=propagateSubtreeFacts/1/3/1,propagateSubtreeFacts/2/3/2,propagateEraseableSyntaxSubtreeFacts/1/3/3,propagateEraseableSyntaxListSubtreeFacts/1/1/4,propagateNodeListSubtreeFacts/1/1/5,propagateSubtreeFacts/3/3/6,propagateEraseableSyntaxSubtreeFacts/2/3/7,propagateEraseableSyntaxSubtreeFacts/3/3/8,IfElse/1/2/9,IfElse/2/2/10;2397=propagateSubtreeFacts/1/1/1;2403=propagateSubtreeFacts/1/2/1,propagateEraseableSyntaxSubtreeFacts/1/2/2,propagateEraseableSyntaxSubtreeFacts/2/2/3,propagateSubtreeFacts/2/2/4;2407=propagateSubtreeFacts/1/1/1;2413=propagateSubtreeFacts/1/1/1;2458=propagateSubtreeFacts/1/4/1,propagateSubtreeFacts/2/4/2,propagateSubtreeFacts/3/4/3,propagateSubtreeFacts/4/4/4,IsPrivateIdentifier/1/1/5,IfElse/1/1/5;2482=propagateEraseableSyntaxListSubtreeFacts/1/1/1,propagateNodeListSubtreeFacts/1/1/2,propagateEraseableSyntaxSubtreeFacts/1/2/3,propagateEraseableSyntaxSubtreeFacts/2/2/4,propagateSubtreeFacts/1/1/5,ModifierFlags/1/1/6,IfElse/1/1/6;2494=propagateSubtreeFacts/1/3/1,propagateSubtreeFacts/2/3/2,propagateEraseableSyntaxListSubtreeFacts/1/1/3,propagateNodeListSubtreeFacts/1/1/4,propagateEraseableSyntaxSubtreeFacts/1/2/5,propagateEraseableSyntaxSubtreeFacts/2/2/6,propagateSubtreeFacts/3/3/7,IfElse/1/2/8,IfElse/2/2/9;2528=propagateSubtreeFacts/2/3/1,propagateSubtreeFacts/3/3/2;2544=propagateSubtreeFacts/2/2/1,propagateEraseableSyntaxListSubtreeFacts/1/1/2,propagateNodeListSubtreeFacts/1/1/3,IfElse/1/1/4;2554=propagateEraseableSyntaxListSubtreeFacts/1/1/1,propagateNodeListSubtreeFacts/1/1/2;2582=propagateSubtreeFacts/2/3/1,propagateEraseableSyntaxListSubtreeFacts/1/1/2,propagateSubtreeFacts/3/3/3;2606=propagateSubtreeFacts/2/3/1,propagateSubtreeFacts/3/3/2;2612=propagateSubtreeFacts/2/3/1,propagateSubtreeFacts/3/3/2;2634=propagateEraseableSyntaxListSubtreeFacts/1/1/1;2710=propagateNodeListSubtreeFacts/1/1/1,propagateSubtreeFacts/2/2/2;2722=propagateSubtreeFacts/2/2/1;2728=propagateEraseableSyntaxListSubtreeFacts/1/1/1,propagateSubtreeFacts/2/2/2;2734=propagateEraseableSyntaxListSubtreeFacts/1/1/1,propagateSubtreeFacts/2/2/2;2758=propagateSubtreeFacts/2/2/1;3298=IsStringOrNumericLiteralLike/1/1/1,literalIsName/1/1/1,IsPrivateIdentifier/1/1/2;3592=NewTextRange/1/1/1;3646=visit/2/5/2,visit/3/5/2,visit/4/5/4,visit/5/5/4,visitNodeList/1/1/5")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/ast/ast_generated.go", "ast_generated.cs", "ANQCGADqAZgJgoLGosailKQAChqCgoKCxqLGogAJGIKCgoLGosaiAAwegoKCgqaigpTGgqaypqKmgsiiAAoagoKCpqKClMaCprKmoqaCxqIAChqCgoKmooKUxoKmsqaixqIACBaCgsaCxqIADB6CgoKCgqaigpTGgqaypqKmgsqiAAoagoKCgqaigpTGgqaypqKmgsiiAAoagoKCgqaigpTGgqaypqKmgsiiAA0ggoKCgoKCpqKClMaCrLKmoqaCzKIADiKCgoKCgoKmooKUxoKssqaixqKmogAJGIKCgqaigpTGgqaypqLGogAJGIKCgqaigpTGgqaypqLGogAKGoKCgqaigpTGgqaypqLGogALHIKCgoKmooKUxoKmsqaipoLIogALHIKCgoKmooKUxoKmsqaipoLIogALHIKCgqaigpTGgqaypqKmgsaiAAwegoKCgqaigpTGgqaypqKmgsiipqIAChqCgoKmooKUxoKmsqaipoLGogAMHoKCgoKCpqKClMaCprKmoqaCyqIADB6CgoKCpqKClMaCprKmosaiAAgWgoLGgsaiAAoagoKCgqaigpTGgqaypqKmgsiiAAkYgoKCpqKClMaCprKmoqaCxqIADB6CgoKCpqKClMaCprKmoqaCxqIACxyigoKCprKClMaCprKmosaiAA8kooKCgoKCprKClMaCrLKmoqaCxqIAChqCgoKCgqaigpTGgqaypqLGogAKGoKCgqaigpTGgqaypqLGoqaiABAmsoKCgoKCgoKmwoKUxoIAAhCypqKmgsaiABAmooKCgoKCprKClMaCrLKmoqaCxqIAChqigoKmsoKUxoKmsqaCxqIADySygoKCgoKCgoKCpsKClMaCAAIUsqaipoLGogAKGrKCgoKCgoKmwoKUxoKusqaipoLGogAKGrKCgoKCgoKmwoKUxoKusqaipoLGogALHIKCgoKmooKUxoKmsqaixqIAECaygoKCgoKCpsKClMaCrrKmoqaCxqIAECaygoKCgoKmsoKCgoKCpsKClKSkxsaCrLKmopSkpNiCxqKmogAMHqKCgoKmsoKUxoKmsqaipoLGogAOIrKCgoKCpsKClMaCprKmoqaCxqIAChqCgoKmooKUxoKmsqaipoLGogAIFoKCxoLGogAJGIKCxoLGogAOIqKCgoKCgqaigoKCgoKmsoKUpKTGxoKssqailKSk2ILMoqaiAAkYgoKCpqKClMaCprKmoqaCxqIACxyigoKmsoKUxoKmsqaipoKmgsaiAAoagoKCpqKClMaCprKmoqaCxqIADiKigoKCgoKmsoKUxoKmsqaixqIADB6ygoKCpsKClMaCprKmoqaCxqIAChqigoKmsoKUxoKmsqaipoKmgsaiAAoagoKCpqKClMaCprKmoqaCxqIADiKigoKCgqaygpTGgqaypqKmgsaiAAwegoKCgoKmooKUxoKmsqaixqIADB6CgoKCgqaigpTGgqaypqLGogAOIqKCgoKCgoKCprKClMaCAAIQsqaixqIACBaygoKCgoKCgoKmwoKUxoIAAhKypqKmgsaiAAgWsoKCgoKCgoKCpsKClMaCAAISsqaipoLGogAOIqKCgoKCprKClMaCprKmosaiAA0gsoKCgoKCgoKmwoKUxoIAAhCypqKmgsaiAA8ksoKCgoKCgoKCgoKmwoKUxoIAAhaypqKmgsaiAA4isoKCgoKCgqbCgpTGgq6ypqKmgsaiAA4isoKCgoKCgqbCgpTGgq6ypqKmgsaiAAoagoLGgsaiAA8kooKCgqaygpTGgqaypqLGogAIFoKCxoLGogAJGIKCxqLGopSkAAgWgoKCgoLGosaiAAgWgoKCgoLGosaiAAgWgoKCgoLGosaiAAgWgoKCgoLGosaiAAoagoKCgoLGosaiAA8kooKCgoKCgqaygpTGgq6ypqLGogAKGoKCgoKmooKUxoKmsqaipoLGogAKGoKCgoKmooKUxoKmsqaipoLGogAKGoKCgoKmooKUxoKmsqaixqIADiKigoKCgoKCgoKmsoKUxoIAAhKypqLGogAPJLKCgoKCgoKCgoKmwoKUxoIAAhSypqKmgsaiAAoagoKCgqaigpTGgqaypqLGogAKGoKCgoKmooKUxoKmsqaixqIADiKCgoKCgoKCpqKClMaCrrKmoqaCzqIADSCigoKCgoKCprKClMaCprKmoqaCxqIADSCCgoKCgoKCpqKClMaCprKmoqaCyqIADiKCgoKCgoKCgqaigpTGgqyypqLGogAMHoKCgoKCpqKClMaCprKmosaiAAweooKCgqaygpTGgqaypqKmgsaiAAkYgoKCgoKmooKUxoKmsqaixqIACRiCgoKmooKUxoKmsqaixqIACxyCgoKCpqKClMaCprKmoqaCyKIAChqCgoKCpqKClMaCprKmoqaCyKIADSCCgoKCgoKCgqaigpTGgqyypqLGogAJGIKCgqaigpTGgqaypqKmgsaiAAscgoKCgqaigpTGgqaypqKmgsaiAAwegoKCgqaigpTGgqaypqKmgsaiAAscgoKCpqKClMaCprKmosaiAA4isoKCgoKCgqbCgpTGgq6ypqKmgsaiAA8ksoKCgoKCgoKmwoKUxoIAAhCypqKmgsaiAAkYgoKCpqKClMaCprKmoqaCxqIACRiCgoKmooKUxoKmsqaipoLGogAJGIKCgqaigpTGgqaypqKmgsaiAAkYgoKCpqKClMaCprKmosaiAAoagoKCgqaigpTGgqaypqLGogAIFoKCxqLGopSkAAgWgoKCpqKClMaCprKmosaiAAgWgoKCpqKClMaCprKmosaiAA0ggoKCgoKCpqKClMaCrLKmosaiAAoagoKCgqaigpTGgqaypqLGogAJGIKCgqaigpTGgqaypqLGogAJGIKCgqaigpTGgqaypqLGogAKGoKCgoKmooKUxoKmsqaixqIACRiCgoKCpqKClMaCprKmosaiAAscgoKCgqaigpTGgqaypqLGogAJGIKCgqaigpTGgqaypqLGogAIFoKCxoLGogALHIKCgoKCpqKClMaCprKmosaiAAscooKCgqaygpTGgqaypqKmgqiCxqIADB6CgoKCgqaigpTGgqaypqKmgsaiAAkYgoKCgqaigpTGgqaypqLGogAQJoKCgoKCgoKCpqKClMaCAAIQsqaixqIAChqCgoKmooKUxoKmsqaixqIACRiCgoKmooKUxoKmsqaixqIADSCigoKCgoKmsoKUxoKssqaipoLGogAJGIKCgqaigpTGgqaypqLGogAJGIKCgqaigpTGgqaypqLGogAJGIKCgqaigpTGgqaypqLGogAIFoKCgoKCpqKClMaCprKmosaiAAgWooKCgoKCprKClMaCrLKmosaiAAkYgoKCgoKCxqLGogAJGIKCgoKCgsaixqIACRiCgoKCgoLGosaiAAoagoKCgqaigpTGgqaypqLGogAKGoKCgoKmooKUxoKmsqaixqIACxyCgoKCgqaigpTGgqaypqLGogAJGIKCgqaigpTGgqaypqKmgsaiAAwegoKCgoKmooKUxoKmsqaixqIACxyCgoKmooKUxoKmsqaixqIACxyigoKCprKClMaCprKmoqaCxqIADB6CgoKCgqaigpTGgqaypqLGogAMHoKCgoKCpqKClMaCprKmosaiAAwegoKCgoKmooKUxoKmsqaixqIACBaCgsaCxqIACBaCgsaCxqIADB6igoKCprKClMaCprKmoqaCxqIAChqCgoKmooKUxoKmsqaixqIACRiCgoKmooKUxoKmsqaixqIAChqCgoKCpqKClMaCprKmosaiAAoagoKCgoLGosaiAAkYgoKCpqKClMaCprKCpqLGogAKGoKCgoKmooKUxoKmsqaixqIACRiCgoKmooKUxoKmsqaixqIACRiCgoKmooKUxoKmsqaixqIACRiCgoKmooKUxoKmsqaixqIACBaCgsaCxqIACRiCgoKmooKUxoKmsqaixqIACRiCgoKmooKUxoKmsqaixqIACRiCgoKCgqaigpTGgqaypqLGogAIFoKCgoKmooKUxoKmsqaixqIAChqCgoKCgoKmooKUxoKssqaixqIACRiCgoKCgqaigpTGgqaypqLGogAIFoKCgoKmooKUxoKmsqaixqIACBaCgoKCpqKClMaCprKmosaiAAgWgoKCgqaigpTGgqaypqLGogAIFoKCgoKmooKUxoKmsqaixqIACBaCgoKCpqKClMaCprKmosaiAAgWgoKCgqaigpTGgqaypqLGogAJGIKCgoKCpqKClMaCprKmosaiAAkYgoKCgoKmooKUxoKmsqaixqIACRiCgoKCgqaigpTGgqaypqLGogAJGIKCgoKCpqKClMaCprKmosaiAAkYgoKCgoKmooKUxoKmsqaixqIACRiCgoKCgqaigpTGgqaypqLGogALHIKCgoKCgoKmooKUxoKusqaixqIAChqigoKCgoKmsoKUxoKssqaipoLGogAJGIKCgoKCpqKClMaCprKmosaiAAoaooKCgoKCprKClMaCrLKmoqaCxqIAChqCgoKCgqaigpTGgqaypqLGogAJGKKCgqaygpTGgqaypqKmgsairqIAESqygoKCgoKCpsKClMaCrLKmoqaCxqIADySygoKCgoKmwoKUxoKmsqaipoLGogAPJKKCgoKCgoKmsoKUxoKssqaixqIADB6CgoKCgoKCpqKClMaCrLKmosaiAA4iooKCgoKmsoKUxoKmsqaipoLGogAOIqKCgoKCprKClMaCprKmoqaCxqIACBaCgoKCxqLGogAJGKKCgoKCprKClMaCprKmoqaCxqIACRiigoKCgqaygpTGgqaypqKmgsaiAAkYooKCgoKmsoKUxoKmsqaipoLGogAPJLKCgoKCgoKmwoKUxoKusqaipoLGogAKGoKCgoKmooKUxoKmsqaipoLIogALHIKCgoKmooKUxoKmsoKmosaiAAweooKCgoKCgoKmsoKUxoKmgqaipoLGoqaizoKUpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKQABBCCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmgs6ClKSmgqaClKSmgqaCpoKUpKaClKSmgpSkpoKmgpSkpoKmgpSkpoKUpKaClKSmgpSkpoKUpKaClKSmgpSkpoKUpKaClKSmgpSkpoKUpKaClKSmgpSkpoKUpKaClKSmgpSkpoKUpKaClKSmgpSkpoKUpKaClKSmgpSkpoKUpA==", "6858-6858:1;8621-8621:1", "", "709=propagateSubtreeFacts/2/2/1;896=propagateSubtreeFacts/2/3/1,propagateSubtreeFacts/3/3/2;951=propagateSubtreeFacts/2/2/1;1006=propagateSubtreeFacts/2/2/1;1049=visit/2/4/1,visit/3/4/2,visit/4/4/3;1066=propagateSubtreeFacts/2/4/1,propagateSubtreeFacts/3/4/2,propagateSubtreeFacts/4/4/3;1110=visit/2/4/1,visit/3/4/2,visit/4/4/3;1333=propagateSubtreeFacts/2/2/1;1389=propagateSubtreeFacts/2/2/1;1501=propagateNodeListSubtreeFacts/1/1/1;1619=propagateSubtreeFacts/2/3/1,propagateSubtreeFacts/3/3/2;1752=propagateSubtreeFacts/2/2/1;1965=visit/2/4/1,visit/3/4/2,visit/4/4/3;2143=visit/1/5/1,visit/2/5/2,visit/3/5/3,visit/4/5/4,visit/5/5/5;2209=visit/2/4/1,visit/3/4/2,visit/4/4/3;2331=visit/1/5/1,visit/2/5/2,visitNodeList/1/2/3,visitNodeList/2/2/4,visit/3/5/5,visit/4/5/6,visit/5/5/7;2394=visit/1/1/1,visitNodeList/1/3/2,visitNodeList/2/3/3,visitNodeList/3/3/4;2457=visit/1/1/1,visitNodeList/1/3/2,visitNodeList/2/3/3,visitNodeList/3/3/4;2578=visit/1/1/1,visitNodeList/1/3/2,visitNodeList/2/3/3,visitNodeList/3/3/4;2669=visit/1/2/1,visitNodeList/1/1/2,visit/2/2/3;3002=visit/1/3/1,visit/2/3/2,visit/3/3/3;3030=propagateSubtreeFacts/1/3/1,propagateSubtreeFacts/2/3/2,propagateSubtreeFacts/3/3/3;3662=visitNodeList/1/2/1,visitNodeList/2/2/2,visit/1/3/3,visit/2/3/4,visit/3/3/5;3721=visit/1/4/1,visitNodeList/1/2/2,visitNodeList/2/2/3,visit/2/4/4,visit/3/4/5,visit/4/4/6;3784=visit/1/4/1,visitNodeList/1/2/2,visitNodeList/2/2/3,visit/2/4/4,visit/3/4/5,visit/4/4/6;3910=visit/1/3/1,visit/2/3/2,visitNodeList/1/2/3,visitNodeList/2/2/4,visit/3/3/5;3982=visit/1/6/1,visit/2/6/2,visit/3/6/3,visitNodeList/1/2/4,visitNodeList/2/2/5,visit/4/6/6,visit/5/6/7,visit/6/6/8;4049=visit/1/4/1,visit/2/4/2,visit/3/4/3,visit/4/4/4;4116=visit/1/4/1,visit/2/4/2,visit/3/4/3,visit/4/4/4;4478=visit/1/4/1,visit/2/4/2,visit/3/4/3,visit/4/4/4;4702=visitNodeList/1/2/1,visitNodeList/2/2/2,visit/1/4/3,visit/2/4/4,visit/3/4/5,visit/4/4/6;4769=visit/1/5/1,visit/2/5/2,visitNodeList/1/2/3,visitNodeList/2/2/4,visit/3/5/5,visit/4/5/6,visit/5/5/7;4933=visit/2/5/1,visit/3/5/2,visit/4/5/3,visit/5/5/4;4950=propagateSubtreeFacts/2/5/1,propagateSubtreeFacts/3/5/2,propagateSubtreeFacts/4/5/3,propagateSubtreeFacts/5/5/4;5075=propagateSubtreeFacts/2/3/1,propagateSubtreeFacts/3/3/2;5121=visit/2/2/1,visitNodeList/1/2/2,visitNodeList/2/2/3;5404=propagateNodeListSubtreeFacts/1/1/1;5459=propagateSubtreeFacts/2/2/1;5504=visit/2/3/1,visitNodeList/1/1/2,visit/3/3/3;5784=visit/1/4/1,visit/2/4/2,visit/3/4/3,visit/4/4/4;5853=visit/1/5/1,visit/2/5/2,visit/3/5/3,visit/4/5/4,visit/5/5/5;6300=visit/2/4/1,visit/3/4/2,visit/4/4/3;6801=propagateSubtreeFacts/2/2/1;6961=visit/2/5/1,visit/3/5/2,visit/4/5/3,visit/5/5/4,visitNodeList/1/1/5;7119=visit/2/4/1,visit/3/4/2,visit/4/4/3;7374=visitNodeList/1/2/1,visitNodeList/2/2/2,visit/1/1/3;8820=visit/2/2/1,visitNodeList/1/2/2,visitNodeList/2/2/3;9526=visit/2/4/1,visit/3/4/2,visit/4/4/3,visitNodeList/1/1/4;9582=visit/2/3/1,visit/3/3/2,visitNodeList/1/1/3;9693=visit/2/3/1,visit/3/3/2,visitNodeList/1/1/3;9877=visit/1/3/1,visit/2/3/2,visit/3/3/3;10010=visit/1/3/1,visit/2/3/2,visit/3/3/3;10066=visit/2/3/1,visit/3/3/2,visitNodeList/1/1/3;10459=visit/1/4/1,visit/2/4/2,visit/3/4/3,visit/4/4/4;10531=propagateSubtreeFacts/2/2/1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/ast/deepclone.go", "deepclone.cs", "AAgMgoKUgoKClJSIgpTKgpSCgoKUlIKCgqamgpSCgoKUlIKCgqaoxoKmooKCgpSmgg==", "9-25:1;28-46:2;47-65:3")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/ast/diagnostic.go", "diagnostic.cs", "ADF0goKCgoKCgoKCgoKCgoKCgpSgooKCgpSigoKmooKmsoKUpqKCprKClKaCgqaCgpSogoKUqoKClIKClIKChJSCgoKCgpSClJSClMYAABoADh4AABYADBqyAAwasoKUpqKuggAWKuKClIKCgpSCgriClIKUgpSmlIKCgpSClIKUAA0SooKClAAIDuKClIKClJSClNbSgpTWgoKClKbSgpTWsoKCgpSm0oKUgoKClIL2ooKUprKClKiygpQAAhSigpSClKaygpSqgoKClIKCgqamgoKCgpSCgoK4poKCgpSCgoKmprKClIKClIKClIKClIKClIKClIKClIKClIKClIKClIKClA==", "", "", "190=NewAdHocMessage/1/1/5;207=Code/1/1/3,Category/1/1/4,Key/1/1/6,StringifyArgs/1/1/7,ReportsUnnecessary/1/1/8,ReportsDeprecated/1/1/9;327=Loc/1/1/2,Code/1/1/3;450=RelatedInformation/1/2/1,RelatedInformation/2/2/1,EqualFunc/1/1/1;460=Loc/1/2/1,Loc/2/2/1,Code/1/2/2,Code/2/2/2,Category/1/2/3,Category/2/2/3,Source/1/2/4,Source/2/2/4,getDiagnosticMessageIdentity/1/2/5,getDiagnosticMessageIdentity/2/2/5,MessageArgs/1/2/6,MessageArgs/2/2/6,Equal/1/1/6,MessageChain/1/2/7,MessageChain/2/2/7,EqualFunc/1/1/7;482=MessageArgs/1/2/1,MessageArgs/2/2/1,Equal/1/1/1,MessageChain/1/2/2,MessageChain/2/2/2,EqualFunc/1/1/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/ast/flow.go", "flow.cs", "AC1kgoKCgoLGggAKFoKCgoI=")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/ast/functionflags.go", "functionflags.cs", "AAwaooKUgoKUgqSClKSCtoKU")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/ast/kind_stringer_generated.go", "kind_stringer_generated.cs", "/oaCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCyrSCgoKU")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/ast/parseoptions.go", "parseoptions.cs", "ABMmooKmlqaq2siUqIKUpqKmooKmgIK2gqaCgriCpqaigoKmpqKqgoKUpoKCgoKCgpSUgqaCrIKUgoKClIKUgoKUlJQ=", "111-117:1;133-145:1", "", "35=isFileForcedToBeModuleByFormat/1/1/2;107=IsImportEqualsDeclaration/1/1/1,AsImportEqualsDeclaration/1/1/1,IsExternalModuleReference/1/1/1,IsImportDeclaration/1/1/2,IsExportAssignment/1/1/2,IsExportDeclaration/1/1/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/ast/positionmap.go", "positionmap.cs", "ABRQgoKCgoKCgpSCgoKUgoKUgsqCqIKClpKCgoKUpoSUqIKCmJKCgoKClKaClA==")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/ast/precedence.go", "precedence.cs", "ACX6AqKUpKSkyqKCgoKCgpQACBiClKSmpKSUAAIkpgAFFKaogpSmgpSogpSoqAACMqimyoKUpKSkpKSkpqSkpKSoqKKCpIKkgqSCpIKkgpSugqQAEIAEopSkpKSkpKSGlKSqAAI+pA==")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/ast/subtreefacts.go", "subtreefacts.cs", "AEOyAYKmgqaCgoKClKaCpqKClKaigpSCgpSmgoKU")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/ast/symbol.go", "symbol.cs", "ABgugqaCgpSCqIKClAAdQKKClKiCpoKClK6CgpSClA==")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/ast/utilities.go", "utilities.cs", "ABcsgoKEgoKmpoKChIKCpqaigpSmgqaCpqKooqiCqKKmgqiCpoKCgpSClKaypqKmgqaCpqKCgpamooKUpqKCgpSmopqkpqIAARKkpqKmoq6CqqKCgpSClKSClKSClKSClKS0tIKUtIKUtNqCpoKmoqaipqKCgoKClMqipqKapKaipqKmopykqKKClqSkpKSkpqKCpqKmoqailKSmgqaigoKUqKKCmramoqiCAAIYooKsoqaipqKCpoIAARCkqIKmggABEKSogqaCAAEUpKiCpqKmgqainKSmqKKmggABEKSopKaCAAEQpKikpoKmgqaipqKmogABEqSmopSkpqKmooKoogABEqSmop6kpqKmgqaipqKcpKaCpoIAAR6krqKmggABKKSuoq6igqqigpSClKiipoKmgoKmggABJqSmoqaCpqKClIKmogASLqKUpKSkpKSklKTWqKKCgpSmqIKmooKUpoKoooKUqKKClKiigoKUlKYACQyGmoKClIKCgoKmpqKCgqKmoqiigoKUlKaygoKCgoKCgoKUppSoooKClJQACBaCgpSoooKUpKSUpqKCgpSUpoKUpKSkpKSkpKSkpKSkpKSkpKaCgoKUpqKmgqaCpoSmogABGqSmogABFKSmopykpqIAATakpqIAARCkpqKmgqqCgoKUqKSUpqKClKaigoKClIKClIKUpqKCgoKUgoKUgpSogqiCqIKCgoKCgsqsgqqCgpiCgqaogqiClKSogqaigpSmooKClKaipqKmooKUqKKoopSkyKKqoqaipqKmooKUpKaCpqKmoqaipqKmoqaiqqKsgoKCpqaipoKqopSClKSClKSmooKUpoKmgoKUgoKUpqKEpKaCgpSCgpSClKaklJSCgpSkpKSCgpSkpqKCgpSkpIKClKSkgoIACAqCgtgADDKilIKCgoKUhJSCpoTYgoKClMamooKKpgACFoKCpqKClLS0pKaCpqKqoqaipqKUpKSmsoKCgpamoqaCgqqCgqaCgoKUgoKUpqKmoqiypoKmhKaigoKUpqKmqJSkgqSmoqaipoKmgqaCgoKUqKKClKaigpSqgoKCgoK4pqKUpKSkpqKClAACFIKmpIKCgqaCpqKCgoKUlIKUtIaChsaCzNqigpS0gpSkpoaChtimooKCgoKClIKmpoKmgqaipoKmgqyCpqKmoqailKSClKSkpIKUpKailKSmopSkpKSmooKCgoKCuKaiAAISpqSkgpSkpIKUpKTYooKUqKSCpIKkpKSkyKKCgpSYpKSkpIKClIKUpKSmooKClIIAAhKClJSkqqSkpoKCqoKmoqaipqKClIKmoqaipqKUpIKUpKSkpKaCgqailqSCxqSmoqaipqKmgoKClLamoqqimKikAAoaopQAAiKupKaCgoKUAAQWqKamoqaCpqKClIKUpqKClIKUpoIAAhaCqoKClIIACRiCpqKCgpS4goKUgoKClJSCgoKmpJSkgsaCxoKCgoKCgoKClIKmxoKCooKUpIKkgqSUpKSmooKElIKCgoKCgoKClIKUiriCuqaCgqiygoKUgoKUpqKmopSkpqKmoqailKSmooKCpqaigoKmpqKUpKSkpqKUgoLGpIKCxrSmoqailIKkpoKCgoKCgoKmpqKCgpSmgoKUhpSGlKaCgp6k3qKClAACJqKWpKSkpJTWpqKogoKCgoKCgoK4goKCgpSmgpS4oqaygoKCgoKUlIKCgpSClIKUpqasgoKCgoKClIKClIKCqIKUrKKClIKClIKUpqKCgqamooKCgpSKgoKUgpSU5qKClKaigoKCgrimooKCpqyigpSmgqaigpSClIKClIKmqqKClIKCgqamooKCpqaiprKClKaipqKClKailKSkpKailKSCpKSmgqailKSAgtaAgtamoqailqSkpqKUpKaCpqKClKaCpqKClIKUqIKmgqiCpqKClKaimKSmoqailqSmopikyKKUpMiinKSCgpTIgqaCpoKCgpSmoqiigpSCgpSoooKUqqKmoq6igpSCgpSmooKUgpSClKaipqKWpIKClIKCgpSUpKaCpqKUpKSkpqKClJSkpqKClIKUtLS0goKUpqKmgqaipqKCpoKCgpSClIKUgpSClIKUgpSClIKUgpSClIKUgpSClIKUpqSCgoKmprKUAAgSAAkUAAcQAAgSAAgSAAkUAAwaAAkUAAoWAAoW7gALGAAKFgAIEtwACxgACBIACBIABxDuAAgSAAcQAAcQAAcQAAgSpqIAARikyIKmooKClKaiqqKUpKSkyKKqoqaipqKClKaigpSmooKUpqKClKaigpSmgqaCpoKmgqaCpoKmjoKClJSEgqamooKUpKSkpqKCgpSmoqaCpqKUpKSkpMiCpoKCgpSmsqaiqqKogqaCAAcQgvyCpoLGgoKUpKTcgp6kyKKmopimpKailKTOogAAIqaCAAEYqKaopoymhAACEKainIKIgpTOgoiClM6mgoKUAAISooKUhoKCgoKUgoLcpqKmoqailKSkpKairKKsgoKClKqCgoKWgpSClKyigpakgtimgqainKSkgpSClKTIpAABHqbIgqaCpqKmsoKUgpSClKaipqKClIKmopSkpIKUpIKUgpSkpqKClIKYpqSEgpSGgoK4poKmooKCpqiigoKCgpSCmJSmgqaCpsSClJampqiogpaqpoKClIKmsoKCgoKCgoKUgpSClIKUgpSCgoKUgrimgqaCpqKUrKgACxaigoKCgpSWgoKCgriCgoKClIKmgoKCgoKmgoK4AAcQsoSmgoKCgoKUgoKmpqKUgqQAAhaigpSClIKUprKClJSkpKampqKuoqyirqKCgoKClIKCgsqmsoKClIKCgpSUgsqCqKKqopSkpKSkpKSU1qSuoqai", "885-894:1;1159-1169:1;2221-2223:1;2434-2447:1;2505-2505:1;2640-2652:1;2718-2724:1;3622-3624:1;3835-3844:1;4059-4061:1;4066-4068:1;4242-4242:1;4279-4281:1;4384-4386:1;4390-4392:2;4571-4577:1", "", "130=IsLeftHandSideExpression/1/1/1;420=IsOptionalChainRoot/1/1/1,Expression/1/1/2;745=Expression/1/1/1;1316=IsDeclaration/1/1/2;1386=Expression/1/2/1,Name/1/1/1,IsIdentifier/1/1/1,Expression/2/2/1,IsBindableStaticNameExpression/1/1/1,IsBindableStaticElementAccessExpression/1/1/2;1392=Expression/1/3/1,Expression/2/3/2,IsEntityNameExpression/1/1/2,Expression/3/3/3,IsBindableStaticAccessExpression/1/1/3;1461=IsHeritageClause/1/1/1,IsClassLike/1/1/1;1578=GetElementOrPropertyAccessName/1/1/1;1585=Expression/2/2/1,IsInJSFile/2/2/1,IsEntityNameExpressionEx/2/2/1;1648=IsPropertyAccessEntityNameExpression/1/1/1,isElementAccessEntityNameExpression/1/1/2;1684=Expression/1/2/1,Expression/2/2/1,HasSamePropertyAccessName/1/1/1;1818=AsTypeReferenceNode/1/1/1,IsHeritageClause/1/1/2;2214=IsJSDocImplementsTag/1/1/1,IsJSDocAugmentsTag/1/1/2;2764=FileExtensionIsOneOf/1/1/2;2767=FileExtensionIsOneOf/1/1/2;2954=GetPragmaArgument/1/1/4;3035=Type/1/1/1,IsRequireCall/1/1/2;3366=IsPartOfTypeQuery/1/1/1,isIdentifierInNonEmittingHeritageClause/1/1/2,isPartOfPossiblyValidTypeOrAbstractComputedPropertyName/1/1/3,IsExpressionNode/1/1/4,isShorthandPropertyNameUseSite/1/1/4;3582=AsTypeParameterDeclaration/1/4/1,Name/1/1/3,AsTypeParameterDeclaration/2/4/4,AsTypeParameterDeclaration/3/4/5,AsTypeParameterDeclaration/4/4/6;3591=AsParameterDeclaration/1/2/1,AsParameterDeclaration/2/2/3,Name/1/1/4,QuestionToken/1/1/5,Type/1/1/6,Initializer/1/1/7;3601=AsConstructorTypeNode/1/1/1,TypeParameterList/1/1/3,ParameterList/1/1/4,Type/1/1/5;3609=AsPropertySignatureDeclaration/1/1/1,Name/1/1/3,PostfixToken/1/1/4,Type/1/1/5,Initializer/1/1/6;3618=AsPropertyDeclaration/1/1/1,Name/1/1/3,PostfixToken/1/1/4,Type/1/1/5,Initializer/1/1/6;3627=AsMethodSignatureDeclaration/1/1/1,Name/1/1/3,PostfixToken/1/1/4,TypeParameterList/1/1/5,ParameterList/1/1/6,Type/1/1/7;3637=AsMethodDeclaration/1/3/1,AsMethodDeclaration/2/3/3,Name/1/1/4,PostfixToken/1/1/5,TypeParameterList/1/1/6,ParameterList/1/1/7,Type/1/1/8,AsMethodDeclaration/3/3/9,Body/1/1/10;3650=AsConstructorDeclaration/1/2/1,TypeParameterList/1/1/3,ParameterList/1/1/4,Type/1/1/5,AsConstructorDeclaration/2/2/6,Body/1/1/7;3660=AsGetAccessorDeclaration/1/2/1,Name/1/1/3,TypeParameterList/1/1/4,ParameterList/1/1/5,Type/1/1/6,AsGetAccessorDeclaration/2/2/7,Body/1/1/8;3671=AsSetAccessorDeclaration/1/2/1,Name/1/1/3,TypeParameterList/1/1/4,ParameterList/1/1/5,Type/1/1/6,AsSetAccessorDeclaration/2/2/7,Body/1/1/8;3682=AsIndexSignatureDeclaration/1/1/1,ParameterList/1/1/3,Type/1/1/4;3689=AsFunctionExpression/1/3/1,AsFunctionExpression/2/3/3,Name/1/1/4,TypeParameterList/1/1/5,ParameterList/1/1/6,Type/1/1/7,AsFunctionExpression/3/3/8,Body/1/1/9;3701=AsArrowFunction/1/3/1,TypeParameterList/1/1/3,ParameterList/1/1/4,Type/1/1/5,AsArrowFunction/2/3/6,AsArrowFunction/3/3/7,Body/1/1/8;3712=AsClassExpression/1/2/1,Name/1/1/3,TypeParameterList/1/1/4,AsClassExpression/2/2/5,MemberList/1/1/6;3721=AsVariableStatement/1/2/1,AsVariableStatement/2/2/3;3727=AsFunctionDeclaration/1/3/1,AsFunctionDeclaration/2/3/3,Name/1/1/4,TypeParameterList/1/1/5,ParameterList/1/1/6,Type/1/1/7,AsFunctionDeclaration/3/3/8,Body/1/1/9;3739=AsClassDeclaration/1/2/1,Name/1/1/3,TypeParameterList/1/1/4,AsClassDeclaration/2/2/5,MemberList/1/1/6;3748=AsInterfaceDeclaration/1/2/1,Name/1/1/3,TypeParameterList/1/1/4,AsInterfaceDeclaration/2/2/5,MemberList/1/1/6;3757=AsTypeAliasDeclaration/1/1/1,Name/1/1/3,TypeParameterList/1/1/4,Type/1/1/5;3765=AsEnumDeclaration/1/1/1,Name/1/1/3,MemberList/1/1/4;3772=AsModuleDeclaration/1/2/1,AsModuleDeclaration/2/2/3,Name/1/1/4,Attributes/1/1/5,Body/1/1/6;3781=AsImportEqualsDeclaration/1/2/1,IsTypeOnly/1/1/3,Name/1/1/4,AsImportEqualsDeclaration/2/2/5;3789=AsImportDeclaration/1/2/1,ImportClause/1/1/3,ModuleSpecifier/1/1/4,AsImportDeclaration/2/2/5;3797=AsExportAssignment/1/2/1,AsExportAssignment/2/2/3,Type/1/1/4,Expression/1/1/5;3805=AsExportDeclaration/1/3/1,IsTypeOnly/1/1/3,AsExportDeclaration/2/3/4,ModuleSpecifier/1/1/5,AsExportDeclaration/3/3/6;3872=IsTypeDeclaration/1/1/1,GetNameOfDeclaration/1/1/2;4053=IsTemplateLiteralKind/1/1/1,TemplateLiteralLikeData/1/1/1;4059=Initializer/1/1/1;4530=IsClassLike/1/1/1,HasAbstractModifier/1/1/1,HasAmbientModifier/1/1/1;4533=IsClassLike/1/1/1;4539=GetThisParameter/1/1/2;4738=AsJsxNamespacedName/3/4/1,Name/1/2/1,Text/3/4/1,AsJsxNamespacedName/4/4/1,Name/2/2/1,Text/4/4/1;4741=Expression/1/2/1,Expression/2/2/1,TagNamesAreEquivalent/1/1/1;4756=isArgumentOfElementAccessExpression/1/1/2,IsLiteralComputedPropertyDeclarationName/1/1/3;4762=AsElementAccessExpression/1/1/2;4813=Expression/1/1/1;4862=AsLiteralTypeNode/1/1/1,IsStringLiteralLike/1/1/1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/ast/visitor.go", "visitor.cs", "ABk4ooKUxoIAAhKCgqaCgoKCgpSCgqamAAISgoKmgoKUAAIWooKmgoKCpgACFqKCpoKCgqYAAhSCgqaCgoKmgoKUhMa0tpaCpoKClIK4uKiygqamooKUpqKClIKUpqKClKaigpSmooKUpqKClKaigpSmooKUpqKClKaigoKClKaClJSClA==")]
// </GoSourcePositionMaps>

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

[GoPackage("ast", ImportPath = "github.com/microsoft/TypeScript/tsc/internal/ast")]
public static partial class ast_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    internal partial interface nodeData {}
    internal partial struct diagnosticLocationKey {}
    internal partial struct hasFileNameImpl {}
    internal partial struct newParentInChildrenSetter_state {}
    internal partial struct positionMapEntry {}
    internal partial struct sourceFileDataCell<T> {}
    internal partial struct sourceFileDataKey {}
    public partial interface HasFileName {}
    public partial interface NodeFactoryCoercible {}
    public partial interface SourceFileLike {}
    public partial struct AccessKind {}
    public partial struct AccessorDeclarationBase {}
    public partial struct AllAccessorDeclarations {}
    public partial struct ArrayLiteralExpression {}
    public partial struct ArrayTypeNode {}
    public partial struct ArrowFunction {}
    public partial struct AsExpression {}
    public partial struct AwaitExpression {}
    public partial struct BigIntLiteral {}
    public partial struct BinaryExpression {}
    public partial struct BindingElement {}
    public partial struct BindingPattern {}
    public partial struct Block {}
    public partial struct BodyBase {}
    public partial struct BreakStatement {}
    public partial struct CallExpression {}
    public partial struct CallSignatureDeclaration {}
    public partial struct CaseBlock {}
    public partial struct CaseOrDefaultClause {}
    public partial struct CatchClause {}
    public partial struct CheckFlags {}
    public partial struct CheckJsDirective {}
    public partial struct ClassDeclaration {}
    public partial struct ClassElementBase {}
    public partial struct ClassExpression {}
    public partial struct ClassLikeBase {}
    public partial struct ClassStaticBlockDeclaration {}
    public partial struct CommentDirective {}
    public partial struct CommentDirectiveKind {}
    public partial struct CommentRange {}
    public partial struct CompositeBase {}
    public partial struct ComputedPropertyName {}
    public partial struct ConditionalExpression {}
    public partial struct ConditionalTypeNode {}
    public partial struct ConstructSignatureDeclaration {}
    public partial struct ConstructorDeclaration {}
    public partial struct ConstructorTypeNode {}
    public partial struct ContentMapperSourceFileInfo {}
    public partial struct ContinueStatement {}
    public partial struct DebuggerStatement {}
    public partial struct DeclarationBase {}
    public partial struct Decorator {}
    public partial struct DeleteExpression {}
    public partial struct Diagnostic {}
    public partial struct DiagnosticsCollection {}
    public partial struct DoStatement {}
    public partial struct ElementAccessExpression {}
    public partial struct EmptyStatement {}
    public partial struct EnumDeclaration {}
    public partial struct EnumMember {}
    public partial struct ExportAssignment {}
    public partial struct ExportDeclaration {}
    public partial struct ExportSpecifier {}
    public partial struct ExportableBase {}
    public partial struct ExpressionBase {}
    public partial struct ExpressionStatement {}
    public partial struct ExpressionWithTypeArguments {}
    public partial struct ExternalModuleIndicatorOptions {}
    public partial struct ExternalModuleReference {}
    public partial struct FileReference {}
    public partial struct FindAncestorResult {}
    public partial struct FlowFlags {}
    public partial struct FlowList {}
    public partial struct FlowNode {}
    public partial struct FlowNodeBase {}
    public partial struct FlowReduceLabelData {}
    public partial struct FlowSwitchClauseData {}
    public partial struct ForInOrOfStatement {}
    public partial struct ForStatement {}
    public partial struct FunctionDeclaration {}
    public partial struct FunctionExpression {}
    public partial struct FunctionFlags {}
    public partial struct FunctionLikeBase {}
    public partial struct FunctionLikeWithBodyBase {}
    public partial struct FunctionOrConstructorTypeNodeBase {}
    public partial struct FunctionTypeNode {}
    public partial struct GetAccessorDeclaration {}
    public partial struct HeritageClause {}
    public partial struct Identifier {}
    public partial struct IfStatement {}
    public partial struct ImportAttribute {}
    public partial struct ImportAttributes {}
    public partial struct ImportClause {}
    public partial struct ImportDeclaration {}
    public partial struct ImportEqualsDeclaration {}
    public partial struct ImportSpecifier {}
    public partial struct ImportTypeNode {}
    public partial struct IndexSignatureDeclaration {}
    public partial struct IndexedAccessTypeNode {}
    public partial struct InferTypeNode {}
    public partial struct InterfaceDeclaration {}
    public partial struct IntersectionTypeNode {}
    public partial struct IterationStatementBase {}
    public partial struct JSDeclarationKind {}
    public partial struct JSDoc {}
    public partial struct JSDocAllType {}
    public partial struct JSDocAugmentsTag {}
    public partial struct JSDocCallbackTag {}
    public partial struct JSDocCommentBase {}
    public partial struct JSDocDeprecatedTag {}
    public partial struct JSDocImplementsTag {}
    public partial struct JSDocImportTag {}
    public partial struct JSDocLink {}
    public partial struct JSDocLinkCode {}
    public partial struct JSDocLinkPlain {}
    public partial struct JSDocNameReference {}
    public partial struct JSDocNonNullableType {}
    public partial struct JSDocNullableType {}
    public partial struct JSDocOptionalType {}
    public partial struct JSDocOverloadTag {}
    public partial struct JSDocOverrideTag {}
    public partial struct JSDocParameterOrPropertyTag {}
    public partial struct JSDocPrivateTag {}
    public partial struct JSDocProtectedTag {}
    public partial struct JSDocPublicTag {}
    public partial struct JSDocReadonlyTag {}
    public partial struct JSDocReturnTag {}
    public partial struct JSDocSatisfiesTag {}
    public partial struct JSDocSeeTag {}
    public partial struct JSDocSignature {}
    public partial struct JSDocTagBase {}
    public partial struct JSDocTemplateTag {}
    public partial struct JSDocText {}
    public partial struct JSDocThisTag {}
    public partial struct JSDocThrowsTag {}
    public partial struct JSDocTypeBase {}
    public partial struct JSDocTypeExpression {}
    public partial struct JSDocTypeLiteral {}
    public partial struct JSDocTypeTag {}
    public partial struct JSDocTypedefTag {}
    public partial struct JSDocUnknownTag {}
    public partial struct JSDocVariadicType {}
    public partial struct JsxAttribute {}
    public partial struct JsxAttributes {}
    public partial struct JsxClosingElement {}
    public partial struct JsxClosingFragment {}
    public partial struct JsxElement {}
    public partial struct JsxExpression {}
    public partial struct JsxFragment {}
    public partial struct JsxNamespacedName {}
    public partial struct JsxOpeningElement {}
    public partial struct JsxOpeningFragment {}
    public partial struct JsxSelfClosingElement {}
    public partial struct JsxSpreadAttribute {}
    public partial struct JsxText {}
    public partial struct KeywordExpression {}
    public partial struct KeywordTypeNode {}
    public partial struct Kind {}
    public partial struct LabeledStatement {}
    public partial struct LeftHandSideExpressionBase {}
    public partial struct LiteralExpressionBase {}
    public partial struct LiteralLikeNodeBase {}
    public partial struct LiteralTypeNode {}
    public partial struct LocalsContainerBase {}
    public partial struct MappedDiagnosticDirective {}
    public partial struct MappedDiagnosticDirectivePolicy {}
    public partial struct MappedTypeNode {}
    public partial struct MemberExpressionBase {}
    public partial struct MetaProperty {}
    public partial struct MethodDeclaration {}
    public partial struct MethodSignatureDeclaration {}
    public partial struct MissingDeclaration {}
    public partial struct ModifierFlags {}
    public partial struct ModifierList {}
    public partial struct ModifiersBase {}
    public partial struct ModuleBlock {}
    public partial struct ModuleDeclaration {}
    public partial struct ModuleInstanceState {}
    public partial struct MutableNode {}
    public partial struct NamedExports {}
    public partial struct NamedImports {}
    public partial struct NamedMemberBase {}
    public partial struct NamedTupleMember {}
    public partial struct NamespaceExport {}
    public partial struct NamespaceExportDeclaration {}
    public partial struct NamespaceImport {}
    public partial struct NewExpression {}
    public partial struct NoSubstitutionTemplateLiteral {}
    public partial struct Node {}
    public partial struct NodeBase {}
    public partial struct NodeDefault {}
    public partial struct NodeFactory {}
    public partial struct NodeFactoryHooks {}
    public partial struct NodeFlags {}
    public partial struct NodeId {}
    public partial struct NodeList {}
    public partial struct NodeVisitor {}
    public partial struct NodeVisitorHooks {}
    public partial struct NodeWithTypeArgumentsBase {}
    public partial struct NonNullExpression {}
    public partial struct NotEmittedStatement {}
    public partial struct NotEmittedTypeElement {}
    public partial struct NumericLiteral {}
    public partial struct ObjectLiteralElementBase {}
    public partial struct ObjectLiteralExpression {}
    public partial struct OmittedExpression {}
    public partial struct OperatorPrecedence {}
    public partial struct OperatorPrecedenceFlags {}
    public partial struct OptionalTypeNode {}
    public partial struct OuterExpressionKinds {}
    public partial struct ParameterDeclaration {}
    public partial struct ParenthesizedExpression {}
    public partial struct ParenthesizedTypeNode {}
    public partial struct PartiallyEmittedExpression {}
    public partial struct PatternAmbientModule {}
    public partial struct PositionMap {}
    public partial struct PostfixUnaryExpression {}
    public partial struct Pragma {}
    public partial struct PragmaArgument {}
    public partial struct PragmaArgumentSpecification {}
    public partial struct PragmaSpecification {}
    public partial struct PrefixUnaryExpression {}
    public partial struct PrimaryExpressionBase {}
    public partial struct PrivateIdentifier {}
    public partial struct PropertyAccessExpression {}
    public partial struct PropertyAssignment {}
    public partial struct PropertyDeclaration {}
    public partial struct PropertySignatureDeclaration {}
    public partial struct QualifiedName {}
    public partial struct RegularExpressionLiteral {}
    public partial struct RepopulateDiagnosticInfo {}
    public partial struct RepopulateDiagnosticKind {}
    public partial struct RestTypeNode {}
    public partial struct ReturnStatement {}
    public partial struct SatisfiesExpression {}
    public partial struct SemanticMeaning {}
    public partial struct SemicolonClassElement {}
    public partial struct SetAccessorDeclaration {}
    public partial struct ShorthandPropertyAssignment {}
    public partial struct SourceFile {}
    public partial struct SourceFileDataKey<T> {}
    public partial struct SourceFileMetaData {}
    public partial struct SourceFileParseOptions {}
    public partial struct SpreadAssignment {}
    public partial struct SpreadElement {}
    public partial struct StatementBase {}
    public partial struct StringLiteral {}
    public partial struct SubtreeFacts {}
    public partial struct SwitchStatement {}
    public partial struct Symbol {}
    public partial struct SymbolFlags {}
    public partial struct SymbolId {}
    public partial struct SymbolTable {}
    public partial struct SyntaxList {}
    public partial struct SyntheticExpression {}
    public partial struct SyntheticReferenceExpression {}
    public partial struct TaggedTemplateExpression {}
    public partial struct TemplateExpression {}
    public partial struct TemplateHead {}
    public partial struct TemplateLiteralLikeNodeBase {}
    public partial struct TemplateLiteralTypeNode {}
    public partial struct TemplateLiteralTypeSpan {}
    public partial struct TemplateMiddle {}
    public partial struct TemplateSpan {}
    public partial struct TemplateTail {}
    public partial struct ThisTypeNode {}
    public partial struct ThrowStatement {}
    public partial struct Token {}
    public partial struct TokenCacheKey {}
    public partial struct TokenFlags {}
    public partial struct TryStatement {}
    public partial struct TupleTypeNode {}
    public partial struct TypeAliasDeclaration {}
    public partial struct TypeAssertion {}
    public partial struct TypeElementBase {}
    public partial struct TypeLiteralNode {}
    public partial struct TypeNodeBase {}
    public partial struct TypeOfExpression {}
    public partial struct TypeOperatorNode {}
    public partial struct TypeParameterDeclaration {}
    public partial struct TypePrecedence {}
    public partial struct TypePredicateNode {}
    public partial struct TypeQueryNode {}
    public partial struct TypeReferenceNode {}
    public partial struct TypeSyntaxBase {}
    public partial struct UnaryExpressionBase {}
    public partial struct UnionOrIntersectionTypeNodeBase {}
    public partial struct UnionTypeNode {}
    public partial struct UpdateExpressionBase {}
    public partial struct VariableDeclaration {}
    public partial struct VariableDeclarationList {}
    public partial struct VariableStatement {}
    public partial struct VoidExpression {}
    public partial struct WhileStatement {}
    public partial struct WithStatement {}
    public partial struct YieldExpression {}
    // </TypeAccessibility>

    // Go initializes an imported package before the importing package, for every import
    // form - not only the blank one. .NET would never load an assembly nothing has touched
    // yet, so each import that initializes anything is forced below: once per assembly, and
    // ahead of this package's own `init` functions, which this file being the first compile
    // item of the project guarantees.

    // <ImportInitializers>
    [GoInit] internal static void initᴛᴛimportꓸfmt() => builtin.initPackage(typeof(fmt_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcollections() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.collections_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcore() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸdebug() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.debug_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸdiagnostics() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸlocale() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.locale_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸspanmap() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.spanmap_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸstringutil() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtspath() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸzeeboꓸxxh3() => builtin.initPackage(typeof(go.github.com.zeebo.xxh3_package));
    [GoInit] internal static void initᴛᴛimportꓸiter() => builtin.initPackage(typeof(iter_package));
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
[GoMethodHost(typeof(ast_package.AccessorDeclarationBase)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛAccessorDeclarationBase;
[GoMethodHost(typeof(ast_package.ArrayLiteralExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛArrayLiteralExpression;
[GoMethodHost(typeof(ast_package.ArrayTypeNode)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛArrayTypeNode;
[GoMethodHost(typeof(ast_package.ArrowFunction)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛArrowFunction;
[GoMethodHost(typeof(ast_package.AsExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛAsExpression;
[GoMethodHost(typeof(ast_package.AwaitExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛAwaitExpression;
[GoMethodHost(typeof(ast_package.BigIntLiteral)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛBigIntLiteral;
[GoMethodHost(typeof(ast_package.BinaryExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛBinaryExpression;
[GoMethodHost(typeof(ast_package.BindingElement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛBindingElement;
[GoMethodHost(typeof(ast_package.BindingPattern)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛBindingPattern;
[GoMethodHost(typeof(ast_package.Block)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛBlock;
[GoMethodHost(typeof(ast_package.BodyBase)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛBodyBase;
[GoMethodHost(typeof(ast_package.BreakStatement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛBreakStatement;
[GoMethodHost(typeof(ast_package.CallExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛCallExpression;
[GoMethodHost(typeof(ast_package.CallSignatureDeclaration)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛCallSignatureDeclaration;
[GoMethodHost(typeof(ast_package.CaseBlock)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛCaseBlock;
[GoMethodHost(typeof(ast_package.CaseOrDefaultClause)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛCaseOrDefaultClause;
[GoMethodHost(typeof(ast_package.CatchClause)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛCatchClause;
[GoMethodHost(typeof(ast_package.ClassDeclaration)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛClassDeclaration;
[GoMethodHost(typeof(ast_package.ClassExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛClassExpression;
[GoMethodHost(typeof(ast_package.ClassLikeBase)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛClassLikeBase;
[GoMethodHost(typeof(ast_package.ClassStaticBlockDeclaration)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛClassStaticBlockDeclaration;
[GoMethodHost(typeof(ast_package.CompositeBase)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛCompositeBase;
[GoMethodHost(typeof(ast_package.ComputedPropertyName)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛComputedPropertyName;
[GoMethodHost(typeof(ast_package.ConditionalExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛConditionalExpression;
[GoMethodHost(typeof(ast_package.ConditionalTypeNode)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛConditionalTypeNode;
[GoMethodHost(typeof(ast_package.ConstructSignatureDeclaration)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛConstructSignatureDeclaration;
[GoMethodHost(typeof(ast_package.ConstructorDeclaration)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛConstructorDeclaration;
[GoMethodHost(typeof(ast_package.ConstructorTypeNode)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛConstructorTypeNode;
[GoMethodHost(typeof(ast_package.ContinueStatement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛContinueStatement;
[GoMethodHost(typeof(ast_package.DebuggerStatement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛDebuggerStatement;
[GoMethodHost(typeof(ast_package.DeclarationBase)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛDeclarationBase;
[GoMethodHost(typeof(ast_package.Decorator)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛDecorator;
[GoMethodHost(typeof(ast_package.DeleteExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛDeleteExpression;
[GoMethodHost(typeof(ast_package.Diagnostic)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛDiagnostic;
[GoMethodHost(typeof(ast_package.DiagnosticsCollection)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛDiagnosticsCollection;
[GoMethodHost(typeof(ast_package.DoStatement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛDoStatement;
[GoMethodHost(typeof(ast_package.ElementAccessExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛElementAccessExpression;
[GoMethodHost(typeof(ast_package.EmptyStatement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛEmptyStatement;
[GoMethodHost(typeof(ast_package.EnumDeclaration)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛEnumDeclaration;
[GoMethodHost(typeof(ast_package.EnumMember)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛEnumMember;
[GoMethodHost(typeof(ast_package.ExportAssignment)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛExportAssignment;
[GoMethodHost(typeof(ast_package.ExportDeclaration)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛExportDeclaration;
[GoMethodHost(typeof(ast_package.ExportSpecifier)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛExportSpecifier;
[GoMethodHost(typeof(ast_package.ExportableBase)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛExportableBase;
[GoMethodHost(typeof(ast_package.ExpressionStatement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛExpressionStatement;
[GoMethodHost(typeof(ast_package.ExpressionWithTypeArguments)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛExpressionWithTypeArguments;
[GoMethodHost(typeof(ast_package.ExternalModuleReference)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛExternalModuleReference;
[GoMethodHost(typeof(ast_package.FlowNodeBase)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛFlowNodeBase;
[GoMethodHost(typeof(ast_package.FlowSwitchClauseData)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛFlowSwitchClauseData;
[GoMethodHost(typeof(ast_package.ForInOrOfStatement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛForInOrOfStatement;
[GoMethodHost(typeof(ast_package.ForStatement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛForStatement;
[GoMethodHost(typeof(ast_package.FunctionDeclaration)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛFunctionDeclaration;
[GoMethodHost(typeof(ast_package.FunctionExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛFunctionExpression;
[GoMethodHost(typeof(ast_package.FunctionLikeBase)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛFunctionLikeBase;
[GoMethodHost(typeof(ast_package.FunctionLikeWithBodyBase)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛFunctionLikeWithBodyBase;
[GoMethodHost(typeof(ast_package.FunctionTypeNode)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛFunctionTypeNode;
[GoMethodHost(typeof(ast_package.GetAccessorDeclaration)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛGetAccessorDeclaration;
[GoMethodHost(typeof(ast_package.HeritageClause)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛHeritageClause;
[GoMethodHost(typeof(ast_package.Identifier)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛIdentifier;
[GoMethodHost(typeof(ast_package.IfStatement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛIfStatement;
[GoMethodHost(typeof(ast_package.ImportAttribute)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛImportAttribute;
[GoMethodHost(typeof(ast_package.ImportAttributes)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛImportAttributes;
[GoMethodHost(typeof(ast_package.ImportClause)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛImportClause;
[GoMethodHost(typeof(ast_package.ImportDeclaration)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛImportDeclaration;
[GoMethodHost(typeof(ast_package.ImportEqualsDeclaration)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛImportEqualsDeclaration;
[GoMethodHost(typeof(ast_package.ImportSpecifier)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛImportSpecifier;
[GoMethodHost(typeof(ast_package.ImportTypeNode)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛImportTypeNode;
[GoMethodHost(typeof(ast_package.IndexSignatureDeclaration)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛIndexSignatureDeclaration;
[GoMethodHost(typeof(ast_package.IndexedAccessTypeNode)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛIndexedAccessTypeNode;
[GoMethodHost(typeof(ast_package.InferTypeNode)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛInferTypeNode;
[GoMethodHost(typeof(ast_package.InterfaceDeclaration)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛInterfaceDeclaration;
[GoMethodHost(typeof(ast_package.IntersectionTypeNode)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛIntersectionTypeNode;
[GoMethodHost(typeof(ast_package.JSDoc)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDoc;
[GoMethodHost(typeof(ast_package.JSDocAllType)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocAllType;
[GoMethodHost(typeof(ast_package.JSDocAugmentsTag)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocAugmentsTag;
[GoMethodHost(typeof(ast_package.JSDocCallbackTag)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocCallbackTag;
[GoMethodHost(typeof(ast_package.JSDocDeprecatedTag)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocDeprecatedTag;
[GoMethodHost(typeof(ast_package.JSDocImplementsTag)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocImplementsTag;
[GoMethodHost(typeof(ast_package.JSDocImportTag)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocImportTag;
[GoMethodHost(typeof(ast_package.JSDocLink)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocLink;
[GoMethodHost(typeof(ast_package.JSDocLinkCode)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocLinkCode;
[GoMethodHost(typeof(ast_package.JSDocLinkPlain)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocLinkPlain;
[GoMethodHost(typeof(ast_package.JSDocNameReference)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocNameReference;
[GoMethodHost(typeof(ast_package.JSDocNonNullableType)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocNonNullableType;
[GoMethodHost(typeof(ast_package.JSDocNullableType)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocNullableType;
[GoMethodHost(typeof(ast_package.JSDocOptionalType)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocOptionalType;
[GoMethodHost(typeof(ast_package.JSDocOverloadTag)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocOverloadTag;
[GoMethodHost(typeof(ast_package.JSDocOverrideTag)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocOverrideTag;
[GoMethodHost(typeof(ast_package.JSDocParameterOrPropertyTag)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocParameterOrPropertyTag;
[GoMethodHost(typeof(ast_package.JSDocPrivateTag)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocPrivateTag;
[GoMethodHost(typeof(ast_package.JSDocProtectedTag)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocProtectedTag;
[GoMethodHost(typeof(ast_package.JSDocPublicTag)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocPublicTag;
[GoMethodHost(typeof(ast_package.JSDocReadonlyTag)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocReadonlyTag;
[GoMethodHost(typeof(ast_package.JSDocReturnTag)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocReturnTag;
[GoMethodHost(typeof(ast_package.JSDocSatisfiesTag)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocSatisfiesTag;
[GoMethodHost(typeof(ast_package.JSDocSeeTag)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocSeeTag;
[GoMethodHost(typeof(ast_package.JSDocSignature)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocSignature;
[GoMethodHost(typeof(ast_package.JSDocTemplateTag)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocTemplateTag;
[GoMethodHost(typeof(ast_package.JSDocText)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocText;
[GoMethodHost(typeof(ast_package.JSDocThisTag)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocThisTag;
[GoMethodHost(typeof(ast_package.JSDocThrowsTag)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocThrowsTag;
[GoMethodHost(typeof(ast_package.JSDocTypeExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocTypeExpression;
[GoMethodHost(typeof(ast_package.JSDocTypeLiteral)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocTypeLiteral;
[GoMethodHost(typeof(ast_package.JSDocTypeTag)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocTypeTag;
[GoMethodHost(typeof(ast_package.JSDocTypedefTag)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocTypedefTag;
[GoMethodHost(typeof(ast_package.JSDocUnknownTag)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocUnknownTag;
[GoMethodHost(typeof(ast_package.JSDocVariadicType)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJSDocVariadicType;
[GoMethodHost(typeof(ast_package.JsxAttribute)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJsxAttribute;
[GoMethodHost(typeof(ast_package.JsxAttributes)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJsxAttributes;
[GoMethodHost(typeof(ast_package.JsxClosingElement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJsxClosingElement;
[GoMethodHost(typeof(ast_package.JsxClosingFragment)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJsxClosingFragment;
[GoMethodHost(typeof(ast_package.JsxElement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJsxElement;
[GoMethodHost(typeof(ast_package.JsxExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJsxExpression;
[GoMethodHost(typeof(ast_package.JsxFragment)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJsxFragment;
[GoMethodHost(typeof(ast_package.JsxNamespacedName)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJsxNamespacedName;
[GoMethodHost(typeof(ast_package.JsxOpeningElement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJsxOpeningElement;
[GoMethodHost(typeof(ast_package.JsxOpeningFragment)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJsxOpeningFragment;
[GoMethodHost(typeof(ast_package.JsxSelfClosingElement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJsxSelfClosingElement;
[GoMethodHost(typeof(ast_package.JsxSpreadAttribute)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJsxSpreadAttribute;
[GoMethodHost(typeof(ast_package.JsxText)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛJsxText;
[GoMethodHost(typeof(ast_package.KeywordExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛKeywordExpression;
[GoMethodHost(typeof(ast_package.KeywordTypeNode)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛKeywordTypeNode;
[GoMethodHost(typeof(ast_package.Kind)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛKind;
[GoMethodHost(typeof(ast_package.LabeledStatement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛLabeledStatement;
[GoMethodHost(typeof(ast_package.LiteralLikeNodeBase)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛLiteralLikeNodeBase;
[GoMethodHost(typeof(ast_package.LiteralTypeNode)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛLiteralTypeNode;
[GoMethodHost(typeof(ast_package.LocalsContainerBase)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛLocalsContainerBase;
[GoMethodHost(typeof(ast_package.MappedTypeNode)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛMappedTypeNode;
[GoMethodHost(typeof(ast_package.MetaProperty)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛMetaProperty;
[GoMethodHost(typeof(ast_package.MethodDeclaration)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛMethodDeclaration;
[GoMethodHost(typeof(ast_package.MethodSignatureDeclaration)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛMethodSignatureDeclaration;
[GoMethodHost(typeof(ast_package.MissingDeclaration)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛMissingDeclaration;
[GoMethodHost(typeof(ast_package.ModifierList)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛModifierList;
[GoMethodHost(typeof(ast_package.ModifiersBase)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛModifiersBase;
[GoMethodHost(typeof(ast_package.ModuleBlock)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛModuleBlock;
[GoMethodHost(typeof(ast_package.ModuleDeclaration)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛModuleDeclaration;
[GoMethodHost(typeof(ast_package.MutableNode)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛMutableNode;
[GoMethodHost(typeof(ast_package.NamedExports)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛNamedExports;
[GoMethodHost(typeof(ast_package.NamedImports)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛNamedImports;
[GoMethodHost(typeof(ast_package.NamedMemberBase)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛNamedMemberBase;
[GoMethodHost(typeof(ast_package.NamedTupleMember)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛNamedTupleMember;
[GoMethodHost(typeof(ast_package.NamespaceExport)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛNamespaceExport;
[GoMethodHost(typeof(ast_package.NamespaceExportDeclaration)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛNamespaceExportDeclaration;
[GoMethodHost(typeof(ast_package.NamespaceImport)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛNamespaceImport;
[GoMethodHost(typeof(ast_package.NewExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛNewExpression;
[GoMethodHost(typeof(ast_package.NoSubstitutionTemplateLiteral)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛNoSubstitutionTemplateLiteral;
[GoMethodHost(typeof(ast_package.Node)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛNode;
[GoMethodHost(typeof(ast_package.NodeDefault)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛNodeDefault;
[GoMethodHost(typeof(ast_package.NodeFactory)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛNodeFactory;
[GoMethodHost(typeof(ast_package.NodeList)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛNodeList;
[GoMethodHost(typeof(ast_package.NodeVisitor)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛNodeVisitor;
[GoMethodHost(typeof(ast_package.NonNullExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛNonNullExpression;
[GoMethodHost(typeof(ast_package.NotEmittedStatement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛNotEmittedStatement;
[GoMethodHost(typeof(ast_package.NotEmittedTypeElement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛNotEmittedTypeElement;
[GoMethodHost(typeof(ast_package.NumericLiteral)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛNumericLiteral;
[GoMethodHost(typeof(ast_package.ObjectLiteralExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛObjectLiteralExpression;
[GoMethodHost(typeof(ast_package.OmittedExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛOmittedExpression;
[GoMethodHost(typeof(ast_package.OptionalTypeNode)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛOptionalTypeNode;
[GoMethodHost(typeof(ast_package.ParameterDeclaration)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛParameterDeclaration;
[GoMethodHost(typeof(ast_package.ParenthesizedExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛParenthesizedExpression;
[GoMethodHost(typeof(ast_package.ParenthesizedTypeNode)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛParenthesizedTypeNode;
[GoMethodHost(typeof(ast_package.PartiallyEmittedExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛPartiallyEmittedExpression;
[GoMethodHost(typeof(ast_package.PositionMap)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛPositionMap;
[GoMethodHost(typeof(ast_package.PostfixUnaryExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛPostfixUnaryExpression;
[GoMethodHost(typeof(ast_package.PragmaSpecification)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛPragmaSpecification;
[GoMethodHost(typeof(ast_package.PrefixUnaryExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛPrefixUnaryExpression;
[GoMethodHost(typeof(ast_package.PrivateIdentifier)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛPrivateIdentifier;
[GoMethodHost(typeof(ast_package.PropertyAccessExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛPropertyAccessExpression;
[GoMethodHost(typeof(ast_package.PropertyAssignment)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛPropertyAssignment;
[GoMethodHost(typeof(ast_package.PropertyDeclaration)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛPropertyDeclaration;
[GoMethodHost(typeof(ast_package.PropertySignatureDeclaration)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛPropertySignatureDeclaration;
[GoMethodHost(typeof(ast_package.QualifiedName)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛQualifiedName;
[GoMethodHost(typeof(ast_package.RegularExpressionLiteral)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛRegularExpressionLiteral;
[GoMethodHost(typeof(ast_package.RestTypeNode)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛRestTypeNode;
[GoMethodHost(typeof(ast_package.ReturnStatement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛReturnStatement;
[GoMethodHost(typeof(ast_package.SatisfiesExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛSatisfiesExpression;
[GoMethodHost(typeof(ast_package.SemicolonClassElement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛSemicolonClassElement;
[GoMethodHost(typeof(ast_package.SetAccessorDeclaration)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛSetAccessorDeclaration;
[GoMethodHost(typeof(ast_package.ShorthandPropertyAssignment)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛShorthandPropertyAssignment;
[GoMethodHost(typeof(ast_package.SourceFile)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛSourceFile;
[GoMethodHost(typeof(ast_package.SpreadAssignment)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛSpreadAssignment;
[GoMethodHost(typeof(ast_package.SpreadElement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛSpreadElement;
[GoMethodHost(typeof(ast_package.StringLiteral)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛStringLiteral;
[GoMethodHost(typeof(ast_package.SwitchStatement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛSwitchStatement;
[GoMethodHost(typeof(ast_package.Symbol)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛSymbol;
[GoMethodHost(typeof(ast_package.SyntaxList)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛSyntaxList;
[GoMethodHost(typeof(ast_package.SyntheticExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛSyntheticExpression;
[GoMethodHost(typeof(ast_package.SyntheticReferenceExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛSyntheticReferenceExpression;
[GoMethodHost(typeof(ast_package.TaggedTemplateExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛTaggedTemplateExpression;
[GoMethodHost(typeof(ast_package.TemplateExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛTemplateExpression;
[GoMethodHost(typeof(ast_package.TemplateHead)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛTemplateHead;
[GoMethodHost(typeof(ast_package.TemplateLiteralLikeNodeBase)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛTemplateLiteralLikeNodeBase;
[GoMethodHost(typeof(ast_package.TemplateLiteralTypeNode)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛTemplateLiteralTypeNode;
[GoMethodHost(typeof(ast_package.TemplateLiteralTypeSpan)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛTemplateLiteralTypeSpan;
[GoMethodHost(typeof(ast_package.TemplateMiddle)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛTemplateMiddle;
[GoMethodHost(typeof(ast_package.TemplateSpan)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛTemplateSpan;
[GoMethodHost(typeof(ast_package.TemplateTail)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛTemplateTail;
[GoMethodHost(typeof(ast_package.ThisTypeNode)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛThisTypeNode;
[GoMethodHost(typeof(ast_package.ThrowStatement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛThrowStatement;
[GoMethodHost(typeof(ast_package.Token)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛToken;
[GoMethodHost(typeof(ast_package.TryStatement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛTryStatement;
[GoMethodHost(typeof(ast_package.TupleTypeNode)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛTupleTypeNode;
[GoMethodHost(typeof(ast_package.TypeAliasDeclaration)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛTypeAliasDeclaration;
[GoMethodHost(typeof(ast_package.TypeAssertion)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛTypeAssertion;
[GoMethodHost(typeof(ast_package.TypeLiteralNode)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛTypeLiteralNode;
[GoMethodHost(typeof(ast_package.TypeOfExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛTypeOfExpression;
[GoMethodHost(typeof(ast_package.TypeOperatorNode)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛTypeOperatorNode;
[GoMethodHost(typeof(ast_package.TypeParameterDeclaration)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛTypeParameterDeclaration;
[GoMethodHost(typeof(ast_package.TypePredicateNode)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛTypePredicateNode;
[GoMethodHost(typeof(ast_package.TypeQueryNode)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛTypeQueryNode;
[GoMethodHost(typeof(ast_package.TypeReferenceNode)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛTypeReferenceNode;
[GoMethodHost(typeof(ast_package.TypeSyntaxBase)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛTypeSyntaxBase;
[GoMethodHost(typeof(ast_package.UnionTypeNode)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛUnionTypeNode;
[GoMethodHost(typeof(ast_package.VariableDeclaration)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛVariableDeclaration;
[GoMethodHost(typeof(ast_package.VariableDeclarationList)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛVariableDeclarationList;
[GoMethodHost(typeof(ast_package.VariableStatement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛVariableStatement;
[GoMethodHost(typeof(ast_package.VoidExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛVoidExpression;
[GoMethodHost(typeof(ast_package.WhileStatement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛWhileStatement;
[GoMethodHost(typeof(ast_package.WithStatement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛWithStatement;
[GoMethodHost(typeof(ast_package.YieldExpression)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛYieldExpression;
[GoMethodHost(typeof(ast_package.hasFileNameImpl)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class astᴛhasFileNameImpl;
// </MethodHolders>
