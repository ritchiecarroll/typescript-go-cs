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
using static go.github.com.microsoft.TypeScript.tsc.@internal.core_package;

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
[assembly: GoTypeAlias("ResolutionMode", "go.github.com.microsoft.TypeScript.tsc.@internal.core_package.ModuleKind")]
// </ExportedTypeAliases>

// As types are cast to interfaces in Go source code, the go2cs code converter
// will generate an assembly level `GoImplement` attribute for each unique cast.
// This allows the interface to be implemented in the C# source code using source
// code generation (see go2cs-gen). Resolving each duck-typed cast at compile time
// this way is what keeps startup free of reflection.

// <InterfaceImplementations>
[assembly: GoImplement<LimitedSemaphore, Semaphore>(Pointer = true)]
[assembly: GoImplement<UnlimitedSemaphore, Semaphore>(Pointer = true)]
[assembly: GoImplement<UnlimitedSemaphore, Semaphore>]
[assembly: GoImplement<parallelWorkGroup, WorkGroup>(Pointer = true)]
[assembly: GoImplement<singleThreadedWorkGroup, WorkGroup>(Pointer = true)]
// </InterfaceImplementations>

// <ImplicitConversions>
// </ImplicitConversions>

// The struct types whose fields this package reaches through a pointer, as Go spells it, `p.f`:
// go2cs-gen emits a field property on the pointer for each of their fields.
[assembly: GoFieldAccess(typeof(LimitedSemaphore))]
[assembly: GoFieldAccess(typeof(breadthFirstSearchJob<>))]
[assembly: GoFieldAccess(typeof(parallelWorkGroup))]
[assembly: GoFieldAccess(typeof(singleThreadedWorkGroup))]

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/core/arena.go", "arena.cs", "AA0agoKElIKCroKClIKCgpaUgoKCpoKCgqaCgpSCgsaEgoI=")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/core/bfs.go", "bfs.cs", "AB8ygqaCpoKCggALIooACAyOgoKWjIiigoKCgoKClIKCgoKCwoKCqIqmgoaCgpSCuIiWgoKCuJKUgoSClIKCpoKCgoa4poKCgoKUpoKmgoKCgoKUgpSogoKCgpSC", "86-167:1;100-142:1.1;138-140:1.1.1;169-176:2", "", "158=Invoke/1/1/1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/core/binarysearch.go", "binarysearch.cs", "AAQQooKClJKCgoKCgoKUpg==")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/core/compileroptions.go", "compileroptions.cs", "AJoB1gKCtJS2hJSClIKCuKaCgpSmgoKmgoKUgpSClIKUpoKUlKSk1siCgpSCgpSmgqaCpoKmgqaCgpSWpKaCpoKClKaCgqaCgpSmgoKUgoKUgoa4goKClKiCpoKmgqaCpoKClKaCpoKmgpSkpoKClIKUAB5OgqaCABE+AAcYgpS0pKSkpKQADRiClKSk6IKUpAAlTIKUtKSkpKSk", "318-321:1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/core/context.go", "context.cs", "AAscgqaCgpQACBaCpoKClA==")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/core/core.go", "core.cs", "ABkwgoKClIKClKaCsoKCgoKCpqamgpKCgoLugrKCgoKCgqampoKClIKClKaCgpSCgoKClJSmgoKUgoKUpoKCgoKCpqaCgoKCgpSUpoKCgoKCpqaCgoKCgoKCgpSmpoKCgoKCgoKClKamgoKUpoKCgqamgoKCpqaikoKCpriCgoKmpoKCgoKmpoKCgqamgoKCgqamgoKUpoKClKaCgpSmgoKCpqaCgoKCpqaigoKCpqaCgpSClKaigpSClIKUgpSCgpSmgoKCgqamgoKCpoKCqIKCpoKUgoKUgoK02KaCgpSmgoKCgoKUvIKClKqCgpSqooKU3IKCpoKCgoKCgoKCgpSClKSClLaCgoKClLi+opYABBCGgoSCgpSmpoKCgpSmgoKUqIKmgoKmgoKCpKSkpKS2poKUpKSkpM6CgpQAAiCCpoKmooKCgoKCgoKCgoKCgpSCgoKCgpiClIKClIKCgoKCgoK41oIABxDMgoKClIKUgoKUgoKCgoKCgpSCkoKUlIKUlIKUgpSElJSCgpSmgqaCgoKmpoKCgpS4oqaCgpSmopKCgpSCggAGEIKCgoKClMqCroKugoKCgriClIKCgqbOsoKUgqiCgpSCgpSCgoKmpoKCsoKCgoKCprimgoKUgoKCgoKmgqaogoKCgpQ=", "53-61:1;209-216:1;391-397:1;436-468:1;475-477:1;738-749:1;754-762:1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/core/languagevariant_stringer_generated.go", "languagevariant_stringer_generated.cs", "/oaCgsq0goKClA==")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/core/linkstore.go", "linkstore.cs", "AA4YgoKClIKUgoKmgoKmggAQJIKCgoKElIKCgqaCgoKClKamgqaCgoKCgqaUgpQ=")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/core/modulekind_stringer_generated.go", "modulekind_stringer_generated.cs", "/oaCgoKCgoKCgoKCgoKCggAGFIKCtoKUpIKkgqQ=")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/core/nodemodules.go", "nodemodules.cs", "AAcSADl0AAgQgoKCgpSCtoKClA==")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/core/pattern.go", "pattern.cs", "AAwUgoKClMaCpoKClKqCgpSClMaCgoKCgoKCpg==", "", "", "31=HasPrefix/1/1/1,HasSuffix/1/1/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/core/projectreference.go", "projectreference.cs", "AA0coqaCgpQ=")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/core/scriptkind_stringer_generated.go", "scriptkind_stringer_generated.cs", "/oaCgoKCgoIABRC0gpSkpA==")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/core/scripttarget_stringer_generated.go", "scripttarget_stringer_generated.cs", "/oaCgoKCgoKCgoKCgoKCgoKCggAFEoK2gpSkgqQ=")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/core/semaphore.go", "semaphore.cs", "AAwU6IK2gtbugoKUppCSxoKCpoK0pA==", "15-15:1;19-19:1;36-36:1;50-50:1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/core/stack.go", "stack.cs", "AAwOgqaCgoKUgoKCgqaCgoKUpoI=")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/core/text.go", "text.cs", "AA0cgqaCxoKmgqaCpoKmgqaCpoKmgqaCpoKmgoKCqoKCgsaCgoKU")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/core/textchange.go", "textchange.cs", "AA4UgsaCgoKCgoKClJSUlA==")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/core/tristate.go", "tristate.cs", "AA4ggqaCpoKmgqaCpoKClKaCpLS0tKaClKSk6IKClA==")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/core/tristate_stringer_generated.go", "tristate_stringer_generated.cs", "/oaCgoLKtIKCgpQ=")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/core/typeacquisition.go", "typeacquisition.cs", "ABAYsoKUgqY=", "", "", "28=Equal/1/2/1,Equal/2/2/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/core/version.go", "version.cs", "AAgUgqaCgoKCgpSUlIKUpoI=")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/core/watchoptions.go", "watchoptions.cs", "AC1eooKClA==")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/core/workgroup.go", "workgroup.cs", "ABEogoKUAAcQtIKCpoK4ooKiAAoQtNKCpoKCoqSigoKCgpTo0oKCgpSCgoKCAAwUgoIABxCipIKElOqC", "39-41:1;109-117:1;112-115:1.1", "", "", "", "145:25,146:17")]
// </GoSourcePositionMaps>

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

