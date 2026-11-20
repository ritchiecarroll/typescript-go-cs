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
global using jsonꓸMarshalerTo = go.github.com.go_json_experiment.json_package.MarshalerTo;
global using jsonꓸUnmarshalerFrom = go.github.com.go_json_experiment.json_package.UnmarshalerFrom;
// </ImportedTypeAliases>

using go;
using static go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;

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
[assembly: GoImplement<OrderedMap<@string, @string>, go.github.com.go_json_experiment.json_package.MarshalerTo>(Pointer = true)]
[assembly: GoImplement<OrderedMap<@string, @string>, go.github.com.go_json_experiment.json_package.UnmarshalerFrom>(Pointer = true)]
// </InterfaceImplementations>

// <ImplicitConversions>
// </ImplicitConversions>

// The struct types whose fields this package reaches through a pointer, as Go spells it, `p.f`:
// go2cs-gen emits a field property on the pointer for each of their fields.
[assembly: GoFieldAccess(typeof(CopyOnWriteSet<>))]
[assembly: GoFieldAccess(typeof(OrderedSet<>))]
[assembly: GoFieldAccess(typeof(Set<>))]
[assembly: GoFieldAccess(typeof(SyncMap<,>))]
[assembly: GoFieldAccess(typeof(SyncSet<>))]

// Go source positions are recorded here, one `GoPositionMap` attribute per converted
// source file in this compilation, so that `runtime.Caller` and the tracebacks built on it
// can name the GO file and line a frame was converted from rather than the emitted C# one.
// Each record carries the Go file's identity and an encoded C#-line to Go-line table
// TOGETHER: a frame either has a record and reports a position that exists in the Go tree,
// or has none - golib, the BCL and hand-written conversions - and reports its own C# position.

// <GoSourcePositionMaps>
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/collections/cow.go", "cow.cs", "AA4ggoKogoKogoKmgoKUgpSUrqKCgpAACxCCgqiCroI=", "52-52:1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/collections/multimap.go", "multimap.cs", "AA0agsqCgoKUxoKCpoKmgoKUpoKCgoKClILcgqaCpoKmgqaC")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/collections/ordered_map.go", "ordered_map.cs", "AB08graCgqaCAAoWgoKClMiCgqaClKiCgqiCqIKCgoKmgoKogoKogoKCgqaChIKCgoKAgoKClLaqgoKCqoKCAAUQgoKCqoKC3oKCgqqCgoIABRCCgoKoooKmqKKCpoKmgvy0goCCtoSCgqaCpoK4xoKClIKClIKUlKSkprSCgoKUipSClIKCgoKUgpSUgpTGgqqygoKmgoKCpg==", "130-142:1;148-160:1;165-178:1;296-298:1", "", "196=Clone/1/2/1,Clone/2/2/2")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/collections/ordered_set.go", "ordered_set.cs", "AAsWguyCqIKogoKogqqCqIKogg==", "", "", "13=newMapWithSizeHint/1/1/1;46=clone/1/1/1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/collections/set.go", "set.cs", "AAsUguqigpSCpoKClKaCpqKClKaigpSmooKUqKKClIKmooKUgqaygpSClIKClKaigpSCgoKUgpSUprKClIKUpqKClIKCpqaigpSCgqbGooKClA==")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/collections/syncmap.go", "syncmap.cs", "ABAcsoKClKaCprKCgqamgqaCpoKCgoKmgoKmvoKCgoKUpoKCkoKUpoKCgoKUyoKCkoKU", "44-56:1;64-67:1;73-76:1;81-88:1;82-87:1.1;93-96:1")]
[assembly: GoPositionMap("github.com/microsoft/TypeScript/tsc@/internal/collections/syncset.go", "syncset.cs", "AA0SgoKmgqyCgqaCpoIAAhCCgoKClKaCgoKCpqaCgoKCgpSmgoKCgpQ=", "31-33:1;41-44:1;50-53:1;61-64:1;69-76:1;70-75:1.1")]
// </GoSourcePositionMaps>

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

[GoPackage("collections", ImportPath = "github.com/microsoft/TypeScript/tsc/internal/collections")]
public static partial class collections_package
{
    // C# nested types declared with no access modifier are always private, and the
    // Go type declarations in this package's converted sources are deliberately
    // bare so they read more like the original Go code. The real accessibility for
    // the types - public for a Go-exported name, internal otherwise - are defined
    // via declarations below.

    // <TypeAccessibility>
    internal partial struct noCopy {}
    public partial struct CopyOnWriteMap<K, V> {}
    public partial struct CopyOnWriteSet<K> {}
    public partial struct MapEntry<K, V> {}
    public partial struct MultiMap<K, V> {}
    public partial struct OrderedMap<K, V> {}
    public partial struct OrderedSet<T> {}
    public partial struct Set<T> {}
    public partial struct SyncMap<K, V> {}
    public partial struct SyncSet<T> {}
    // </TypeAccessibility>

    // Go initializes an imported package before the importing package, for every import
    // form - not only the blank one. .NET would never load an assembly nothing has touched
    // yet, so each import that initializes anything is forced below: once per assembly, and
    // ahead of this package's own `init` functions, which this file being the first compile
    // item of the project guarantees.

    // <ImportInitializers>
    [GoInit] internal static void initᴛᴛimportꓸerrors() => builtin.initPackage(typeof(errors_package));
    [GoInit] internal static void initᴛᴛimportꓸgithub_comꓸmicrosoftꓸTypeScriptꓸtscꓸinternalꓸjson() => builtin.initPackage(typeof(go.github.com.microsoft.TypeScript.tsc.@internal.json_package));
    [GoInit] internal static void initᴛᴛimportꓸiter() => builtin.initPackage(typeof(iter_package));
    [GoInit] internal static void initᴛᴛimportꓸmaps() => builtin.initPackage(typeof(maps_package));
    [GoInit] internal static void initᴛᴛimportꓸreflect() => builtin.initPackage(typeof(reflect_package));
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
[GoMethodHost(typeof(collections_package.CopyOnWriteMap<,>)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class collectionsᴛCopyOnWriteMap;
[GoMethodHost(typeof(collections_package.CopyOnWriteSet<>)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class collectionsᴛCopyOnWriteSet;
[GoMethodHost(typeof(collections_package.MultiMap<,>)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class collectionsᴛMultiMap;
[GoMethodHost(typeof(collections_package.OrderedMap<,>)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class collectionsᴛOrderedMap;
[GoMethodHost(typeof(collections_package.OrderedSet<>)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class collectionsᴛOrderedSet;
[GoMethodHost(typeof(collections_package.Set<>)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class collectionsᴛSet;
[GoMethodHost(typeof(collections_package.SyncMap<,>)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class collectionsᴛSyncMap;
[GoMethodHost(typeof(collections_package.SyncSet<>)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class collectionsᴛSyncSet;
[GoMethodHost(typeof(collections_package.noCopy)), global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)] public static partial class collectionsᴛnoCopy;
// </MethodHolders>
