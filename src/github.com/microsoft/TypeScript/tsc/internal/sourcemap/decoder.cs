namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using errors = errors_package;
using iter = iter_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static sourcemap_package;

partial class sourcemap_package {

partial struct Mapping {
    public nint GeneratedLine;
    public core.UTF16Offset GeneratedCharacter;
    public SourceIndex SourceIndex;
    public nint SourceLine;
    public core.UTF16Offset SourceCharacter;
    public NameIndex NameIndex;
}

} partial class sourcemapᴛMapping {

public static bool ΔEquals(this ж<Mapping> Ꮡm, ж<Mapping> Ꮡother) {
    ref var m = ref Ꮡm.DerefOrNull();
    ref var other = ref Ꮡother.DerefOrNull();

    return Ꮡm == Ꮡother || m.GeneratedLine == other.GeneratedLine && m.GeneratedCharacter == other.GeneratedCharacter && m.SourceIndex == other.SourceIndex && m.SourceLine == other.SourceLine && m.SourceCharacter == other.SourceCharacter && m.NameIndex == other.NameIndex;
}

public static bool IsSourceMapping(this ref Mapping m) {
    return m.SourceIndex != MissingSource && m.SourceLine != MissingLineOrColumn && m.SourceCharacter != MissingUTF16Column;
}

} partial class sourcemap_package {

public static SourceIndex MissingSource => -1;
public static NameIndex MissingName => -1;
public const nint MissingLineOrColumn = -1;
public static core.UTF16Offset MissingUTF16Column => -1;

partial struct MappingsDecoder {
    internal @string mappings;
    internal bool done;
    internal nint pos;
    internal nint generatedLine;
    internal core.UTF16Offset generatedCharacter;
    internal SourceIndex sourceIndex;
    internal nint sourceLine;
    internal core.UTF16Offset sourceCharacter;
    internal NameIndex nameIndex;
    internal error error;
    internal core.Arena<Mapping> mappingArena;
}

public static ж<MappingsDecoder> DecodeMappings(@string mappings) {
    return Ꮡ(new MappingsDecoder(mappings: mappings));
}

} partial class sourcemapᴛMappingsDecoder {

public static @string MappingsString(this ref MappingsDecoder d) {
    return d.mappings;
}

public static nint Pos(this ref MappingsDecoder d) {
    return d.pos;
}

public static error Error(this ref MappingsDecoder d) {
    return d.error;
}

public static ж<Mapping> State(this ref MappingsDecoder d) {
    return d.captureMapping(true, true);
}

public static iter.Seq<ж<Mapping>> Values(this ж<MappingsDecoder> Ꮡd) {
    return (Func<ж<Mapping>, bool> yield) => {
        for (var (value, done) = Ꮡd.Value.Next(); !done; (value, done) = Ꮡd.Value.Next()) {
            if (!yield(value)) {
                break;
            }
        }
    };
}

public static (ж<Mapping> value, bool done) Next(this ref MappingsDecoder d) {
    while (!d.done && d.pos < len(d.mappings)) {
        var ch = d.mappings[d.pos];
        if (ch == ';') {
            d.generatedLine++;
            d.generatedCharacter = 0;
            d.pos++;
            continue;
        }

        if (ch == ',') {
            d.pos++;
            continue;
        }

        var hasSource = false;
        var hasName = false;
        d.generatedCharacter += ((core.UTF16Offset)d.base64VLQFormatDecode());
        if (d.hasReportedError()) {
            return d.stopIterating();
        }
        if (d.generatedCharacter < 0) {
            return d.setErrorAndStopIterating("Invalid generatedCharacter found");
        }

        if (!d.isSourceMappingSegmentEnd()) {
            hasSource = true;

            d.sourceIndex += ((SourceIndex)d.base64VLQFormatDecode());
            if (d.hasReportedError()) {
                return d.stopIterating();
            }
            if (d.sourceIndex < 0) {
                return d.setErrorAndStopIterating("Invalid sourceIndex found");
            }
            if (d.isSourceMappingSegmentEnd()) {
                return d.setErrorAndStopIterating("Unsupported Format: No entries after sourceIndex");
            }

            d.sourceLine += d.base64VLQFormatDecode();
            if (d.hasReportedError()) {
                return d.stopIterating();
            }
            if (d.sourceLine < 0) {
                return d.setErrorAndStopIterating("Invalid sourceLine found");
            }
            if (d.isSourceMappingSegmentEnd()) {
                return d.setErrorAndStopIterating("Unsupported Format: No entries after sourceLine");
            }

            d.sourceCharacter += ((core.UTF16Offset)d.base64VLQFormatDecode());
            if (d.hasReportedError()) {
                return d.stopIterating();
            }
            if (d.sourceCharacter < 0) {
                return d.setErrorAndStopIterating("Invalid sourceCharacter found");
            }

            if (!d.isSourceMappingSegmentEnd()) {
                hasName = true;
                d.nameIndex += ((NameIndex)d.base64VLQFormatDecode());
                if (d.hasReportedError()) {
                    return d.stopIterating();
                }
                if (d.nameIndex < 0) {
                    return d.setErrorAndStopIterating("Invalid nameIndex found");
                }

                if (!d.isSourceMappingSegmentEnd()) {
                    return d.setErrorAndStopIterating("Unsupported Error Format: Entries after nameIndex");
                }
            }
        }

        return (d.captureMapping(hasSource, hasName), false);
    }

    return d.stopIterating();
}

internal static ж<Mapping> captureMapping(this ref MappingsDecoder d, bool hasSource, bool hasName) {
    var mapping = d.mappingArena.New();
    mapping.GeneratedLine = d.generatedLine;
    mapping.GeneratedCharacter = d.generatedCharacter;
    mapping.SourceIndex = core.IfElse(hasSource, d.sourceIndex, MissingSource);
    mapping.SourceLine = core.IfElse(hasSource, d.sourceLine, MissingLineOrColumn);
    mapping.SourceCharacter = core.IfElse(hasSource, d.sourceCharacter, MissingUTF16Column);
    mapping.NameIndex = core.IfElse(hasName, d.nameIndex, MissingName);
    return mapping;
}

internal static (ж<Mapping>, bool) stopIterating(this ref MappingsDecoder d) {
    d.done = true;
    return (default, true);
}

internal static void setError(this ref MappingsDecoder d, @string err) {
    d.error = errors.New(err);
}

internal static (ж<Mapping>, bool) setErrorAndStopIterating(this ref MappingsDecoder d, @string err) {
    d.setError(err);
    return d.stopIterating();
}

internal static bool hasReportedError(this ref MappingsDecoder d) {
    return d.error != nil;
}

internal static bool isSourceMappingSegmentEnd(this ref MappingsDecoder d) {
    return d.pos == len(d.mappings) || d.mappings[d.pos] == ',' || d.mappings[d.pos] == ';';
}

internal static nint base64VLQFormatDecode(this ref MappingsDecoder d) {
    var moreDigits = true;
    nint shiftCount = 0;
    nint value = 0;
    for (; moreDigits; d.pos++) {
        if (d.pos >= len(d.mappings)) {
            d.setError("Error in decoding base64VLQFormatDecode, past the mapping string");
            return -1;
        }

        nint currentByte = base64FormatDecode(d.mappings[d.pos]);
        if (currentByte == -1) {
            d.setError("Invalid character in VLQ");
            return -1;
        }

        moreDigits = (currentByte & 32) != 0;

        value = value | ((currentByte & 31).Lsh((int64)(shiftCount)));
        shiftCount += 5;
    }

    if ((value & 1) == 0) {
        value = value >> 1;
    } else {
        value = value >> 1;
        value = -value;
    }

    return value;
}

} partial class sourcemap_package {

internal static nint base64FormatDecode(byte ch) {
    switch (ᐧ) {
    case {} when ch >= (byte)'A' && ch <= (byte)'Z': {
        return (nint)((byte)(ch - 'A'));
    }
    case {} when ch >= (byte)'a' && ch <= (byte)'z': {
        return (nint)((byte)(ch - 'a' + 26));
    }
    case {} when ch >= (byte)'0' && ch <= (byte)'9': {
        return (nint)((byte)(ch - '0' + 52));
    }
    case {} when ch is (byte)'+': {
        return 62;
    }
    case {} when ch is (byte)'/': {
        return 63;
    }
    default: {
        return -1;
    }}

}

} // end sourcemap_package
