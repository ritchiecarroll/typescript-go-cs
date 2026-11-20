namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using base64 = encoding.base64_package;
using errors = errors_package;
using slices = slices_package;
using strings = strings_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using json = go.github.com.microsoft.TypeScript.tsc.@internal.json_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using encoding;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using io = io_package;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using static sourcemap_package;

partial class sourcemap_package {

partial struct SourceIndex /*num:nint*/;

partial struct NameIndex /*num:nint*/;

internal static SourceIndex sourceIndexNotSet => -1;
internal static NameIndex nameIndexNotSet => -1;
internal const nint notSet = -1;
internal static core.UTF16Offset notSetUTF16 => -1;

partial struct Generator {
    internal tspath.ComparePathsOptions pathOptions;
    internal @string @file;
    internal @string sourceRoot;
    internal @string sourcesDirectoryPath;
    internal slice<@string> rawSources;
    internal slice<@string> sources;
    internal map<@string, SourceIndex> sourceToSourceIndexMap;
    internal slice<ж<@string>> sourcesContent;
    internal slice<@string> names;
    internal map<@string, NameIndex> nameToNameIndexMap;
    internal strings.Builder mappings;
    internal nint lastGeneratedLine;
    internal core.UTF16Offset lastGeneratedCharacter;
    internal SourceIndex lastSourceIndex;
    internal nint lastSourceLine;
    internal core.UTF16Offset lastSourceCharacter;
    internal NameIndex lastNameIndex;
    internal bool hasLast;
    internal nint pendingGeneratedLine;
    internal core.UTF16Offset pendingGeneratedCharacter;
    internal SourceIndex pendingSourceIndex;
    internal nint pendingSourceLine;
    internal core.UTF16Offset pendingSourceCharacter;
    internal NameIndex pendingNameIndex;
    internal bool hasPending;
    internal bool hasPendingSource;
    internal bool hasPendingName;
}

partial struct RawSourceMap {
    public nint Version; /*`json:"version"`*/
    public @string File; /*`json:"file"`*/
    public @string SourceRoot; /*`json:"sourceRoot"`*/
    public slice<@string> Sources; /*`json:"sources"`*/
    public slice<@string> Names; /*`json:"names"`*/
    public @string Mappings; /*`json:"mappings"`*/
    public slice<ж<@string>> SourcesContent; /*`json:"sourcesContent,omitzero"`*/
}

public static ж<Generator> NewGenerator(@string @file, @string sourceRoot, @string sourcesDirectoryPath, tspath.ComparePathsOptions options) {
    return Ꮡ(new Generator(
        @file: @file,
        sourceRoot: sourceRoot,
        sourcesDirectoryPath: sourcesDirectoryPath,
        pathOptions: options
    ));
}

} partial class sourcemapᴛGenerator {

public static slice<@string> Sources(this ref Generator gen) { return gen.rawSources; }

public static SourceIndex AddSource(this ref Generator gen, @string fileName) {
    @string source = tspath.GetRelativePathToDirectoryOrUrl(
        gen.sourcesDirectoryPath,
        fileName,
        true,
        gen.pathOptions);

    var (sourceIndex, found) = gen.sourceToSourceIndexMap[source, ꟷ];
    if (!found) {
        sourceIndex = (SourceIndex)len(gen.sources);
        gen.sources = append(gen.sources, source);
        gen.rawSources = append(gen.rawSources, fileName);
        if (gen.sourceToSourceIndexMap == nil) {
            gen.sourceToSourceIndexMap = new map<@string, SourceIndex>();
        }
        gen.sourceToSourceIndexMap[source] = sourceIndex;
    }

    return sourceIndex;
}

public static error SetSourceContent(this ref Generator gen, SourceIndex sourceIndex, @string contentʗp) {
    ref var content = ref heap(contentʗp, out var Ꮡcontent);

    if (sourceIndex < 0 || (nint)sourceIndex >= len(gen.sources)) {
        return errors.New("sourceIndex is out of range");
    }
    while (len(gen.sourcesContent) <= (nint)sourceIndex) {
        gen.sourcesContent = append(gen.sourcesContent, (ж<@string>)(nil));
    }
    gen.sourcesContent[sourceIndex] = Ꮡcontent;
    return default;
}

public static NameIndex AddName(this ref Generator gen, @string name) {
    var (nameIndex, found) = gen.nameToNameIndexMap[name, ꟷ];
    if (!found) {
        nameIndex = (NameIndex)len(gen.names);
        gen.names = append(gen.names, name);
        if (gen.nameToNameIndexMap == nil) {
            gen.nameToNameIndexMap = new map<@string, NameIndex>();
        }
        gen.nameToNameIndexMap[name] = nameIndex;
    }
    return nameIndex;
}

internal static bool isNewGeneratedPosition(this ref Generator gen, nint generatedLine, core.UTF16Offset generatedCharacter) {
    return !gen.hasPending || gen.pendingGeneratedLine != generatedLine || gen.pendingGeneratedCharacter != generatedCharacter;
}

internal static bool isBacktrackingSourcePosition(this ref Generator gen, SourceIndex sourceIndex, nint sourceLine, core.UTF16Offset sourceCharacter) {
    return sourceIndex != sourceIndexNotSet && sourceLine != notSet && sourceCharacter != notSetUTF16 && gen.pendingSourceIndex == sourceIndex && (gen.pendingSourceLine > sourceLine || gen.pendingSourceLine == sourceLine && gen.pendingSourceCharacter > sourceCharacter);
}

internal static bool shouldCommitMapping(this ref Generator gen) {
    return gen.hasPending && (!gen.hasLast || gen.lastGeneratedLine != gen.pendingGeneratedLine || gen.lastGeneratedCharacter != gen.pendingGeneratedCharacter || gen.lastSourceIndex != gen.pendingSourceIndex || gen.lastSourceLine != gen.pendingSourceLine || gen.lastSourceCharacter != gen.pendingSourceCharacter || gen.lastNameIndex != gen.pendingNameIndex);
}

internal static void appendMappingCharCode(this ж<Generator> Ꮡgen, rune charCode) {
    Ꮡgen.Ꮡmappings.WriteRune(charCode);
}

internal static void appendBase64VLQ(this ж<Generator> Ꮡgen, nint inValue) {
    if (inValue < 0) {
        inValue = ((-inValue) << 1) + 1;
    } else {
        inValue = inValue << 1;
    }

    for (;;) {
        nint currentDigit = inValue & 31;
        inValue = inValue >> 5;
        if (inValue > 0) {
            currentDigit = currentDigit | 32;
        }
        Ꮡgen.appendMappingCharCode(base64FormatEncode(currentDigit));
        if (inValue <= 0) {
            break;
        }
    }
}

internal static void commitPendingMapping(this ж<Generator> Ꮡgen) {
    ref var gen = ref Ꮡgen.DerefOrNull();

    if (!gen.shouldCommitMapping()) {
        return;
    }

    if (gen.lastGeneratedLine < gen.pendingGeneratedLine) {
        for (;;) {
            Ꮡgen.appendMappingCharCode(';');
            gen.lastGeneratedLine++;
            if (gen.lastGeneratedLine >= gen.pendingGeneratedLine) {
                break;
            }
        }
        gen.lastGeneratedCharacter = 0;
    } else {
        if (gen.lastGeneratedLine != gen.pendingGeneratedLine) {
            throw panic("generatedLine cannot backtrack");
        }
        if (gen.hasLast) {
            Ꮡgen.appendMappingCharCode(',');
        }
    }

    Ꮡgen.appendBase64VLQ((nint)(gen.pendingGeneratedCharacter - gen.lastGeneratedCharacter));
    gen.lastGeneratedCharacter = gen.pendingGeneratedCharacter;

    if (gen.hasPendingSource) {
        Ꮡgen.appendBase64VLQ((nint)(gen.pendingSourceIndex - gen.lastSourceIndex));
        gen.lastSourceIndex = gen.pendingSourceIndex;

        Ꮡgen.appendBase64VLQ(gen.pendingSourceLine - gen.lastSourceLine);
        gen.lastSourceLine = gen.pendingSourceLine;

        Ꮡgen.appendBase64VLQ((nint)(gen.pendingSourceCharacter - gen.lastSourceCharacter));
        gen.lastSourceCharacter = gen.pendingSourceCharacter;

        if (gen.hasPendingName) {
            Ꮡgen.appendBase64VLQ((nint)(gen.pendingNameIndex - gen.lastNameIndex));
            gen.lastNameIndex = gen.pendingNameIndex;
        }
    }

    gen.hasLast = true;
}

internal static void addMapping(this ж<Generator> Ꮡgen, nint generatedLine, core.UTF16Offset generatedCharacter, SourceIndex sourceIndex, nint sourceLine, core.UTF16Offset sourceCharacter, NameIndex nameIndex) {
    ref var gen = ref Ꮡgen.DerefOrNull();

    if (gen.isNewGeneratedPosition(generatedLine, generatedCharacter) || gen.isBacktrackingSourcePosition(sourceIndex, sourceLine, sourceCharacter)) {
        Ꮡgen.commitPendingMapping();
        gen.pendingGeneratedLine = generatedLine;
        gen.pendingGeneratedCharacter = generatedCharacter;
        gen.hasPendingSource = false;
        gen.hasPendingName = false;
        gen.hasPending = true;
    }

    if (sourceIndex != sourceIndexNotSet && sourceLine != notSet && sourceCharacter != notSetUTF16) {
        gen.pendingSourceIndex = sourceIndex;
        gen.pendingSourceLine = sourceLine;
        gen.pendingSourceCharacter = sourceCharacter;
        gen.hasPendingSource = true;
        if (nameIndex != nameIndexNotSet) {
            gen.pendingNameIndex = nameIndex;
            gen.hasPendingName = true;
        }
    }
}

public static error AddGeneratedMapping(this ж<Generator> Ꮡgen, nint generatedLine, core.UTF16Offset generatedCharacter) {
    ref var gen = ref Ꮡgen.DerefOrNull();

    if (generatedLine < gen.pendingGeneratedLine) {
        return errors.New("generatedLine cannot backtrack");
    }
    if (generatedCharacter < 0) {
        return errors.New("generatedCharacter cannot be negative");
    }
    Ꮡgen.addMapping(generatedLine, generatedCharacter, sourceIndexNotSet, notSet, notSetUTF16, nameIndexNotSet);
    gen.hasPendingSource = false;
    gen.hasPendingName = false;
    return default;
}

public static error AddSourceMapping(this ж<Generator> Ꮡgen, nint generatedLine, core.UTF16Offset generatedCharacter, SourceIndex sourceIndex, nint sourceLine, core.UTF16Offset sourceCharacter) {
    ref var gen = ref Ꮡgen.DerefOrNull();

    if (generatedLine < gen.pendingGeneratedLine) {
        return errors.New("generatedLine cannot backtrack");
    }
    if (generatedCharacter < 0) {
        return errors.New("generatedCharacter cannot be negative");
    }
    if (sourceIndex < 0 || (nint)sourceIndex >= len(gen.sources)) {
        return errors.New("sourceIndex is out of range");
    }
    if (sourceLine < 0) {
        return errors.New("sourceLine cannot be negative");
    }
    if (sourceCharacter < 0) {
        return errors.New("sourceCharacter cannot be negative");
    }
    if (gen.hasPending && !gen.isNewGeneratedPosition(generatedLine, generatedCharacter) && !gen.hasPendingSource) {
        return default;
    }
    Ꮡgen.addMapping(generatedLine, generatedCharacter, sourceIndex, sourceLine, sourceCharacter, nameIndexNotSet);
    return default;
}

public static error AddNamedSourceMapping(this ж<Generator> Ꮡgen, nint generatedLine, core.UTF16Offset generatedCharacter, SourceIndex sourceIndex, nint sourceLine, core.UTF16Offset sourceCharacter, NameIndex nameIndex) {
    ref var gen = ref Ꮡgen.DerefOrNull();

    if (generatedLine < gen.pendingGeneratedLine) {
        return errors.New("generatedLine cannot backtrack");
    }
    if (generatedCharacter < 0) {
        return errors.New("generatedCharacter cannot be negative");
    }
    if (sourceIndex < 0 || (nint)sourceIndex >= len(gen.sources)) {
        return errors.New("sourceIndex is out of range");
    }
    if (sourceLine < 0) {
        return errors.New("sourceLine cannot be negative");
    }
    if (sourceCharacter < 0) {
        return errors.New("sourceCharacter cannot be negative");
    }
    if (nameIndex < 0 || (nint)nameIndex >= len(gen.names)) {
        return errors.New("nameIndex is out of range");
    }
    if (gen.hasPending && !gen.isNewGeneratedPosition(generatedLine, generatedCharacter) && !gen.hasPendingSource) {
        return default;
    }
    Ꮡgen.addMapping(generatedLine, generatedCharacter, sourceIndex, sourceLine, sourceCharacter, nameIndex);
    return default;
}

public static ж<RawSourceMap> RawSourceMap(this ж<Generator> Ꮡgen) {
    ref var gen = ref Ꮡgen.DerefOrNull();

    Ꮡgen.commitPendingMapping();
    var sources = slices.Clone<slice<@string>, @string>(gen.sources);
    if (sources == nil) {
        sources = [];
    }
    var names = slices.Clone<slice<@string>, @string>(gen.names);
    if (names == nil) {
        names = [];
    }
    return Ꮡ(new RawSourceMap(
        Version: 3,
        File: gen.@file,
        SourceRoot: gen.sourceRoot,
        Sources: sources,
        Names: names,
        Mappings: gen.mappings.String(),
        SourcesContent: slices.Clone<slice<ж<@string>>, ж<@string>>(gen.sourcesContent)
    ));
}

internal static slice<byte> bytes(this ж<Generator> Ꮡgen) {
    var (buf, err) = json.Marshal(Ꮡgen.RawSourceMap().OrTypedNil());
    if (err != nil) {
        throw panic(err.Error());
    }
    return buf;
}

public static @string String(this ж<Generator> Ꮡgen) {
    return (@string)Ꮡgen.bytes();
}

public static @string Base64DataURL(this ж<Generator> Ꮡgen) {
    @string prefix = "data:application/json;base64,";
    var data = Ꮡgen.bytes();
    ref var sb = ref heap(new strings.Builder(), out var Ꮡsb);
    Ꮡsb.Grow(len(prefix) + base64.StdEncoding.EncodedLen(len(data)));
    Ꮡsb.WriteString(prefix);
    var encoder = base64.NewEncoder(base64.StdEncoding, new strings_BuilderжWriter(Ꮡsb));
    (_, _) = encoder.Write(data);
    encoder.Close();
    return sb.String();
}

} partial class sourcemap_package {

internal static rune base64FormatEncode(nint value) {
    switch (ᐧ) {
    case {} when value >= 0 && value < 26: {
        return 'A' + (rune)value;
    }
    case {} when value >= 26 && value < 52: {
        return 'a' + (rune)value - 26;
    }
    case {} when value >= 52 && value < 62: {
        return '0' + (rune)value - 52;
    }
    case {} when value is 62: {
        return '+';
    }
    case {} when value is 63: {
        return '/';
    }
    default: {
        throw panic("not a base64 value");
        break;
    }}

}

} // end sourcemap_package
