namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using fmt = fmt_package;
using slices = slices_package;
using sort = sort_package;
using sync = sync_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using json = go.github.com.microsoft.TypeScript.tsc.@internal.json_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using static spanmap_package;

partial class spanmap_package {

partial struct Kind /*num:int32*/;

public static Kind KindVerbatim => /* iota */ 0;
public static Kind KindAtom => 1;
public static Kind KindAlias => 2;

partial struct Feature /*num:int32*/;

public static Feature FeatureHover => /* 1 << iota */ 1;
public static Feature FeatureSignatureHelp => 2;
public static Feature FeatureCompletion => 4;
public static Feature FeatureDefinition => 8;
public static Feature FeatureTypeDefinition => 16;
public static Feature FeatureImplementation => 32;
public static Feature FeatureReferences => 64;
public static Feature FeatureDocumentHighlights => 128;
public static Feature FeatureRename => 256;
public static Feature FeatureCallHierarchy => 512;
public static Feature FeatureCodeActions => 1024;
public static Feature FeatureFormatting => 2048;
public static Feature FeatureInlayHints => 4096;
public static Feature FeatureSemanticTokens => 8192;
public static Feature FeatureFoldingRanges => 16384;
public static Feature FeatureSelectionRanges => 32768;
public static Feature FeatureLinkedEditing => 65536;
public static Feature FeatureAutoInsert => 131072;
public static Feature FeatureDocumentSymbols => 262144;
public static Feature FeatureCodeLens => 524288;
public static Feature FeatureNone => 0;
public static Feature FeatureAll => /* (FeatureCodeLens << 1) - 1 */ 1048575;

internal static Feature featureMask => /* FeatureAll */ 1048575;

partial struct Fidelity /*num:int32*/;

public static Fidelity FidelityExact => /* iota */ 0;
public static Fidelity FidelityAtom => 1;
public static Fidelity FidelityApproximate => 2;
public static Fidelity FidelityNone => 3;

} partial class spanmapᴛFidelity {

public static bool IsExact(this Fidelity f) {
    return f == FidelityExact;
}

public static bool IsSingleSegment(this Fidelity f) {
    return f == FidelityExact || f == FidelityAtom;
}

public static bool IsNone(this Fidelity f) {
    return f == FidelityNone;
}

} partial class spanmap_package {

partial struct Segment {
    public core.TextPos VirtualStart;
    public core.TextPos VirtualEnd;
    public core.TextPos OriginalStart;
    public core.TextPos OriginalEnd;
    public Kind Kind;
    public Feature Features;
}

partial struct MappedPosition {
    public core.TextPos Position;
    public Fidelity Fidelity;
}

partial struct MappedSpan {
    public core.TextRange Span;
    public Fidelity Fidelity;
}

partial struct SpanMap {
    internal slice<Segment> segments;
    internal sync.Once origOnce;
    internal ж<originalIndex> originalIndex;
}

partial struct MappingErrorKind /*num:nint*/;

public static MappingErrorKind MappingErrorKindOverlap => /* iota */ 0;
public static MappingErrorKind MappingErrorKindOutOfBounds => 1;
public static MappingErrorKind MappingErrorKindVerbatimMismatch => 2;
public static MappingErrorKind MappingErrorKindKind => 3;
public static MappingErrorKind MappingErrorKindFeature => 4;

partial struct MappingError {
    public MappingErrorKind Kind;
    public core.TextPos VirtualPos;
    public core.TextPos OriginalPos;
}

} partial class spanmapᴛMappingError {

public static @string Error(this ref MappingError p) {
    switch (p.Kind) {
    case var ᴛ when ᴛ == MappingErrorKindOverlap: {
        return fmt.Sprintf("content mapper position mappings overlap or are out of order near virtual offset %d"u8, p.VirtualPos);
    }
    case var ᴛ when ᴛ == MappingErrorKindOutOfBounds: {
        return fmt.Sprintf("content mapper position mapping points outside the original content at original offset %d"u8, p.OriginalPos);
    }
    case var ᴛ when ᴛ == MappingErrorKindVerbatimMismatch: {
        return fmt.Sprintf("content mapper verbatim mapping does not match the original content at virtual offset %d, original offset %d"u8, p.VirtualPos, p.OriginalPos);
    }
    case var ᴛ when ᴛ == MappingErrorKindKind: {
        return fmt.Sprintf("content mapper position mapping has an invalid kind at virtual offset %d"u8, p.VirtualPos);
    }
    case var ᴛ when ᴛ == MappingErrorKindFeature: {
        return fmt.Sprintf("content mapper position mappings have invalid features near original offset %d"u8, p.OriginalPos);
    }
    default: {
        return "content mapper produced an invalid position mapping";
    }}

}

} partial class spanmapᴛSpanMap {

public static ж<MappingError> Validate(this ж<SpanMap> Ꮡm, @string @virtual, @string original) {
    ref var m = ref Ꮡm.DerefOrNull();

    if (Ꮡm == nil) {
        return default;
    }
    var virtualLen = (core.TextPos)(int32)len(@virtual);
    var origLen = (core.TextPos)(int32)len(original);
    core.TextPos previousVirtualEnd = default;
    foreach (var i in range(len(m.segments))) {
        var s = Ꮡ(m.segments, i);
        if (s.VirtualStart < previousVirtualEnd || s.VirtualEnd < s.VirtualStart || s.VirtualEnd > virtualLen) {
            return Ꮡ(new MappingError(Kind: MappingErrorKindOverlap, VirtualPos: s.VirtualStart));
        }
        previousVirtualEnd = s.VirtualEnd;
        if (s.OriginalStart < 0 || s.OriginalEnd < s.OriginalStart || s.OriginalEnd > origLen) {
            return Ꮡ(new MappingError(Kind: MappingErrorKindOutOfBounds, VirtualPos: s.VirtualStart, OriginalPos: s.OriginalEnd));
        }
        if (s.Kind != KindVerbatim && s.Kind != KindAtom && s.Kind != KindAlias) {
            return Ꮡ(new MappingError(Kind: MappingErrorKindKind, VirtualPos: s.VirtualStart, OriginalPos: s.OriginalStart));
        }
        if (s.Kind == KindVerbatim) {
            if (s.VirtualEnd - s.VirtualStart != s.OriginalEnd - s.OriginalStart || @virtual.slice(s.VirtualStart, s.VirtualEnd) != original.slice(s.OriginalStart, s.OriginalEnd)) {
                return Ꮡ(new MappingError(Kind: MappingErrorKindVerbatimMismatch, VirtualPos: s.VirtualStart, OriginalPos: s.OriginalStart));
            }
        }
        if ((Feature)(s.Features & ~featureMask) != 0) {
            return Ꮡ(new MappingError(Kind: MappingErrorKindFeature, VirtualPos: s.VirtualStart, OriginalPos: s.OriginalStart));
        }
    }
    return default;
}

} partial class spanmap_package {

public static ж<SpanMap> New(slice<Segment> segments) {
    var sorted = slices.Clone<slice<Segment>, Segment>(segments);
    slices.SortFunc(sorted, (Segment a, Segment b) => (nint)(a.VirtualStart - b.VirtualStart));
    return Ꮡ(new SpanMap(segments: sorted));
}

} partial class spanmapᴛSpanMap {

public static slice<Segment> Segments(this ж<SpanMap> Ꮡm) {
    ref var m = ref Ꮡm.DerefOrNull();

    if (Ꮡm == nil) {
        return default;
    }
    return slices.Clone<slice<Segment>, Segment>(m.segments);
}

public static (core.TextRange, Fidelity) VirtualToOriginalSpan(this ж<SpanMap> Ꮡm, core.TextRange r) {
    ref var m = ref Ꮡm.DerefOrNull();

    if (Ꮡm == nil) {
        return (r, FidelityExact);
    }
    var virtualStart = (core.TextPos)(int32)r.Pos();
    var virtualEnd = max((core.TextPos)(int32)r.End(), virtualStart);
    if (virtualStart == virtualEnd) {
        var (position, fidelity) = Ꮡm.VirtualToOriginalPosition(virtualStart);
        return (core.NewTextRange((nint)position, (nint)position), fidelity);
    }

    var (startIdx, startIn) = m.segmentIndexAt(virtualStart);
    var endProbe = virtualEnd - 1;
    var (endIdx, endIn) = m.segmentIndexAt(endProbe);

    if (startIdx == endIdx && startIn == endIn) {
        if (startIn) {
            var seg = Ꮡ(m.segments, startIdx);
            if (seg.Kind == KindVerbatim) {
                var origStartΔ1 = clamp(seg.OriginalStart + (virtualStart - seg.VirtualStart), seg.OriginalStart, seg.OriginalEnd);
                var origEndΔ1 = clamp(seg.OriginalStart + (virtualEnd - seg.VirtualStart), origStartΔ1, seg.OriginalEnd);
                return (core.NewTextRange((nint)origStartΔ1, (nint)origEndΔ1), FidelityExact);
            }
            return (core.NewTextRange((nint)seg.OriginalStart, (nint)seg.OriginalEnd), FidelityAtom);
        }
        var pos = m.insertionPoint(startIdx);
        return (core.NewTextRange((nint)pos, (nint)pos), FidelityNone);
    }

    var origStart = m.mapLow(virtualStart, startIdx, startIn);
    var origEnd = max(m.mapHigh(virtualEnd, endIdx, endIn), origStart);
    return (core.NewTextRange((nint)origStart, (nint)origEnd), FidelityApproximate);
}

public static (core.TextRange, Fidelity) VirtualToOriginalSpanForFeature(this ж<SpanMap> Ꮡm, core.TextRange r, Feature feature) {
    ref var m = ref Ꮡm.DerefOrNull();

    var (mapped, fidelity) = Ꮡm.VirtualToOriginalSpan(r);
    if (Ꮡm == nil || m.virtualSpanSupportsFeature(r, feature)) {
        return (mapped, fidelity);
    }
    return (mapped, FidelityNone);
}

internal static bool virtualSpanSupportsFeature(this ref SpanMap m, core.TextRange r, Feature feature) {
    var start = (core.TextPos)(int32)r.Pos();
    var end = max((core.TextPos)(int32)r.End(), start);
    if (start == end) {
        var (indexΔ1, insideΔ1) = m.segmentIndexAt(start);
        return insideΔ1 && supportsFeature(m.segments[indexΔ1], feature);
    }
    var (index, inside) = m.segmentIndexAt(start);
    if (!inside) {
        return false;
    }
    var coveredThrough = start;
    while (index < len(m.segments) && coveredThrough < end) {
        var segment = m.segments[index];
        if (segment.VirtualStart > coveredThrough || segment.VirtualEnd <= coveredThrough || !supportsFeature(segment, feature)) {
            return false;
        }
        coveredThrough = segment.VirtualEnd;
        index++;
    }
    return coveredThrough >= end;
}

public static (core.TextPos, Fidelity) VirtualToOriginalPosition(this ж<SpanMap> Ꮡm, core.TextPos pos) {
    ref var m = ref Ꮡm.DerefOrNull();

    if (Ꮡm == nil) {
        return (pos, FidelityExact);
    }
    var (idx, @in) = m.segmentIndexAt(pos);
    if (!@in) {
        return (m.insertionPoint(idx), FidelityNone);
    }
    var seg = Ꮡ(m.segments, idx);
    if (seg.Kind == KindVerbatim) {
        return (clamp(seg.OriginalStart + (pos - seg.VirtualStart), seg.OriginalStart, seg.OriginalEnd), FidelityExact);
    }
    return (seg.OriginalStart, FidelityAtom);
}

public static (core.TextPos, bool) VirtualToOriginalPositionExact(this ж<SpanMap> Ꮡm, core.TextPos pos) {
    ref var m = ref Ꮡm.DerefOrNull();

    var (mapped, fidelity) = Ꮡm.VirtualToOriginalPosition(pos);
    if (fidelity != FidelityExact || Ꮡm == nil) {
        return (mapped, fidelity == FidelityExact);
    }
    var (index, inside) = m.segmentIndexAt(pos);
    if (!inside || m.segments[index].Kind != KindVerbatim) {
        return (mapped, false);
    }
    if (index > 0) {
        var previous = m.segments[index - 1];
        if (previous.VirtualEnd == pos && (previous.Kind != KindVerbatim || previous.OriginalEnd != m.segments[index].OriginalStart)) {
            return (mapped, false);
        }
    }
    return (mapped, true);
}

public static (core.TextPos, Fidelity) VirtualToOriginalPositionForFeature(this ж<SpanMap> Ꮡm, core.TextPos pos, Feature feature) {
    ref var m = ref Ꮡm.DerefOrNull();

    var (mapped, fidelity) = Ꮡm.VirtualToOriginalPosition(pos);
    if (Ꮡm == nil) {
        return (mapped, fidelity);
    }
    var (index, inside) = m.segmentIndexAt(pos);
    if (!inside || !supportsFeature(m.segments[index], feature)) {
        return (mapped, FidelityNone);
    }
    return (mapped, fidelity);
}

public static (Segment, bool) AliasForVirtualSpan(this ж<SpanMap> Ꮡm, core.TextRange r) {
    ref var m = ref Ꮡm.DerefOrNull();

    if (Ꮡm == nil) {
        return (new Segment(), false);
    }
    var (index, inside) = m.segmentIndexAt((core.TextPos)(int32)r.Pos());
    if (!inside) {
        return (new Segment(), false);
    }
    var segment = m.segments[index];
    return (segment, segment.Kind == KindAlias && r.Pos() == (nint)segment.VirtualStart && r.End() == (nint)segment.VirtualEnd);
}

internal static (nint, bool) segmentIndexAt(this ref SpanMap m, core.TextPos pos) {
    var (idx, found) = slices.BinarySearchFunc(m.segments, pos, (Segment s, core.TextPos p) => (nint)(s.VirtualStart - p));
    if (found) {
        return (idx, true);
    }
    nint prev = idx - 1;
    if (prev >= 0 && (pos < m.segments[prev].VirtualEnd || prev == len(m.segments) - 1 && pos == m.segments[prev].VirtualEnd)) {
        return (prev, true);
    }
    return (prev, false);
}

internal static core.TextPos insertionPoint(this ref SpanMap m, nint prev) {
    if (prev < 0) {
        return 0;
    }
    return m.segments[prev].OriginalEnd;
}

internal static core.TextPos mapLow(this ref SpanMap m, core.TextPos pos, nint idx, bool @in) {
    if (!@in) {
        return m.insertionPoint(idx);
    }
    var seg = Ꮡ(m.segments, idx);
    if (seg.Kind == KindVerbatim) {
        return clamp(seg.OriginalStart + (pos - seg.VirtualStart), seg.OriginalStart, seg.OriginalEnd);
    }
    return seg.OriginalStart;
}

internal static core.TextPos mapHigh(this ref SpanMap m, core.TextPos pos, nint idx, bool @in) {
    if (!@in) {
        return m.insertionPoint(idx);
    }
    var seg = Ꮡ(m.segments, idx);
    if (seg.Kind == KindVerbatim) {
        return clamp(seg.OriginalStart + (pos - seg.VirtualStart), seg.OriginalStart, seg.OriginalEnd);
    }
    return seg.OriginalEnd;
}

public static slice<MappedPosition> OriginalToVirtualPositions(this ж<SpanMap> Ꮡm, core.TextPos pos, Feature feature) {
    if (Ꮡm == nil) {
        return [new(Position: pos, Fidelity: FidelityExact)];
    }
    var groups = Ꮡm.origIndex().segmentGroupsAtOriginalPosition(pos);
    if (len(groups) == 0) {
        return default;
    }
    slice<MappedPosition> results = default;
    foreach (var (_, group) in range(groups)) {
        foreach (var (_, segment) in range(group.segments)) {
            if (!supportsFeature(segment, feature)) {
                continue;
            }
            var mapped = new MappedPosition(Fidelity: FidelityAtom);
            if (segment.Kind == KindVerbatim) {
                mapped.Position = clamp(segment.VirtualStart + (pos - segment.OriginalStart), segment.VirtualStart, segment.VirtualEnd);
                mapped.Fidelity = FidelityExact;
            } else if (group.atEnd) {
                mapped.Position = segment.VirtualEnd;
            } else {
                mapped.Position = segment.VirtualStart;
            }
            if (!slices.Contains(results, mapped)) {
                results = append(results, mapped);
            }
        }
    }
    slices.SortFunc(results, (MappedPosition a, MappedPosition b) => (nint)(a.Position - b.Position));
    return results;
}

public static slice<MappedSpan> OriginalToVirtualSpans(this ж<SpanMap> Ꮡm, core.TextRange r, Feature feature) {
    var containingᴷ = new stackAppend<Segment>();

    if (Ꮡm == nil) {
        return [new(Span: r, Fidelity: FidelityExact)];
    }
    var start = (core.TextPos)(int32)r.Pos();
    var end = max((core.TextPos)(int32)r.End(), start);
    if (start == end) {
        return core.Map(Ꮡm.OriginalToVirtualPositions(start, feature), MappedSpan (MappedPosition position) => new MappedSpan(
                Span: core.NewTextRange((nint)position.Position, (nint)position.Position),
                Fidelity: position.Fidelity
            ));
    }
    var lastCharacter = end - 1;
    var originalIndex = Ꮡm.origIndex();
    var (startSegments, startInside) = originalIndex.segmentsAtOriginalPosition(start);
    var (endSegments, endInside) = originalIndex.segmentsAtOriginalPosition(lastCharacter);
    if (!startInside || !endInside) {
        return default;
    }
    slice<Segment> containing = default;
    foreach (var (_, segment) in range(startSegments)) {
        if (end <= segment.OriginalEnd) {
            containing = append(containingᴷ, containing, segment);
        }
    }
    if (len(containing) > 0) {
        var resultsΔ1 = originalToVirtualSpansInSegments(containing, start, end, feature);
        if (len(resultsΔ1) > 0) {
            slices.SortFunc(resultsΔ1, (MappedSpan a, MappedSpan b) => a.Span.Pos() - b.Span.Pos());
            return resultsΔ1;
        }
    }
    var starts = originalStartProjections(startSegments, start, feature);
    var ends = originalEndProjections(endSegments, end, feature);
    if (len(starts) == 0 || len(ends) == 0) {
        return default;
    }
    slices.Sort<slice<core.TextPos>, core.TextPos>(starts);
    slices.Sort<slice<core.TextPos>, core.TextPos>(ends);
    var results = new slice<MappedSpan>(0, min(len(starts), len(ends)));
    foreach (var (i, virtualStart) in range(starts)) {
        var (endIndex, _) = slices.BinarySearch(ends, virtualStart);
        if (endIndex == len(ends) || i + 1 < len(starts) && starts[i + 1] <= ends[endIndex]) {
            continue;
        }
        results = append(results, new MappedSpan(
            Span: core.NewTextRange((nint)virtualStart, (nint)ends[endIndex]),
            Fidelity: FidelityApproximate
        ));
    }
    return results;
}

public static slice<MappedSpan> OriginalToVirtualIntersectingSpans(this ж<SpanMap> Ꮡm, core.TextRange r, Feature feature) {
    ref var m = ref Ꮡm.DerefOrNull();

    if (Ꮡm == nil) {
        return [new(Span: r, Fidelity: FidelityExact)];
    }
    if (r.Pos() == r.End()) {
        return Ꮡm.OriginalToVirtualSpans(r, feature);
    }
    var results = new slice<MappedSpan>(0);
    foreach (var (_, segment) in range(m.segments)) {
        if (!supportsFeature(segment, feature)) {
            continue;
        }
        var start = max((core.TextPos)(int32)r.Pos(), segment.OriginalStart);
        var end = min((core.TextPos)(int32)r.End(), segment.OriginalEnd);
        if (start >= end) {
            continue;
        }
        if (segment.Kind == KindVerbatim) {
            results = append(results, new MappedSpan(
                Span: core.NewTextRange(
                    (nint)(segment.VirtualStart + (start - segment.OriginalStart)),
                    (nint)(segment.VirtualStart + (end - segment.OriginalStart))),
                Fidelity: FidelityExact
            ));
        } else {
            results = append(results, new MappedSpan(
                Span: core.NewTextRange((nint)segment.VirtualStart, (nint)segment.VirtualEnd),
                Fidelity: FidelityAtom
            ));
        }
    }
    return results;
}

} partial class spanmap_package {

internal static slice<core.TextPos> originalStartProjections(slice<Segment> segments, core.TextPos start, Feature feature) {
    var results = new slice<core.TextPos>(0, len(segments));
    foreach (var (_, segment) in range(segments)) {
        if (!supportsFeature(segment, feature)) {
            continue;
        }
        if (segment.Kind == KindVerbatim) {
            results = append(results, clamp(segment.VirtualStart + (start - segment.OriginalStart), segment.VirtualStart, segment.VirtualEnd));
        } else {
            results = append(results, segment.VirtualStart);
        }
    }
    return results;
}

internal static slice<core.TextPos> originalEndProjections(slice<Segment> segments, core.TextPos end, Feature feature) {
    var results = new slice<core.TextPos>(0, len(segments));
    foreach (var (_, segment) in range(segments)) {
        if (!supportsFeature(segment, feature)) {
            continue;
        }
        if (segment.Kind == KindVerbatim) {
            results = append(results, clamp(segment.VirtualStart + (end - segment.OriginalStart), segment.VirtualStart, segment.VirtualEnd));
        } else {
            results = append(results, segment.VirtualEnd);
        }
    }
    return results;
}

internal static slice<MappedSpan> originalToVirtualSpansInSegments(slice<Segment> segments, core.TextPos start, core.TextPos end, Feature feature) {
    var results = new slice<MappedSpan>(0, len(segments));
    foreach (var (_, segment) in range(segments)) {
        if (!supportsFeature(segment, feature)) {
            continue;
        }
        if (segment.Kind == KindVerbatim) {
            var virtualStart = clamp(segment.VirtualStart + (start - segment.OriginalStart), segment.VirtualStart, segment.VirtualEnd);
            var virtualEnd = clamp(segment.VirtualStart + (end - segment.OriginalStart), virtualStart, segment.VirtualEnd);
            results = append(results, new MappedSpan(Span: core.NewTextRange((nint)virtualStart, (nint)virtualEnd), Fidelity: FidelityExact));
        } else {
            results = append(results, new MappedSpan(Span: core.NewTextRange((nint)segment.VirtualStart, (nint)segment.VirtualEnd), Fidelity: FidelityAtom));
        }
    }
    return results;
}

internal static bool sameOriginalRange(Segment left, Segment right) {
    return left.OriginalStart == right.OriginalStart && left.OriginalEnd == right.OriginalEnd;
}

partial struct originalIndex {
    internal slice<Segment> segments;
    internal nint leafCount;
    internal slice<core.TextPos> maxEnds;
}

} partial class spanmapᴛSpanMap {

internal static ж<originalIndex> origIndex(this ж<SpanMap> Ꮡm) {
    ref var m = ref Ꮡm.DerefOrNull();

    Ꮡm.ᏑorigOnce.Do(() => {
        var segments = slices.Clone<slice<Segment>, Segment>(Ꮡm.Value.segments);
        slices.SortFunc(segments, (Segment a, Segment b) => {
            {
                nint c = (nint)(a.OriginalStart - b.OriginalStart); if (c != 0) {
                    return c;
                }
            }
            {
                nint c = (nint)(a.OriginalEnd - b.OriginalEnd); if (c != 0) {
                    return c;
                }
            }
            return (nint)(a.VirtualStart - b.VirtualStart);
        });
        ref var leafCount = ref heap<nint>(1, out var ᏑleafCount);
        while (leafCount < len(segments)) {
            leafCount *= 2;
        }
        var maxEnds = new slice<core.TextPos>(2 * leafCount);
        foreach (var (i, segment) in range(segments)) {
            maxEnds[leafCount + i] = segment.OriginalEnd;
        }
        for (nint i = leafCount - 1; i > 0; i--) {
            maxEnds[i] = max(maxEnds[2 * i], maxEnds[2 * i + 1]);
        }
        Ꮡm.Value.originalIndex = Ꮡ(new originalIndex(segments: segments, leafCount: leafCount, maxEnds: maxEnds));
    });
    return m.originalIndex;
}

} partial class spanmapᴛoriginalIndex {

internal static (slice<Segment>, bool) segmentsAtOriginalPosition(this ж<originalIndex> Ꮡi, core.TextPos pos) {
    ref var i = ref Ꮡi.DerefOrNull();

    nint start = sort.Search(len(i.segments), (nint index) => Ꮡi.Value.segments[index].OriginalStart >= pos);
    var results = i.segmentsEndingAfterPosition(start, pos);
    nint end = sort.Search(len(i.segments), (nint index) => Ꮡi.Value.segments[index].OriginalStart > pos);
    results = appendꓸꓸꓸ(results, i.segments.slice(start, end));
    return (results, len(results) > 0);
}

internal static slice<Segment> segmentsEndingAfterPosition(this ref originalIndex i, nint limit, core.TextPos pos) {
    ref var results = ref heap<slice<Segment>>(out var Ꮡresults);
    i.collectSegmentsEndingAtOrAfter(1, 0, i.leafCount, limit, pos, false, Ꮡresults);
    return results;
}

internal static void collectSegmentsEndingAtOrAfter(this ref originalIndex i, nint node, nint start, nint end, nint limit, core.TextPos pos, bool includeEnd, ж<slice<Segment>> Ꮡresults) {
    ref var results = ref Ꮡresults.DerefOrNull();

    if (start >= limit || i.maxEnds[node] < pos || !includeEnd && i.maxEnds[node] == pos) {
        return;
    }
    if (end - start == 1) {
        results = append(results, i.segments[start]);
        return;
    }
    nint middle = start + (end - start) / 2;
    i.collectSegmentsEndingAtOrAfter(2 * node, start, middle, limit, pos, includeEnd, Ꮡresults);
    i.collectSegmentsEndingAtOrAfter(2 * node + 1, middle, end, limit, pos, includeEnd, Ꮡresults);
}

} partial class spanmap_package {

partial struct segmentGroupAtOriginalPosition {
    internal slice<Segment> segments;
    internal bool atEnd;
}

} partial class spanmapᴛoriginalIndex {

internal static slice<segmentGroupAtOriginalPosition> segmentGroupsAtOriginalPosition(this ж<originalIndex> Ꮡi, core.TextPos pos) {
    ref var i = ref Ꮡi.DerefOrNull();
    var groupsᴷ = new stackAppend<segmentGroupAtOriginalPosition>(moves: true);

    nint limit = sort.Search(len(i.segments), (nint index) => Ꮡi.Value.segments[index].OriginalStart > pos);
    ref var segments = ref heap<slice<Segment>>(out var Ꮡsegments);
    i.collectSegmentsEndingAtOrAfter(1, 0, i.leafCount, limit, pos, true, Ꮡsegments);
    slice<segmentGroupAtOriginalPosition> groups = default;
    for (nint start = 0; start < len(segments); ) {
        nint end = start + 1;
        while (end < len(segments) && sameOriginalRange(segments[start], segments[end])) {
            end++;
        }
        var segment = segments[start];
        if (pos <= segment.OriginalEnd) {
            groups = append(groupsᴷ, groups, new segmentGroupAtOriginalPosition(
                segments: segments.slice(start, end),
                atEnd: pos == segment.OriginalEnd && pos != segment.OriginalStart
            ));
        }
        start = end;
    }
    groups = move2heap(groupsᴷ, groups);
    return groups;
}

} partial class spanmap_package {

internal static bool supportsFeature(Segment segment, Feature feature) {
    return (Feature)(segment.Features & feature) != 0;
}

internal static core.TextPos clamp(core.TextPos v, core.TextPos lo, core.TextPos hi) {
    return max(lo, min(v, hi));
}

public static (ж<SpanMap>, error) Unmarshal(slice<byte> data) {
    ref var tuples = ref heap<slice<slice<int32>>>(out var Ꮡtuples);
    var err = json.Unmarshal(data, Ꮡtuples); if (err != nil) {
        return (default, err);
    }
    var segments = new slice<Segment>(len(tuples));
    foreach (var (i, t) in range(tuples)) {
        if (len(t) != 5 && len(t) != 6) {
            return (default, fmt.Errorf("span map segment %d: expected 5 or 6 values, got %d"u8, i, len(t)));
        }
        segments[i] = new Segment(
            VirtualStart: ((core.TextPos)t[0]),
            VirtualEnd: ((core.TextPos)(t[0] + t[1])),
            OriginalStart: ((core.TextPos)t[2]),
            OriginalEnd: ((core.TextPos)(t[2] + t[3])),
            Kind: ((Kind)t[4]),
            Features: FeatureAll
        );
        if (len(t) == 6) {
            segments[i].Features = (Feature)t[5];
        }
    }
    return (New(segments), default);
}

} partial class spanmapᴛSpanMap {

public static (slice<byte>, error) Marshal(this ref SpanMap m) {
    var tuples = new slice<slice<int32>>(len(m.segments));
    foreach (var (i, s) in range(m.segments)) {
        tuples[i] = [
            (int32)s.VirtualStart,
            (int32)(s.VirtualEnd - s.VirtualStart),
            (int32)s.OriginalStart,
            (int32)(s.OriginalEnd - s.OriginalStart),
            (int32)s.Kind
        ];
        if (s.Features != FeatureAll) {
            tuples[i] = append(tuples[i], (int32)s.Features);
        }
    }
    return json.Marshal(tuples);
}

} // end spanmapᴛSpanMap
