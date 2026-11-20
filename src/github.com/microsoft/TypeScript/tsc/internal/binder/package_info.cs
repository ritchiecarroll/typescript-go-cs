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
global using astꓸBinaryOperatorToken = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸCaseClausesList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸClassLikeDeclaration = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸDeclaration = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸExpression = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸFlowLabel = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FlowNode;
global using astꓸForInitializer = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸIdentifierNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸParameterDeclarationNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸStatementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
// </ImportedTypeAliases>

using go;
using static go.github.com.microsoft.TypeScript.tsc.@internal.binder_package;

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
[assembly: GoImplement<referenceResolver, ReferenceResolver>(Pointer = true)]
// </InterfaceImplementations>

// <ImplicitConversions>
// </ImplicitConversions>

// The struct types whose fields this package reaches through a pointer, as Go spells it, `p.f`:
// go2cs-gen emits a field property on the pointer for each of their fields.
[assembly: GoFieldAccess(typeof(ActiveLabel))]
[assembly: GoFieldAccess(typeof(Binder))]
[assembly: GoFieldAccess(typeof(astꓸBinaryOperatorToken))]
[assembly: GoFieldAccess(typeof(astꓸCaseClausesList))]
[assembly: GoFieldAccess(typeof(astꓸDeclaration))]
[assembly: GoFieldAccess(typeof(astꓸElementList))]
[assembly: GoFieldAccess(typeof(astꓸExpression))]
[assembly: GoFieldAccess(typeof(astꓸFlowLabel))]
[assembly: GoFieldAccess(typeof(astꓸForInitializer))]
[assembly: GoFieldAccess(typeof(astꓸIdentifierNode))]
[assembly: GoFieldAccess(typeof(astꓸStatementList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.BinaryExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.BindingElement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.BodyBase))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.CaseBlock))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.CaseOrDefaultClause))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.CatchClause))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ClassStaticBlockDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ConditionalExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ConditionalTypeNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ConstructorDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.DeclarationBase))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ElementAccessExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ExportAssignment))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ExportDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ExportableBase))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FlowList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FlowNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FlowNodeBase))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ForInOrOfStatement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ForStatement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FunctionDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FunctionExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.HeritageClause))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Identifier))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.IfStatement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.InferTypeNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.KeywordExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.LocalsContainerBase))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.MetaProperty))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ParameterDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.PostfixUnaryExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.PrefixUnaryExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.QualifiedName))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.SourceFile))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.SwitchStatement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Symbol))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.TryStatement))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package.CompilerOptions))]

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/binder/binder.go", "binder.cs", "AFC4AYK0hoK4lIKSyIKmooKmoqKCgoKCgoKi1oKCgoKCAAIWgqaigoSClLS0tIKCAAEygoKCgoKUhIKChoKCioKClJSCgoKUgoiCgoKcgoKCuIKClIKClJSElIKCgpSCgpSUgpSCgqaKgpS4goKCgpSqooKUgoKCgoKUgoKmlISChJSUgpSChIKUgoKUlJSUpKSkpKSkpqKCgpSCgpTGgsaigoKCgpQAASKCgpaCgpSCgoKUpoKClKaCgpSmgpSkpKSkqqSmgoKCpoKCgoKmgqaCpoKmsoKUgoKUlIKUgpSCpoKCgoKClKaCgqaCgoKmgoKCgqaigpSmgsaEgpTYsoKWgoKClJSClJSmooKUgpSmsoIAAiyUgrSClLSCxrS0gsaUtLS0tLS0tLS0tLS0tLS0grS0tLS0tLS0tLS0tLS0tLSUtLSCxraCxrS0tLS0tLS0tL6CgoKCgoKUlIKUlIKClKaCgoKCpoKCgoKChIKCuIKmsoKCgpSClIKUgoKChIKCgoLKgoKEioKUgtyCgoKCpqKClJS0tLTYooK4ooKEgoSCgriigoSYgoKEyoKmgqamgpS4ooKUtIKCxqqigpSCgoKClKaGgriCgpSCgoKmpqKClLSCgpS0AAAUgoKClIKmooKUgpSClLiOgoKCgoKmgoKCgoKUpqKCgoKCuILugoKCgr6igoKCgoLcgoKCgpSCgoKWgoL8opSkpMaigoKC2KKClIqUpoKCxoKCxsaigpSChJSCgoKUlIS4soKUyoKClMamgoKUuKKCgpS0AAMWtOqigoaUgoKUmIKCgriigpSCpoKCgpSClKaCgoKUpoKktIKClLTIooKCgpSmuKKClIKCgqaUgoKmpqKCgqaClKykhIKClIKCgoKCgpSCyqKEgsqGgpSClMiihqaCgoKCprjGooS4hIKUgpSmooKGuKaCgrikgoa4qqakgoK4hKakgoLYooKClMaihriEgpSClKy4goIAACSCgoKGlIKClIKUgoKCgoKCgoKCjIKCgoKqgpSUgoKCgoKChIKCgoKCgpSmgpSClIKCgoKmgpSCgoKCgoKClJSCgoKEgpSUlJSGgoKmgoKmgpSCgqaCgoKCgoKCgoKCgoKUuLKGlIKClIKUgoKmgoK4lLS0tLS0tLS0tLS0tLS0tLS0hoKClLS0tLS0tLSCgrS0tLSCtLSmoqaCgriigriigriigoKmgoLKooKCgoKUpqKCgoKCgoKmooKCgriigoKCgoKCxoLGopSCgpTYgpS0tPiC6qKClLiygoKCgoKCgoKCgqaygoKCgoKCgoKCgqaygoKMgoKClIKCgoKCgoKCgoKCgoKmsoKCjIKClIKCgoKClIKCgpSCgqaygoKCgoKCgoKCgoKmsoKClIKCprKCgqayprKmsoKCgoKCpriCgoKmpoKCgoK4voKCgoKCgoKUgoKCgoSIgoKCgpSCggAAHoKCgoKEmIKYgpqClLi4soKCgoKCgoKCgoaClIKCprKCgoKCgoKCgpSClIKCgpSCgoKCgoKCyrKCgoKCgpSmsoKCpqaCgsqygoLugoKElIKCpqKCgoKCgoKClIKCyqKCgoK4soKCgoKCgoKUgoKCgoKUprKCgoKCgoKCgoKClJSUpoKCgpSCgoKUgoKCgoLusoKCgpSUgoKCgoKClLiigoKCuLKCgoKCgoKCgoKCgoKCgoKCgpSUpqKCgriigpS0tIKCpriigpS4ooKCgoKCgpSUlLgAAxiCgpSCgpSCgoK4ooKCgriilIK0grSCgrSmsoKCmoKCgoKUgoK4goKCyqKClLisgoKCgqaigoKCgoKCqKKClIKCgpSCgoLGooKCuKKUtLS02KLGgoKUprKCgoKUloKClILYsoKKAAMaoqakpKSkgpSkpKSkpKSClLakgpS2pqKUpKSkpKSkpqKClIKUtqailKSkgqaCpqaigoKCpoKCpqaClKSCgqikpKSmopSkgpSk1qaixqKmsoKsoqaCxqKCgpSmooKmgoKCgpSCgoK4pqKUpKSkpqKClKaCpoI=", "121-129:1;909-911:1;1228-1231:1;2106-2108:1", "", "857=Has/1/1/4;1595=HasSyntacticModifier/1/1/1,isGeneratorFunctionExpression/1/1/2,GetImmediatelyInvokedFunctionExpression/1/1/3;2334=Text/1/1/2;2776=isAssignmentDeclaration/1/2/1,isAssignmentDeclaration/2/2/1,isEffectiveModuleDeclaration/1/1/2;2894=IsEntityNameExpression/1/1/1,isNarrowableReference/1/1/1;2898=IsAssignmentOperator/1/1/1,IsLeftHandSideExpression/1/1/1;2929=isNarrowingTypeOfOperands/1/2/1,isNarrowingTypeOfOperands/2/2/1,IsBooleanLiteral/1/2/2,isNarrowingExpression/1/2/2,IsBooleanLiteral/2/2/2,isNarrowingExpression/2/2/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/binder/nameresolver.go", "nameresolver.cs", "ACEysoKCgoKCgoKCgoKChpSGgpSGhIKCgoKCgo6GAAEQhIKCiqqGlIKUpoK0gpTEgpSCgpSChoKCgoKUAAEagoKmgoKClPqCgpSCgoKCpsaCgoKE6oKChIKUiIKUlJSCgoKC2IKCgoKCgpQABRiChIKCgpTIgoK2goKUgoKCggAEGIIAARqCtoKEgsiChILIgoKCgsiCgraClIq6goKmgpSCgoK4goKUgoKmgrimwoKCjIKCgpSCgoKmpqaigqaylKSkgpSkgpSClIKUyKKCuqKCqKaCgpaCgoKCuKaChJTGooKUgoKCpqaipqKGmIKWgpSmgoKCgoK4pqKUpqQ=", "", "", "56=AsModuleDeclaration/1/2/1,AsModuleDeclaration/2/2/2;66=Type/1/1/2;72=Type/1/1/2,FindAncestor/1/1/2;237=Name/1/1/1,IsBindingPattern/1/1/1;245=Name/1/1/1,IsBindingPattern/1/1/1;438=IsFunctionLikeDeclaration/1/1/1,IsStatic/1/1/1,Name/1/1/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/binder/referenceresolver.go", "referenceresolver.cs", "AB46AAcQgvyCgoKmpoKCgpSUpqKCgpSUpqKCgpSUpqKCgqaCgqaCpoLKpoKCgqaCgpSkgpSCtIK0pqaipqKCgpSClJSmuoKAgoiCgpSUgoKCgoSCgpSUlsimgoaCuKaCgpSmooKCgoIAASbYtoKCgoK4pqSChJSClA==", "180-182:1")]
// </GoSourcePositionMaps>

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

