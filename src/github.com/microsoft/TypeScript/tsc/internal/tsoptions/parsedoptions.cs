namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using contentmapper = go.github.com.microsoft.TypeScript.tsc.@internal.contentmapper_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class tsoptions_package {

partial struct ParsedOptions {
    public ж<core.CompilerOptions> CompilerOptions; /*`json:"compilerOptions"`*/
    public ж<core.WatchOptions> WatchOptions; /*`json:"watchOptions"`*/
    public ж<core.TypeAcquisition> TypeAcquisition; /*`json:"typeAcquisition"`*/
    public slice<@string> FileNames; /*`json:"fileNames"`*/
    public slice<ж<core.ProjectReference>> ProjectReferences; /*`json:"projectReferences"`*/
    public slice<ж<contentmapper.Mapper>> ContentMappers; /*`json:"contentMappers"`*/
}

} // end tsoptions_package
