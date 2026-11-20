namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using slices = slices_package;
using sync = sync_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using tsoptions = go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using ꓸꓸꓸжprocessingDiagnostic = Span<ж<compiler_package.processingDiagnostic>>;
using static compiler_package;

partial class compiler_package {

partial struct includeProcessor {
    internal map<tspath.Path, slice<ж<FileIncludeReason>>> fileIncludeReasons;
    internal slice<ж<processingDiagnostic>> processingDiagnostics;
    internal collections.SyncMap<ж<FileIncludeReason>, ж<referenceFileLocation>> reasonToReferenceLocation;
    internal collections.SyncMap<ж<FileIncludeReason>, ж<ast.Diagnostic>> includeReasonToRelatedInfo;
    internal collections.SyncMap<tspath.Path, slice<ж<ast.Diagnostic>>> redirectAndFileFormat;
    internal ж<ast.DiagnosticsCollection> computedDiagnostics;
    internal sync.Once computedDiagnosticsOnce;
    internal ж<ast.ObjectLiteralExpression> compilerOptionsSyntax;
    internal sync.Once compilerOptionsSyntaxOnce;
}

internal static void updateFileIncludeProcessor(ref Program p) {
    p.includeProcessor = Ꮡ(new includeProcessor(
        fileIncludeReasons: p.includeProcessor.fileIncludeReasons,
        processingDiagnostics: p.includeProcessor.processingDiagnostics
    ));
}

} partial class compilerᴛincludeProcessor {

internal static ж<ast.DiagnosticsCollection> getDiagnostics(this ж<includeProcessor> Ꮡi, ж<Program> p) {
    ref var i = ref Ꮡi.DerefOrNull();

    Ꮡi.ᏑcomputedDiagnosticsOnce.Do(() => {
        Ꮡi.Value.computedDiagnostics = Ꮡ(new ast.DiagnosticsCollection());
        foreach (var (_, d) in range(Ꮡi.Value.processingDiagnostics)) {
            Ꮡi.Value.computedDiagnostics.Add(d.toDiagnostic(p));
        }
        foreach (var (_, resolutions) in p.Value.resolvedModules) {
            foreach (var (_, resolvedModule) in resolutions) {
                foreach (var (_, diag) in range(resolvedModule.ResolutionDiagnostics)) {
                    Ꮡi.Value.computedDiagnostics.Add(diag);
                }
            }
        }
        foreach (var (_, typeResolutions) in p.Value.typeResolutionsInFile) {
            foreach (var (_, resolvedTypeRef) in typeResolutions) {
                foreach (var (_, diag) in range(resolvedTypeRef.ResolutionDiagnostics)) {
                    Ꮡi.Value.computedDiagnostics.Add(diag);
                }
            }
        }
    });
    return i.computedDiagnostics;
}

internal static void addProcessingDiagnostic(this ref includeProcessor i, params ꓸꓸꓸжprocessingDiagnostic dʗp) {
    var d = dʗp.sslice();

    i.processingDiagnostics = appendꓸꓸꓸ(i.processingDiagnostics, d);
}

internal static void addProcessingDiagnosticsForFileCasing(this ref includeProcessor i, tspath.Path @file, @string existingCasing, @string currentCasing, ж<FileIncludeReason> reason) {
    if (!reason.isReferencedFile() && slices.ContainsFunc(i.fileIncludeReasons[@file], (ж<FileIncludeReason> r) => r.isReferencedFile())) {
        i.addProcessingDiagnostic(Ꮡ(new processingDiagnostic(
            kind: processingDiagnosticKindExplainingFileInclude,
            data: Ꮡ(new includeExplainingDiagnostic(
                @file: @file,
                diagnosticReason: reason,
                message: diagnostics.Already_included_file_name_0_differs_from_file_name_1_only_in_casing,
                args: [existingCasing, currentCasing]
            ))
        )));
    } else {
        i.addProcessingDiagnostic(Ꮡ(new processingDiagnostic(
            kind: processingDiagnosticKindExplainingFileInclude,
            data: Ꮡ(new includeExplainingDiagnostic(
                @file: @file,
                diagnosticReason: reason,
                message: diagnostics.File_name_0_differs_from_already_included_file_name_1_only_in_casing,
                args: [currentCasing, existingCasing]
            ))
        )));
    }
}

internal static ж<referenceFileLocation> getReferenceLocation(this ж<includeProcessor> Ꮡi, ж<FileIncludeReason> Ꮡr, ж<Program> program) {
    ref var r = ref Ꮡr.DerefOrNull();

    var (existing, ok) = Ꮡi.ᏑreasonToReferenceLocation.Load(Ꮡr); if (ok) {
        return existing;
    }

    var (loc, _) = Ꮡi.ᏑreasonToReferenceLocation.LoadOrStore(Ꮡr, r.getReferencedLocation(program));
    return loc;
}

internal static ж<ast.ObjectLiteralExpression> getCompilerOptionsObjectLiteralSyntax(this ж<includeProcessor> Ꮡi, ж<Program> program) {
    ref var i = ref Ꮡi.DerefOrNull();

    Ꮡi.ᏑcompilerOptionsSyntaxOnce.Do(() => {
        var configFile = program.Value.opts.Config.ConfigFile;
        if (configFile != nil) {
            var compilerOptionsProperty = tsoptions.ForEachTsConfigPropArray<ast.PropertyAssignment>(configFile.SourceFile, "compilerOptions", core.Identity<ж<ast.PropertyAssignment>>); if (compilerOptionsProperty != nil && (~compilerOptionsProperty).Initializer != nil && ast.IsObjectLiteralExpression((~compilerOptionsProperty).Initializer)) {
                Ꮡi.Value.compilerOptionsSyntax = (~compilerOptionsProperty).Initializer.AsObjectLiteralExpression();
            }
        } else {
            Ꮡi.Value.compilerOptionsSyntax = default;
        }
    });
    return i.compilerOptionsSyntax;
}

internal static ж<ast.Diagnostic> getRelatedInfo(this ж<includeProcessor> Ꮡi, ж<FileIncludeReason> r, ж<Program> program) {
    var (existing, ok) = Ꮡi.ᏑincludeReasonToRelatedInfo.Load(r); if (ok) {
        return existing;
    }

    var (relatedInfo, _) = Ꮡi.ᏑincludeReasonToRelatedInfo.LoadOrStore(r, r.toRelatedInfo(program));
    return relatedInfo;
}

internal static slice<ж<ast.Diagnostic>> explainRedirectAndImpliedFormat(this ж<includeProcessor> Ꮡi, ж<Program> Ꮡprogram, tspath.Path filePath, Func<@string, @string> toFileName) {
    ref var program = ref Ꮡprogram.DerefOrNull();

    var (existing, ok) = Ꮡi.ᏑredirectAndFileFormat.Load(filePath); if (ok) {
        return existing;
    }
    ast.HasFileName @file = default;
    ж<ast.SourceFile> sourceFile = default;
    var redirectsFile = program.redirectFilesByPath[filePath];
    if (redirectsFile != nil) {
        @file = new redirectsFileжHasFileName(redirectsFile);
    } else {
        sourceFile = program.GetSourceFileByPath(filePath);
        if (sourceFile == nil) {
            return default;
        }
        @file = new ast.SourceFileжHasFileName(sourceFile);
    }
    slice<ж<ast.Diagnostic>> result = default;
    @string source = program.GetSourceOfProjectReferenceIfOutputIncluded(@file); if (source != @file.FileName()) {
        result = append(result, ast.NewCompilerDiagnostic(
            diagnostics.File_is_output_of_project_reference_source_0,
            toFileName(source)));
    }

    if (redirectsFile != nil) {
        var targetFile = program.GetSourceFileByPath(redirectsFile.target);
        result = append(result, ast.NewCompilerDiagnostic(
            diagnostics.File_redirects_to_file_0,
            toFileName(targetFile.FileName())));
    }

    if (sourceFile != nil && ast.IsExternalOrCommonJSModule(sourceFile)) {
        var metaData = program.GetSourceFileMetaData(@file.Path());
        switch (program.GetImpliedNodeFormatForEmit(@file)) {
        case var ᴛ when ᴛ == core.ModuleKindESNext: {
            if (metaData.PackageJsonType == "module"u8) {
                result = append(result, ast.NewCompilerDiagnostic(
                    diagnostics.File_is_ECMAScript_module_because_0_has_field_type_with_value_module,
                    toFileName(metaData.PackageJsonDirectory + "/package.json"u8)));
            }
            break;
        }
        case var ᴛ when ᴛ == core.ModuleKindCommonJS: {
            if (metaData.PackageJsonType != ""u8) {
                result = append(result, ast.NewCompilerDiagnostic(diagnostics.File_is_CommonJS_module_because_0_has_field_type_whose_value_is_not_module, toFileName(metaData.PackageJsonDirectory + "/package.json"u8)));
            } else if (metaData.PackageJsonDirectory != ""u8) {
                if (metaData.PackageJsonType == ""u8) {
                    result = append(result, ast.NewCompilerDiagnostic(diagnostics.File_is_CommonJS_module_because_0_does_not_have_field_type, toFileName(metaData.PackageJsonDirectory + "/package.json"u8)));
                }
            } else {
                result = append(result, ast.NewCompilerDiagnostic(diagnostics.File_is_CommonJS_module_because_package_json_was_not_found));
            }
            break;
        }}

    }

    (result, _) = Ꮡi.ᏑredirectAndFileFormat.LoadOrStore(filePath, result);
    return result;
}

} // end compilerᴛincludeProcessor
