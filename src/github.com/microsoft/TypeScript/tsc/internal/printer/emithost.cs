namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using tsoptions = go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class printer_package {

partial interface EmitHost {
    ж<core.CompilerOptions> Options();
    slice<ж<ast.SourceFile>> SourceFiles();
    bool UseCaseSensitiveFileNames();
    @string GetCurrentDirectory();
    @string CommonSourceDirectory();
    bool IsEmitBlocked(@string @file);
    error WriteFile(@string fileName, @string text);
    core.ModuleKind GetEmitModuleFormatOfFile(ast.HasFileName @file);
    EmitResolver GetEmitResolver();
    ж<tsoptions.SourceOutputAndProjectReference> GetProjectReferenceFromSource(tspath.Path path);
    bool IsSourceFileFromExternalLibrary(ж<ast.SourceFile> @file);
}

} // end printer_package
