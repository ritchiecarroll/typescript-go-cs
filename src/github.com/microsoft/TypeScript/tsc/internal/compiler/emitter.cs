namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using binder = go.github.com.microsoft.TypeScript.tsc.@internal.binder_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using outputpaths = go.github.com.microsoft.TypeScript.tsc.@internal.outputpaths_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using sourcemap = go.github.com.microsoft.TypeScript.tsc.@internal.sourcemap_package;
using stringutil = go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package;
using tracing = go.github.com.microsoft.TypeScript.tsc.@internal.tracing_package;
using Δtransformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using declarations = go.github.com.microsoft.TypeScript.tsc.@internal.transformers.declarations_package;
using estransforms = go.github.com.microsoft.TypeScript.tsc.@internal.transformers.estransforms_package;
using inliners = go.github.com.microsoft.TypeScript.tsc.@internal.transformers.inliners_package;
using jsxtransforms = go.github.com.microsoft.TypeScript.tsc.@internal.transformers.jsxtransforms_package;
using moduletransforms = go.github.com.microsoft.TypeScript.tsc.@internal.transformers.moduletransforms_package;
using tstransforms = go.github.com.microsoft.TypeScript.tsc.@internal.transformers.tstransforms_package;
using tsoptions = go.github.com.microsoft.TypeScript.tsc.@internal.tsoptions_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.github.com.microsoft.TypeScript.tsc.@internal.transformers;
using spanmap = go.github.com.microsoft.TypeScript.tsc.@internal.spanmap_package;
using static compiler_package;

