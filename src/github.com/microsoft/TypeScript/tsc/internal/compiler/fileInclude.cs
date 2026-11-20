namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using fmt = fmt_package;
using sync = sync_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using module = go.github.com.microsoft.TypeScript.tsc.@internal.module_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using tsoptions = go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using ꓸꓸꓸany = Span<any>;
using static compiler_package;

partial class compiler_package {

partial struct fileIncludeKind /*num:nint*/;

internal static UntypedInt fileIncludeKindImport => iota;
internal static UntypedInt fileIncludeKindReferenceFile => 1;
internal static UntypedInt fileIncludeKindTypeReferenceDirective => 2;
internal static UntypedInt fileIncludeKindLibReferenceDirective => 3;
internal static UntypedInt fileIncludeKindRootFile => 4;
internal static UntypedInt fileIncludeKindLibFile => 5;
internal static UntypedInt fileIncludeKindAutomaticTypeDirectiveFile => 6;
internal static UntypedInt fileIncludeKindContentMapperSupplemental => 7;

partial struct FileIncludeReason {
    internal fileIncludeKind kind;
    internal any data;
    internal ж<ast.Diagnostic> relativeFileNameDiag;
    internal sync.Once relativeFileNameDiagOnce;
    internal ж<ast.Diagnostic> diag;
    internal sync.Once diagOnce;
}

partial struct referencedFileData {
    internal tspath.Path @file;
    internal nint index;
    internal ж<ast.Node> synthetic;
}

partial struct referenceFileLocation {
    internal ж<ast.SourceFile> @file;
    internal ж<ast.Node> node;
    internal ж<ast.FileReference> @ref;
    internal module.PackageId packageId;
    internal bool isSynthetic;
}

} partial class compilerᴛreferenceFileLocation {

internal static @string text(this ref referenceFileLocation r) {
    if (r.node != nil) {
        if (!ast.NodeIsSynthesized(r.node)) {
            return r.@file.Text().slice(scanner.SkipTrivia(r.@file.Text(), r.node.Loc.Pos()), r.node.End());
        } else {
            return fmt.Sprintf(@"""%s"""u8, r.node.Text());
        }
    } else {
        return r.@file.Text().slice(r.@ref.Value.TextRange.Pos(), r.@ref.Value.TextRange.End());
    }
}

internal static ж<ast.Diagnostic> diagnosticAt(this ref referenceFileLocation r, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();

    if (r.node != nil) {
        return tsoptions.CreateDiagnosticForNodeInSourceFile(r.@file, r.node, message, args.ꓸꓸꓸ);
    } else {
        return ast.NewDiagnostic(r.@file, r.@ref.TextRange, message, args.ꓸꓸꓸ);
    }
}

} partial class compiler_package {

partial struct automaticTypeDirectiveFileData {
    internal @string typeReference;
    internal module.PackageId packageId;
}

} partial class compilerᴛFileIncludeReason {

internal static nint asIndex(this ref FileIncludeReason r) {
    return r.data._<nint>();
}

internal static (nint, bool) asLibFileIndex(this ref FileIncludeReason r) {
    var (index, ok) = r.data._<nint>(ᐧ);
    return (index, ok);
}

internal static bool isReferencedFile(this ж<FileIncludeReason> Ꮡr) {
    ref var r = ref Ꮡr.DerefOrNull();

    return Ꮡr != nil && r.kind <= fileIncludeKindLibReferenceDirective;
}

internal static ж<referencedFileData> asReferencedFileData(this ref FileIncludeReason r) {
    return r.data._<ж<referencedFileData>>();
}

internal static ж<automaticTypeDirectiveFileData> asAutomaticTypeDirectiveFileData(this ref FileIncludeReason r) {
    return r.data._<ж<automaticTypeDirectiveFileData>>();
}

internal static ж<referenceFileLocation> getReferencedLocation(this ref FileIncludeReason r, ж<Program> Ꮡprogram) {
    ref var program = ref Ꮡprogram.DerefOrNull();

    var @ref = r.asReferencedFileData();
    var @file = program.GetSourceFileByPath(@ref.@file);
    switch (r.kind) {
    case var ᴛ when ᴛ == fileIncludeKindImport: {
        ж<ast.Node> specifier = default;
        ref var isSynthetic = ref heap(new bool(), out var ᏑisSynthetic);
        if (@ref.synthetic != nil) {
            specifier = @ref.synthetic;
            isSynthetic = true;
        } else if (@ref.index < len(@file.Imports())) {
            specifier = @file.Imports()[@ref.index];
        } else {
            nint augIndex = len(@file.Imports());
            foreach (var (_, imp) in range(@file.ModuleAugmentations)) {
                if (imp.Kind == ast.KindStringLiteral) {
                    if (augIndex == @ref.index) {
                        specifier = imp;
                        break;
                    }
                    augIndex++;
                }
            }
        }
        var resolution = program.GetResolvedModuleFromModuleSpecifier(new ast.SourceFileжHasFileName(@file), specifier);
        return Ꮡ(new referenceFileLocation(
            @file: @file,
            node: specifier,
            packageId: resolution.PackageId,
            isSynthetic: isSynthetic
        ));
    }
    case var ᴛ when ᴛ == fileIncludeKindReferenceFile: {
        return Ꮡ(new referenceFileLocation(
            @file: @file,
            @ref: @file.ReferencedFiles[@ref.index]
        ));
    }
    case var ᴛ when ᴛ == fileIncludeKindTypeReferenceDirective: {
        return Ꮡ(new referenceFileLocation(
            @file: @file,
            @ref: @file.TypeReferenceDirectives[@ref.index]
        ));
    }
    case var ᴛ when ᴛ == fileIncludeKindLibReferenceDirective: {
        return Ꮡ(new referenceFileLocation(
            @file: @file,
            @ref: @file.LibReferenceDirectives[@ref.index]
        ));
    }
    default: {
        throw panic(fmt.Sprintf("unknown reason: %v"u8, r.kind));
        break;
    }}

}

internal static ж<ast.Diagnostic> toDiagnostic(this ж<FileIncludeReason> Ꮡr, ж<Program> program, bool relativeFileName) {
    ref var r = ref Ꮡr.DerefOrNull();

    if (relativeFileName) {
        Ꮡr.ᏑrelativeFileNameDiagOnce.Do(() => {
            Ꮡr.Value.relativeFileNameDiag = Ꮡr.computeDiagnostic(program, (@string fileName) => tspath.GetRelativePathFromDirectory(program.Value.GetCurrentDirectory(), fileName, program.Value.comparePathsOptions));
        });
        return r.relativeFileNameDiag;
    } else {
        Ꮡr.ᏑdiagOnce.Do(() => {
            Ꮡr.Value.diag = Ꮡr.computeDiagnostic(program, (@string fileName) => fileName);
        });
        return r.diag;
    }
}

internal static ж<ast.Diagnostic> computeDiagnostic(this ж<FileIncludeReason> Ꮡr, ж<Program> Ꮡprogram, Func<@string, @string> toFileName) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var program = ref Ꮡprogram.DerefOrNull();

    if (Ꮡr.isReferencedFile()) {
        return Ꮡr.computeReferenceFileDiagnostic(Ꮡprogram, toFileName);
    }
    switch (r.kind) {
    case var ᴛ when ᴛ == fileIncludeKindRootFile: {
        if (program.opts.Config.ConfigFile != nil) {
            var config = program.opts.Config;
            @string fileName = tspath.GetNormalizedAbsolutePath(config.FileNames()[r.asIndex()], program.GetCurrentDirectory());
            @string matchedFileSpec = config.GetMatchedFileSpec(fileName); if (matchedFileSpec != ""u8) {
                return ast.NewCompilerDiagnostic(diagnostics.Part_of_files_list_in_tsconfig_json, matchedFileSpec, toFileName(fileName));
            } else if (config.GetMatchedIncludeSpec(fileName) is var (matchedIncludeSpec, isDefaultIncludeSpec) && matchedIncludeSpec != ""u8) {
                if (isDefaultIncludeSpec) {
                    return ast.NewCompilerDiagnostic(diagnostics.Matched_by_default_include_pattern_Asterisk_Asterisk_Slash_Asterisk);
                } else {
                    return ast.NewCompilerDiagnostic(diagnostics.Matched_by_include_pattern_0_in_1, matchedIncludeSpec, toFileName(config.ConfigName()));
                }
            } else {
                return ast.NewCompilerDiagnostic(diagnostics.Root_file_specified_for_compilation);
            }
        } else {
            return ast.NewCompilerDiagnostic(diagnostics.Root_file_specified_for_compilation);
        }
        break;
    }
    case var ᴛ when ᴛ == fileIncludeKindAutomaticTypeDirectiveFile: {
        var data = r.asAutomaticTypeDirectiveFileData();
        if (!program.Options().UsesWildcardTypes()) {
            if (data.packageId.Name != ""u8) {
                return ast.NewCompilerDiagnostic(diagnostics.Entry_point_of_type_library_0_specified_in_compilerOptions_with_packageId_1, data.typeReference, data.ᏑpackageId.String());
            } else {
                return ast.NewCompilerDiagnostic(diagnostics.Entry_point_of_type_library_0_specified_in_compilerOptions, data.typeReference);
            }
        } else {
            if (data.packageId.Name != ""u8) {
                return ast.NewCompilerDiagnostic(diagnostics.Entry_point_for_implicit_type_library_0_with_packageId_1, data.typeReference, data.ᏑpackageId.String());
            } else {
                return ast.NewCompilerDiagnostic(diagnostics.Entry_point_for_implicit_type_library_0, data.typeReference);
            }
        }
        break;
    }
    case var ᴛ when ᴛ == fileIncludeKindLibFile: {
        var (index, ok) = r.asLibFileIndex(); if (ok) {
            return ast.NewCompilerDiagnostic(diagnostics.Library_0_specified_in_compilerOptions, program.Options().Lib[index]);
        } else {
            @string target = program.Options().GetEmitScriptTarget().String(); if (target != ""u8) {
                return ast.NewCompilerDiagnostic(diagnostics.Default_library_for_target_0, target);
            } else {
                return ast.NewCompilerDiagnostic(diagnostics.Default_library);
            }
        }
        break;
    }
    case var ᴛ when ᴛ == fileIncludeKindContentMapperSupplemental: {
        var canonical = program.GetSourceFileByPath(r.data._<tspath.Path>());
        return ast.NewCompilerDiagnostic(diagnostics.Supplemental_virtual_file_produced_by_the_content_mapper_for_file_0, toFileName(canonical.FileName()));
    }
    default: {
        throw panic(fmt.Sprintf("unknown reason: %v"u8, r.kind));
        break;
    }}

}

