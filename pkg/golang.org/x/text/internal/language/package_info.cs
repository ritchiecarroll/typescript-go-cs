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
using static go.golang.org.x.text.@internal.language_package;

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
[assembly: GoImplement<ValueError, error>]
[assembly: GoImplement<bytesSort, sort_package.Interface>]
[assembly: GoImplement<bytes_package.Buffer, io_package.Writer>(Pointer = true)]
[assembly: GoImplement<sortVariants, sort_package.Interface>]
[assembly: GoImplement<variantsSort, sort_package.Interface>]
// </InterfaceImplementations>

// <ImplicitConversions>
[assembly: GoImplicitConv<CompactCoreInfo, Language>(Inverted = true, ValueType = "uint32")]
[assembly: GoImplicitConv<CompactCoreInfo, Region>(Inverted = true, ValueType = "uint32")]
[assembly: GoImplicitConv<CompactCoreInfo, Script>(Inverted = true, ValueType = "uint32")]
[assembly: GoImplicitConv<Language, CompactCoreInfo>(Inverted = true, ValueType = "uint16")]
[assembly: GoImplicitConv<Region, CompactCoreInfo>(Inverted = true, ValueType = "uint16")]
[assembly: GoImplicitConv<Script, CompactCoreInfo>(Inverted = true, ValueType = "uint16")]
// </ImplicitConversions>

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("golang.org/x/text@v0.42.0/internal/language/compact.go", "compact.cs", "AAwYooKUgoKCyII=")]
[assembly: GoPositionMap("golang.org/x/text@v0.42.0/internal/language/compose.go", "compose.cs", "ABYugpSCgpSClIKCgoKCgoKGgoKEgoKUqoKCgoSCgoKmgoK+goKClJSCgoKUpq6CgoKUgoKClJSmqKKCgs6CqoKCxrKClKaigoKCgpTqgqaCpoI=")]
[assembly: GoPositionMap("golang.org/x/text@v0.42.0/internal/language/coverage.go", "coverage.cs", "AAgSgoKEgqaCooKClIKm")]
[assembly: GoPositionMap("golang.org/x/text@v0.42.0/internal/language/language.go", "language.cs", "ABt6goLMgqiCqIKClKqCrLKClIKClIKCgoKUgoKCgoKCgpSClKyCgoKClIKClKiCgpSClIKoooKCgpSClKiCgoKqgoKUqIKClKqCrIKEgoKCgoKmlIKCgoKCpoKCgoIACRaCgpSChoKClKbIwoKCgriCgoKUgoKClAAICIKogqyCgoKCgqaoooKCgoKUAAMWgoKCgpSUyIK+goKUgqiCgoTItIKClKamgqasgoKCqIKCgoKCgoKogoKUgoKCgpSUpgACEMKCgpSWgoKClIKolpaCgpSCgoKUlJamgoKUggAIEMKCgoK4gpSC7MKCgoK4gpSC6oKswoKCgriClIIACAqCgpSqgoKUqoKClIKClJSCmoKUxgADEoaClIKUrIKClAAJFsKCgoK4goKUAAgIgg==", "254-259:1;472-477:1;490-495:1;514-519:1;610-615:1", "", "", "", "205:21,206:13,411:21,412:13,434:21,435:13,461:21,462:13,549:21,550:13")]
[assembly: GoPositionMap("golang.org/x/text@v0.42.0/internal/language/lookup.go", "lookup.cs", "ABEkgoKUgoKUpoIABRKCgpTMgsiChoKUqoKClIKUyoKCgoKUqoKCgryChIKIgoKUpoCCpIKCloKCppTKgoKCgoKUgoKUrIKCgoKCgoKUgoKUqIKClIKCgoKYqIKqgoKU7oKCgpSCpqqCgoKUqoKCgoKmgoKmlKaCgqqCgoKWgqaCgqyCgpaClAAIEoKqgoKClJSCrIKClIKClKSkqoKsgu6CgsqCgpSoggAHFgAfSKqygoKUgpQ=", "30-32:1;55-57:1;251-253:1;268-270:1")]
[assembly: GoPositionMap("golang.org/x/text@v0.42.0/internal/language/match.go", "match.cs", "ABMigoK4goK4goLcvIKSgoKClILIgoKCgpSUyILGhIKUgoSCmIKAgqSEgoK4hIKCgoKCgoKCpoKCgoKKgoKCgqaCmILegoKCgoKqgoKCgoKCpoKClIKCgt6CgoKUgoKUgoKUlMaCgoKqooKClILIgoKUgoKUyoKCpg==")]
[assembly: GoPositionMap("golang.org/x/text@v0.42.0/internal/language/parse.go", "parse.cs", "ABAkgqiCgoKmrJgABRSCgoLGooKClKiiqKIADx6CgoKUlIKqgoKCxoKCgqaogoKCgsqCgr6CgoKCgoKClJSCgoK6goKqgoKCgpSClKiCgoKCgq6igoKCgoKCgpSClIKCgpSClIKClIKqooKCgpQABBTEgpSCgoKCpoKCpIKCgpSUgqaC5rKCgoKCgpSCgpSCgoKCgoKmgoKUgpSmlKzChIKCgoKChoKCgoKCgoKUlJSCgoKUlIKCgpSUlIKCgoKmmKKCgoKCgoKChoKGgpSCgoKClIaCgriUgoKSgoKElIKCgpSClIKCpgAJEIKmgoKmggALEIKmgqaCgoKUlMyigoKCgoKCgoKCgoKCgoKCgoKClJSCgpSCgoKElKqikpSCgoKEgoKCgoKUgoKUgpiSgoKCgpaGgoKCgoKCgpSUgoKCgoKCgoKmlIKCgpSC2IKAgoKkgsa0tKiigpSClIKugoKCgpSUpg==", "235-241:1", "", "", "", "219:21,220:13")]
[assembly: GoPositionMap("golang.org/x/text@v0.42.0/internal/language/tables.go", "tables.cs", "AHqKBACXAoAFvADEAYoDAB1uALoBrgMAM7ABzAAWNAAvfM4ALGgAe4ICAJcBugIApQrQFABp4AEAywGcAwBlzgEAIkgACyQAL3QAFTgAFDA=")]
[assembly: GoPositionMap("golang.org/x/text@v0.42.0/internal/language/tags.go", "tags.cs", "AAgSgoKClKqCgoKUqoKCgpSqgoKClA==")]
// </GoSourcePositionMaps>

