namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static outputpaths_package;

partial class outputpaths_package {

partial interface OutputPathsHost {
    @string CommonSourceDirectory();
    slice<@string> ContentMapperExtensions();
    @string GetCurrentDirectory();
    bool UseCaseSensitiveFileNames();
}

partial struct OutputPaths {
    internal @string jsFilePath;
    internal @string sourceMapFilePath;
    internal @string declarationFilePath;
    internal @string declarationMapPath;
}

} partial class outputpathsᴛOutputPaths {

public static @string DeclarationFilePath(this ref OutputPaths o) {
    return o.declarationFilePath;
}

public static @string JsFilePath(this ref OutputPaths o) {
    return o.jsFilePath;
}

public static @string SourceMapFilePath(this ref OutputPaths o) {
    return o.sourceMapFilePath;
}

public static @string DeclarationMapPath(this ref OutputPaths o) {
    return o.declarationMapPath;
}

} partial class outputpaths_package {

partial struct ForceEmitPaths {
    public bool Dts;
    public bool Js;
    public bool DeclarationMap;
}

public static ж<OutputPaths> GetOutputPathsFor(ж<ast.SourceFile> ᏑsourceFile, ж<core.CompilerOptions> Ꮡoptions, OutputPathsHost host, ForceEmitPaths force) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();
    ref var options = ref Ꮡoptions.DerefOrNull();

    @string ownOutputFilePath = getOwnEmitOutputFilePath(sourceFile.FileName(), ref (Ꮡoptions).DerefOrNull(), host, GetOutputExtension(sourceFile.FileName(), options.Jsx));
    var isJsonFile = ast.IsJsonSourceFile(ᏑsourceFile);
    var isJsonEmittedToSameLocation = isJsonFile && tspath.ComparePaths(sourceFile.FileName(), ownOutputFilePath, new tspath.ComparePathsOptions(
        CurrentDirectory: host.GetCurrentDirectory(),
        UseCaseSensitiveFileNames: host.UseCaseSensitiveFileNames()
    )) == 0;
    var paths = Ꮡ(new OutputPaths());
    if (sourceFile.ContentMapper() == ""u8 && (force.Js || options.EmitDeclarationOnly != core.TSTrue) && !isJsonEmittedToSameLocation) {
        paths.jsFilePath = ownOutputFilePath;
        if (!ast.IsJsonSourceFile(ᏑsourceFile)) {
            paths.sourceMapFilePath = GetSourceMapFilePath(paths.jsFilePath, Ꮡoptions);
        }
    }
    if (force.Dts || options.GetEmitDeclarations() && !isJsonFile) {
        paths.declarationFilePath = GetDeclarationEmitOutputFilePath(sourceFile.FileName(), Ꮡoptions, host);
        if (options.GetAreDeclarationMapsEnabled() || force.DeclarationMap && options.DeclarationMap.IsTrue()) {
            paths.declarationMapPath = paths.declarationFilePath + ".map"u8;
        }
    }
    return paths;
}

public static bool ForEachEmittedFile(OutputPathsHost host, ж<core.CompilerOptions> options, Func<ж<OutputPaths>, ж<ast.SourceFile>, bool> action, slice<ж<ast.SourceFile>> sourceFiles, bool forceDtsEmit) {
    foreach (var (_, sourceFile) in range(sourceFiles)) {
        if (action(GetOutputPathsFor(sourceFile, options, host, new ForceEmitPaths(Dts: forceDtsEmit)), sourceFile)) {
            return true;
        }
    }
    return false;
}