[GoPackage("core", ImportPath = "github.com/microsoft/TypeScript/tsc/internal/core")]
public static partial class core_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    [GoLocalName("result")] internal partial struct BreadthFirstSearchParallelEx_result<K, N> {}
    internal partial struct breadthFirstSearchJob<N> {}
    internal partial struct key {}
    internal partial struct levenshteinBuffers {}
    internal partial struct noCopy {}
    internal partial struct parallelWorkGroup {}
    internal partial struct singleThreadedWorkGroup {}
    public partial interface Semaphore {}
    public partial interface WorkGroup {}
    public partial struct Arena<T> {}
    public partial struct BreadthFirstSearchLevel<K, N> {}
    public partial struct BreadthFirstSearchOptions<K, N> {}
    public partial struct BreadthFirstSearchResult<N> {}
    public partial struct BuildOptions {}
    public partial struct CheckerLifetime {}
    public partial struct CompilerOptions {}
    public partial struct ECMALineStarts {}
    public partial struct JsxEmit {}
    public partial struct LanguageVariant {}
    public partial struct LimitedSemaphore {}
    public partial struct LinkStore<K, V> {}
    public partial struct ModuleDetectionKind {}
    public partial struct ModuleKind {}
    public partial struct ModuleResolutionKind {}
    public partial struct NewLineKind {}
    public partial struct PagedLinkStore<V> {}
    public partial struct Pattern {}
    public partial struct PollingKind {}
    public partial struct ProjectReference {}
    public partial struct ScriptKind {}
    public partial struct ScriptTarget {}
    public partial struct Stack<T> {}
    public partial struct TextChange {}
    public partial struct TextPos {}
    public partial struct TextRange {}
    public partial struct ThrottleGroup {}
    public partial struct Tristate {}
    public partial struct TypeAcquisition {}
    public partial struct UTF16Offset {}
    public partial struct UnlimitedSemaphore {}
    public partial struct WatchDirectoryKind {}
    public partial struct WatchFileKind {}
    public partial struct WatchOptions {}
    // </TypeAccessibility>

    // Go initializes an imported package before the importing package, for every import
    // form - not only the blank one. .NET would never load an assembly nothing has touched
    // yet, so each import that initializes anything is forced below: once per assembly, and
    // ahead of this package's own `init` functions, which this file being the first compile
    // item of the project guarantees.

    // <ImportInitializers>
    [GoInit] internal static void initᴛᴛimportꓸcontext() => builtin.initPackage(typeof(context_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸcollections() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.collections_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸdebug() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.debug_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸjson() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.json_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸstringutil() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸtspath() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package));
    [GoInit] internal static void initᴛᴛimportꓸgolang_orgꓸxꓸsyncꓸerrgroup() => builtin.initPackage(typeof(golang.org.x.sync.errgroup_package));
    [GoInit] internal static void initᴛᴛimportꓸiter() => builtin.initPackage(typeof(iter_package));
    [GoInit] internal static void initᴛᴛimportꓸmaps() => builtin.initPackage(typeof(maps_package));
    [GoInit] internal static void initᴛᴛimportꓸmath() => builtin.initPackage(typeof(math_package));
    [GoInit] internal static void initᴛᴛimportꓸos() => builtin.initPackage(typeof(os_package));
    [GoInit] internal static void initᴛᴛimportꓸreflect() => builtin.initPackage(typeof(reflect_package));
    [GoInit] internal static void initᴛᴛimportꓸruntimeꓸdebug() => builtin.initPackage(typeof(runtime.debug_package));
    [GoInit] internal static void initᴛᴛimportꓸslices() => builtin.initPackage(typeof(slices_package));
    [GoInit] internal static void initᴛᴛimportꓸsort() => builtin.initPackage(typeof(sort_package));
    [GoInit] internal static void initᴛᴛimportꓸstrconv() => builtin.initPackage(typeof(strconv_package));
    [GoInit] internal static void initᴛᴛimportꓸstrings() => builtin.initPackage(typeof(strings_package));
    [GoInit] internal static void initᴛᴛimportꓸsync() => builtin.initPackage(typeof(sync_package));
    [GoInit] internal static void initᴛᴛimportꓸsyncꓸatomic() => builtin.initPackage(typeof(go.sync.atomic_package));
    [GoInit] internal static void initᴛᴛimportꓸtime() => builtin.initPackage(typeof(time_package));
    [GoInit] internal static void initᴛᴛimportꓸunicode() => builtin.initPackage(typeof(unicode_package));
    [GoInit] internal static void initᴛᴛimportꓸunicodeꓸutf8() => builtin.initPackage(typeof(go.unicode.utf8_package));
    // </ImportInitializers>
}

