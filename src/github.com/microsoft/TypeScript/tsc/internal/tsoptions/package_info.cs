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
global using astꓸExpression = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸPropertyName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTokenNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸTokenSyntaxKind = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Kind;
global using coreꓸResolutionMode = go.github.com.microsoft.TypeScript.tsc.@internal.core_package.ModuleKind;
// </ImportedTypeAliases>

using go;
using static go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package;

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
[assembly: GoTypeAlias("CompilerOptionsValue", "object")]
// </ExportedTypeAliases>

// As types are cast to interfaces in Go source code, the go2cs code converter
// will generate an assembly level `GoImplement` attribute for each unique cast.
// This allows the interface to be implemented in the C# source code using source
// code generation (see go2cs-gen). Resolving each duck-typed cast at compile time
// this way is what keeps startup free of reflection.

// <InterfaceImplementations>
[assembly: GoImplement<ParseConfigHost, go.github.com.microsoft.TypeScript.tsc.@internal.module_package.ResolutionHost>]
[assembly: GoImplement<ParsedCommandLine, go.github.com.microsoft.TypeScript.tsc.@internal.module_package.ResolvedProjectReference>(Pointer = true)]
[assembly: GoImplement<ParsedCommandLine, go.github.com.microsoft.TypeScript.tsc.@internal.outputpaths_package.OutputPathsHost>(Pointer = true)]
[assembly: GoImplement<buildOptionsParser, optionParser>(ConstraintProxy = true)]
[assembly: GoImplement<compilerOptionsParser, optionParser>(ConstraintProxy = true)]
[assembly: GoImplement<resolverHost, ParseConfigHost>(Promoted = true)]
[assembly: GoImplement<resolverHost, ParseConfigHost>]
[assembly: GoImplement<resolverHost, go.github.com.microsoft.TypeScript.tsc.@internal.module_package.ResolutionHost>(Pointer = true)]
[assembly: GoImplement<typeAcquisitionParser, optionParser>(ConstraintProxy = true)]
[assembly: GoImplement<watchOptionsParser, optionParser>(ConstraintProxy = true)]
// </InterfaceImplementations>

// <ImplicitConversions>
// </ImplicitConversions>