[GoPackage("binder", ImportPath = "github.com/microsoft/TypeScript/tsc/internal/binder")]
public static partial class binder_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    internal partial struct referenceResolver {}
    public partial interface ReferenceResolver {}
    public partial struct ActiveLabel {}
    public partial struct Binder {}
    public partial struct ContainerFlags {}
    public partial struct ExpandoAssignmentInfo {}
    public partial struct NameResolver {}
    public partial struct ReferenceResolverHooks {}
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
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtspath() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package));
    [GoInit] internal static void initᴛᴛimportꓸslices() => builtin.initPackage(typeof(slices_package));
    [GoInit] internal static void initᴛᴛimportꓸstrconv() => builtin.initPackage(typeof(strconv_package));
    [GoInit] internal static void initᴛᴛimportꓸsync() => builtin.initPackage(typeof(sync_package));
    // </ImportInitializers>
}

// A Go method is an extension method on its receiver type. The methods of one type are
// in a class of their own beside the package class, so that a method and a package-level
// type, constant or variable can share a name as they do in Go. Each class is declared
// below with the type it holds the methods of; its blocks are in the converted sources,
// in Go's declaration order.

// <MethodHolders>
[GoMethodHost(typeof(binder_package.ActiveLabel)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class binderᴛActiveLabel;
[GoMethodHost(typeof(binder_package.Binder)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class binderᴛBinder;
[GoMethodHost(typeof(binder_package.NameResolver)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class binderᴛNameResolver;
[GoMethodHost(typeof(binder_package.referenceResolver)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class binderᴛreferenceResolver;
// </MethodHolders>