partial class compiler_package {

partial struct EmitOnly /*num:byte*/;

public static EmitOnly EmitAll => /* iota */ 0;
public static EmitOnly EmitOnlyJs => 1;
public static EmitOnly EmitOnlyDts => 2;
public static EmitOnly EmitOnlyBuilderSignature => 3;

partial struct emitter {
    internal EmitHost host;
    internal EmitOnly emitOnly;
    internal ast.DiagnosticsCollection emitterDiagnostics;
    internal printer.EmitTextWriter writer;
    internal ж<outputpaths.OutputPaths> paths;
    internal ж<ast.SourceFile> sourceFile;
    internal EmitResult emitResult;
    internal bool forceEmit;
    internal Func<@string, @string, ж<WriteFileData>, error> writeFile;
    internal ж<tracing.Tracing> tr;
}

} partial class compilerᴛemitter {

internal static void emit(this ж<emitter> Ꮡe) {
    GoFrame ᒐ = default;
    try {
        ref var e = ref Ꮡe.DerefOrNull();

        if (e.tr != nil) {
            defer(Ꮡe.Value.tr.Push(tracing.PhaseEmit, "emit", new map<@string, any>{["path"] = ((@string)Ꮡe.Value.sourceFile.Path())}, true), ref ᒐ);
        }
        Ꮡe.emitJSFile(e.sourceFile, e.paths.JsFilePath(), e.paths.SourceMapFilePath());
        Ꮡe.emitDeclarationFile(e.sourceFile, e.paths.DeclarationFilePath(), e.paths.DeclarationMapPath());
        e.emitResult.Diagnostics = Ꮡe.ᏑemitterDiagnostics.GetDiagnostics();
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
}

} partial class compiler_package {

partial interface declarationTransformer {
    ж<ast.SourceFile> TransformSourceFile(ж<ast.SourceFile> sourceFile);
    slice<ж<ast.Diagnostic>> GetDiagnostics();
}

} partial class compilerᴛemitter {

internal static slice<declarationTransformer> getDeclarationTransformers(this ref emitter e, ж<printer.EmitContext> emitContext, ж<ast.SourceFile> sourceFile, @string declarationFilePath, @string declarationMapPath) {
    var forceDtsEmit = e.emitOnly == EmitOnlyBuilderSignature || e.forceEmit && e.emitOnly == EmitOnlyDts;
    return slice<declarationTransformer>([new declarations_DeclarationTransformerжdeclarationTransformer(declarations.NewDeclarationTransformer(e.host, emitContext, e.host.Options(), declarationFilePath, declarationMapPath)), new declarations_SupplementalReferencesTransformerжdeclarationTransformer(declarations.NewSupplementalReferencesTransformer(e.host, sourceFile, declarationFilePath, forceDtsEmit))
    ]);
}

internal static ж<ast.SourceFile> runScriptTransformers(this ж<emitter> Ꮡe, ж<printer.EmitContext> emitContext, ж<ast.SourceFile> ᏑsourceFile) {
    GoFrame ᒐ = default;
    try {
        ref var e = ref Ꮡe.DerefOrNull();
        ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

        if (e.tr != nil) {
            defer(Ꮡe.Value.tr.Push(tracing.PhaseEmit, "transformNodes", new map<@string, any>{["path"] = ((@string)ᏑsourceFile.Value.Path())}, false), ref ᒐ);
        }
        foreach (var (_, transformer) in range(getScriptTransformers(emitContext, e.host, ᏑsourceFile))) {
            ᏑsourceFile = transformer.TransformSourceFile(ᏑsourceFile); sourceFile = ref ᏑsourceFile.DerefOrNull();
        }
        return ᏑsourceFile;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static (ж<ast.SourceFile>, slice<ж<ast.Diagnostic>>) runDeclarationTransformers(this ж<emitter> Ꮡe, ж<printer.EmitContext> emitContext, ж<ast.SourceFile> ᏑsourceFile, @string declarationFilePath, @string declarationMapPath) {
    GoFrame ᒐ = default;
    try {
        ref var e = ref Ꮡe.DerefOrNull();
        ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

        if (e.tr != nil) {
            defer(Ꮡe.Value.tr.Push(tracing.PhaseEmit, "transformNodes", new map<@string, any>{["path"] = ((@string)ᏑsourceFile.Value.Path())}, false), ref ᒐ);
        }
        slice<ж<ast.Diagnostic>> diags = default;
        foreach (var (_, transformer) in range(e.getDeclarationTransformers(emitContext, ᏑsourceFile, declarationFilePath, declarationMapPath))) {
            ᏑsourceFile = transformer.TransformSourceFile(ᏑsourceFile); sourceFile = ref ᏑsourceFile.DerefOrNull();
            diags = appendꓸꓸꓸ(diags, transformer.GetDiagnostics());
        }
        return (ᏑsourceFile, diags);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

} partial class compiler_package {

internal static ж<Δtransformers.Transformer> getModuleTransformer(ж<Δtransformers.TransformOptions> Ꮡopts) {
    ref var opts = ref Ꮡopts.DerefOrNull();

    switch (opts.CompilerOptions.GetEmitModuleKind()) {
    case var ᴛ when ᴛ == core.ModuleKindPreserve: {
        return moduletransforms.NewESModuleTransformer(Ꮡopts);
    }
    case var ᴛ when ᴛ == core.ModuleKindESNext || ᴛ == core.ModuleKindES2022 || ᴛ == core.ModuleKindES2020 || ᴛ == core.ModuleKindES2015 || ᴛ == core.ModuleKindNode20 || ᴛ == core.ModuleKindNode18 || ᴛ == core.ModuleKindNode16 || ᴛ == core.ModuleKindNodeNext || ᴛ == core.ModuleKindCommonJS: {
        return moduletransforms.NewImpliedModuleTransformer(Ꮡopts);
    }
    default: {
        return moduletransforms.NewCommonJSModuleTransformer(Ꮡopts);
    }}

}

internal static slice<ж<Δtransformers.Transformer>> getScriptTransformers(ж<printer.EmitContext> emitContext, printer.EmitHost host, ж<ast.SourceFile> ᏑsourceFile) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    slice<ж<Δtransformers.Transformer>> tx = default;
    var options = host.Options();

    var importElisionEnabled = !options.VerbatimModuleSyntax.IsTrue() && !ast.IsInJSFile(ᏑsourceFile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode());
    var jsxTransformEnabled = options.GetJSXTransformEnabled() && sourceFile.LanguageVariant == core.LanguageVariantJSX;

    var emitResolver = host.GetEmitResolver();

    binder.ReferenceResolver referenceResolver = default;
    if (importElisionEnabled || jsxTransformEnabled || !options.GetIsolatedModules() || options.EmitDecoratorMetadata.IsTrue()) {
        referenceResolver = emitResolver;
    } else {
        referenceResolver = binder.NewReferenceResolver(options, new binder.ReferenceResolverHooks());
    }

    ref var opts = ref heap<Δtransformers.TransformOptions>(new Δtransformers.TransformOptions(
        Context: emitContext,
        CompilerOptions: options,
        Resolver: referenceResolver,
        EmitResolver: emitResolver,
        GetEmitModuleFormatOfFile: host.GetEmitModuleFormatOfFile
    ), out var Ꮡopts);

    {
        if (options.EmitDecoratorMetadata.IsTrue()) {
            tx = append(tx, tstransforms.NewMetadataTransformer(Ꮡopts));
        }

        tx = append(tx, tstransforms.NewTypeEraserTransformer(Ꮡopts));

        if (importElisionEnabled) {
            tx = append(tx, tstransforms.NewImportElisionTransformer(Ꮡopts));
        }

        tx = append(tx, tstransforms.NewRuntimeSyntaxTransformer(Ꮡopts));

        if (options.ExperimentalDecorators.IsTrue()) {
            tx = append(tx, tstransforms.NewLegacyDecoratorsTransformer(Ꮡopts));
        }
    }

    if (jsxTransformEnabled) {
        tx = append(tx, jsxtransforms.NewJSXTransformer(Ꮡopts));
    }

    var downleveler = estransforms.GetESTransformer(Ꮡopts);
    if (downleveler != nil) {
        tx = append(tx, downleveler);
    }

    tx = append(tx, estransforms.NewUseStrictTransformer(Ꮡopts));

    tx = append(tx, getModuleTransformer(Ꮡopts));

    if (!options.GetIsolatedModules()) {
        tx = append(tx, inliners.NewConstEnumInliningTransformer(Ꮡopts));
    }
    return tx;
}

} partial class compilerᴛemitter {

internal static void emitJSFile(this ж<emitter> Ꮡe, ж<ast.SourceFile> ᏑsourceFile, @string jsFilePath, @string sourceMapFilePath) {
    GoFrame ᒐ = default;
    try {
        ref var e = ref Ꮡe.DerefOrNull();
        ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

        var options = e.host.Options();

        if (ᏑsourceFile == nil || e.emitOnly != EmitAll && e.emitOnly != EmitOnlyJs || len(jsFilePath) == 0) {
            return;
        }

        if (!e.forceEmit && (options.NoEmit == core.TSTrue || e.host.IsEmitBlocked(jsFilePath))) {
            e.emitResult.EmitSkipped = true;
            return;
        }

        if (e.tr != nil) {
            defer(Ꮡe.Value.tr.Push(tracing.PhaseEmit, "emitJsFileOrBundle", new map<@string, any>{["jsFilePath"] = jsFilePath}, true), ref ᒐ);
        }

        var (emitContext, putEmitContext) = printer.GetEmitContext();
        var putEmitContextʗ1 = putEmitContext;
        defer(putEmitContextʗ1, ref ᒐ);

        ᏑsourceFile = Ꮡe.runScriptTransformers(emitContext, ᏑsourceFile); sourceFile = ref ᏑsourceFile.DerefOrNull();

        var printerOptions = new printer.PrinterOptions(
            RemoveComments: options.RemoveComments.IsTrue(),
            NewLine: options.NewLine,
            NoEmitHelpers: options.NoEmitHelpers.IsTrue(),
            SourceMap: options.SourceMap.IsTrue(),
            InlineSourceMap: options.InlineSourceMap.IsTrue(),
            InlineSources: options.InlineSources.IsTrue(),
            Target: options.Target
        );

        var printerΔ1 = printer.NewPrinter(printerOptions, new printer.PrintHandlers(), emitContext);

        Ꮡe.printSourceFile(jsFilePath, sourceMapFilePath, ᏑsourceFile, printerΔ1, options, shouldEmitSourceMaps(ref (options).DerefOrNull(), ᏑsourceFile));
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
}

internal static void emitDeclarationFile(this ж<emitter> Ꮡe, ж<ast.SourceFile> ᏑsourceFile, @string declarationFilePath, @string declarationMapPath) {
    GoFrame ᒐ = default;
    try {
        ref var e = ref Ꮡe.DerefOrNull();
        ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

        var options = e.host.Options();

        if (ᏑsourceFile == nil || e.emitOnly == EmitOnlyJs || len(declarationFilePath) == 0) {
            return;
        }
        var emitDeclarationMap = e.emitOnly != EmitOnlyBuilderSignature && options.DeclarationMap.IsTrue();
        var contentMappedSource = ᏑsourceFile;

        if (e.tr != nil) {
            defer(Ꮡe.Value.tr.Push(tracing.PhaseEmit, "emitDeclarationFileOrBundle", new map<@string, any>{["declarationFilePath"] = declarationFilePath}, true), ref ᒐ);
        }

        var (emitContext, putEmitContext) = printer.GetEmitContext();
        var putEmitContextʗ1 = putEmitContext;
        defer(putEmitContextʗ1, ref ᒐ);
        (ᏑsourceFile, var diags) = Ꮡe.runDeclarationTransformers(emitContext, ᏑsourceFile, declarationFilePath, declarationMapPath); sourceFile = ref ᏑsourceFile.DerefOrNull();

        foreach (var (_, elem) in range(diags)) {
            Ꮡe.ᏑemitterDiagnostics.Add(elem);
        }

        if (!e.forceEmit && e.emitOnly != EmitOnlyBuilderSignature && (options.NoEmit == core.TSTrue || e.host.IsEmitBlocked(declarationFilePath))) {
            e.emitResult.EmitSkipped = true;
            return;
        }

        var declBlocked = len(diags) > 0 && !e.forceEmit && e.emitOnly != EmitOnlyBuilderSignature;
        if (declBlocked) {
            e.emitResult.EmitSkipped = true;
            return;
        }

        var printerOptions = new printer.PrinterOptions(
            RemoveComments: options.RemoveComments.IsTrue(),
            NewLine: options.NewLine,
            NoEmitHelpers: true,
            Target: options.GetEmitScriptTarget(),
            SourceMap: emitDeclarationMap,
            InlineSourceMap: options.InlineSourceMap.IsTrue(),
            OnlyPrintJSDocStyle: true,
            OmitBraceSourceMapPositions: true
        );

        var printHandlers = new printer.PrintHandlers();
        {
            var spanMap = contentMappedSource.SpanMap(); if (emitDeclarationMap && spanMap != nil) {
                var originalSource = newDeclarationMapSource(contentMappedSource);
                var contentMappedSourceʗ1 = contentMappedSource;
                var originalSourceʗ1 = originalSource;
                var spanMapʗ1 = spanMap;
                printHandlers.MapSourcePosition = (sourcemap.Source, nint, bool) (sourcemap.Source source, nint pos) => {
                    if (source.FileName() != contentMappedSourceʗ1.FileName()) {
                        return (source, pos, true);
                    }
                    var (mapped, ok) = spanMapʗ1.VirtualToOriginalPositionExact((core.TextPos)(int32)pos);
                    if (!ok) {
                        return (default, 0, false);
                    }
                    return (new declarationMapSourceжSource(originalSourceʗ1), (nint)mapped, true);
                };
            }
        }
        var printerΔ1 = printer.NewPrinter(printerOptions, printHandlers, emitContext);

        var declarationMapOptions = Ꮡ(new core.CompilerOptions(
            SourceMap: core.IfElse(emitDeclarationMap, core.TSTrue, core.TSFalse),
            SourceRoot: options.SourceRoot,
            MapRoot: options.MapRoot
        ));
        Ꮡe.printSourceFile(declarationFilePath, declarationMapPath, ᏑsourceFile, printerΔ1, declarationMapOptions, shouldEmitSourceMaps(ref (declarationMapOptions).DerefOrNull(), ᏑsourceFile));
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
    finally { ᒐ.Run(); }
}

} partial class compiler_package {

partial struct declarationMapSource {
    internal @string fileName;
    internal @string text;
    internal slice<core.TextPos> lineMap;
}

internal static ж<declarationMapSource> newDeclarationMapSource(ж<ast.SourceFile> ᏑsourceFile) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    ref var text = ref heap<@string>(sourceFile.OriginalText(), out var Ꮡtext);
    return Ꮡ(new declarationMapSource(
        fileName: sourceFile.OriginalFileName(),
        text: text,
        lineMap: ((slice<core.TextPos>)core.ComputeECMALineStarts(text))
    ));
}

} partial class compilerᴛdeclarationMapSource {

internal static @string FileName(this ref declarationMapSource s) { return s.fileName; }
internal static @string Text(this ref declarationMapSource s) { return s.text; }
internal static slice<core.TextPos> ECMALineMap(this ref declarationMapSource s) { return s.lineMap; }

} partial class compilerᴛemitter {

internal static void printSourceFile(this ж<emitter> Ꮡe, @string jsFilePath, @string sourceMapFilePath, ж<ast.SourceFile> ᏑsourceFile, ж<printer.Printer> printer_, ж<core.CompilerOptions> ᏑmapOptions, bool shouldEmitSourceMaps) {
    ref var e = ref Ꮡe.DerefOrNull();
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();
    ref var mapOptions = ref ᏑmapOptions.DerefOrNull();

    var options = e.host.Options();
    ж<sourcemap.Generator> sourceMapGenerator = default;
    if (shouldEmitSourceMaps) {
        sourceMapGenerator = sourcemap.NewGenerator(
            tspath.GetBaseFileName(tspath.NormalizeSlashes(jsFilePath)),
            getSourceRoot(ref (ᏑmapOptions).DerefOrNull()),
            e.getSourceMapDirectory(ᏑmapOptions, jsFilePath, ᏑsourceFile),
            new tspath.ComparePathsOptions(
                UseCaseSensitiveFileNames: e.host.UseCaseSensitiveFileNames(),
                CurrentDirectory: e.host.GetCurrentDirectory()
            ));
    }

    printer_.Write(ᏑsourceFile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode(), ᏑsourceFile, e.writer, sourceMapGenerator);

    ref var sourceMapUrlPos = ref heap<nint>(-1, out var ᏑsourceMapUrlPos);
    if (sourceMapGenerator != nil) {
        if (mapOptions.SourceMap.IsTrue() || mapOptions.InlineSourceMap.IsTrue()) {
            e.emitResult.SourceMaps = append(e.emitResult.SourceMaps, Ꮡ(new SourceMapEmitResult(
                InputSourceFileNames: sourceMapGenerator.Sources(),
                SourceMap: sourceMapGenerator.RawSourceMap(),
                GeneratedFile: jsFilePath
            )));
        }

        @string sourceMappingURL = e.getSourceMappingURL(
            ᏑmapOptions,
            sourceMapGenerator,
            jsFilePath,
            sourceMapFilePath,
            ᏑsourceFile);

        if (len(sourceMappingURL) > 0) {
            if (!e.writer.IsAtStartOfLine()) {
                e.writer.RawWrite(core.IfElse(options.NewLine == core.NewLineKindCRLF, (@string)"\r\n", (@string)"\n"));
            }
            sourceMapUrlPos = e.writer.GetTextPos();
            e.writer.WriteComment("//# sourceMappingURL=");
            e.writer.WriteComment(sourceMappingURL);
        }

        if (len(sourceMapFilePath) > 0) {
            @string sourceMap = sourceMapGenerator.String();
            var errΔ1 = e.writeText(sourceMapFilePath, sourceMap, Ꮡ(new WriteFileData(SourceFile: e.sourceFile)));
            if (errΔ1 != nil) {
                Ꮡe.ᏑemitterDiagnostics.Add(ast.NewCompilerDiagnostic(diagnostics.Could_not_write_file_0_Colon_1, jsFilePath, errΔ1.Error()));
            } else {
                e.emitResult.EmittedFiles = append(e.emitResult.EmittedFiles, sourceMapFilePath);
            }
        }
    } else {
        e.writer.WriteLine();
    }

    @string text = e.writer.String();
    if (options.EmitBOM.IsTrue()) {
        text = stringutil.AddUTF8ByteOrderMark(text);
    }
    var data = Ꮡ(new WriteFileData(
        SourceMapUrlPos: sourceMapUrlPos,
        Diagnostics: Ꮡe.ᏑemitterDiagnostics.GetDiagnostics(),
        SourceFile: e.sourceFile
    ));
    var err = e.writeText(jsFilePath, text, data);
    var skippedDtsWrite = data.SkippedDtsWrite;
    if (err != nil) {
        Ꮡe.ᏑemitterDiagnostics.Add(ast.NewCompilerDiagnostic(diagnostics.Could_not_write_file_0_Colon_1, jsFilePath, err.Error()));
    } else if (!skippedDtsWrite) {
        e.emitResult.EmittedFiles = append(e.emitResult.EmittedFiles, jsFilePath);
    }

    e.writer.Clear();
}

internal static error writeText(this ref emitter e, @string fileName, @string text, ж<WriteFileData> data) {
    if (e.writeFile != nil) {
        return e.writeFile(fileName, text, data);
    }
    return e.host.WriteFile(fileName, text);
}

} partial class compiler_package {

internal static bool shouldEmitSourceMaps(ref core.CompilerOptions mapOptions, ж<ast.SourceFile> ᏑsourceFile) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    return (mapOptions.SourceMap.IsTrue() || mapOptions.InlineSourceMap.IsTrue()) && !tspath.FileExtensionIs(sourceFile.FileName(), tspath.ExtensionJson);
}

internal static @string getSourceRoot(ref core.CompilerOptions mapOptions) {
    @string sourceRoot = tspath.NormalizeSlashes(mapOptions.SourceRoot);
    if (len(sourceRoot) > 0) {
        sourceRoot = tspath.EnsureTrailingDirectorySeparator(sourceRoot);
    }
    return sourceRoot;
}

} partial class compilerᴛemitter {

internal static @string getSourceMapDirectory(this ref emitter e, ж<core.CompilerOptions> ᏑmapOptions, @string filePath, ж<ast.SourceFile> ᏑsourceFile) {
    ref var mapOptions = ref ᏑmapOptions.DerefOrNull();
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    if (len(mapOptions.SourceRoot) > 0) {
        return e.host.CommonSourceDirectory();
    }
    if (len(mapOptions.MapRoot) > 0) {
        @string sourceMapDir = tspath.NormalizeSlashes(mapOptions.MapRoot);
        if (ᏑsourceFile != nil) {
            sourceMapDir = tspath.GetDirectoryPath(outputpaths.GetSourceFilePathInNewDir(
                sourceFile.FileName(),
                sourceMapDir,
                e.host.GetCurrentDirectory(),
                e.host.CommonSourceDirectory(),
                e.host.UseCaseSensitiveFileNames()));
        }
        if (tspath.GetRootLength(sourceMapDir) == 0) {
            sourceMapDir = tspath.CombinePaths(e.host.CommonSourceDirectory(), sourceMapDir);
        }
        return sourceMapDir;
    }
    return tspath.GetDirectoryPath(tspath.NormalizePath(filePath));
}

internal static @string getSourceMappingURL(this ref emitter e, ж<core.CompilerOptions> ᏑmapOptions, ж<sourcemap.Generator> sourceMapGenerator, @string filePath, @string sourceMapFilePath, ж<ast.SourceFile> ᏑsourceFile) {
    ref var mapOptions = ref ᏑmapOptions.DerefOrNull();
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    if (mapOptions.InlineSourceMap.IsTrue()) {
        return sourceMapGenerator.Base64DataURL();
    }

    @string sourceMapFile = tspath.GetBaseFileName(tspath.NormalizeSlashes(sourceMapFilePath));
    if (len(mapOptions.MapRoot) > 0) {
        @string sourceMapDir = tspath.NormalizeSlashes(mapOptions.MapRoot);
        if (ᏑsourceFile != nil) {
            sourceMapDir = tspath.GetDirectoryPath(outputpaths.GetSourceFilePathInNewDir(
                sourceFile.FileName(),
                sourceMapDir,
                e.host.GetCurrentDirectory(),
                e.host.CommonSourceDirectory(),
                e.host.UseCaseSensitiveFileNames()));
        }
        if (tspath.GetRootLength(sourceMapDir) == 0) {
            sourceMapDir = tspath.CombinePaths(e.host.CommonSourceDirectory(), sourceMapDir);
            return stringutil.EncodeURI(
                tspath.GetRelativePathToDirectoryOrUrl(
                    tspath.GetDirectoryPath(tspath.NormalizePath(filePath)),
                    tspath.CombinePaths(sourceMapDir, sourceMapFile),
                    true,
                    new tspath.ComparePathsOptions(
                        UseCaseSensitiveFileNames: e.host.UseCaseSensitiveFileNames(),
                        CurrentDirectory: e.host.GetCurrentDirectory()
                    )));
        } else {
            return stringutil.EncodeURI(tspath.CombinePaths(sourceMapDir, sourceMapFile));
        }
    }
    return stringutil.EncodeURI(sourceMapFile);
}

} partial class compiler_package {

partial interface SourceFileMayBeEmittedHost {
    ж<core.CompilerOptions> Options();
    ж<tsoptions.SourceOutputAndProjectReference> GetProjectReferenceFromSource(tspath.Path path);
    bool IsSourceFileFromExternalLibrary(ж<ast.SourceFile> @file);
    @string GetCurrentDirectory();
    bool UseCaseSensitiveFileNames();
    slice<ж<ast.SourceFile>> SourceFiles();
}

internal static bool sourceFileMayBeEmitted(ж<ast.SourceFile> ᏑsourceFile, SourceFileMayBeEmittedHost host, bool forceDtsEmit, bool forceJsEmit) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    var options = host.Options();
    if (!forceJsEmit && options.NoEmitForJsFiles.IsTrue() && ast.IsSourceFileJS(ᏑsourceFile)) {
        return false;
    }

    if (sourceFile.IsDeclarationFile) {
        return false;
    }

    if (sourceFile.ContentMapper() != ""u8 && !forceDtsEmit && !options.GetEmitDeclarations()) {
        return false;
    }

    if (host.IsSourceFileFromExternalLibrary(ᏑsourceFile)) {
        return false;
    }

    if (forceDtsEmit || forceJsEmit) {
        return true;
    }

    if (host.GetProjectReferenceFromSource(sourceFile.Path()) != nil) {
        return false;
    }

    if (!ast.IsJsonSourceFile(ᏑsourceFile)) {
        return true;
    }

    if (options.OutDir == ""u8) {
        return false;
    }

    if (options.RootDir != ""u8 || options.ConfigFilePath != ""u8) {
        @string commonDir = tspath.GetNormalizedAbsolutePath(outputpaths.GetCommonSourceDirectory(options, () => default, host.GetCurrentDirectory(), host.UseCaseSensitiveFileNames(), default), host.GetCurrentDirectory());
        @string outputPath = outputpaths.GetSourceFilePathInNewDirWorker(sourceFile.FileName(), options.OutDir, host.GetCurrentDirectory(), commonDir, host.UseCaseSensitiveFileNames());
        if (tspath.ComparePaths(sourceFile.FileName(), outputPath, new tspath.ComparePathsOptions(
            UseCaseSensitiveFileNames: host.UseCaseSensitiveFileNames(),
            CurrentDirectory: host.GetCurrentDirectory()
        )) == 0) {
            return false;
        }
    }

    return true;
}

internal static slice<ж<ast.SourceFile>> getSourceFilesToEmit(SourceFileMayBeEmittedHost host, slice<ж<ast.SourceFile>> targetSourceFiles, bool forceDtsEmit, bool forceJsEmit) {
    if (targetSourceFiles == nil) {
        targetSourceFiles = host.SourceFiles();
    }
    return core.Filter(targetSourceFiles, (ж<ast.SourceFile> sourceFile) => sourceFileMayBeEmitted(sourceFile, host, forceDtsEmit, forceJsEmit));
}

internal static bool isSourceFileNotJson(ж<ast.SourceFile> @file) {
    return !ast.IsJsonSourceFile(@file);
}

internal static slice<ж<ast.Diagnostic>> getDeclarationDiagnostics(EmitHost host, ж<ast.SourceFile> @file) {
    var fullFiles = core.Filter<ж<ast.SourceFile>>(getSourceFilesToEmit(new EmitHostᴠSourceFileMayBeEmittedHost(host), core.SingleElementSlice(@file), false, false), isSourceFileNotJson);
    if (!core.Some(fullFiles, (ж<ast.SourceFile> f) => f == @file)) {
        return [];
    }
    var options = host.Options();
    var transform = declarations.NewDeclarationTransformer(host, nil, options, "", "");
    transform.of(declarations.DeclarationTransformer.ᏑTransformer).TransformSourceFile(@file);
    return transform.GetDiagnostics();
}

} // end compiler_package
