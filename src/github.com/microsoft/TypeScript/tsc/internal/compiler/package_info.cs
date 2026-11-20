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
global using astꓸDeclarationName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸExpression = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸImportAttributesNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸImportClauseNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸLiteralLikeNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸModuleName = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸStringLiteralLike = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using astꓸStringLiteralNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using coreꓸResolutionMode = go.github.com.microsoft.TypeScript.tsc.@internal.core_package.ModuleKind;
// </ImportedTypeAliases>

using go;
using static go.github.com.microsoft.TypeScript.tsc.@internal.compiler_package;

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
[assembly: GoImplement<CompilerHost, go.github.com.microsoft.TypeScript.tsc.@internal.module_package.ResolutionHost>]
[assembly: GoImplement<EmitHost, SourceFileMayBeEmittedHost>]
[assembly: GoImplement<Program, ProgramLike>(Pointer = true)]
[assembly: GoImplement<Program, SourceFileMayBeEmittedHost>(Pointer = true)]
[assembly: GoImplement<Program, go.github.com.microsoft.TypeScript.tsc.@internal.checker_package.Program>(Pointer = true)]
[assembly: GoImplement<Program, go.github.com.microsoft.TypeScript.tsc.@internal.outputpaths_package.OutputPathsHost>(Pointer = true)]
[assembly: GoImplement<checkerPool, CheckerPool>(Pointer = true)]
[assembly: GoImplement<compilerHost, CompilerHost>(Pointer = true)]
[assembly: GoImplement<compilerHost, go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package.ParseConfigHost>(Pointer = true)]
[assembly: GoImplement<declarationMapSource, go.github.com.microsoft.TypeScript.tsc.@internal.sourcemap_package.Source>(Pointer = true)]
[assembly: GoImplement<emitHost, EmitHost>(Pointer = true)]
[assembly: GoImplement<emitHost, SourceFileMayBeEmittedHost>(Pointer = true)]
[assembly: GoImplement<emitHost, go.github.com.microsoft.TypeScript.tsc.@internal.outputpaths_package.OutputPathsHost>(Pointer = true)]
[assembly: GoImplement<go.github.com.microsoft.TypeScript.tsc.@internal.outputpaths_package.OutputPaths, go.github.com.microsoft.TypeScript.tsc.@internal.transformers.declarations_package.OutputPaths>(Pointer = true)]
[assembly: GoImplement<go.github.com.microsoft.TypeScript.tsc.@internal.transformers.declarations_package.DeclarationTransformer, declarationTransformer>(Pointer = true)]
[assembly: GoImplement<go.github.com.microsoft.TypeScript.tsc.@internal.transformers.declarations_package.SupplementalReferencesTransformer, declarationTransformer>(Pointer = true)]
[assembly: GoImplement<parseTask, go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.HasFileName>(Pointer = true)]
[assembly: GoImplement<projectReferenceDtsFakingHost, go.github.com.microsoft.TypeScript.tsc.@internal.module_package.ResolutionHost>(Pointer = true)]
[assembly: GoImplement<projectReferenceDtsFakingVfs, go.github.com.microsoft.TypeScript.tsc.@internal.vfs_package.FS>(Pointer = true)]
[assembly: GoImplement<redirectsFile, go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.HasFileName>(Pointer = true)]
// </InterfaceImplementations>

// <ImplicitConversions>
// </ImplicitConversions>

