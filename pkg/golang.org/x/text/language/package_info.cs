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
// </ImportedTypeAliases>

using go;
using static go.golang.org.x.text.language_package;

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
[assembly: GoImplement<allSubtags, Coverage>]
[assembly: GoImplement<bases, sort_package.Interface>]
[assembly: GoImplement<coverage, Coverage>(Pointer = true)]
[assembly: GoImplement<matcher, Matcher>(Pointer = true)]
[assembly: GoImplement<tagSort, sort_package.Interface>(Pointer = true)]
// </InterfaceImplementations>

// <ImplicitConversions>
// </ImplicitConversions>

// The struct types whose fields this package reaches through a pointer, as Go spells it, `p.f`:
// go2cs-gen emits a field property on the pointer for each of their fields.
[assembly: GoFieldAccess(typeof(coverage))]
[assembly: GoFieldAccess(typeof(haveTag))]
[assembly: GoFieldAccess(typeof(matchHeader))]
[assembly: GoFieldAccess(typeof(matcher))]

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("golang.org/x/text@v0.42.0/language/coverage.go", "coverage.cs", "ABZGAAYagoKClKyCgoKUqoKCgoKUqIIADR6CgpQACAyCpoKmgsqCgoKClIKylIKCgoKCppSmgoKUpoKClAAEEqKCgpS0tLS0xMTE1LY=", "175-175:1;177-177:2;179-179:3;181-181:4")]
[assembly: GoPositionMap("golang.org/x/text@v0.42.0/language/language.go", "language.cs", "ABEwgsaCpoKogoK4gsqCgsqigqiCABRogoKUgoKCgqaCgoKUgoKUgsYAABSCgtiCgpSChMaCgpSmgoKCpoKCgqbIpIKCqoCSgqQACyS0gsiiqKKogoKCgqyigpSCgoKUgpQAAiKigIKkgpKAhoKUpIKCpoKCpqyigpSCgIKkgoCCpKqygpSCkoKClAADFoLIgoKClIIACBSCyKKCyoKClKiCzLKClIKosoKUgoKUAAMWooKCpq6iggAGGKKCpgAFFoKCyIKogqiCAAgYgoLKgqiCAAgUgoKsgoLKgqyCqoKsgqqCqoKqggACFIKCrIIACBSCgsiC")]
[assembly: GoPositionMap("golang.org/x/text@v0.42.0/language/match.go", "match.cs", "ABIogoAAAxjCgoKClIKmggAGHIKCAAIiosbCgoKCloKCgoKChpSCgoL+goKCgoCEvoKCgoKUlMoAGMACgoKCgoKUrIKEhKbKooSCgqqihoKUgqbKgoKUgoLGgoKUgpSsgriClIKCmLKCkoKUhrKCgoK+koKClIKCgoKUgoKUzoKCggADEoaCgoKUlJTKooKygoSCgoKWhoKYloKCgoK4hpSCgqaCsoKCqIKCgoKCgpS4goKUlAAUQqSCgpaCloKCgqaGlIKIgpSCgoQAAxCCgoKUqoKCgpSogoKClKaCgoKUpoKCgpSogoKClKiCgoKCgoKCgoKC2IKigqaqgpSCgoKCgoKCpoLKqIaqgoKCpsqGgoKCqKKCgoKUgg==", "21-21:1;402-420:1", "", "170=VariantOrPrivateUseTags/1/2/2,VariantOrPrivateUseTags/2/2/2")]
[assembly: GoPositionMap("golang.org/x/text@v0.42.0/language/parse.go", "parse.cs", "ABhEggAEFuKCgoK4koKUgoKUAAgaogAEGuKCgoK4goKUggAIBpTCgqS0tLS0goKUtIKClLSCgsaCgsiCyKaCAAES0oKCgoK4gqaCgoKmloKCgoKUqIKCgoaCloK4gpSC6oKClKaCgpSqAA4agqaCpoKC", "47-52:1;90-95:1;161-167:1", "", "", "", "38:21,39:13,78:21,79:13,174:21,175:13")]
[assembly: GoPositionMap("golang.org/x/text@v0.42.0/language/tables.go", "tables.cs", "ACpkAC9sABk6AHPsAQAcOg==")]
[assembly: GoPositionMap("golang.org/x/text@v0.42.0/language/tags.go", "tags.cs", "AAwagoKClMqCgoKUyoKCgpSqgoKClKqCgoKUqISUkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKS")]
// </GoSourcePositionMaps>

