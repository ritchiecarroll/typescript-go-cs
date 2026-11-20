namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using slices = slices_package;
using strings = strings_package;

partial class tspath_package {

public static readonly @string ExtensionTs = ".ts"u8;
public static readonly @string ExtensionTsx = ".tsx"u8;
public static readonly @string ExtensionDts = ".d.ts"u8;
public static readonly @string ExtensionJs = ".js"u8;
public static readonly @string ExtensionJsx = ".jsx"u8;
public static readonly @string ExtensionJson = ".json"u8;
public static readonly @string ExtensionTsBuildInfo = ".tsbuildinfo"u8;
public static readonly @string ExtensionMjs = ".mjs"u8;
public static readonly @string ExtensionMts = ".mts"u8;
public static readonly @string ExtensionDmts = ".d.mts"u8;
public static readonly @string ExtensionCjs = ".cjs"u8;
public static readonly @string ExtensionCts = ".cts"u8;
public static readonly @string ExtensionDcts = ".d.cts"u8;

public static readonly ж<slice<@string>> ᏑSupportedDeclarationExtensions = new StaticBox<slice<@string>>([ExtensionDts, ExtensionDcts, ExtensionDmts]);
public static ref slice<@string> SupportedDeclarationExtensions => ref ((StaticBox<slice<@string>>)ᏑSupportedDeclarationExtensions).ValueSlot;
public static readonly ж<slice<@string>> ᏑSupportedTSImplementationExtensions = new StaticBox<slice<@string>>([ExtensionTs, ExtensionTsx, ExtensionMts, ExtensionCts]);
public static ref slice<@string> SupportedTSImplementationExtensions => ref ((StaticBox<slice<@string>>)ᏑSupportedTSImplementationExtensions).ValueSlot;
internal static slice<@string> supportedTSExtensionsForExtractExtension = [ExtensionDts, ExtensionDcts, ExtensionDmts, ExtensionTs, ExtensionTsx, ExtensionMts, ExtensionCts];
public static readonly ж<slice<slice<@string>>> ᏑAllSupportedExtensions = new StaticBox<slice<slice<@string>>>([[ExtensionTs, ExtensionTsx, ExtensionDts, ExtensionJs, ExtensionJsx], [ExtensionCts, ExtensionDcts, ExtensionCjs], [ExtensionMts, ExtensionDmts, ExtensionMjs]]);
public static ref slice<slice<@string>> AllSupportedExtensions => ref ((StaticBox<slice<slice<@string>>>)ᏑAllSupportedExtensions).ValueSlot;
public static readonly ж<slice<slice<@string>>> ᏑSupportedTSExtensions = new StaticBox<slice<slice<@string>>>([[ExtensionTs, ExtensionTsx, ExtensionDts], [ExtensionCts, ExtensionDcts], [ExtensionMts, ExtensionDmts]]);
public static ref slice<slice<@string>> SupportedTSExtensions => ref ((StaticBox<slice<slice<@string>>>)ᏑSupportedTSExtensions).ValueSlot;
public static readonly ж<slice<@string>> ᏑSupportedTSExtensionsFlat = new StaticBox<slice<@string>>([ExtensionTs, ExtensionTsx, ExtensionDts, ExtensionCts, ExtensionDcts, ExtensionMts, ExtensionDmts]);
public static ref slice<@string> SupportedTSExtensionsFlat => ref ((StaticBox<slice<@string>>)ᏑSupportedTSExtensionsFlat).ValueSlot;
public static readonly ж<slice<slice<@string>>> ᏑSupportedJSExtensions = new StaticBox<slice<slice<@string>>>([[ExtensionJs, ExtensionJsx], [ExtensionMjs], [ExtensionCjs]]);
public static ref slice<slice<@string>> SupportedJSExtensions => ref ((StaticBox<slice<slice<@string>>>)ᏑSupportedJSExtensions).ValueSlot;
public static readonly ж<slice<@string>> ᏑSupportedJSExtensionsFlat = new StaticBox<slice<@string>>([ExtensionJs, ExtensionJsx, ExtensionMjs, ExtensionCjs]);
public static ref slice<@string> SupportedJSExtensionsFlat => ref ((StaticBox<slice<@string>>)ᏑSupportedJSExtensionsFlat).ValueSlot;
public static readonly ж<slice<slice<@string>>> ᏑAllSupportedExtensionsWithJson = new StaticBox<slice<slice<@string>>>(slices.Concat<slice<slice<@string>>, slice<@string>>(AllSupportedExtensions, slice<slice<@string>>([[ExtensionJson]])));
public static ref slice<slice<@string>> AllSupportedExtensionsWithJson => ref ((StaticBox<slice<slice<@string>>>)ᏑAllSupportedExtensionsWithJson).ValueSlot;
public static readonly ж<slice<slice<@string>>> ᏑSupportedTSExtensionsWithJson = new StaticBox<slice<slice<@string>>>(slices.Concat<slice<slice<@string>>, slice<@string>>(SupportedTSExtensions, slice<slice<@string>>([[ExtensionJson]])));
public static ref slice<slice<@string>> SupportedTSExtensionsWithJson => ref ((StaticBox<slice<slice<@string>>>)ᏑSupportedTSExtensionsWithJson).ValueSlot;
public static readonly ж<slice<@string>> ᏑSupportedTSExtensionsWithJsonFlat = new StaticBox<slice<@string>>(slices.Concat<slice<@string>, @string>(SupportedTSExtensionsFlat, slice<@string>([ExtensionJson])));
public static ref slice<@string> SupportedTSExtensionsWithJsonFlat => ref ((StaticBox<slice<@string>>)ᏑSupportedTSExtensionsWithJsonFlat).ValueSlot;
public static readonly ж<slice<@string>> ᏑExtensionsNotSupportingExtensionlessResolution = new StaticBox<slice<@string>>([ExtensionMts, ExtensionDmts, ExtensionMjs, ExtensionCts, ExtensionDcts, ExtensionCjs]);
public static ref slice<@string> ExtensionsNotSupportingExtensionlessResolution => ref ((StaticBox<slice<@string>>)ᏑExtensionsNotSupportingExtensionlessResolution).ValueSlot;

public static bool ExtensionIsTs(@string ext) {
    return ext == ExtensionTs || ext == ExtensionTsx || ext == ExtensionDts || ext == ExtensionMts || ext == ExtensionDmts || ext == ExtensionCts || ext == ExtensionDcts || len(ext) >= 7 && ext[..3] == ".d." && ext.slice(len(ext) - 3) == ".ts";
}

internal static slice<@string> extensionsToRemove = [ExtensionDts, ExtensionDmts, ExtensionDcts, ExtensionMjs, ExtensionMts, ExtensionCjs, ExtensionCts, ExtensionTs, ExtensionJs, ExtensionTsx, ExtensionJsx, ExtensionJson];

public static @string RemoveFileExtension(@string path) {
    foreach (var (_, ext) in range(extensionsToRemove)) {
        if (strings.HasSuffix(path, ext)) {
            return path.slice(0, len(path) - len(ext));
        }
    }

    return path;
}

public static @string RemoveAnyFileExtension(@string path) {
    @string withoutExtension = RemoveFileExtension(path); if (withoutExtension != path) {
        return withoutExtension;
    }
    @string extension = GetAnyExtensionFromPath(path, default, false); if (extension != ""u8) {
        return RemoveExtension(path, extension);
    }
    return path;
}

public static @string TryGetExtensionFromPath(@string p) {
    foreach (var (_, ext) in range(extensionsToRemove)) {
        if (FileExtensionIs(p, ext)) {
            return ext;
        }
    }
    return "";
}

public static @string RemoveExtension(@string path, @string extension) {
    return path.slice(0, len(path) - len(extension));
}

public static bool FileExtensionIsOneOf(@string path, slice<@string> extensions) {
    foreach (var (_, ext) in range(extensions)) {
        if (FileExtensionIs(path, ext)) {
            return true;
        }
    }
    return false;
}

public static @string TryExtractTSExtension(@string fileName) {
    foreach (var (_, ext) in range(supportedTSExtensionsForExtractExtension)) {
        if (FileExtensionIs(fileName, ext)) {
            return ext;
        }
    }
    return "";
}

public static bool HasTSFileExtension(@string path) {
    return FileExtensionIsOneOf(path, SupportedTSExtensionsFlat);
}

public static bool HasImplementationTSFileExtension(@string path) {
    return FileExtensionIsOneOf(path, SupportedTSImplementationExtensions) && !IsDeclarationFileName(path);
}

public static bool HasJSFileExtension(@string path) {
    return FileExtensionIsOneOf(path, SupportedJSExtensionsFlat);
}

public static bool HasJSONFileExtension(@string path) {
    return FileExtensionIs(path, ExtensionJson);
}

public static bool IsDeclarationFileName(@string fileName) {
    return GetDeclarationFileExtension(fileName) != ""u8;
}

public static bool ExtensionIsOneOf(@string ext, slice<@string> extensions) {
    return slices.Contains(extensions, ext);
}

public static @string GetDeclarationFileExtension(@string fileName) {
    @string @base = GetBaseFileName(fileName);
    foreach (var (_, ext) in range(SupportedDeclarationExtensions)) {
        if (strings.HasSuffix(@base, ext)) {
            return ext;
        }
    }
    if (strings.HasSuffix(@base, ExtensionTs)) {
        nint index = strings.Index(@base, ".d.");
        if (index >= 0) {
            return @base.slice(index);
        }
    }
    return "";
}

public static @string GetDeclarationEmitExtensionForPath(@string path) {
    switch (ᐧ) {
    case {} when FileExtensionIsOneOf(path, [ExtensionMjs, ExtensionMts]): {
        return ExtensionDmts;
    }
    case {} when FileExtensionIsOneOf(path, [ExtensionCjs, ExtensionCts]): {
        return ExtensionDcts;
    }
    case {} when FileExtensionIsOneOf(path, [ExtensionTs, ExtensionTsx, ExtensionJs, ExtensionJsx]): {
        return ExtensionDts;
    }
    default: {
        @string ext = GetAnyExtensionFromPath(path, default, false);
        if (ext != ""u8) {
            return ".d"u8 + ext + ".ts"u8;
        }
        return ExtensionDts;
    }}

}

public static @string ChangeAnyExtension(@string path, @string ext, slice<@string> extensions, bool ignoreCase) {
    @string pathext = GetAnyExtensionFromPath(path, extensions, ignoreCase);
    if (pathext != ""u8) {
        @string result = path.slice(0, len(path) - len(pathext));
        if (ext == ""u8) {
            return result;
        }
        if (strings.HasPrefix(ext, ".")) {
            return result + ext;
        }
        return result + "."u8 + ext;
    }
    return path;
}

public static @string ChangeExtension(@string path, @string newExtension) {
    return ChangeAnyExtension(path, newExtension, extensionsToRemove, false);
}

public static @string ChangeFullExtension(@string path, @string newExtension) {
    @string declarationExtension = GetDeclarationFileExtension(path);
    if (declarationExtension != ""u8) {
        @string ext = newExtension;
        if (!strings.HasPrefix(ext, ".")) {
            ext = "."u8 + ext;
        }
        return path.slice(0, len(path) - len(declarationExtension)) + ext;
    }
    return ChangeExtension(path, newExtension);
}

public static slice<@string> GetPossibleOriginalInputExtensionForExtension(@string path) {
    if (FileExtensionIsOneOf(path, [ExtensionDmts, ExtensionMjs, ExtensionMts])) {
        return [ExtensionMts, ExtensionMjs];
    }
    if (FileExtensionIsOneOf(path, [ExtensionDcts, ExtensionCjs, ExtensionCts])) {
        return [ExtensionCts, ExtensionCjs];
    }
    @string ext = GetDeclarationFileExtension(path); if (ext != ""u8 && ext != ExtensionDts) {
        @string inner = ext.slice(len(".d."), len(ext) - len(".ts"));
        return ["."u8 + inner];
    }
    return [ExtensionTsx, ExtensionTs, ExtensionJsx, ExtensionJs];
}

} // end tspath_package