// A Go method is an extension method on its receiver type. The methods of one type are
// in a class of their own beside the package class, so that a method and a package-level
// type, constant or variable can share a name as they do in Go. Each class is declared
// below with the type it holds the methods of; its blocks are in the converted sources,
// in Go's declaration order.

// <MethodHolders>
[GoMethodHost(typeof(core_package.Arena<>)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class coreᴛArena;
[GoMethodHost(typeof(core_package.BreadthFirstSearchLevel<,>)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class coreᴛBreadthFirstSearchLevel;
[GoMethodHost(typeof(core_package.CompilerOptions)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class coreᴛCompilerOptions;
[GoMethodHost(typeof(core_package.JsxEmit)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class coreᴛJsxEmit;
[GoMethodHost(typeof(core_package.LanguageVariant)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class coreᴛLanguageVariant;
[GoMethodHost(typeof(core_package.LimitedSemaphore)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class coreᴛLimitedSemaphore;
[GoMethodHost(typeof(core_package.LinkStore<,>)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class coreᴛLinkStore;
[GoMethodHost(typeof(core_package.ModuleKind)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class coreᴛModuleKind;
[GoMethodHost(typeof(core_package.ModuleResolutionKind)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class coreᴛModuleResolutionKind;
[GoMethodHost(typeof(core_package.NewLineKind)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class coreᴛNewLineKind;
[GoMethodHost(typeof(core_package.PagedLinkStore<>)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class coreᴛPagedLinkStore;
[GoMethodHost(typeof(core_package.Pattern)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class coreᴛPattern;
[GoMethodHost(typeof(core_package.ScriptKind)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class coreᴛScriptKind;
[GoMethodHost(typeof(core_package.ScriptTarget)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class coreᴛScriptTarget;
[GoMethodHost(typeof(core_package.Stack<>)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class coreᴛStack;
[GoMethodHost(typeof(core_package.TextChange)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class coreᴛTextChange;
[GoMethodHost(typeof(core_package.TextRange)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class coreᴛTextRange;
[GoMethodHost(typeof(core_package.ThrottleGroup)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class coreᴛThrottleGroup;
[GoMethodHost(typeof(core_package.Tristate)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class coreᴛTristate;
[GoMethodHost(typeof(core_package.TypeAcquisition)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class coreᴛTypeAcquisition;
[GoMethodHost(typeof(core_package.UnlimitedSemaphore)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class coreᴛUnlimitedSemaphore;
[GoMethodHost(typeof(core_package.WatchOptions)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class coreᴛWatchOptions;
[GoMethodHost(typeof(core_package.noCopy)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class coreᴛnoCopy;
[GoMethodHost(typeof(core_package.parallelWorkGroup)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class coreᴛparallelWorkGroup;
[GoMethodHost(typeof(core_package.singleThreadedWorkGroup)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class coreᴛsingleThreadedWorkGroup;
// </MethodHolders>
