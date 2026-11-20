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
global using astꓸDeclarationName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸElementList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸExpression = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸParameterDeclarationNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸParameterList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
global using astꓸTokenNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTypeList = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList;
// </ImportedTypeAliases>

using go;
using static go.github.com.microsoft.TypeScript.tsc.@internal.pseudochecker_package;

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
[assembly: GoImplement<PseudoGetAccessor, pseudoObjectElementData>(Pointer = true)]
[assembly: GoImplement<PseudoObjectMethod, pseudoObjectElementData>(Pointer = true)]
[assembly: GoImplement<PseudoPropertyAssignment, pseudoObjectElementData>(Pointer = true)]
[assembly: GoImplement<PseudoSetAccessor, pseudoObjectElementData>(Pointer = true)]
[assembly: GoImplement<PseudoTypeBase, pseudoTypeData>(Pointer = true)]
[assembly: GoImplement<PseudoTypeDirect, pseudoTypeData>(Pointer = true)]
[assembly: GoImplement<PseudoTypeInferred, pseudoTypeData>(Pointer = true)]
[assembly: GoImplement<PseudoTypeLiteral, pseudoTypeData>(Pointer = true)]
[assembly: GoImplement<PseudoTypeMaybeConstLocation, pseudoTypeData>(Pointer = true)]
[assembly: GoImplement<PseudoTypeNoResult, pseudoTypeData>(Pointer = true)]
[assembly: GoImplement<PseudoTypeObjectLiteral, pseudoTypeData>(Pointer = true)]
[assembly: GoImplement<PseudoTypeSingleCallSignature, pseudoTypeData>(Pointer = true)]
[assembly: GoImplement<PseudoTypeTuple, pseudoTypeData>(Pointer = true)]
[assembly: GoImplement<PseudoTypeUnion, pseudoTypeData>(Pointer = true)]
// </InterfaceImplementations>

// <ImplicitConversions>
// </ImplicitConversions>

// The struct types whose fields this package reaches through a pointer, as Go spells it, `p.f`:
// go2cs-gen emits a field property on the pointer for each of their fields.
[assembly: GoFieldAccess(typeof(PseudoObjectElement))]
[assembly: GoFieldAccess(typeof(PseudoType))]
[assembly: GoFieldAccess(typeof(PseudoTypeDefault))]
[assembly: GoFieldAccess(typeof(PseudoTypeDirect))]
[assembly: GoFieldAccess(typeof(PseudoTypeInferred))]
[assembly: GoFieldAccess(typeof(PseudoTypeMaybeConstLocation))]
[assembly: GoFieldAccess(typeof(PseudoTypeUnion))]
[assembly: GoFieldAccess(typeof(astꓸDeclarationName))]
[assembly: GoFieldAccess(typeof(astꓸElementList))]
[assembly: GoFieldAccess(typeof(astꓸExpression))]
[assembly: GoFieldAccess(typeof(astꓸParameterList))]
[assembly: GoFieldAccess(typeof(astꓸTokenNode))]
[assembly: GoFieldAccess(typeof(astꓸTypeList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.DeclarationBase))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FunctionLikeBase))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.IntersectionTypeNode))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.ParameterDeclaration))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Symbol))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.UnionTypeNode))]

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/pseudochecker/checker.go", "checker.cs", "AAkmgg==")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/pseudochecker/lookup.go", "lookup.cs", "AA0WopSqpILIgqaCpqKUpKSkpKSkpJbIyKSCyKKCgpSCgoKCgrqoooKCmKaigoKUgoKEgpSCgoSUuqaigoKUgoKEgpSCgrqmooKCgpSCgoKCgpSUlKaCgoKUgpSmooKWgpSCgpSCgpTGgsiigoSCgqaClKaigoKCgqaCgpKCgpSClIKUpqaCgoKCgoKUgqamqKKUpqiCxqSkpKSCxqSkpoKUpKSkpKSkpKaigpaClIKClIKC3gAKFP6CgtioopaIgt4ABxKGgoLerKKClIKCgoKUgoKUgoKUgoKUgoKCuKaigpSCloKClK6igpSCgqbIgpikqqSuxqKCgpSCgpSClIKmgoKUpqKClIKCggAGEKKClIKUgoKUxqKmooKUlqikpKSkpKTKooKUgoKUhIKUgpSmooKClAACEIKCgqamioKWxqKCgpaCgpSUgoKCpqKCgpSCgoKIgpSUgoKClIKUgpacpqKClIKUgoKCgoKIlN7GgoSClIKUgpQ=", "226-238:1;486-489:1;716-728:1", "", "373=Name/1/1/2,FunctionLikeData/1/1/4,NewPseudoTypeDirect/1/1/4;379=Name/1/1/2,AsMethodDeclaration/1/1/4,cloneTypeParameters/1/1/4,ParameterList/1/1/5,cloneParameters/1/1/5,createReturnFromSignature/1/1/6;390=Name/1/1/2,AsPropertyAssignment/1/2/3,AsPropertyAssignment/2/2/3,Initializer/1/1/4,typeFromExpression/1/1/4;414=AsParameterDeclaration/1/1/1;416=typeFromAccessor/1/1/4;422=AsSetAccessorDeclaration/1/1/4,cloneParameters/1/1/4;743=Name/1/1/2,typeFromParameterWorker/1/1/4")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/pseudochecker/type.go", "type.cs", "ACRigoKCggAMFv6CgoKCgoKCggAGEoLGAAocgqaCxgAIFILGAAomgsaCAAkSgsaCAAsUggAKGIIACRCCAAkSguqCAA0UlIKUpKSkABEigoKCgoKCAAoWggAJEIIAChKC/IIAChKC/IIAChKC/IIACRKC6oIACRKCyoLKguqC")]
// </GoSourcePositionMaps>

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