internal static ж<ast.Diagnostic> computeReferenceFileDiagnostic(this ж<FileIncludeReason> Ꮡr, ж<Program> Ꮡprogram, Func<@string, @string> toFileName) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var program = ref Ꮡprogram.DerefOrNull();

    var referenceLocation = program.includeProcessor.getReferenceLocation(Ꮡr, Ꮡprogram);
    @string referenceText = referenceLocation.text();
    switch (r.kind) {
    case var ᴛ when ᴛ == fileIncludeKindImport: {
        if (!referenceLocation.isSynthetic) {
            if (referenceLocation.packageId.Name != ""u8) {
                return ast.NewCompilerDiagnostic(diagnostics.Imported_via_0_from_file_1_with_packageId_2, referenceText, toFileName(referenceLocation.@file.FileName()), referenceLocation.ᏑpackageId.String());
            } else {
                return ast.NewCompilerDiagnostic(diagnostics.Imported_via_0_from_file_1, referenceText, toFileName(referenceLocation.@file.FileName()));
            }
        } else if (program.importHelpersImportSpecifiers[referenceLocation.@file.Path(), ꟷ] is var (specifier, ok) && ok && specifier == referenceLocation.node) {
            if (referenceLocation.packageId.Name != ""u8) {
                return ast.NewCompilerDiagnostic(diagnostics.Imported_via_0_from_file_1_with_packageId_2_to_import_importHelpers_as_specified_in_compilerOptions, referenceText, toFileName(referenceLocation.@file.FileName()), referenceLocation.ᏑpackageId.String());
            } else {
                return ast.NewCompilerDiagnostic(diagnostics.Imported_via_0_from_file_1_to_import_importHelpers_as_specified_in_compilerOptions, referenceText, toFileName(referenceLocation.@file.FileName()));
            }
        } else {
            if (referenceLocation.packageId.Name != ""u8) {
                return ast.NewCompilerDiagnostic(diagnostics.Imported_via_0_from_file_1_with_packageId_2_to_import_jsx_and_jsxs_factory_functions, referenceText, toFileName(referenceLocation.@file.FileName()), referenceLocation.ᏑpackageId.String());
            } else {
                return ast.NewCompilerDiagnostic(diagnostics.Imported_via_0_from_file_1_to_import_jsx_and_jsxs_factory_functions, referenceText, toFileName(referenceLocation.@file.FileName()));
            }
        }
        break;
    }
    case var ᴛ when ᴛ == fileIncludeKindReferenceFile: {
        return ast.NewCompilerDiagnostic(diagnostics.Referenced_via_0_from_file_1, referenceText, toFileName(referenceLocation.@file.FileName()));
    }
    case var ᴛ when ᴛ == fileIncludeKindTypeReferenceDirective: {
        if (referenceLocation.packageId.Name != ""u8) {
            return ast.NewCompilerDiagnostic(diagnostics.Type_library_referenced_via_0_from_file_1_with_packageId_2, referenceText, toFileName(referenceLocation.@file.FileName()), referenceLocation.ᏑpackageId.String());
        } else {
            return ast.NewCompilerDiagnostic(diagnostics.Type_library_referenced_via_0_from_file_1, referenceText, toFileName(referenceLocation.@file.FileName()));
        }
        break;
    }
    case var ᴛ when ᴛ == fileIncludeKindLibReferenceDirective: {
        return ast.NewCompilerDiagnostic(diagnostics.Library_referenced_via_0_from_file_1, referenceText, toFileName(referenceLocation.@file.FileName()));
    }
    default: {
        throw panic(fmt.Sprintf("unknown reason: %v"u8, r.kind));
        break;
    }}

}