namespace go.golang.org.x.text;

[GoPackage("language", ImportPath = "golang.org/x/text/language")]
public static partial class language_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    internal partial struct allSubtags {}
    internal partial struct bases {}
    internal partial struct bestMatch {}
    internal partial struct coverage {}
    internal partial struct haveTag {}
    internal partial struct matchHeader {}
    internal partial struct mutualIntelligibility {}
    internal partial struct regionIntelligibility {}
    internal partial struct scriptIntelligibility {}
    internal partial struct tagSort {}
    public partial interface Coverage {}
    public partial interface Matcher {}
    public partial interface ValueError {}
    public partial struct Base {}
    public partial struct CanonType {}
    public partial struct Confidence {}
    public partial struct Extension {}
    public partial struct Region {}
    public partial struct Script {}
    public partial struct Tag {}
    public partial struct Variant {}
    public partial struct matcher {}
    // </TypeAccessibility>

    // Go initializes an imported package before the importing package, for every import
    // form - not only the blank one. .NET would never load an assembly nothing has touched
    // yet, so each import that initializes anything is forced below: once per assembly, and
    // ahead of this package's own `init` functions, which this file being the first compile
    // item of the project guarantees.

    // <ImportInitializers>
    [GoInit] internal static void initᴛᴛimportꓸerrors() => builtin.initPackage(typeof(errors_package));
    [GoInit] internal static void initᴛᴛimportꓸfmt() => builtin.initPackage(typeof(fmt_package));
    [GoInit] internal static void initᴛᴛimportꓸgolang_orgꓸxꓸtextꓸinternalꓸlanguage() => builtin.initPackage(typeof(go.golang.org.x.text.@internal.language_package));
    [GoInit] internal static void initᴛᴛimportꓸgolang_orgꓸxꓸtextꓸinternalꓸlanguageꓸcompact() => builtin.initPackage(typeof(go.golang.org.x.text.@internal.language.compact_package));
    [GoInit] internal static void initᴛᴛimportꓸsort() => builtin.initPackage(typeof(sort_package));
    [GoInit] internal static void initᴛᴛimportꓸstrconv() => builtin.initPackage(typeof(strconv_package));
    [GoInit] internal static void initᴛᴛimportꓸstrings() => builtin.initPackage(typeof(strings_package));
    // </ImportInitializers>
}

// A Go method is an extension method on its receiver type. The methods of one type are
// in a class of their own beside the package class, so that a method and a package-level
// type, constant or variable can share a name as they do in Go. Each class is declared
// below with the type it holds the methods of; its blocks are in the converted sources,
// in Go's declaration order.

// <MethodHolders>
[GoMethodHost(typeof(language_package.Base)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛBase;
[GoMethodHost(typeof(language_package.CanonType)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛCanonType;
[GoMethodHost(typeof(language_package.Confidence)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛConfidence;
[GoMethodHost(typeof(language_package.Extension)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛExtension;
[GoMethodHost(typeof(language_package.Region)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛRegion;
[GoMethodHost(typeof(language_package.Script)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛScript;
[GoMethodHost(typeof(language_package.Tag)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛTag;
[GoMethodHost(typeof(language_package.Variant)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛVariant;
[GoMethodHost(typeof(language_package.allSubtags)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛallSubtags;
[GoMethodHost(typeof(language_package.bases)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛbases;
[GoMethodHost(typeof(language_package.bestMatch)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛbestMatch;
[GoMethodHost(typeof(language_package.coverage)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛcoverage;
[GoMethodHost(typeof(language_package.matchHeader)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛmatchHeader;
[GoMethodHost(typeof(language_package.matcher)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛmatcher;
[GoMethodHost(typeof(language_package.tagSort)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛtagSort;
// </MethodHolders>