// The struct types whose fields this package reaches through a pointer, as Go spells it, `p.f`:
// go2cs-gen emits a field property on the pointer for each of their fields.
[assembly: GoFieldAccess(typeof(CommandLineOption))]
[assembly: GoFieldAccess(typeof(ExtendedConfigCacheEntry))]
[assembly: GoFieldAccess(typeof(ParseCommandLineWorkerDiagnostics))]
[assembly: GoFieldAccess(typeof(ParsedBuildCommandLine))]
[assembly: GoFieldAccess(typeof(ParsedCommandLine))]
[assembly: GoFieldAccess(typeof(ParsedOptions))]
[assembly: GoFieldAccess(typeof(TSConfig))]
[assembly: GoFieldAccess(typeof(TsConfigSourceFile))]
[assembly: GoFieldAccess(typeof(astꓸExpression))]
[assembly: GoFieldAccess(typeof(astꓸTokenNode))]
[assembly: GoFieldAccess(typeof(buildOptionsParser))]
[assembly: GoFieldAccess(typeof(commandLineParser))]
[assembly: GoFieldAccess(typeof(compilerOptionsParser))]
[assembly: GoFieldAccess(typeof(configFileSpecs))]
[assembly: GoFieldAccess(typeof(extendsResult))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.PrefixUnaryExpression))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.contentmapper_package.Mapper))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package.BuildOptions))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package.CompilerOptions))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package.ProjectReference))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package.TypeAcquisition))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.module_package.ResolvedModule))]
[assembly: GoFieldAccess(typeof(parsedTsconfig))]
[assembly: GoFieldAccess(typeof(projectReferenceParseResult))]
[assembly: GoFieldAccess(typeof(watchOptionsParser))]
[assembly: GoFieldAccess(typeof(wildcardDirectoryMatch))]

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/tsoptions/commandlineoption.go", "commandlineoption.cs", "ADqeAYKClKaCgpSmgoKUpoLIADqcAQAMHA==", "", "", "158=NewSetFromItems/1/3/1,NewSetFromItems/2/3/2,NewSetFromItems/3/3/3")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/tsoptions/commandlineparser.go", "commandlineparser.cs", "ABQggqaCpoKmggAPHIiClIKCgoK4goKCpoiClIKCgoKCpgANHISogpSClIKUgqamjAAHEIKCxqKCgoKCgpSUtIKCgpSCgpTYAAgKhMbSgoKClIKUgoKUgpSUgqaCgoKCgoKUgpSCgoKClIKClKaClKaixISUhoKUxqyCgoKUgoKCgoKClIKUpoKCuoKCgoKCgqaUlIKmgoKClKaUtpaClJaCxoKClJS0goKCgsi0goKCxoKmpoLGooKCgpSCgoKUlIKUgqSCgoKUgpSotIKCgpSClMisgpSCgoKUgoKU", "178-184:1;366-373:1;380-387:2", "", "58=FS/1/1/1,UseCaseSensitiveFileNames/1/1/1,GetCurrentDirectory/1/1/2;80=convertMapToOptions/1/2/1,convertMapToOptions/2/2/3,FS/1/1/9,UseCaseSensitiveFileNames/1/1/9,GetCurrentDirectory/1/1/10")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/tsoptions/contentmappers.go", "contentmappers.cs", "AA4igoKCgpSUgoKClIKClIKClJiCgpSCgpSCgg==")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/tsoptions/declsbuild.go", "declsbuild.cs", "AAgSAAwUAC1k")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/tsoptions/declscompiler.go", "declscompiler.cs", "AAwWpADWAfQDAKUG6A6UgoKUgpSCkoKCgpSClLiCgoKCgpSCgrimiKqIqoI=", "1221-1231:1;1254-1256:1;1263-1265:1;1269-1271:1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/tsoptions/declstypeacquisition.go", "declstypeacquisition.cs", "1t4=", "", "", "6=commandLineOptionsToMap/1/1/3")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/tsoptions/declswatch.go", "declswatch.cs", "AAoQ")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/tsoptions/diagnostics.go", "diagnostics.cs", "ABw4lIQADhwACRQ=")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/tsoptions/enummaps.go", "enummaps.cs", "AAsWAG3qAZKWhIKClIKClKYACBIAECIAECLcAAcQygAOHoKmooKClKYACBLu")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/tsoptions/errors.go", "errors.cs", "ABIcooKCgqaigpSmoqSkpOiMAAweAAIUgoKEgoKUpoKUgoKmpsKmooKUpoKUpKSkpMiClKSkpKQ=", "23-23:1", "", "57=UnknownOptionDiagnostic/1/1/2,AlternateMode/1/1/6,UnknownDidYouMeanDiagnostic/1/1/7,commandLineOptionsToMap/1/1/8")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/tsoptions/namemap.go", "namemap.cs", "AAoUkpKWgoKCgoKCpgAMFoKmhIKClKaChIKCgqY=")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/tsoptions/parsedbuildcommandline.go", "parsedbuildcommandline.cs", "ABw6ooKspqKClA==", "30-36:1;31-35:1.1;41-43:1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/tsoptions/parsedcommandline.go", "parsedcommandline.cs", "AB4+soKClIKCgpSCAB1GjAALFIKCggAUKIK2ooKUpoKmgqaCgoKU0oLKgpSUgriigpwABhCmgoKCgoKCuKaCpoKmgoKCgoKUgtyCgoKClIKCgoKUgoKCgsqClIKCgoKUgoKCAAgSoqiigqaCggAGEKaigoKmkoKCgoKCprioooKUpoKmgqaigpSmgqaCqIKmooKCgoKmpoKmooKUqoKuooKCgoampqKClKaigpSmgoSUqqKCgqaCgoKCuIKCgqaCgoK4pqKCgoKCpoK4poKmgoKmgqamooLeggAKFqaigpQ=", "136-154:1;158-172:1;159-163:1.1;160-162:1.1.1;198-208:1;212-250:1;263-271:1;282-293:1;335-341:1;359-361:1;370-372:1;380-382:1;490-492:1", "", "167=GetCurrentDirectory/1/1/3,UseCaseSensitiveFileNames/1/1/4;480=GetCurrentDirectory/1/1/2,CompilerOptions/1/1/3,ContentMapperExtensions/1/1/5")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/tsoptions/parsinghelpers.go", "parsinghelpers.cs", "ABIggoKUgpSClLiCgoKUgoKCppSmgoCCgoKUpKaCgpSmgoCSpICCgqQADhaCgoKAgoKCgraAgoKCgraU9oKCgpSCgoKClKaUgoKUppSCgpSmgpSqgoKClIKCgoKUlAANBoKCgoCCpICCpICCpICCpICCpICCgpSkgIKkgIKkgIK2AA4agqaCpoIACg6CpoKmggAKDoKmgqaCAAoOgqaCpoLGgoKUgpSCpoKCgpSUtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLSClMa0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLakpqKClKaigpSUtILGgsaCxrS0tKaigpSClJS0tLS0pqKClIKUgoKUlLS0tLS0tK6CgqiCgoCEgIKAgoKCAAoSgoKUgoKWgoKCuoK4pqaClIKCgqamgoKClIKCgIKogIKCgpTIgoKm", "719-721:1;724-729:2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/tsoptions/showconfig.go", "showconfig.cs", "AA0eggAIJgAZOsKClILMgoKCgqiWuKyUuoKCgoKCgpSUuIK4goKCgpSogoKmqIKClIKUqoKCgqasgoKUgpSCgoKmgoKogqaWgqaClIKEgoKUpqS0goSCgoKClIKmhIKCgoSCgpSmgqa2hIKCgqa2goKCgqbIttiqhoKCgoKCgrqsroKCppSEgoKogqqCqIKWgqiCgpS8goKCgqasooKUgoKCgpSUlKSCgoKUlA==", "16-18:1", "", "24=computeFn/1/14/1,computeFn/2/14/2,computeFn/3/14/3,computeFn/4/14/4,computeFn/5/14/5,computeFn/6/14/6,computeFn/7/14/7,computeFn/8/14/8,computeFn/9/14/9,computeFn/10/14/10,computeFn/11/14/11,computeFn/12/14/12,computeFn/13/14/13,computeFn/14/14/14;59=GetCurrentDirectory/1/1/1,UseCaseSensitiveFileNames/1/1/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/tsoptions/tsconfigparsing.go", "tsconfigparsing.cs", "ACpI7OwACRIAJ2aCgpSCgpSClIKCpqaCgpSCgoKmpoKClIKCgqYAEBqCgpQACxasgoKCgoIAAhCCgpSCgoKktLSCgoKCgoKUggAGEAAMHoKCgoKCgoKUgrim3oKC3gAOHKKClKaCuAAJFKiCgpSCgoKUiIKCgqaUppSigoKUgpSCgpSmgpSClIKUgpSCpqaMgqaUpILGgsiClKYAAhCCgoCCgoKUgoKUgoKYpMqCgoKUpoKCgoKUgqamAAMQgoKClIKUpoKktIKkxoKUpoKClKa4AAISgoKClIKClIKCgpSUgoKCgoKUgoKClJSCuJSmjoKCgoKUgoKCgoKCppaCgpSClJQADBaCgoKUpoIACBCCgoKClMaUpIKUpqKClIKElIKCgoSClIKCpoKCgpSmjIKClJaGlIKCgoKCgqamhqyCuIKClAANGLQAABaEpr6CgpSCgoKCpoKUgoKUgoKCgoKmgoKCgpaCgrisrIKClKaCgpSmgqaylKSkpoKUpqSClKSOtMymgoKUlK6CgoKClIKmgpSClKSCgoKUpIKClLSCgpSClIKClLqigoKUpoKCgoLupoKCgpSmgoKCgpSmgoKC1qyCgpSCgoKCgoKUgoKClNymgoKC7oKmgrgAAhCClIqClKaCpoKCgoK4po6CuIKCpoKCpoKCgqoAAhKCgoKCgoKClIKClKaCgoKUlIKKppKCgoKCgqKAgqSCgIKAgoKCgpSClIK4lKaCgoKCggAIDoKCgoCCpICCgra4hIKmgoKCgqaClIKUgpSClIKUgoKmlAAJFIKCAAISAAQWlIKClKaCgoKCgoKUkoKCgoKCgqaCgoKmlIKCgoKClIKCgoKClJSCgpS4goKCgoKCgoKClIKUpoKClIKCgoKIgoKCgoKmgoKCgoKmgoKCgqaCpgALGIKmgoKUgoKCgoKClIKCpoKClIKCgoaCgoKCgpTItIKUgoLYlIKGgoKGgoKUgoKCgoKClIKUgqrCgoKCgoKClIKUxKaSgoKCgoKCgpSCgpSCgpSClO6mkoKCkqYAFCqigoKmgqaygoKCgpSUgoKCgoKUgoKUpsaCgpSClKaGgqaMgoKUlIKUgoKUlKaCgpKClLiCgpSmspKCgoKmuLKCgoKCgoKAhqSmpqaCgoKYuIKugoKUgoKUgoKUgoKAhra4ooKUgoKCgpSCgpSCgsa4gpSUgpSCqoKClK6CgoKAhraswoKClKaygoKClIKCyqaigoKCpqaCpoKCgoKClKaClKaigqqCgoKCgpS4gpSClIKUgpSClIKUgpSClIKUgryCgoKCpoKUiIKUgoiUpqqigoKCpoKUgoKClIIAAxaOgoiIiIKCgoaChoKCpoKCgoKCgoKUgoKUgoKCpgABEJKClJyCgoK4goKUgpSClKaygoKClJSClIKCgoKmgpSmooKUgpSClKiOgqYAABCCgoSmhg==", "189-257:1;251-251:1.1;415-422:1;425-427:2;610-610:1;611-611:2;1123-1179:1;1129-1164:1.1;1136-1153:1.1.1;1265-1282:1;1283-1283:2;1300-1300:3;1385-1385:4;1403-1405:5;1414-1416:6;1455-1457:7;1460-1475:8;1477-1506:9;1479-1479:9.1;1547-1554:1;1615-1620:1;1631-1639:1;1645-1658:1;1649-1651:1.1;1664-1671:1;1666-1668:1.1;1686-1703:1;1696-1698:1.1;1725-1727:1;1747-1749:1;1757-1759:1;1942-1942:1;1975-1975:2;1996-1999:3", "", "57=commandLineOptionsToMap/1/1/4;67=commandLineOptionsToMap/1/1/3;220=Name/1/1/2,extraKeyDidYouMeanDiagnostics/1/1/3;227=Name/1/1/4;265=GetTextOfPropertyName/1/1/4;589=Values/1/1/2;809=Elements/1/1/2;958=ParseSourceFile/1/1/1;966=NewNodeList/1/1/1,NewToken/1/1/1,NewSourceFile/1/1/1,AsSourceFile/1/1/1;1084=FS/1/1/1,UseCaseSensitiveFileNames/1/1/1;1433=GetNormalizedAbsolutePath/1/1/1;1452=Invoke/1/1/5,FS/1/1/14,UseCaseSensitiveFileNames/1/1/14;1981=GetDirectoryPath/1/1/3")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/tsoptions/wildcarddirectories.go", "wildcarddirectories.cs", "AAoUAAIcgqaUgpSUgoKCpoKCgoKUgpSCpoKCgpSUgqaCzIKCgoLcpoKClAAIFISChIKCgpiUAAcQgoKCggAHEA==", "", "", "97=toCanonicalKey/1/1/1;109=toCanonicalKey/1/1/1")]
// </GoSourcePositionMaps>

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

