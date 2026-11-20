namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class outputpaths_package {

internal static @string computeCommonSourceDirectoryOfFilenames(slice<@string> fileNames, @string currentDirectory, bool useCaseSensitiveFileNames) {
    slice<@string> commonPathComponents = default;
    foreach (var (_, sourceFile) in range(fileNames)) {
        var sourcePathComponents = tspath.GetNormalizedPathComponents(sourceFile, currentDirectory);

        sourcePathComponents = sourcePathComponents.slice(0, len(sourcePathComponents) - 1);

        if (commonPathComponents == nil) {
            commonPathComponents = sourcePathComponents;
            continue;
        }

        nint n = min(len(commonPathComponents), len(sourcePathComponents));
        foreach (var i in range(n)) {
            if (tspath.GetCanonicalFileName(commonPathComponents[i], useCaseSensitiveFileNames) != tspath.GetCanonicalFileName(sourcePathComponents[i], useCaseSensitiveFileNames)) {
                if (i == 0) {
                    return "";
                }

                commonPathComponents = commonPathComponents.slice(0, i);
                break;
            }
        }

        if (len(sourcePathComponents) < len(commonPathComponents)) {
            commonPathComponents = commonPathComponents.slice(0, len(sourcePathComponents));
        }
    }

    if (len(commonPathComponents) == 0) {
        return currentDirectory;
    }

    return tspath.GetPathFromPathComponents(commonPathComponents);
}

public static @string GetComputedCommonSourceDirectory(slice<@string> emittedFiles, @string currentDirectory, bool useCaseSensitiveFileNames) {
    @string commonSourceDirectory = computeCommonSourceDirectoryOfFilenames(emittedFiles, currentDirectory, useCaseSensitiveFileNames);
    if (len(commonSourceDirectory) > 0) {
        commonSourceDirectory = tspath.EnsureTrailingDirectorySeparator(commonSourceDirectory);
    }
    return commonSourceDirectory;
}

public static @string GetCommonSourceDirectory(ж<core.CompilerOptions> Ꮡoptions, Func<slice<@string>> files, @string currentDirectory, bool useCaseSensitiveFileNames, Func<slice<@string>, @string, bool> checkSourceFilesBelongToPath) {
    ref var options = ref Ꮡoptions.DerefOrNull();

    @string commonSourceDirectory = default;
    if (options.RootDir != ""u8) {
        commonSourceDirectory = options.RootDir;
        if (checkSourceFilesBelongToPath != nil) {
            checkSourceFilesBelongToPath(files(), options.RootDir);
        }
    } else if (options.ConfigFilePath != ""u8) {
        commonSourceDirectory = tspath.GetDirectoryPath(options.ConfigFilePath);
        if (checkSourceFilesBelongToPath != nil) {
            checkSourceFilesBelongToPath(files(), commonSourceDirectory);
        }
    } else {
        commonSourceDirectory = computeCommonSourceDirectoryOfFilenames(files(), currentDirectory, useCaseSensitiveFileNames);
    }

    if (len(commonSourceDirectory) > 0) {
        commonSourceDirectory = tspath.EnsureTrailingDirectorySeparator(commonSourceDirectory);
    }

    return commonSourceDirectory;
}

} // end outputpaths_package
