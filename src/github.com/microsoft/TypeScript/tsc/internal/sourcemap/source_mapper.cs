global using SourceMappedPosition = go.github.com.microsoft.TypeScript.tsc.@internal.sourcemap_package.MappedPosition;

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using base64 = encoding.base64_package;
using slices = slices_package;
using strings = strings_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using json = go.github.com.microsoft.TypeScript.tsc.@internal.json_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using stringutil = go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using encoding;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using iter = iter_package;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using static sourcemap_package;

partial class sourcemap_package {

partial interface Host {
    bool UseCaseSensitiveFileNames();
    ж<ECMALineInfo> GetECMALineInfo(@string fileName);
    (@string, bool) ReadFile(@string fileName);
}

partial struct MappedPosition {
    internal nint generatedPosition;
    internal nint sourcePosition;
    internal SourceIndex sourceIndex;
    internal NameIndex nameIndex;
}

internal static UntypedInt missingPosition => -1;

} partial class sourcemapᴛMappedPosition {

internal static bool isSourceMappedPosition(this ref MappedPosition m) {
    return m.sourceIndex != MissingSource && m.sourcePosition != missingPosition;
}

} partial class sourcemap_package {

partial struct DocumentPositionMapper {
    internal bool useCaseSensitiveFileNames;
    internal slice<@string> sourceFileAbsolutePaths;
    internal map<@string, SourceIndex> sourceToSourceIndexMap;
    internal @string generatedAbsoluteFilePath;
    internal slice<ж<MappedPosition>> generatedMappings;
    internal map<SourceIndex, slice<ж<SourceMappedPosition>>> sourceMappings;
}

internal static ж<DocumentPositionMapper> createDocumentPositionMapper(Host host, ref RawSourceMap sourceMap, @string mapPath) {
    @string mapDirectory = tspath.GetDirectoryPath(mapPath);
    @string sourceRoot = default;
    if (sourceMap.SourceRoot != ""u8) {
        sourceRoot = tspath.GetNormalizedAbsolutePath(sourceMap.SourceRoot, mapDirectory);
    } else {
        sourceRoot = mapDirectory;
    }
    ref var generatedAbsoluteFilePath = ref heap<@string>(tspath.GetNormalizedAbsolutePath(sourceMap.File, mapDirectory), out var ᏑgeneratedAbsoluteFilePath);
    var sourceFileAbsolutePaths = core.Map(sourceMap.Sources, @string (@string source) => tspath.GetNormalizedAbsolutePath(source, sourceRoot));
    ref var useCaseSensitiveFileNames = ref heap<bool>(host.UseCaseSensitiveFileNames(), out var ᏑuseCaseSensitiveFileNames);
    var sourceToSourceIndexMap = new map<@string, SourceIndex>(len(sourceFileAbsolutePaths));
    foreach (var (i, source) in range(sourceFileAbsolutePaths)) {
        sourceToSourceIndexMap[tspath.GetCanonicalFileName(source, useCaseSensitiveFileNames)] = (SourceIndex)i;
    }

    slice<ж<MappedPosition>> decodedMappings = default;
    slice<ж<MappedPosition>> generatedMappings = default;
    var sourceMappings = new map<SourceIndex, slice<ж<SourceMappedPosition>>>();

    var decoder = DecodeMappings(sourceMap.Mappings);
    foreach (var mapping in range<ж<Mapping>>(decoder.Values().Invoke)) {
        ref var generatedPosition = ref heap<nint>(-1, out var ᏑgeneratedPosition);
        var lineInfo = host.GetECMALineInfo(generatedAbsoluteFilePath);
        if (lineInfo != nil) {
            generatedPosition = scanner.ComputePositionOfLineAndUTF16Character(
                lineInfo.lineStarts,
                mapping.GeneratedLine,
                mapping.GeneratedCharacter,
                lineInfo.text,
                true);
        }

        ref var sourcePosition = ref heap<nint>(-1, out var ᏑsourcePosition);
        if (mapping.IsSourceMapping()) {
            var lineInfoΔ1 = host.GetECMALineInfo(sourceFileAbsolutePaths[mapping.SourceIndex]);
            if (lineInfoΔ1 != nil) {
                nint pos = scanner.ComputePositionOfLineAndUTF16Character(
                    lineInfoΔ1.lineStarts,
                    mapping.SourceLine,
                    mapping.SourceCharacter,
                    lineInfoΔ1.text,
                    true);
                sourcePosition = pos;
            }
        }

        decodedMappings = append(decodedMappings, Ꮡ(new MappedPosition(
            generatedPosition: generatedPosition,
            sourceIndex: mapping.SourceIndex,
            sourcePosition: sourcePosition,
            nameIndex: mapping.NameIndex
        )));
    }
    if (decoder.Error() != nil) {
        decodedMappings = default;
    }

    foreach (var (_, mapping) in range(decodedMappings)) {
        if (!mapping.isSourceMappedPosition()) {
            continue;
        }
        ref var sourceIndex = ref heap<SourceIndex>(mapping.sourceIndex, out var ᏑsourceIndex);
        var list = sourceMappings[sourceIndex];
        list = append(list, Ꮡ(new SourceMappedPosition(
            generatedPosition: mapping.generatedPosition,
            sourceIndex: sourceIndex,
            sourcePosition: mapping.sourcePosition,
            nameIndex: mapping.nameIndex
        )));
        sourceMappings[sourceIndex] = list;
    }
    foreach (var (i, list) in sourceMappings) {
        slices.SortFunc(list, (ж<SourceMappedPosition> a, ж<SourceMappedPosition> b) => {
            debug.Assert(a.sourceIndex == b.sourceIndex, "All source mappings should have the same source index");
            return a.sourcePosition - b.sourcePosition;
        });
        sourceMappings[i] = core.DeduplicateSorted(list, (ж<SourceMappedPosition> a, ж<SourceMappedPosition> b) => a.generatedPosition == b.generatedPosition && a.sourceIndex == b.sourceIndex && a.sourcePosition == b.sourcePosition);
    }

    generatedMappings = decodedMappings;
    slices.SortFunc(generatedMappings, (ж<MappedPosition> a, ж<MappedPosition> b) => a.generatedPosition - b.generatedPosition);
    generatedMappings = core.DeduplicateSorted(generatedMappings, (ж<MappedPosition> a, ж<MappedPosition> b) => a.generatedPosition == b.generatedPosition && a.sourceIndex == b.sourceIndex && a.sourcePosition == b.sourcePosition);

    return Ꮡ(new DocumentPositionMapper(
        useCaseSensitiveFileNames: useCaseSensitiveFileNames,
        sourceFileAbsolutePaths: sourceFileAbsolutePaths,
        sourceToSourceIndexMap: sourceToSourceIndexMap,
        generatedAbsoluteFilePath: generatedAbsoluteFilePath,
        generatedMappings: generatedMappings,
        sourceMappings: sourceMappings
    ));
}

partial struct DocumentPosition {
    public @string FileName;
    public nint Pos;
}

} partial class sourcemapᴛDocumentPositionMapper {

public static ж<DocumentPosition> GetSourcePosition(this ж<DocumentPositionMapper> Ꮡd, ж<DocumentPosition> Ꮡloc) {
    ref var d = ref Ꮡd.DerefOrNull();
    ref var loc = ref Ꮡloc.DerefOrNull();

    if (Ꮡd == nil) {
        return default;
    }
    if (len(d.generatedMappings) == 0) {
        return default;
    }

    var (targetIndex, _) = slices.BinarySearchFunc(d.generatedMappings, loc.Pos, (ж<MappedPosition> m, nint pos) => m.generatedPosition - pos);

    if (targetIndex < 0 || targetIndex >= len(d.generatedMappings)) {
        return default;
    }

    var mapping = d.generatedMappings[targetIndex];
    if (!mapping.isSourceMappedPosition()) {
        return default;
    }

    return Ꮡ(new DocumentPosition(
        FileName: d.sourceFileAbsolutePaths[mapping.sourceIndex],
        Pos: mapping.sourcePosition
    ));
}

public static ж<DocumentPosition> GetGeneratedPosition(this ж<DocumentPositionMapper> Ꮡd, ж<DocumentPosition> Ꮡloc) {
    ref var d = ref Ꮡd.DerefOrNull();
    ref var loc = ref Ꮡloc.DerefOrNull();

    if (Ꮡd == nil) {
        return default;
    }
    var (sourceIndex, ok) = d.sourceToSourceIndexMap[tspath.GetCanonicalFileName(loc.FileName, d.useCaseSensitiveFileNames), ꟷ];
    if (!ok) {
        return default;
    }
    if (sourceIndex < 0 || (nint)sourceIndex >= len(d.sourceMappings)) {
        return default;
    }
    var sourceMappings = d.sourceMappings[sourceIndex];
    var (targetIndex, _) = slices.BinarySearchFunc(sourceMappings, loc.Pos, (ж<SourceMappedPosition> m, nint pos) => m.sourcePosition - pos);

    if (targetIndex < 0 || targetIndex >= len(sourceMappings)) {
        return default;
    }

    var mapping = sourceMappings[targetIndex];
    if (mapping.sourceIndex != sourceIndex) {
        return default;
    }

    return Ꮡ(new DocumentPosition(
        FileName: d.generatedAbsoluteFilePath,
        Pos: mapping.generatedPosition
    ));
}

} partial class sourcemap_package {

public static ж<DocumentPositionMapper> GetDocumentPositionMapper(Host host, @string generatedFileName) {
    var possibleMapLocationsᴷ = new stackAppend<@string>();

    @string mapFileName = tryGetSourceMappingURL(host, generatedFileName);
    if (mapFileName != ""u8) {
        var (base64Object, matched) = tryParseBase64Url(mapFileName); if (matched) {
            if (base64Object != ""u8) {
                var (decoded, err) = base64.StdEncoding.DecodeString(base64Object); if (err == nil) {
                    return convertDocumentToSourceMapper(host, (@string)decoded, generatedFileName);
                }
            }
            mapFileName = "";
        }
    }

    slice<@string> possibleMapLocations = default;
    if (mapFileName != ""u8) {
        possibleMapLocations = append(possibleMapLocationsᴷ, possibleMapLocations, mapFileName);
    }
    possibleMapLocations = append(possibleMapLocations, generatedFileName + ".map"u8);
    foreach (var (_, location) in range(possibleMapLocations)) {
        @string mapFileNameΔ1 = tspath.GetNormalizedAbsolutePath(location, tspath.GetDirectoryPath(generatedFileName));
        var (mapFileContents, ok) = host.ReadFile(mapFileNameΔ1); if (ok) {
            return convertDocumentToSourceMapper(host, mapFileContents, mapFileNameΔ1);
        }
    }
    return default;
}

internal static ж<DocumentPositionMapper> convertDocumentToSourceMapper(Host host, @string contents, @string mapFileName) {
    var sourceMap = tryParseRawSourceMap(contents);
    if (sourceMap == nil || len(sourceMap.Sources) == 0 || sourceMap.File == ""u8 || sourceMap.Mappings == ""u8) {
        return default;
    }

    if (core.Some(sourceMap.SourcesContent, (ж<@string> s) => s != nil)) {
        return default;
    }

    return createDocumentPositionMapper(host, ref (sourceMap).DerefOrNull(), mapFileName);
}

internal static ж<RawSourceMap> tryParseRawSourceMap(@string contents) {
    var sourceMap = Ꮡ(new RawSourceMap());
    var err = json.Unmarshal(slice<byte>(contents), sourceMap.OrTypedNil());
    if (err != nil) {
        return default;
    }
    if (sourceMap.Version != 3) {
        return default;
    }
    return sourceMap;
}

internal static @string tryGetSourceMappingURL(Host host, @string fileName) {
    var lineInfo = host.GetECMALineInfo(fileName);
    return TryGetSourceMappingURL(lineInfo);
}

internal static (@string parseableUrl, bool isBase64Url) tryParseBase64Url(@string url) {
    bool found = default;
    (url, found) = strings.CutPrefix(url, @"data:"); if (!found) {
        return ("", false);
    }
    (url, found) = strings.CutPrefix(url, @"application/json;"); if (!found) {
        return ("", true);
    }
    (url, found) = strings.CutPrefix(url, @"charset="); if (found) {
        if (!strings.EqualFold(url[..(int)(len(@"utf-8;"))], @"utf-8;")) {
            return ("", true);
        }
        url = url[(int)(len(@"utf-8;"))..];
    }
    (url, found) = strings.CutPrefix(url, @"base64,"); if (!found) {
        return ("", true);
    }
    foreach (var (_, r) in url) {
        if (!(stringutil.IsASCIILetter(r) || stringutil.IsDigit(r) || r == '+' || r == '/' || r == '=')) {
            return ("", true);
        }
    }
    return (url, true);
}

} // end sourcemap_package
