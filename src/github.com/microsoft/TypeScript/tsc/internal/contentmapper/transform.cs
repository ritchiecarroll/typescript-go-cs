namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using slices = slices_package;
using strconv = strconv_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using parser = go.github.com.microsoft.TypeScript.tsc.@internal.parser_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using spanmap = go.github.com.microsoft.TypeScript.tsc.@internal.spanmap_package;

partial class contentmapper_package {

partial struct SourceFiles {
    public ж<ast.SourceFile> Canonical;
    public slice<ж<ast.SourceFile>> Supplemental;
}

public static (SourceFiles, error) TransformAndParse(ast.SourceFileParseOptions parseOptions, @string content, ж<Mapper> mapper, Project project) {
    var (transformIdentity, err) = project.Identity(mapper);
    if (err != nil) {
        return (new SourceFiles(), new TransformErrorжerror(NewTransformError(TransformErrorKindProject, err)));
    }
    (var result, err) = project.Transform(mapper, new Request(
        FileName: parseOptions.FileName,
        Content: content
    ));
    if (err != nil) {
        return (new SourceFiles(), err);
    }
    return ParseResult(parseOptions, content, mapper, transformIdentity, result);
}

public static (SourceFiles, error) ParseResult(ast.SourceFileParseOptions parseOptions, @string content, ж<Mapper> Ꮡmapper, @string transformIdentity, Result result) {
    ref var mapper = ref Ꮡmapper.DerefOrNull();

    if (result.Mappings == nil) {
        return (new SourceFiles(), new TransformErrorжerror(NewTransformError(TransformErrorKindMappings, default)));
    }
    {
        var problem = result.Mappings.Validate(result.Text, content); if (problem != nil) {
            return (new SourceFiles(), new spanmap_MappingErrorжerror(problem));
        }
    }
    @string virtualExtension = result.VirtualExtension;
    if (!IsSupportedVirtualExtension(virtualExtension)) {
        return (new SourceFiles(), new TransformErrorжerror(NewTransformError(TransformErrorKindResponse, default)));
    }
    var baseParseOptions = parseOptions;
    @string virtualFileName = baseParseOptions.FileName + virtualExtension;
    parseOptions = baseParseOptions;
    if (isModuleVirtualExtension(virtualExtension)) {
        parseOptions.ExternalModuleIndicatorOptions.Force = true;
    }
    var sourceFile = parser.ParseSourceFile(parseOptions, result.Text, core.GetScriptKindFromFileName(virtualFileName));
    if (len(result.Diagnostics) > 0) {
        foreach (var (_, diagnostic) in range(result.Diagnostics)) {
            diagnostic.SetFile(sourceFile);
        }
        sourceFile.SetDiagnostics(appendꓸꓸꓸ(sourceFile.Diagnostics(), result.Diagnostics));
    }
    var files = new SourceFiles(Canonical: sourceFile, Supplemental: new slice<ж<ast.SourceFile>>(0, len(result.Supplemental)));
    foreach (var (i, supplemental) in range(result.Supplemental)) {
        if (supplemental.Mappings == nil) {
            return (new SourceFiles(), new TransformErrorжerror(NewTransformError(TransformErrorKindMappings, default)));
        }
        var problem = supplemental.Mappings.Validate(supplemental.Text, content); if (problem != nil) {
            return (new SourceFiles(), new spanmap_MappingErrorжerror(problem));
        }
        var supplementalOptions = baseParseOptions;
        if (!IsSupportedVirtualExtension(supplemental.VirtualExtension)) {
            return (new SourceFiles(), new TransformErrorжerror(NewTransformError(TransformErrorKindResponse, default)));
        }
        @string suffix = "."u8 + strconv.Itoa(i) + supplemental.VirtualExtension;
        supplementalOptions.FileName += suffix;
        supplementalOptions.Path = (tspath.Path)(((@string)parseOptions.Path) + suffix);
        if (isModuleVirtualExtension(supplemental.VirtualExtension)) {
            supplementalOptions.ExternalModuleIndicatorOptions.Force = true;
        }

        var @file = parser.ParseSourceFile(supplementalOptions, supplemental.Text, core.GetScriptKindFromFileName(supplementalOptions.FileName));

        files.Supplemental = append(files.Supplemental, @file);
    }
    @string mapperIdentity = mapper.Identity();
    sourceFile.SetContentMapperInfo(new ast.ContentMapperSourceFileInfo(
        ContentMapper: mapperIdentity,
        TransformIdentity: transformIdentity,
        ParseOptions: baseParseOptions,
        VirtualFileName: virtualFileName,
        OriginalText: content,
        SpanMap: result.Mappings,
        DiagnosticDirectives: result.DiagnosticDirectives,
        SupplementalSourceFiles: files.Supplemental
    ));
    foreach (var (i, @file) in range(files.Supplemental)) {
        var supplemental = result.Supplemental[i];
        @file.SetContentMapperInfo(new ast.ContentMapperSourceFileInfo(
            ContentMapper: mapperIdentity,
            TransformIdentity: transformIdentity,
            ParseOptions: baseParseOptions,
            VirtualFileName: @file.FileName(),
            OriginalText: content,
            SpanMap: supplemental.Mappings,
            DiagnosticDirectives: supplemental.DiagnosticDirectives,
            CanonicalSourceFile: sourceFile
        ));
    }
    return (files, default);
}

internal static bool isModuleVirtualExtension(@string extension) {
    return slices.Contains(slice<@string>([tspath.ExtensionMts, tspath.ExtensionCts, tspath.ExtensionMjs, tspath.ExtensionCjs]), extension);
}

public static error CheckSupplementalFileNameCollisions(SourceFiles files, Func<@string, bool> fileExists) {
    foreach (var (_, @file) in range(files.Supplemental)) {
        if (fileExists(@file.FileName())) {
            return new SupplementalFileCollisionErrorжerror(Ꮡ(new SupplementalFileCollisionError(FileName: @file.FileName())));
        }
    }
    return default;
}

} // end contentmapper_package