internal static ж<ast.Diagnostic> toRelatedInfo(this ж<FileIncludeReason> Ꮡr, ж<Program> Ꮡprogram) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var program = ref Ꮡprogram.DerefOrNull();

    if (Ꮡr.isReferencedFile()) {
        return Ꮡr.computeReferenceFileRelatedInfo(Ꮡprogram);
    }
    if (program.opts.Config.ConfigFile == nil) {
        return default;
    }
    var config = program.opts.Config;
    switch (r.kind) {
    case var ᴛ when ᴛ == fileIncludeKindRootFile: {
        @string fileName = tspath.GetNormalizedAbsolutePath(config.FileNames()[r.asIndex()], program.GetCurrentDirectory());
        @string matchedFileSpec = config.GetMatchedFileSpec(fileName); if (matchedFileSpec != ""u8) {
            var filesNode = tsoptions.GetTsConfigPropArrayElementValue(config.ConfigFile.SourceFile, "files", matchedFileSpec); if (filesNode != nil) {
                return tsoptions.CreateDiagnosticForNodeInSourceFile(config.ConfigFile.SourceFile, filesNode.of(ast.StringLiteral.ᏑLiteralExpressionBase).of(ast.LiteralExpressionBase.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), diagnostics.File_is_matched_by_files_list_specified_here);
            }
        } else if (config.GetMatchedIncludeSpec(fileName) is var (matchedIncludeSpec, isDefaultIncludeSpec) && matchedIncludeSpec != ""u8 && !isDefaultIncludeSpec) {
            var includeNode = tsoptions.GetTsConfigPropArrayElementValue(config.ConfigFile.SourceFile, "include", matchedIncludeSpec); if (includeNode != nil) {
                return tsoptions.CreateDiagnosticForNodeInSourceFile(config.ConfigFile.SourceFile, includeNode.of(ast.StringLiteral.ᏑLiteralExpressionBase).of(ast.LiteralExpressionBase.ᏑPrimaryExpressionBase).of(ast.PrimaryExpressionBase.ᏑMemberExpressionBase).of(ast.MemberExpressionBase.ᏑLeftHandSideExpressionBase).of(ast.LeftHandSideExpressionBase.ᏑUpdateExpressionBase).of(ast.UpdateExpressionBase.ᏑUnaryExpressionBase).of(ast.UnaryExpressionBase.ᏑExpressionBase).of(ast.ExpressionBase.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), diagnostics.File_is_matched_by_include_pattern_specified_here);
            }
        }
        break;
    }
    case var ᴛ when ᴛ == fileIncludeKindAutomaticTypeDirectiveFile: {
        if (!program.Options().UsesWildcardTypes()) {
            var data = r.asAutomaticTypeDirectiveFileData();
            var typesSyntax = tsoptions.GetOptionsSyntaxByArrayElementValue(program.includeProcessor.getCompilerOptionsObjectLiteralSyntax(Ꮡprogram), "types", data.typeReference); if (typesSyntax != nil) {
                return tsoptions.CreateDiagnosticForNodeInSourceFile(config.ConfigFile.SourceFile, typesSyntax.AsNode(), diagnostics.File_is_entry_point_of_type_library_specified_here);
            }
        }
        break;
    }
    case var ᴛ when ᴛ == fileIncludeKindLibFile: {
        var (index, ok) = r.asLibFileIndex(); if (ok) {
            var libSyntax = tsoptions.GetOptionsSyntaxByArrayElementValue(program.includeProcessor.getCompilerOptionsObjectLiteralSyntax(Ꮡprogram), "lib", program.Options().Lib[index]); if (libSyntax != nil) {
                return tsoptions.CreateDiagnosticForNodeInSourceFile(config.ConfigFile.SourceFile, libSyntax.AsNode(), diagnostics.File_is_library_specified_here);
            }
        } else {
            @string target = program.Options().GetEmitScriptTarget().String(); if (target != ""u8) {
                var targetValueSyntax = tsoptions.ForEachPropertyAssignment(program.includeProcessor.getCompilerOptionsObjectLiteralSyntax(Ꮡprogram), "target", tsoptions.GetCallbackForFindingPropertyAssignmentByValue(target)); if (targetValueSyntax != nil) {
                    return tsoptions.CreateDiagnosticForNodeInSourceFile(config.ConfigFile.SourceFile, targetValueSyntax.AsNode(), diagnostics.File_is_default_library_for_target_specified_here);
                }
            }
        }
        break;
    }
    case var ᴛ when ᴛ == fileIncludeKindContentMapperSupplemental: {
        return default;
    }
    default: {
        throw panic(fmt.Sprintf("unknown reason: %v"u8, r.kind));
        break;
    }}

    return default;
}