namespace go.golang.org.x.text.@internal;

[GoPackage("language", ImportPath = "golang.org/x/text/internal/language")]
public static partial class language_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    internal partial struct bytesSort {}
    internal partial struct likelyLangRegion {}
    internal partial struct likelyLangScript {}
    internal partial struct likelyScriptRegion {}
    internal partial struct likelyTag {}
    internal partial struct parentRel {}
    [GoValueClone("bytes")] internal partial struct scanner {}
    internal partial struct scriptRegionFlags {}
    internal partial struct sortVariants {}
    internal partial struct variantsSort {}
    public partial struct AliasType {}
    public partial struct Builder {}
    public partial struct CompactCoreInfo {}
    public partial struct FromTo {}
    public partial struct Language {}
    public partial struct Region {}
    public partial struct Script {}
    public partial struct Tag {}
    [GoValueClone("v")] public partial struct ValueError {}
    public partial struct Variant {}
    // </TypeAccessibility>

    // Go initializes an imported package before the importing package, for every import
    // form - not only the blank one. .NET would never load an assembly nothing has touched
    // yet, so each import that initializes anything is forced below: once per assembly, and
    // ahead of this package's own `init` functions, which this file being the first compile
    // item of the project guarantees.

    // <ImportInitializers>
    [GoInit] internal static void initᴛᴛimportꓸbytes() => builtin.initPackage(typeof(bytes_package));
    [GoInit] internal static void initᴛᴛimportꓸerrors() => builtin.initPackage(typeof(errors_package));
    [GoInit] internal static void initᴛᴛimportꓸfmt() => builtin.initPackage(typeof(fmt_package));
    [GoInit] internal static void initᴛᴛimportꓸgolang_orgꓸxꓸtextꓸinternalꓸtag() => builtin.initPackage(typeof(go.golang.org.x.text.@internal.tag_package));
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
[GoMethodHost(typeof(language_package.Builder)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛBuilder;
[GoMethodHost(typeof(language_package.CompactCoreInfo)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛCompactCoreInfo;
[GoMethodHost(typeof(language_package.Language)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛLanguage;
[GoMethodHost(typeof(language_package.Region)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛRegion;
[GoMethodHost(typeof(language_package.Script)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛScript;
[GoMethodHost(typeof(language_package.Tag)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛTag;
[GoMethodHost(typeof(language_package.ValueError)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛValueError;
[GoMethodHost(typeof(language_package.Variant)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛVariant;
[GoMethodHost(typeof(language_package.bytesSort)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛbytesSort;
[GoMethodHost(typeof(language_package.scanner)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛscanner;
[GoMethodHost(typeof(language_package.sortVariants)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛsortVariants;
[GoMethodHost(typeof(language_package.variantsSort)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class languageᴛvariantsSort;
// </MethodHolders>
