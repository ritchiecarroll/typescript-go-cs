namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static declarations_package;

partial class declarations_package {

partial struct SupplementalReferencesTransformer {
    internal DeclarationEmitHost host;
    internal slice<ж<ast.SourceFile>> supplementalFiles;
    internal @string declarationFilePath;
    internal bool forceDeclarationPaths;
}

public static ж<SupplementalReferencesTransformer> NewSupplementalReferencesTransformer(DeclarationEmitHost host, ж<ast.SourceFile> ᏑsourceFile, @string declarationFilePath, bool forceDeclarationPaths) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    return Ꮡ(new SupplementalReferencesTransformer(
        host: host,
        supplementalFiles: sourceFile.SupplementalSourceFiles(),
        declarationFilePath: declarationFilePath,
        forceDeclarationPaths: forceDeclarationPaths
    ));
}

} partial class declarationsᴛSupplementalReferencesTransformer {

public static ж<ast.SourceFile> TransformSourceFile(this ref SupplementalReferencesTransformer t, ж<ast.SourceFile> ᏑsourceFile) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    foreach (var (_, supplemental) in range(t.supplementalFiles)) {
        if (!t.host.SourceFileMayBeEmitted(supplemental, t.forceDeclarationPaths)) {
            continue;
        }
        @string declarationPath = t.host.GetOutputPathsFor(supplemental, t.forceDeclarationPaths).DeclarationFilePath();
        if (declarationPath == ""u8) {
            continue;
        }
        sourceFile.ReferencedFiles = append(sourceFile.ReferencedFiles, Ꮡ(new ast.FileReference(
            TextRange: core.NewTextRange(-1, -1),
            FileName: tspath.GetRelativePathFromFile(
                t.declarationFilePath,
                declarationPath,
                new tspath.ComparePathsOptions(
                    CurrentDirectory: t.host.GetCurrentDirectory(),
                    UseCaseSensitiveFileNames: t.host.UseCaseSensitiveFileNames()
                ))
        )));
    }
    return ᏑsourceFile;
}

public static slice<ж<ast.Diagnostic>> GetDiagnostics(this ref SupplementalReferencesTransformer t) {
    return default;
}

} // end declarationsᴛSupplementalReferencesTransformer
