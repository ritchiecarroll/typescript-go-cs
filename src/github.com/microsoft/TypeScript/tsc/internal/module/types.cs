namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using fmt = fmt_package;
using bits = math.bits_package;
using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using Δvfs = go.github.com.microsoft.TypeScript.tsc.@internal.vfs_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using math;
using static module_package;

partial class module_package {

partial interface ResolutionHost {
    Δvfs.FS FS();
    @string GetCurrentDirectory();
}

partial struct ModeAwareCacheKey {
    public @string Name;
    public coreꓸResolutionMode Mode;
}

partial interface ResolvedProjectReference {
    @string ConfigName();
    ж<core.CompilerOptions> CompilerOptions();
}

partial struct NodeResolutionFeatures /*num:int32*/;

public static NodeResolutionFeatures NodeResolutionFeaturesImports => /* 1 << iota */ 1;
public static NodeResolutionFeatures NodeResolutionFeaturesSelfName => 2;
public static NodeResolutionFeatures NodeResolutionFeaturesExports => 4;
public static NodeResolutionFeatures NodeResolutionFeaturesExportsPatternTrailers => 8;
public static NodeResolutionFeatures NodeResolutionFeaturesImportsPatternRoot => 16;
public static NodeResolutionFeatures NodeResolutionFeaturesNone => 0;
public static NodeResolutionFeatures NodeResolutionFeaturesAll => /* NodeResolutionFeaturesImports | NodeResolutionFeaturesSelfName | NodeResolutionFeaturesExports | NodeResolutionFeaturesExportsPatternTrailers | NodeResolutionFeaturesImportsPatternRoot */ 31;
public static NodeResolutionFeatures NodeResolutionFeaturesNode16Default => /* NodeResolutionFeaturesImports | NodeResolutionFeaturesSelfName | NodeResolutionFeaturesExports | NodeResolutionFeaturesExportsPatternTrailers */ 15;
public static NodeResolutionFeatures NodeResolutionFeaturesNodeNextDefault => /* NodeResolutionFeaturesAll */ 31;
public static NodeResolutionFeatures NodeResolutionFeaturesBundlerDefault => /* NodeResolutionFeaturesImports | NodeResolutionFeaturesSelfName | NodeResolutionFeaturesExports | NodeResolutionFeaturesExportsPatternTrailers | NodeResolutionFeaturesImportsPatternRoot */ 31;

partial struct PackageId {
    public @string Name;
    public @string SubModuleName;
    public @string Version;
    public @string PeerDependencies;
}

} partial class moduleᴛPackageId {

public static @string String(this ref PackageId p) {
    return fmt.Sprintf("%s@%s%s"u8, p.PackageName(), p.Version, p.PeerDependencies);
}

public static @string PackageName(this ref PackageId p) {
    if (p.SubModuleName != ""u8) {
        return p.Name + "/"u8 + p.SubModuleName;
    }
    return p.Name;
}

} partial class module_package {

partial struct ResolvedModule {
    public slice<ж<ast.Diagnostic>> ResolutionDiagnostics;
    public @string ResolvedFileName;
    public @string OriginalPath;
    public @string Extension;
    public bool ResolvedUsingTsExtension;
    public bool ResolvedUsingExtraExtensions;
    public PackageId PackageId;
    public bool IsExternalLibraryImport;
    public @string AlternateResult;
}

} partial class moduleᴛResolvedModule {

public static bool IsResolved(this ж<ResolvedModule> Ꮡr) {
    ref var r = ref Ꮡr.DerefOrNull();

    return Ꮡr != nil && r.ResolvedFileName != ""u8;
}

} partial class module_package {

partial struct ResolvedTypeReferenceDirective {
    public slice<ж<ast.Diagnostic>> ResolutionDiagnostics;
    public bool Primary;
    public @string ResolvedFileName;
    public @string OriginalPath;
    public PackageId PackageId;
    public bool IsExternalLibraryImport;
}

} partial class moduleᴛResolvedTypeReferenceDirective {

public static bool IsResolved(this ref ResolvedTypeReferenceDirective r) {
    return r.ResolvedFileName != ""u8;
}

} partial class module_package {

partial struct extensions /*num:int32*/;

internal static extensions extensionsTypeScript => /* 1 << iota */ 1;
internal static extensions extensionsJavaScript => 2;
internal static extensions extensionsDeclaration => 4;
internal static extensions extensionsJson => 8;
internal static extensions extensionsImplementationFiles => /* extensionsTypeScript | extensionsJavaScript */ 3;

} partial class moduleᴛextensions {

internal static @string String(this extensions e) {
    var resultᴷ = new stackAppend<@string>();

    var result = new slice<@string>(0, bits.OnesCount((nuint)e));
    if ((extensions)(e & extensionsTypeScript) != 0) {
        result = append(resultᴷ, result, (@string)"TypeScript");
    }
    if ((extensions)(e & extensionsJavaScript) != 0) {
        result = append(result, (@string)"JavaScript");
    }
    if ((extensions)(e & extensionsDeclaration) != 0) {
        result = append(result, (@string)"Declaration");
    }
    if ((extensions)(e & extensionsJson) != 0) {
        result = append(result, (@string)"JSON");
    }
    return strings.Join(result, ", ");
}

internal static slice<@string> Array(this extensions e) {
    var result = slice<@string>([]);
    if ((extensions)(e & extensionsTypeScript) != 0) {
        result = appendꓸꓸꓸ(result, tspath.SupportedTSImplementationExtensions);
    }
    if ((extensions)(e & extensionsJavaScript) != 0) {
        result = appendꓸꓸꓸ(result, tspath.SupportedJSExtensionsFlat);
    }
    if ((extensions)(e & extensionsDeclaration) != 0) {
        result = appendꓸꓸꓸ(result, tspath.SupportedDeclarationExtensions);
    }
    if ((extensions)(e & extensionsJson) != 0) {
        result = append(result, tspath.ExtensionJson);
    }
    return result;
}

} // end moduleᴛextensions