public static @string GetOutputJSFileName(@string inputFileName, ж<core.CompilerOptions> Ꮡoptions, OutputPathsHost host) {
    ref var options = ref Ꮡoptions.DerefOrNull();

    if (options.EmitDeclarationOnly.IsTrue() || isContentMappedFileName(inputFileName, host)) {
        return "";
    }
    @string outputFileName = GetOutputJSFileNameWorker(inputFileName, Ꮡoptions, host);
    if (!tspath.FileExtensionIs(outputFileName, tspath.ExtensionJson) || tspath.ComparePaths(inputFileName, outputFileName, new tspath.ComparePathsOptions(
        CurrentDirectory: host.GetCurrentDirectory(),
        UseCaseSensitiveFileNames: host.UseCaseSensitiveFileNames()
    )) != 0) {
        return outputFileName;
    }

    return "";
}

internal static bool isContentMappedFileName(@string fileName, OutputPathsHost host) {
    return tspath.GetLongestExtensionFromPath(fileName, host.ContentMapperExtensions(), !host.UseCaseSensitiveFileNames()) != ""u8;
}

public static @string GetOutputJSFileNameWorker(@string inputFileName, ж<core.CompilerOptions> Ꮡoptions, OutputPathsHost host) {
    ref var options = ref Ꮡoptions.DerefOrNull();

    return tspath.ChangeExtension(
        getOutputPathWithoutChangingExtension(inputFileName, options.OutDir, host),
        GetOutputExtension(inputFileName, options.Jsx));
}

public static @string GetOutputDeclarationFileNameWorker(@string inputFileName, ж<core.CompilerOptions> Ꮡoptions, OutputPathsHost host) {
    ref var options = ref Ꮡoptions.DerefOrNull();

    @string dir = options.DeclarationDir;
    if (len(dir) == 0) {
        dir = options.OutDir;
    }
    return ChangeToDeclarationExtension(getOutputPathWithoutChangingExtension(inputFileName, dir, host), host);
}

public static @string GetOutputExtension(@string fileName, core.JsxEmit jsx) {
    switch (ᐧ) {
    case {} when tspath.FileExtensionIs(fileName, tspath.ExtensionJson): {
        return tspath.ExtensionJson;
    }
    case {} when jsx == core.JsxEmitPreserve && tspath.FileExtensionIsOneOf(fileName, [tspath.ExtensionJsx, tspath.ExtensionTsx]): {
        return tspath.ExtensionJsx;
    }
    case {} when tspath.FileExtensionIsOneOf(fileName, [tspath.ExtensionMts, tspath.ExtensionMjs]): {
        return tspath.ExtensionMjs;
    }
    case {} when tspath.FileExtensionIsOneOf(fileName, [tspath.ExtensionCts, tspath.ExtensionCjs]): {
        return tspath.ExtensionCjs;
    }
    default: {
        return tspath.ExtensionJs;
    }}

}

public static @string GetDeclarationEmitOutputFilePath(@string @file, ж<core.CompilerOptions> Ꮡoptions, OutputPathsHost host) {
    ref var options = ref Ꮡoptions.DerefOrNull();

    ж<@string> outputDir = default;
    if (len(options.DeclarationDir) > 0) {
        outputDir = Ꮡoptions.ᏑDeclarationDir;
    } else if (len(options.OutDir) > 0) {
        outputDir = Ꮡoptions.ᏑOutDir;
    }

    @string path = default;
    if (outputDir != nil) {
        path = GetSourceFilePathInNewDirWorker(@file, outputDir.Value, host.GetCurrentDirectory(), host.CommonSourceDirectory(), host.UseCaseSensitiveFileNames());
    } else {
        path = @file;
    }
    return ChangeToDeclarationExtension(path, host);
}

public static @string ChangeToDeclarationExtension(@string path, OutputPathsHost host) {
    {
        @string extension = tspath.GetLongestExtensionFromPath(path, host.ContentMapperExtensions(), false); if (extension != ""u8) {
            return tspath.RemoveExtension(path, extension) + ".d"u8 + extension + ".ts"u8;
        }
    }
    @string pathWithoutExtension = tspath.RemoveFileExtension(path);
    if (pathWithoutExtension == path) {
        @string extension = tspath.GetAnyExtensionFromPath(path, default, false); if (extension != ""u8) {
            pathWithoutExtension = tspath.RemoveExtension(path, extension);
        }
    }
    return pathWithoutExtension + tspath.GetDeclarationEmitExtensionForPath(path);
}