[GoPackage("pseudochecker", ImportPath = "github.com/microsoft/TypeScript/tsc/internal/pseudochecker")]
public static partial class pseudochecker_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    internal partial interface pseudoObjectElementData {}
    internal partial interface pseudoTypeData {}
    public partial struct PseudoChecker {}
    public partial struct PseudoGetAccessor {}
    public partial struct PseudoObjectElement {}
    public partial struct PseudoObjectElementKind {}
    public partial struct PseudoObjectMethod {}
    public partial struct PseudoParameter {}
    public partial struct PseudoPropertyAssignment {}
    public partial struct PseudoSetAccessor {}
    public partial struct PseudoType {}
    public partial struct PseudoTypeBase {}
    public partial struct PseudoTypeDefault {}
    public partial struct PseudoTypeDirect {}
    public partial struct PseudoTypeInferred {}
    public partial struct PseudoTypeKind {}
    public partial struct PseudoTypeLiteral {}
    public partial struct PseudoTypeMaybeConstLocation {}
    public partial struct PseudoTypeNoResult {}
    public partial struct PseudoTypeObjectLiteral {}
    public partial struct PseudoTypeSingleCallSignature {}
    public partial struct PseudoTypeTuple {}
    public partial struct PseudoTypeUnion {}
    // </TypeAccessibility>

    // Go initializes an imported package before the importing package, for every import
    // form - not only the blank one. .NET would never load an assembly nothing has touched
    // yet, so each import that initializes anything is forced below: once per assembly, and
    // ahead of this package's own `init` functions, which this file being the first compile
    // item of the project guarantees.

    // <ImportInitializers>
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸast() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcore() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸdebug() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.debug_package));
    [GoInit] internal static void initᴛᴛimportꓸslices() => builtin.initPackage(typeof(slices_package));
    // </ImportInitializers>
}

// A Go method is an extension method on its receiver type. The methods of one type are
// in a class of their own beside the package class, so that a method and a package-level
// type, constant or variable can share a name as they do in Go. Each class is declared
// below with the type it holds the methods of; its blocks are in the converted sources,
// in Go's declaration order.

// <MethodHolders>
[GoMethodHost(typeof(pseudochecker_package.PseudoChecker)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class pseudocheckerᴛPseudoChecker;
[GoMethodHost(typeof(pseudochecker_package.PseudoObjectElement)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class pseudocheckerᴛPseudoObjectElement;
[GoMethodHost(typeof(pseudochecker_package.PseudoType)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class pseudocheckerᴛPseudoType;
[GoMethodHost(typeof(pseudochecker_package.PseudoTypeDefault)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class pseudocheckerᴛPseudoTypeDefault;
// </MethodHolders>