// The struct types whose fields this package reaches through a pointer, as Go spells it, `p.f`:
// go2cs-gen emits a field property on the pointer for each of their fields.
[assembly: GoFieldAccess(typeof(EmitResult))]
[assembly: GoFieldAccess(typeof(FileIncludeReason))]
[assembly: GoFieldAccess(typeof(LibFile))]
[assembly: GoFieldAccess(typeof(Program))]
[assembly: GoFieldAccess(typeof(WriteFileData))]
[assembly: GoFieldAccess(typeof(astꓸLiteralLikeNode))]
[assembly: GoFieldAccess(typeof(astꓸModuleName))]
[assembly: GoFieldAccess(typeof(automaticTypeDirectiveFileData))]
[assembly: GoFieldAccess(typeof(checkerPool))]
[assembly: GoFieldAccess(typeof(emitter))]
[assembly: GoFieldAccess(typeof(fileLoader))]
[assembly: GoFieldAccess(typeof(filesParser))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.CheckJsDirective))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FileReference))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.NodeList))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.SourceFile))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.checker_package.Checker))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.contentmapper_package.DiagnosticDirectiveError))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.contentmapper_package.InitializeError))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.contentmapper_package.InvalidVirtualExtensionError))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.contentmapper_package.ProjectError))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.contentmapper_package.SupplementalFileCollisionError))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.contentmapper_package.TransformError))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package.CompilerOptions))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package.ProjectReference))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.module_package.ResolvedModule))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.module_package.ResolvedTypeReferenceDirective))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.packagejson_package.InfoCacheEntry))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.packagejson_package.PackageJson))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.symlinks_package.KnownDirectoryLink))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package.ParsedCommandLine))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package.SourceOutputAndProjectReference))]
[assembly: GoFieldAccess(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package.TsConfigSourceFile))]
[assembly: GoFieldAccess(typeof(includeExplainingDiagnostic))]
[assembly: GoFieldAccess(typeof(includeProcessor))]
[assembly: GoFieldAccess(typeof(jsxRuntimeImportSpecifier))]
[assembly: GoFieldAccess(typeof(lazyValue<>))]
[assembly: GoFieldAccess(typeof(libResolution))]
[assembly: GoFieldAccess(typeof(packageNamesInfo))]
[assembly: GoFieldAccess(typeof(parseTask))]
[assembly: GoFieldAccess(typeof(parseTaskData))]
[assembly: GoFieldAccess(typeof(projectReferenceDtsFakingVfs))]
[assembly: GoFieldAccess(typeof(projectReferenceFileMapper))]
[assembly: GoFieldAccess(typeof(projectReferenceParseTask))]
[assembly: GoFieldAccess(typeof(projectReferenceParser))]
[assembly: GoFieldAccess(typeof(redirectsFile))]
[assembly: GoFieldAccess(typeof(referenceFileLocation))]
[assembly: GoFieldAccess(typeof(referencedFileData))]
[assembly: GoFieldAccess(typeof(sourceFileFromReferenceDiagnostic))]

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/compiler/checkerpool.go", "checkerpool.cs", "AB9IAA2+AYKC3ILKAAUmgoKmgoKCgoKCpoKClIKCgoKClIKCgqaCgoK4goKCgpSCgoKCgoKmgoKCgriClAACEoKClIKClLKCgoKUgpSUpoIAAhiCAAIUgoKCgoKUgoKUgoKUpoKmooKCgoKmlO7KooKUgoKCgr6igqaigoKCgoK6ooKmgoKCgoKCgoKUuJSCgoKCgoKCgoKCgpSCgpSChoKCuIKCgpSCggAEEIKCgpSCgoKCgpSCgoKUgqaqooKCgrKCgqKkpqKCgpKUrKKUgoKCsoKCgoLGpMY=", "245-255:1;334-336:1;352-354:1;364-415:1;368-374:1.1;451-455:1;463-465:1;478-486:1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/compiler/emitHost.go", "emitHost.cs", "ACM8AAYQooL8gqaCpoKmgqaCpoKmgqaCpoKmgqaCpoKmpKaCpoKmgoKClIKmgqaCpoKmgqaCpoKmgoI=", "", "", "47=GetEmitResolver/1/1/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/compiler/emitter.go", "emitter.cs", "ADFcwoKUgoKiAAsOgoK80oKUgpTW0oKUgoKClPailgACFqbIooKWgpSUgoKUpgAHEoSCqJaCqJSCuIKmgoKmlpaClMbSlIKmgoKmgqaCpJQACRiYoqTSlIKUgpSCpoKSlISmgoKmgoKCpgAKIIKAgoKygpSCgpS2lMyiAAoQooIACA6CgrTEgoKCAAkWlIKCgu4ABhCCgpSCgqiCgoKClLiogoKUyoKCgoKCqKaCgpTGoqiGgoKUxrKClIKChgAGEISUlKayhKaCgoKGAAYQhIIACRimAA0YpISCqIKqgqiCqIKqgqiCqIKogoKCuLimgoKUqoKmhIKClIKCgg==", "273-282:1;540-540:1;557-559:1;569-569:1", "", "77=Options/1/1/1,NewDeclarationTransformer/1/1/1,NewSupplementalReferencesTransformer/1/1/2;229=IsTrue/1/5/1,IsTrue/2/5/3,IsTrue/3/5/4,IsTrue/4/5/5,IsTrue/5/5/6;285=IsTrue/1/2/1,GetEmitScriptTarget/1/1/6,IsTrue/2/2/8;317=IfElse/1/1/1;340=OriginalFileName/1/1/1,ComputeECMALineStarts/1/1/3;363=NormalizeSlashes/1/1/1,GetBaseFileName/1/1/1,getSourceRoot/1/1/2,getSourceMapDirectory/1/1/3,UseCaseSensitiveFileNames/1/1/5,GetCurrentDirectory/1/1/6;378=Sources/1/1/1,RawSourceMap/1/1/2;418=GetDiagnostics/1/1/2;446=FileName/1/1/1,FileExtensionIs/1/1/1;469=FileName/1/1/1,GetCurrentDirectory/1/1/3,CommonSourceDirectory/1/1/4,UseCaseSensitiveFileNames/1/1/5;496=FileName/1/1/1,GetCurrentDirectory/1/1/3,CommonSourceDirectory/1/1/4,UseCaseSensitiveFileNames/1/1/5;505=NormalizePath/1/1/2,GetDirectoryPath/1/1/2,CombinePaths/1/1/3,UseCaseSensitiveFileNames/1/1/6,GetCurrentDirectory/1/1/7,GetRelativePathToDirectoryOrUrl/1/1/1;571=UseCaseSensitiveFileNames/1/1/1,GetCurrentDirectory/1/1/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/compiler/fileInclude.go", "fileInclude.cs", "ADVygoKClKa4ooKUAAwSgqaCgqaipoKmgqaigoKUgoKCgoKClIKCgoKClLiC/tra2tiigoKYlIKUuLKClJSCgoKCgoKClKamxoKCgpSmgpTYgoKAgpTWgqTYsoKClIKClJSCgpSmgpTYpIKUxqTYsoKUgpSClIKCgpSCgtiCgoLYgoKUgIKC6KS0prKCgpSUpKSkpA==", "156-160:1;157-159:1.1;163-165:2;164-164:2.1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/compiler/fileloader.go", "fileloader.cs", "AFrKAbSCpoIAH1KogoKCgoKClAAPIIKCgrSylIKCgoKWsoKC3oKmloKU9oKmgoKCAAcQgoKCgoKUksqCgoIACBKmgoKCgpSUgtzsgoKCgraCgoKClIKCggAKFgALGIK4tqK4goKmuIKmgqqkgpSEgoKUgoKCpqaCgsqClJKCgoKEyoLu0oKUgoLKgpQABRaigoKChJSCgoKAgoKkgoKClJSUlMaCgIKkgIKUgpSkpLakpKSClIKClLS0tLS0goKUpqTGqIKClKSkpKS2pqKmggAGFIKClKSkpKSkAAYQooKCpqaCgoLuyIKAgqSCgIKUpKSkpKSkpMaogoKUyNKClIKC1tKCgoKUgqKq0oKCgpSClIKCzNaCgoKmpoyCgpSCgoKClKaCpoKUpoKmgqaCgoK4poKClIKUggAHENyCAAkU3NKCgpSCpJSCgrKCgoKCgpSC7pSCAAcQyoK4gqIACAjigpSClJSClIKCgoKCgriCgoKCgu6UgoKCuoKmgoKUgoKCpoKCgpSCpoKEggABEJScggAQIoK0pNKCgoKCgoLWooKmgoKCgoKCgoKU7oKmwoCCtPaIgoKCgpSCgoKUlIKUpoKCgpSUpqKClLiigpS4goKCgoKClLS0tIK4goK4gqamooKogoKUgoKClAACFIKUgqY=", "345-347:1", "", "143=DefaultLibraryPath/1/1/2,GetCurrentDirectory/1/2/2,GetNormalizedAbsolutePath/1/1/2,FS/1/1/4,UseCaseSensitiveFileNames/1/1/4,GetCurrentDirectory/2/2/5,NewWorkGroup/1/1/8,ContentMapperExtensions/1/1/14;334=NewWorkGroup/1/1/2;368=GetImpliedNodeFormatForFile/1/1/1;381=Contains/1/1/1;406=GetExternalModuleIndicatorOptions/1/1/3;561=NewTextRange/1/1/2,AddMessageChain/1/1/5;790=toPath/1/1/3;971=GetResolutionDiagnostic/1/1/1,IsTrue/1/1/2,GetAllowJS/1/1/3,Imports/1/3/4,Imports/2/3/4,IsInJSFile/1/1/4,Imports/3/3/4;974=IfElse/1/1/9;1156=GetResolvePackageJsonExports/1/1/1,GetResolvePackageJsonImports/1/1/1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/compiler/filesparser.go", "filesparser.cs", "ADVmgqaCptKCgoKUhpSClIKCgqaCgoKCgoKCAAgSAAgSyoKChpSmgoKUgqaCgpSUgoKCgoKClKamgrLugoLKAAcQgoIACxSkgsym0oKUgoKCgoK0AA4UgoIABxAAChIABhCigoKCpqKCAAwYooKmsoKCgoKCptKCpIKCgpSEuoKmgoaCgqaCpoKCgoKEgqaCgsbIooKUgoKClIaCgqamgoKUgoKCgoKClIKCgoKCpgAAEIIACgKiiIKClJSCgqiCgoKUgoKClIIACRSEgoKCppSmgoKClLiClIKmgoKCiAAIEoKClNyCgpSCgriCrIKClKaCgoKUppSCpoKCpoKClJSCgoKUgoKUlIKClJSCyoKUgoKmgoKCgqaCuAAWLrKCgoKClA==", "278-326:1;379-543:1", "", "101=Flatten/1/1/5,Join/1/1/5;180=FileName/1/1/1;196=NormalizePath/1/1/1;445=ParseOptions/1/1/1,ContentMapperParseOptions/1/1/2,ContentMapper/1/1/5,IsContentMapperFailureStub/1/1/6;487=ParseOptions/1/1/1,ContentMapperParseOptions/1/1/2,ContentMapper/1/1/5,IsContentMapperFailureStub/1/1/6;500=Path/1/1/4")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/compiler/host.go", "host.cs", "ACBCAAoWAAAQpgAAEIK0AAsUgqaCpoKmoqaCgoKUpoKClIKClIKClKaCpqKC", "64-64:1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/compiler/includeprocessor.go", "includeprocessor.cs", "ABw4gvyigoKClIKCgriCgoLKpqKmgoYACRQACxiigqaCpqKCgoKGpqamgoKmgqaqgpSCgoKClIKClJSCgsyCgsyCgpSC7IKCgoKm6oI=", "36-55:1;64-66:1;99-110:1", "", "150=Invoke/1/1/2;157=FileName/1/1/2,Invoke/1/1/2;167=Invoke/1/1/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/compiler/processingDiagnostic.go", "processingDiagnostic.cs", "ABw2ggANFIKmgpSCgpSkgoKC3Oak2KKCgoKCgoKCpoKCgoKCgriSgpSCqoKCgoKUlIKUgoKCgpSCpoKClIKUgpSClA==", "80-89:1;90-96:2;106-106:3")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/compiler/program.go", "program.cs", "ADlmggAMEqKCgpSUprKCggAlbIKogqaCqIKogoKClKiCgoKClKiCqoKugoKUqIKogqaCpoKmgoKmgqaCpoKmiKiCpoLGtrqCgoKCgoKCgoKCgqaCuJSCgoK6goKCpsaigoK0goKCggAHFqKClIKCgpQAAxSigoKCpoKCgoKIgoKCgoKUgpSsgoKCpoKYgpSClIKUgoKElIKUgqgACBKCgoKCkoKCgoKCgoKSgqaCpqKCpoKUgoK6gqayAAIiooKCgpSCgoKUpqKClIKCxrKmsqayprLGgoKYooKUgoKUpoKCgoKCqoKmgpSCgoK4prKUgoKChLi2gqaigoKCsoKkoraogoKUpoKCAAMQgoKUqoKClKaCgoKmpqKClIKmgqqigoKCgoLKrIKCgpSClKaCgoKCoqaCAAIQooKClIKCgpTGgoKCgpSC2qKCgpKmgoKClKKCgqaUpqKCiIKUAAUUgoKUhoKCgpSCgoKmgpSCxqKClJSqgqaCgoKClKaCpqIABhCigoKUgoKCuKKClIKmoq6igqaCpoKCmqaigoKUlAAJBrKUgoKClKaSgoKUpqiSgoaUpqKmgpSmsoKCgpSUgqbCgoKUpqKCgoKmoqaSgoKCgpSCpoKCvISCgoKClIKkpoKmgqaClIKUgqaCpoKmgqaCpoKmgqaClIKmgoKUgriCgpSCuIKClIK4gqaUgoKCpoKCAAsYqMKigpSUgpSmwqKCooKCgoKCgqamlIKUpoaClIKCgpSCgpSCyoKClIK4hKaCgriCpoKmgoK4jIKCuAAAEIKCgoKmgoSCgoKClJSCgpQABhC4gqaCgriCpoKClIKUgpSCpoKClIKUgriCgriCgriUgqaCgpSClIKmgqaEgoKClJSChIKsgpaSgoSCgoSUpoKClKiElMqSgoKCgpS4goKmoqaigpKCgpSmkoaCgpSCgoKClIK4goKU2IKCgoKClrimgqiCxoKClIKYpqLGooKUyqIABBKigoKogpSCgqqCgoKmgoaCpoLGooKClIKigpSCgoKmlJaCggAIFMaigpaCgoKUgoKCgoaCgoKCloKmgqamwoKmgqaCkoKC1oKCpsaCgpSqgoKCqoKClIKCgoKCgpSCgoKUgoKCpoKClILGgoKClKaCgoKUpoKCgpSCgoKUpoKCgpSmgoKClKaCpoKmgqaCpoKmgqaigoKUgoKCgpSmpoKmgoKmgoKUpoKClKaigooABhCmgoKCgoIABxC4ACRC0oCCxoLMgriCqoKCgoKUggAHEILigpKWgpaCyoKWoriW+oKCgoKUgpSCgoKmABQsgoKCpgAGEIKUyoKUgoKCpqYAABCClJKClIKUpoKGlJiClpSClIKEpoK4xoKmgoKmgoKCgoKmpoKmgqaiAAIQgqiCqKKqooaCooKClIKUpoKYgoKigoK4hIKopqKCgpSClKaipoKCgqamgqaigpSmoqaCgpSmgqaCpoKmgqaCpoKCgoKGlIKClIKCgoKYgoSCgriEloKIgoKCuKamuKKCpqKCgoKCuKaCgpaCqIKChpSCgoKmhoKClIKCgriC3riCgqaCpoLGooKCgriCgtw=", "62-67:1;401-401:1;409-409:2;443-446:1;561-566:1;630-639:1;661-663:1;688-695:1;702-704:1;711-715:2;723-732:1;749-761:1;772-774:1;809-812:1;852-854:1;863-869:1;871-877:2;879-881:3;883-891:4;893-901:5;894-896:5.1;903-913:6;915-921:7;923-928:8;930-932:9;934-949:10;1069-1071:11;1073-1084:12;1074-1079:12.1;1086-1108:13;1087-1103:13.1;1089-1100:13.1.1;1172-1172:14;1315-1343:15;1345-1351:16;1367-1373:1;1375-1399:2;1446-1448:1;1473-1475:1;1502-1513:1;1514-1516:2;1662-1664:1;1670-1672:1;1678-1680:1;1749-1762:1;1750-1754:1.1;1751-1753:1.1.1;1835-1837:1;1855-1876:2;1883-1885:3;1970-1978:1;2042-2045:1;2045-2047:2;2061-2063:1;2067-2069:1;2071-2080:2;2083-2085:3;2089-2094:4;2173-2226:1;2235-2242:1;2247-2292:1", "", "353=canReplaceFileInProgram/1/1/1;363=Clone/1/1/7;419=ParseOptions/1/2/1,ParseOptions/2/2/1,IsExternalOrCommonJSModule/1/2/3,IsExternalOrCommonJSModule/2/2/3,Imports/1/2/5,Imports/2/2/5,EqualFunc/1/5/5,EqualFunc/2/5/9,Equal/1/1/10,EqualFunc/3/5/11,EqualFunc/4/5/12,EqualFunc/5/5/13,equalCheckJSDirectives/1/1/14;532=IsExternalModuleNameRelative/1/1/1;788=getDiagnostics/1/1/4,GetGlobalDiagnostics/1/1/4;822=Options/2/3/1,IsTrue/2/3/1,Options/3/3/2,IsTrue/3/3/2,Path/1/2/2,IsSourceFileDefaultLibrary/1/1/2,Path/2/2/3,IsSourceFromProjectReference/1/1/3,canIncludeBindAndCheckDiagnostics/1/1/4;891=IsObjectLiteralExpression/1/1/2;1073=Path/1/1/3,FileName/1/1/5,Invoke/1/1/5;1191=GetEmitDeclarations/1/1/4;1198=IsTrue/2/2/1,GetEmitDeclarations/1/1/5;1220=GetBaseFileName/1/1/5,GetRelativePathFromFile/1/1/6;1348=getSourceFilesToEmit/1/1/6;1468=getBindAndCheckDiagnosticsWithChecker/1/1/1,Options/1/1/1,FilterNoEmitSemanticDiagnostics/1/1/1,GetIncludeProcessorDiagnostics/1/1/2;1776=Options/1/1/1,GetCurrentDirectory/1/1/3,UseCaseSensitiveFileNames/1/1/4;2069=EqualFunc/2/2/3;2278=GetSourceFileByPath/1/1/1,SourceFileMayBeEmitted/1/1/1,toPath/1/1/2,AddIfAbsent/1/1/2;2300=CombinePaths/1/2/1,CombinePaths/2/2/2;2343=Code/1/91/2,Code/2/91/3,Code/3/91/4,Code/4/91/5,Code/5/91/6,Code/6/91/7,Code/7/91/8,Code/8/91/9,Code/9/91/10,Code/10/91/11,Code/11/91/12,Code/12/91/13,Code/13/91/14,Code/14/91/15,Code/15/91/17,Code/16/91/18,Code/17/91/19,Code/18/91/20,Code/19/91/21,Code/20/91/22,Code/21/91/23,Code/22/91/24,Code/23/91/25,Code/24/91/26,Code/25/91/27,Code/26/91/28,Code/27/91/29,Code/28/91/30,Code/29/91/31,Code/30/91/32,Code/31/91/33,Code/32/91/34,Code/33/91/35,Code/34/91/36,Code/35/91/37,Code/36/91/38,Code/37/91/39,Code/38/91/40,Code/39/91/41,Code/40/91/42,Code/41/91/43,Code/42/91/44,Code/43/91/45,Code/44/91/46,Code/45/91/47,Code/46/91/48,Code/47/91/49,Code/48/91/50,Code/49/91/51,Code/50/91/52,Code/51/91/53,Code/52/91/54,Code/53/91/55,Code/54/91/56,Code/55/91/57,Code/56/91/58,Code/57/91/59,Code/58/91/60,Code/59/91/61,Code/60/91/62,Code/61/91/63,Code/62/91/64,Code/63/91/65,Code/64/91/66,Code/65/91/67,Code/66/91/68,Code/67/91/69,Code/68/91/70,Code/69/91/71,Code/70/91/72,Code/71/91/73,Code/72/91/74,Code/73/91/75,Code/74/91/76,Code/75/91/77,Code/76/91/78,Code/77/91/79,Code/78/91/80,Code/79/91/81,Code/80/91/82,Code/81/91/83,Code/82/91/84,Code/83/91/85,Code/84/91/86,Code/85/91/87,Code/86/91/88,Code/87/91/89,Code/88/91/90,Code/89/91/91,Code/90/91/92,Code/91/91/94")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/compiler/projectreferencedtsfakinghost.go", "projectreferencedtsfakinghost.cs", "ABsqlIQABxDIgqiCAAoStoKoooKUgpaohKiCqIKogqiCqKKCgpSogqiCqKKCgpSmgqaigqiCpoKCpoKChJSElNyihIKCpoKCloKUgoKUgoKCuoKSgoKUgoSCzJSUpoKCgpSmooKCgqY=", "189-207:1", "", "31=From/1/1/2;156=EnsureTrailingDirectorySeparator/1/1/1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/compiler/projectreferencefilemapper.go", "projectreferencefilemapper.cs", "ABw4goKUpqKEgoKUgqiCgqamgoKCgoKCgqamgqaCpoKmooKmgoKmooSCgqiCgqaCgpSmgoKmhoKUgoKCgqaMgoKUgoKUgqamqIKUgoKCpqqCgoKUgoKClIKClIKCgpS4")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/compiler/projectreferenceparser.go", "projectreferenceparser.cs", "ABYm0oKAgrSCgpSCgrTEggAMGqKCgoKmooKCgISUkoLsooKCgoKCgoK4goKUgoKChIKUgoiCgoKCgpSCuIKU", "35-39:1;62-65:1")]
// </GoSourcePositionMaps>

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