[GoPackage("tsoptions", ImportPath = "github.com/microsoft/TypeScript/tsc/internal/tsoptions")]
public static partial class tsoptions_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    internal partial interface optionParser {}
    internal partial struct buildOptionsParser {}
    internal partial struct commandLineParser {}
    internal partial struct compilerOptionsParser {}
    internal partial struct configFileSpecs {}
    internal partial struct extendsResult {}
    internal partial struct extraValidation {}
    internal partial struct impliedOption {}
    internal partial struct jsonConversionNotifier {}
    internal partial struct parsedTsconfig {}
    internal partial struct projectReferenceParseResult {}
    internal partial struct propOfRaw {}
    internal partial struct resolverHost {}
    internal partial struct tsConfigOptions {}
    internal partial struct typeAcquisitionParser {}
    internal partial struct watchOptionsParser {}
    internal partial struct wildcardDirectoryMatch {}
    [GoLocalName("CompilerOptionsValue")] public partial interface CompilerOptionsValueᴅ {}
    public partial interface ExtendedConfigCache {}
    public partial interface ParseConfigHost {}
    public partial struct AlternateModeDiagnostics {}
    public partial struct CommandLineOption {}
    public partial struct CommandLineOptionKind {}
    public partial struct CommandLineOptionNameMap {}
    public partial struct DidYouMeanOptionsDiagnostics {}
    public partial struct ExtendedConfigCacheEntry {}
    public partial struct NameMap {}
    public partial struct ParseCommandLineWorkerDiagnostics {}
    public partial struct ParsedBuildCommandLine {}
    public partial struct ParsedCommandLine {}
    public partial struct ParsedOptions {}
    public partial struct SourceOutputAndProjectReference {}
    public partial struct TSConfig {}
    public partial struct TsConfigSourceFile {}
    // </TypeAccessibility>

    // Go initializes an imported package before the importing package, for every import
    // form - not only the blank one. .NET would never load an assembly nothing has touched
    // yet, so each import that initializes anything is forced below: once per assembly, and
    // ahead of this package's own `init` functions, which this file being the first compile
    // item of the project guarantees.

    // <ImportInitializers>
    [GoInit] internal static void initᴛᴛimportꓸfmt() => builtin.initPackage(typeof(fmt_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸast() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcollections() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.collections_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcontentmapper() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.contentmapper_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcore() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸdebug() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.debug_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸdiagnostics() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸglob() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.glob_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸjsnum() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.jsnum_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸjson() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.json_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸlocale() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.locale_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸmodule() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.module_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸoutputpaths() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.outputpaths_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸpackagejson() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.packagejson_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸparser() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.parser_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸscanner() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸstringutil() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtspath() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸvfs() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.vfs_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸvfsꓸvfsmatch() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.vfs.vfsmatch_package));
    [GoInit] internal static void initᴛᴛimportꓸiter() => builtin.initPackage(typeof(iter_package));
    [GoInit] internal static void initᴛᴛimportꓸmaps() => builtin.initPackage(typeof(maps_package));
    [GoInit] internal static void initᴛᴛimportꓸreflect() => builtin.initPackage(typeof(reflect_package));
    [GoInit] internal static void initᴛᴛimportꓸslices() => builtin.initPackage(typeof(slices_package));
    [GoInit] internal static void initᴛᴛimportꓸstrconv() => builtin.initPackage(typeof(strconv_package));
    [GoInit] internal static void initᴛᴛimportꓸstrings() => builtin.initPackage(typeof(strings_package));
    [GoInit] internal static void initᴛᴛimportꓸsync() => builtin.initPackage(typeof(sync_package));
    // </ImportInitializers>
}