public static @string GetSourceFilePathInNewDir(@string fileName, @string newDirPath, @string currentDirectory, @string commonSourceDirectory, bool useCaseSensitiveFileNames) {
    return GetSourceFilePathInNewDirWorker(fileName, newDirPath, currentDirectory, commonSourceDirectory, useCaseSensitiveFileNames);
}

internal static @string getOutputPathWithoutChangingExtension(@string inputFileName, @string outputDirectory, OutputPathsHost host) {
    if (len(outputDirectory) > 0) {
        return tspath.ResolvePath(outputDirectory, tspath.GetRelativePathFromDirectory(host.CommonSourceDirectory(), inputFileName, new tspath.ComparePathsOptions(
            UseCaseSensitiveFileNames: host.UseCaseSensitiveFileNames(),
            CurrentDirectory: host.GetCurrentDirectory()
        )));
    }
    return inputFileName;
}

public static @string GetSourceFilePathInNewDirWorker(@string fileName, @string newDirPath, @string currentDirectory, @string commonSourceDirectory, bool useCaseSensitiveFileNames) {
    @string sourceFilePath = tspath.GetNormalizedAbsolutePath(fileName, currentDirectory);
    var (trimmed, ok) = tspath.TrimFilePathPrefix(sourceFilePath, commonSourceDirectory, useCaseSensitiveFileNames); if (ok) {
        sourceFilePath = trimmed;
    }
    return tspath.CombinePaths(newDirPath, sourceFilePath);
}

internal static @string getOwnEmitOutputFilePath(@string fileName, ref core.CompilerOptions options, OutputPathsHost host, @string extension) {
    @string emitOutputFilePathWithoutExtension = default;
    if (len(options.OutDir) > 0) {
        @string currentDirectory = host.GetCurrentDirectory();
        emitOutputFilePathWithoutExtension = tspath.RemoveFileExtension(GetSourceFilePathInNewDir(
            fileName,
            options.OutDir,
            currentDirectory,
            host.CommonSourceDirectory(),
            host.UseCaseSensitiveFileNames()));
    } else {
        emitOutputFilePathWithoutExtension = tspath.RemoveFileExtension(fileName);
    }
    return emitOutputFilePathWithoutExtension + extension;
}

public static @string GetSourceMapFilePath(@string jsFilePath, ж<core.CompilerOptions> Ꮡoptions) {
    ref var options = ref Ꮡoptions.DerefOrNull();

    if (options.SourceMap.IsTrue() && !options.InlineSourceMap.IsTrue()) {
        return jsFilePath + ".map"u8;
    }
    return "";
}

public static @string GetBuildInfoFileName(ж<core.CompilerOptions> Ꮡoptions, tspath.ComparePathsOptions opts) {
    ref var options = ref Ꮡoptions.DerefOrNull();

    if (!options.IsIncremental() && !options.Build.IsTrue()) {
        return "";
    }
    if (options.TsBuildInfoFile != ""u8) {
        return options.TsBuildInfoFile;
    }
    if (options.ConfigFilePath == ""u8) {
        return "";
    }
    @string configFileExtensionLess = tspath.RemoveFileExtension(options.ConfigFilePath);
    @string buildInfoExtensionLess = default;
    if (options.OutDir != ""u8) {
        if (options.RootDir != ""u8) {
            buildInfoExtensionLess = tspath.ResolvePath(options.OutDir, tspath.GetRelativePathFromDirectory(options.RootDir, configFileExtensionLess, opts));
        } else {
            buildInfoExtensionLess = tspath.CombinePaths(options.OutDir, tspath.GetBaseFileName(configFileExtensionLess));
        }
    } else {
        buildInfoExtensionLess = configFileExtensionLess;
    }
    return buildInfoExtensionLess + tspath.ExtensionTsBuildInfo;
}

} // end outputpaths_package