[GoPackage("compiler", ImportPath = "github.com/microsoft/TypeScript/tsc/internal/compiler")]
public static partial class compiler_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    internal partial interface declarationTransformer {}
    internal partial struct automaticTypeDirectiveFileData {}
    internal partial struct checkerAssociationPolicy {}
    internal partial struct checkerPool {}
    internal partial struct compilerHost {}
    internal partial struct declarationMapSource {}
    internal partial struct emitHost {}
    internal partial struct emitter {}
    internal partial struct fileIncludeKind {}
    internal partial struct fileLoader {}
    internal partial struct filesParser {}
    internal partial struct includeExplainingDiagnostic {}
    internal partial struct includeProcessor {}
    internal partial struct jsxRuntimeImportSpecifier {}
    internal partial struct lazyValue<T> {}
    internal partial struct libResolution {}
    internal partial struct packageNamesInfo {}
    internal partial struct parseTask {}
    internal partial struct parseTaskData {}
    internal partial struct processedFiles {}
    internal partial struct processingDiagnostic {}
    internal partial struct processingDiagnosticKind {}
    internal partial struct projectReferenceDtsFakingHost {}
    internal partial struct projectReferenceDtsFakingVfs {}
    internal partial struct projectReferenceFileMapper {}
    internal partial struct projectReferenceParseTask {}
    internal partial struct projectReferenceParser {}
    internal partial struct redirectsFile {}
    internal partial struct referenceFileLocation {}
    internal partial struct referencedFileData {}
    internal partial struct resolvedRef {}
    internal partial struct sourceFileFromReferenceDiagnostic {}
    public partial interface CheckerPool {}
    public partial interface CompilerHost {}
    public partial interface EmitHost {}
    public partial interface ProgramLike {}
    public partial interface SourceFileMayBeEmittedHost {}
    public partial struct DuplicateSourceFile {}
    public partial struct EmitOnly {}
    public partial struct EmitOptions {}
    public partial struct EmitResult {}
    public partial struct FileIncludeReason {}
    public partial struct LibFile {}
    public partial struct Program {}
    public partial struct ProgramOptions {}
    public partial struct SourceMapEmitResult {}
    public partial struct WriteFileData {}
    // </TypeAccessibility>

    // Go initializes an imported package before the importing package, for every import
    // form - not only the blank one. .NET would never load an assembly nothing has touched
    // yet, so each import that initializes anything is forced below: once per assembly, and
    // ahead of this package's own `init` functions, which this file being the first compile
    // item of the project guarantees.

    // <ImportInitializers>
    [GoInit] internal static void initᴛᴛimportꓸcontext() => builtin.initPackage(typeof(context_package));
    [GoInit] internal static void initᴛᴛimportꓸerrors() => builtin.initPackage(typeof(errors_package));
    [GoInit] internal static void initᴛᴛimportꓸfmt() => builtin.initPackage(typeof(fmt_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸast() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.ast_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸbinder() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.binder_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸchecker() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.checker_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcollections() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.collections_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcontentmapper() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.contentmapper_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcore() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.core_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸdiagnostics() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸjson() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.json_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸlocale() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.locale_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸmodule() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.module_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸmodulespecifiers() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.modulespecifiers_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸoutputpaths() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.outputpaths_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸpackagejson() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.packagejson_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸparser() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.parser_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸprinter() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.printer_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸscanner() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸsourcemap() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.sourcemap_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸspanmap() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.spanmap_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸstringutil() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸsymlinks() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.symlinks_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtracing() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.tracing_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtransformers() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtransformersꓸdeclarations() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.transformers.declarations_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtransformersꓸestransforms() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.transformers.estransforms_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtransformersꓸinliners() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.transformers.inliners_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtransformersꓸjsxtransforms() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.transformers.jsxtransforms_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtransformersꓸmoduletransforms() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.transformers.moduletransforms_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtransformersꓸtstransforms() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.transformers.tstransforms_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtsoptions() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtspath() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸvfs() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.vfs_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸvfsꓸcachedvfs() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.vfs.cachedvfs_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸzeeboꓸxxh3() => builtin.initPackage(typeof(go.github.com.zeebo.xxh3_package));
    [GoInit] internal static void initᴛᴛimportꓸio() => builtin.initPackage(typeof(io_package));
    [GoInit] internal static void initᴛᴛimportꓸmaps() => builtin.initPackage(typeof(maps_package));
    [GoInit] internal static void initᴛᴛimportꓸmath() => builtin.initPackage(typeof(math_package));
    [GoInit] internal static void initᴛᴛimportꓸslices() => builtin.initPackage(typeof(slices_package));
    [GoInit] internal static void initᴛᴛimportꓸsort() => builtin.initPackage(typeof(sort_package));
    [GoInit] internal static void initᴛᴛimportꓸstrings() => builtin.initPackage(typeof(strings_package));
    [GoInit] internal static void initᴛᴛimportꓸsync() => builtin.initPackage(typeof(sync_package));
    [GoInit] internal static void initᴛᴛimportꓸsyncꓸatomic() => builtin.initPackage(typeof(go.sync.atomic_package));
    [GoInit] internal static void initᴛᴛimportꓸtime() => builtin.initPackage(typeof(time_package));
    // </ImportInitializers>
}

// A Go method is an extension method on its receiver type. The methods of one type are
// in a class of their own beside the package class, so that a method and a package-level
// type, constant or variable can share a name as they do in Go. Each class is declared
// below with the type it holds the methods of; its blocks are in the converted sources,
// in Go's declaration order.

// <MethodHolders>
[GoMethodHost(typeof(compiler_package.FileIncludeReason)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class compilerᴛFileIncludeReason;
[GoMethodHost(typeof(compiler_package.Program)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class compilerᴛProgram;
[GoMethodHost(typeof(compiler_package.ProgramOptions)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class compilerᴛProgramOptions;
[GoMethodHost(typeof(compiler_package.checkerPool)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class compilerᴛcheckerPool;
[GoMethodHost(typeof(compiler_package.compilerHost)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class compilerᴛcompilerHost;
[GoMethodHost(typeof(compiler_package.declarationMapSource)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class compilerᴛdeclarationMapSource;
[GoMethodHost(typeof(compiler_package.emitHost)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class compilerᴛemitHost;
[GoMethodHost(typeof(compiler_package.emitter)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class compilerᴛemitter;
[GoMethodHost(typeof(compiler_package.fileLoader)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class compilerᴛfileLoader;
[GoMethodHost(typeof(compiler_package.filesParser)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class compilerᴛfilesParser;
[GoMethodHost(typeof(compiler_package.includeProcessor)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class compilerᴛincludeProcessor;
[GoMethodHost(typeof(compiler_package.lazyValue<>)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class compilerᴛlazyValue;
[GoMethodHost(typeof(compiler_package.parseTask)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class compilerᴛparseTask;
[GoMethodHost(typeof(compiler_package.processingDiagnostic)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class compilerᴛprocessingDiagnostic;
[GoMethodHost(typeof(compiler_package.projectReferenceDtsFakingHost)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class compilerᴛprojectReferenceDtsFakingHost;
[GoMethodHost(typeof(compiler_package.projectReferenceDtsFakingVfs)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class compilerᴛprojectReferenceDtsFakingVfs;
[GoMethodHost(typeof(compiler_package.projectReferenceFileMapper)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class compilerᴛprojectReferenceFileMapper;
[GoMethodHost(typeof(compiler_package.projectReferenceParseTask)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class compilerᴛprojectReferenceParseTask;
[GoMethodHost(typeof(compiler_package.projectReferenceParser)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class compilerᴛprojectReferenceParser;
[GoMethodHost(typeof(compiler_package.redirectsFile)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class compilerᴛredirectsFile;
[GoMethodHost(typeof(compiler_package.referenceFileLocation)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class compilerᴛreferenceFileLocation;
// </MethodHolders>