// A Go method is an extension method on its receiver type. The methods of one type are
// in a class of their own beside the package class, so that a method and a package-level
// type, constant or variable can share a name as they do in Go. Each class is declared
// below with the type it holds the methods of; its blocks are in the converted sources,
// in Go's declaration order.

// <MethodHolders>
[GoMethodHost(typeof(tsoptions_package.CommandLineOption)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class tsoptionsᴛCommandLineOption;
[GoMethodHost(typeof(tsoptions_package.CommandLineOptionNameMap)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class tsoptionsᴛCommandLineOptionNameMap;
[GoMethodHost(typeof(tsoptions_package.ExtendedConfigCacheEntry)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class tsoptionsᴛExtendedConfigCacheEntry;
[GoMethodHost(typeof(tsoptions_package.NameMap)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class tsoptionsᴛNameMap;
[GoMethodHost(typeof(tsoptions_package.ParsedBuildCommandLine)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class tsoptionsᴛParsedBuildCommandLine;
[GoMethodHost(typeof(tsoptions_package.ParsedCommandLine)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class tsoptionsᴛParsedCommandLine;
[GoMethodHost(typeof(tsoptions_package.buildOptionsParser)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class tsoptionsᴛbuildOptionsParser;
[GoMethodHost(typeof(tsoptions_package.commandLineParser)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class tsoptionsᴛcommandLineParser;
[GoMethodHost(typeof(tsoptions_package.compilerOptionsParser)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class tsoptionsᴛcompilerOptionsParser;
[GoMethodHost(typeof(tsoptions_package.configFileSpecs)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class tsoptionsᴛconfigFileSpecs;
[GoMethodHost(typeof(tsoptions_package.resolverHost)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class tsoptionsᴛresolverHost;
[GoMethodHost(typeof(tsoptions_package.typeAcquisitionParser)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class tsoptionsᴛtypeAcquisitionParser;
[GoMethodHost(typeof(tsoptions_package.watchOptionsParser)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class tsoptionsᴛwatchOptionsParser;
// </MethodHolders>
