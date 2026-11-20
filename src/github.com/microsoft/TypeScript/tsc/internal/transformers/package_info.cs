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
global using astꓸColonToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸDotDotDotToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸEqualsToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸExclamationToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸExpression = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸIdentifierNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸMemberName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸObjectLiteralElement = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸPropertyName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸQuestionDotToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸQuestionToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸStatement = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTokenNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTokenSyntaxKind = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind;
global using astꓸTypeNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
// </ImportedTypeAliases>

using go;
using static go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;

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
[assembly: GoTypeAlias("TransformerFactory", "System.Func<go.ж<go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.TransformOptions>, go.ж<go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package.Transformer>>")]
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
[assembly: GoFieldAccess(typeof(TransformOptions))]
[assembly: GoFieldAccess(typeof(astꓸBindingElementList))]
[assembly: GoFieldAccess(typeof(astꓸExpression))]
[assembly: GoFieldAccess(typeof(flattener))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.BinaryExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ConditionalExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ElementAccessExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ForStatement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ImportEqualsDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ShorthandPropertyAssignment))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TaggedTemplateExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TryStatement))]
[assembly: GoFieldAccess(typeof(pendingDecl))]

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/chain.go", "chain.cs", "ABUeooKUgoKUAAweooKClJSSgoSCppSkpII=", "45-61:1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/destructuring.go", "destructuring.cs", "ABM2joKChIKCgoIACh4AABCChIKCgoIAEDCCAAcQgqaCpoKmooKClIKUgqqCpoKmgqaigoKUAAcUgqaigpSCgoKCgpSUprKCAAcSsoKCgoKClIK8soKCgoKCgoKClMqCgoKCgoKCuJSCgpSmgoKUprKCgoKCuJSCgoKCgpSCgoKCuIKCgoKUgoKClKaClIKUqrKCgpSCgoKCgoKmlIK4goKClLjSgoKCgpSCgoKCgoiUgoKUgoKUlIKCgpSCpoIADBLCgoKCgpSCgoKCgoKCgpSClJSCgoKCgoKCpoKUgpIABhCCgoKUgoKClKaCgoKCpqiCgoKUgqaCgqiigpSClIKCgpSUgpSClIKUpqKmoqaCgoKUgoKUgoKUgpQ=", "", "", "152=Factory/2/4/1,NewTypeCheck/1/1/1,Factory/3/4/2,NewToken/1/2/2,Factory/4/4/4,NewToken/2/2/4;324=SubtreeFacts/1/2/1,GetTargetOfBindingOrAssignmentElement/1/1/2,SubtreeFacts/2/2/2,IsComputedPropertyName/1/1/3")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/modifiervisitor.go", "modifiervisitor.cs", "ABAaooKClMaCgpSCgg==")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/transformer.go", "transformer.cs", "ABEcsoKUgpSCgoKmgqaCpoKmgg==")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/transformers/utilities.go", "utilities.cs", "AAsYoqaipqKmoqaiAAEwAAIgAAIUpKSqpqSkqKakpKTIsoKCgoKUgoKCgpSCgoKCgpSmsoKCgoKUgoKClIKCgpSCgpQABhCCgqailKSk2LKCgpSCgoKCgqaygoKUgoKCgoKmooKUprKClIKCgoKmgoKUgpQAAhSirKKClIKClIKClIKCAAIQgqiCgoKmgoKCgoKCgrioooKUgoKUqKKCpoKCpoKUqKKCgoKCuIKUqIKUpKSkpKSkpKSkpKSkpKSk", "", "", "53=AsForStatement/1/2/1,AsForStatement/2/2/2;56=Expression/1/1/1;65=AsConditionalExpression/2/3/1,AsConditionalExpression/3/3/2;68=Arguments/1/1/1,Contains/1/1/1;135=Name/1/1/2;232=IsNumericLiteral/1/1/1,IsKeywordKind/1/1/2,IsIdentifier/1/1/3")]
// </GoSourcePositionMaps>

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

[GoPackage("transformers", ImportPath = "github.com/microsoft/TypeScript/tsc/internal/transformers")]
public static partial class transformers_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    internal partial struct chainedTransformer {}
    internal partial struct flattener {}
    internal partial struct modifierVisitor {}
    internal partial struct pendingDecl {}
    internal partial struct restIdElemPair {}
    public partial struct FlattenLevel {}
    public partial struct TransformOptions {}
    public partial struct Transformer {}
    // </TypeAccessibility>

    // Go initializes an imported package before the importing package, for every import
    // form - not only the blank one. .NET would never load an assembly nothing has touched
    // yet, so each import that initializes anything is forced below: once per assembly, and
    // ahead of this package's own `init` functions, which this file being the first compile
    // item of the project guarantees.

    // <ImportInitializers>
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸast() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸbinder() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.binder_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcore() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸprinter() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.printer_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸscanner() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package));
    [GoInit] internal static void initᴛᴛimportꓸslices() => builtin.initPackage(typeof(slices_package));
    [GoInit] internal static void initᴛᴛimportꓸstrconv() => builtin.initPackage(typeof(strconv_package));
    // </ImportInitializers>
}

// A Go method is an extension method on its receiver type. The methods of one type are
// in a class of their own beside the package class, so that a method and a package-level
// type, constant or variable can share a name as they do in Go. Each class is declared
// below with the type it holds the methods of; its blocks are in the converted sources,
// in Go's declaration order.

// <MethodHolders>
[GoMethodHost(typeof(transformers_package.Transformer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class transformersᴛTransformer;
[GoMethodHost(typeof(transformers_package.chainedTransformer)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class transformersᴛchainedTransformer;
[GoMethodHost(typeof(transformers_package.flattener)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class transformersᴛflattener;
[GoMethodHost(typeof(transformers_package.modifierVisitor)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class transformersᴛmodifierVisitor;
// </MethodHolders>
