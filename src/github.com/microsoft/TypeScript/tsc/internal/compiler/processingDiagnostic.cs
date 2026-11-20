namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using slices = slices_package;
using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using tsoptions = go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using iter = iter_package;
using static compiler_package;

partial class compiler_package {

partial struct processingDiagnosticKind /*num:nint*/;

internal static processingDiagnosticKind processingDiagnosticKindUnknownReference => /* iota */ 0;
internal static processingDiagnosticKind processingDiagnosticKindExplainingFileInclude => 1;

partial struct processingDiagnostic {
    internal processingDiagnosticKind kind;
    internal any data;
}

} partial class compilerᴛprocessingDiagnostic {

internal static ж<FileIncludeReason> asFileIncludeReason(this ref processingDiagnostic d) {
    return d.data._<ж<FileIncludeReason>>();
}

} partial class compiler_package {

partial struct includeExplainingDiagnostic {
    internal tspath.Path @file;
    internal ж<FileIncludeReason> diagnosticReason;
    internal ж<diagnostics.Message> message;
    internal slice<any> args;
}

} partial class compilerᴛprocessingDiagnostic {

internal static ж<includeExplainingDiagnostic> asIncludeExplainingDiagnostic(this ref processingDiagnostic d) {
    return d.data._<ж<includeExplainingDiagnostic>>();
}

internal static ж<ast.Diagnostic> toDiagnostic(this ref processingDiagnostic d, ж<Program> program) {
    switch (d.kind) {
    case var ᴛ when ᴛ == processingDiagnosticKindUnknownReference: {
        var @ref = d.asFileIncludeReason();
        var loc = @ref.getReferencedLocation(program);
        switch (@ref.kind) {
        case var ᴛ2 when ᴛ2 == fileIncludeKindTypeReferenceDirective: {
            return loc.diagnosticAt(diagnostics.Cannot_find_type_definition_file_for_0, loc.@ref.FileName);
        }
        case var ᴛ2 when ᴛ2 == fileIncludeKindLibReferenceDirective: {
            @string libName = tspath.ToFileNameLowerCase(loc.@ref.FileName);
            @string unqualifiedLibName = strings.TrimSuffix(strings.TrimPrefix(libName, "lib."), ".d.ts");
            @string suggestion = core.GetSpellingSuggestionForStrings(unqualifiedLibName, slices.Values<slice<@string>, @string>(tsoptions.Libs));
            return loc.diagnosticAt(core.IfElse(
                suggestion != ""u8,
                diagnostics.Cannot_find_lib_definition_for_0_Did_you_mean_1,
                diagnostics.Cannot_find_lib_definition_for_0), libName, suggestion);
        }
        default: {
            throw panic("unknown include kind");
            break;
        }}

        break;
    }
    case var ᴛ when ᴛ == processingDiagnosticKindExplainingFileInclude: {
        return d.createDiagnosticExplainingFile(program);
    }
    default: {
        throw panic("unknown processingDiagnosticKind");
        break;
    }}

}

internal static ж<ast.Diagnostic> createDiagnosticExplainingFile(this ref processingDiagnostic d, ж<Program> Ꮡprogram) {
    ref var program = ref Ꮡprogram.DerefOrNull();

    var diag = d.asIncludeExplainingDiagnostic();
    ref var includeDetails = ref heap<slice<ж<ast.Diagnostic>>>(out var ᏑincludeDetails);
    ref var relatedInfo = ref heap<slice<ж<ast.Diagnostic>>>(out var ᏑrelatedInfo);
    slice<ж<ast.Diagnostic>> redirectInfo = default;
    ref var preferredLocation = ref heap<ж<FileIncludeReason>>(out var ᏑpreferredLocation);
    ref var seenReasons = ref heap(new collections.Set<ж<FileIncludeReason>>(), out var ᏑseenReasons);
    if (diag.diagnosticReason.isReferencedFile() && !program.includeProcessor.getReferenceLocation(diag.diagnosticReason, Ꮡprogram).isSynthetic) {
        preferredLocation = diag.diagnosticReason;
    }

    void processRelatedInfo(ж<FileIncludeReason> includeReason) {
        if (ᏑpreferredLocation.ValueSlot == nil && includeReason.isReferencedFile() && !Ꮡprogram.Value.includeProcessor.getReferenceLocation(includeReason, Ꮡprogram).isSynthetic) {
            ᏑpreferredLocation.ValueSlot = includeReason;
        } else if (ᏑpreferredLocation.ValueSlot != includeReason) {
            var info = Ꮡprogram.Value.includeProcessor.getRelatedInfo(includeReason, Ꮡprogram);
            if (info != nil) {
                ᏑrelatedInfo.ValueSlot = append(ᏑrelatedInfo.ValueSlot, info);
            }
        }
    }
    var processRelatedInfoʗ1 = processRelatedInfo;
    void processInclude(ж<FileIncludeReason> includeReason) {
        if (!ᏑseenReasons.AddIfAbsent(includeReason)) {
            return;
        }
        ᏑincludeDetails.ValueSlot = append(ᏑincludeDetails.ValueSlot, includeReason.toDiagnostic(Ꮡprogram, false));
        processRelatedInfoʗ1(includeReason);
    }

    if (diag.@file != ""u8) {
        var reasons = program.includeProcessor.fileIncludeReasons[diag.@file];
        includeDetails = new slice<ж<ast.Diagnostic>>(0, len(reasons));
        foreach (var (_, reason) in range(reasons)) {
            processInclude(reason);
        }
        redirectInfo = program.includeProcessor.explainRedirectAndImpliedFormat(Ꮡprogram, diag.@file, (@string fileName) => fileName);
    }
    if (diag.diagnosticReason != nil) {
        processInclude(diag.diagnosticReason);
    }
    slice<ж<ast.Diagnostic>> chain = default;
    if (includeDetails != nil && (preferredLocation == nil || ᏑseenReasons.Len() != 1)) {
        var fileReason = ast.NewCompilerDiagnostic(diagnostics.The_file_is_in_the_program_because_Colon);
        fileReason.SetMessageChain(includeDetails);
        chain = [fileReason];
    }
    if (redirectInfo != nil) {
        chain = appendꓸꓸꓸ(chain, redirectInfo);
    }

    ж<ast.Diagnostic> result = default;
    if (preferredLocation != nil) {
        result = program.includeProcessor.getReferenceLocation(preferredLocation, Ꮡprogram).diagnosticAt(diag.message, diag.args.ꓸꓸꓸ);
    }
    if (result == nil) {
        result = ast.NewCompilerDiagnostic(diag.message, diag.args.ꓸꓸꓸ);
    }
    if (chain != nil) {
        result.SetMessageChain(chain);
    }
    if (relatedInfo != nil) {
        result.SetRelatedInfo(relatedInfo);
    }
    return result;
}

} // end compilerᴛprocessingDiagnostic
