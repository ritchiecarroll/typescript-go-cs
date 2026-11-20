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
global using astꓸBindingElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸBindingName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸDeclaration = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸDotDotDotToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸExclamationToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸExpression = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸIdentifierNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸImportAttributesNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸImportClauseNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸImportPhaseModifierSyntaxKind = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind;
global using astꓸImportSpecifierList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸJsxChild = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸKeywordExpressionSyntaxKind = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind;
global using astꓸMemberName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸModuleExportName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸNamedImportBindings = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸObjectLiteralElement = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸPropertyName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸQuestionDotToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸStatementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTokenNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTypeList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTypeNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸVariableDeclarationListNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸVariableDeclarationNodeList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using stringutilꓸComparison = nint;
// </ImportedTypeAliases>

using go;
using static go.github.com.microsoft.TypeScript.tsc.@internal.transformers.jsxtransforms_package;

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
[assembly: GoFieldAccess(typeof(JSXTransformer))]
[assembly: GoFieldAccess(typeof(astꓸJsxChild))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.JsxElement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.JsxExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.JsxNamespacedName))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeVisitor))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.QualifiedName))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.StringLiteral))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package.CompilerOptions))]

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/jsxtransforms/jsx.go", "jsx.cs", "ACBAooKCuMaCgpQABhCCpoKClIKUpoKCpoKmgoKClIKCgoKmgqamgoKCpoKmsoKUgpSUgqSkpKS0tIKkgqSCzKKCgoKUgoKCgqbGgsaCgpSEgoKmxoKmgsaygoKUpoKCgsaygqaCgoKUgoKCgoKopoKCgoKCzIKWgpSCgoKCgoKClAAJFoKUgt6CpoKCgpSmsoKClIKmooKClIKmsoKClIKmgoKClKbCgoKC1oKCgoKClJiCgoKClIKUgpSmooKCgoKCgoKoprjCgoKClIKCgoKCgoKmgoKUgoKUlAAHEoKChJSmgoKUgoaCgoKCgpSUlIKClKaCppSIpoKUpoKClIKCpoKCgoKClKaClMaCgoKmxqKCgpSmooKCAAIQooKCgoKUlrqigpSGgoSClIKClJSCgpSmvoKClIKChoKmgoKEgpaClJaCzLiClIKmprKCgoKCpoKUAAcSioKUgpiagoKmpqKCgoKUpoKClAAGEIKCpoKCptKCgoKClKaCgpSmgoKCgoLMgoK4goKClN6UgpSmwoKUgoKCgoLMgoK4goKClN6UgpSmooKClMaGgoKUAAIkgoKEhIiCgoaCgqqCgoKCuIK4hJTGooKClM6CgoKmgoKCgpSCgqyCgoKUgoKmgoKGlJSUgoKmgqaCgqaCgoKmgoKCpoKmgoKUgriCgpSmgqY=", "", "", "51=Factory/2/3/1,NewUniqueNameEx/1/1/1,Factory/3/3/6,FileName/1/1/6,NewStringLiteral/1/1/6;245=Factory/3/3/1,NewNodeList/1/1/1;256=Factory/2/5/2,Factory/3/5/2,Factory/4/5/2,getSortedSpecifiers/1/1/2,NewNodeList/1/1/2,NewNamedImports/1/1/2,NewImportClause/1/1/2,Factory/5/5/3,NewStringLiteral/1/1/3;276=Factory/5/10/1,Factory/6/10/1,NewNodeList/1/3/1,NewBindingPattern/1/1/1,Factory/7/10/4,Factory/8/10/5,NewIdentifier/1/1/5,Factory/9/10/8,Factory/10/10/8,NewStringLiteral/1/1/8,NewNodeList/2/3/8,NewCallExpression/1/1/4;399=AsJsxNamespacedName/1/2/1,Text/1/2/1,AsJsxNamespacedName/2/2/1,Name/1/1/1,Text/2/2/1;562=AsJsxNamespacedName/1/2/1,Text/1/2/1,AsJsxNamespacedName/2/2/1,Name/1/1/1,Text/2/2/1;618=Factory/3/10/1,Factory/4/10/1,NewIdentifier/1/3/1,getCurrentFileNameExpression/1/1/1,NewPropertyAssignment/1/3/1,Factory/5/10/2,Factory/6/10/2,NewIdentifier/2/3/2,Factory/7/10/2,FormatInt/1/2/2,NewNumericLiteral/1/2/2,NewPropertyAssignment/2/3/2,Factory/8/10/3,Factory/9/10/3,NewIdentifier/3/3/3,Factory/10/10/3,FormatInt/2/2/3,NewNumericLiteral/2/2/3,NewPropertyAssignment/3/3/3;651=getImplicitJsxFragmentReference/1/1/1;691=createReactNamespace/1/1/1,Factory/2/2/3,NewIdentifier/1/1/3;751=Factory/2/2/4,NewNodeList/1/1/4;794=Factory/2/2/4,NewNodeList/1/1/4")]
// </GoSourcePositionMaps>

namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

[GoPackage("jsxtransforms", ImportPath = "github.com/microsoft/TypeScript/tsc/internal/transformers/jsxtransforms")]
public static partial class jsxtransforms_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    public partial struct JSXTransformer {}
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
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸprinter() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.printer_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸscanner() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸstringutil() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtransformers() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package));
    [GoInit] internal static void initᴛᴛimportꓸmaps() => builtin.initPackage(typeof(maps_package));
    [GoInit] internal static void initᴛᴛimportꓸslices() => builtin.initPackage(typeof(slices_package));
    [GoInit] internal static void initᴛᴛimportꓸstrconv() => builtin.initPackage(typeof(strconv_package));
    [GoInit] internal static void initᴛᴛimportꓸstrings() => builtin.initPackage(typeof(strings_package));
    [GoInit] internal static void initᴛᴛimportꓸunicodeꓸutf8() => builtin.initPackage(typeof(unicode.utf8_package));
    // </ImportInitializers>
}

// A Go method is an extension method on its receiver type. The methods of one type are
// in a class of their own beside the package class, so that a method and a package-level
// type, constant or variable can share a name as they do in Go. Each class is declared
// below with the type it holds the methods of; its blocks are in the converted sources,
// in Go's declaration order.

// <MethodHolders>
[GoMethodHost(typeof(jsxtransforms_package.JSXTransformer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class jsxtransformsᴛJSXTransformer;
// </MethodHolders>
