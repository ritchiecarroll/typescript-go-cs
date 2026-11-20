namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class core_package {

partial struct ProjectReference {
    public @string Path; /*`json:"path"`*/
    public @string OriginalPath; /*`json:"originalPath"`*/
    public bool Circular; /*`json:"circular"`*/
}

public static @string ResolveProjectReferencePath(ж<ProjectReference> Ꮡref) {
    ref var @ref = ref Ꮡref.DerefOrNull();

    return ResolveConfigFileNameOfProjectReference(@ref.Path);
}

public static @string ResolveConfigFileNameOfProjectReference(@string path) {
    if (tspath.FileExtensionIs(path, tspath.ExtensionJson)) {
        return path;
    }
    return tspath.CombinePaths(path, "tsconfig.json");
}

} // end core_package