internal static ж<ast.Diagnostic> computeReferenceFileRelatedInfo(this ж<FileIncludeReason> Ꮡr, ж<Program> Ꮡprogram) {
    ref var r = ref Ꮡr.DerefOrNull();
    ref var program = ref Ꮡprogram.DerefOrNull();

    var referenceLocation = program.includeProcessor.getReferenceLocation(Ꮡr, Ꮡprogram);
    if (referenceLocation.isSynthetic) {
        return default;
    }
    switch (r.kind) {
    case var ᴛ when ᴛ == fileIncludeKindImport: {
        return referenceLocation.diagnosticAt(diagnostics.File_is_included_via_import_here);
    }
    case var ᴛ when ᴛ == fileIncludeKindReferenceFile: {
        return referenceLocation.diagnosticAt(diagnostics.File_is_included_via_reference_here);
    }
    case var ᴛ when ᴛ == fileIncludeKindTypeReferenceDirective: {
        return referenceLocation.diagnosticAt(diagnostics.File_is_included_via_type_library_reference_here);
    }
    case var ᴛ when ᴛ == fileIncludeKindLibReferenceDirective: {
        return referenceLocation.diagnosticAt(diagnostics.File_is_included_via_library_reference_here);
    }
    default: {
        throw panic(fmt.Sprintf("unknown reason: %v"u8, r.kind));
        break;
    }}

}

} // end compilerᴛFileIncludeReason
