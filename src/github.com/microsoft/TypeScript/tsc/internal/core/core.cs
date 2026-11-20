namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using iter = iter_package;
using maps = maps_package;
using math = math_package;
using os = os_package;
using rtdebug = runtime.debug_package;
using slices = slices_package;
using sort = sort_package;
using strconv = strconv_package;
using strings = strings_package;
using sync = sync_package;
using unicode = unicode_package;
using utf16 = go.unicode.utf16_package;
using utf8 = go.unicode.utf8_package;
using debug = go.github.com.microsoft.TypeScript.tsc.@internal.debug_package;
using json = go.github.com.microsoft.TypeScript.tsc.@internal.json_package;
using stringutil = go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.unicode;
using ꓸꓸꓸany = Span<any>;

partial class core_package {

public static void ApplyDebugStackLimit() {
    @string v = os.Getenv("TS_GO_DEBUG_STACK_LIMIT");
    if (v == ""u8) {
        return;
    }
    var (n, err) = strconv.Atoi(v);
    if (err != nil || n <= 0) {
        return;
    }
    rtdebug.SetMaxStack(n);
}

public static slice<T> Filter<T>(slice<T> Δslice, Func<T, bool> f) {
    foreach (var (iᴛ1, vᴛ1) in range(Δslice)) {
        var i = iᴛ1;
        var value = vᴛ1;

        if (!f(value)) {
            var result = slices.Clone<slice<T>, T>(Δslice.slice(0, i));
            for (i++; i < len(Δslice); i++) {
                value = Δslice[i];
                if (f(value)) {
                    result = append(result, value);
                }
            }
            return result;
        }
    }
    return Δslice;
}

public static iter.Seq<T> FilterSeq<T>(slice<T> Δslice, Func<T, bool> f) {
    var sliceʗ1 = Δslice;
    return (Func<T, bool> yield) => {
        foreach (var (_, value) in range(sliceʗ1)) {
            if (f(value)) {
                if (!yield(value)) {
                    return;
                }
            }
        }
    };
}

public static slice<T> FilterIndex<T>(slice<T> Δslice, Func<T, nint, slice<T>, bool> f) {
    foreach (var (iᴛ1, vᴛ1) in range(Δslice)) {
        var i = iᴛ1;
        var value = vᴛ1;

        if (!f(value, i, Δslice)) {
            var result = slices.Clone<slice<T>, T>(Δslice.slice(0, i));
            for (i++; i < len(Δslice); i++) {
                value = Δslice[i];
                if (f(value, i, Δslice)) {
                    result = append(result, value);
                }
            }
            return result;
        }
    }
    return Δslice;
}

public static slice<U> Map<T, U>(slice<T> Δslice, Func<T, U> f) {
    if (Δslice == nil) {
        return default;
    }
    var result = new slice<U>(len(Δslice));
    foreach (var (i, value) in range(Δslice)) {
        result[i] = f(value);
    }
    return result;
}

public static (slice<U>, error) TryMap<T, U>(slice<T> Δslice, Func<T, (U, error)> f) {
    if (len(Δslice) == 0) {
        return (default, default);
    }
    var result = new slice<U>(len(Δslice));
    foreach (var (i, value) in range(Δslice)) {
        var (mapped, err) = f(value);
        if (err != nil) {
            return (default, err);
        }
        result[i] = mapped;
    }
    return (result, default);
}

public static slice<U> MapIndex<T, U>(slice<T> Δslice, Func<T, nint, U> f) {
    if (Δslice == nil) {
        return default;
    }
    var result = new slice<U>(len(Δslice));
    foreach (var (i, value) in range(Δslice)) {
        result[i] = f(value, i);
    }
    return result;
}

public static slice<U> MapNonNil<T, U>(slice<T> Δslice, Func<T, U> f) {
    slice<U> result = default;
    foreach (var (_, value) in range(Δslice)) {
        var mapped = f(value);
        if (!AreEqual(mapped, @new<U>().ValueSlot)) {
            result = append(result, mapped);
        }
    }
    return result;
}

public static slice<U> MapFiltered<T, U>(slice<T> Δslice, Func<T, (U, bool)> f) {
    slice<U> result = default;
    foreach (var (_, value) in range(Δslice)) {
        var (mapped, ok) = f(value);
        if (!ok) {
            continue;
        }
        result = append(result, mapped);
    }
    return result;
}

public static slice<U> FlatMap<T, U>(slice<T> Δslice, Func<T, slice<U>> f) {
    slice<U> result = default;
    foreach (var (_, value) in range(Δslice)) {
        var mapped = f(value);
        if (len(mapped) != 0) {
            result = appendꓸꓸꓸ(result, mapped);
        }
    }
    return result;
}

public static slice<T> SameMap<T>(slice<T> Δslice, Func<T, T> f) {
    foreach (var (i, value) in range(Δslice)) {
        var mapped = f(value);
        if (!AreEqual(mapped, value)) {
            var result = new slice<T>(len(Δslice));
            copy(result, Δslice.slice(0, i));
            result[i] = mapped;
            for (nint j = i + 1; j < len(Δslice); j++) {
                result[j] = f(Δslice[j]);
            }
            return result;
        }
    }
    return Δslice;
}

public static slice<T> SameMapIndex<T>(slice<T> Δslice, Func<T, nint, T> f) {
    foreach (var (i, value) in range(Δslice)) {
        var mapped = f(value, i);
        if (!AreEqual(mapped, value)) {
            var result = new slice<T>(len(Δslice));
            copy(result, Δslice.slice(0, i));
            result[i] = mapped;
            for (nint j = i + 1; j < len(Δslice); j++) {
                result[j] = f(Δslice[j], j);
            }
            return result;
        }
    }
    return Δslice;
}

public static bool Same<T>(slice<T> s1, slice<T> s2) {
    if (len(s1) == len(s2)) {
        return len(s1) == 0 || Ꮡelem(s1, 0) == Ꮡelem(s2, 0);
    }
    return false;
}

public static bool Some<T>(slice<T> Δslice, Func<T, bool> f) {
    foreach (var (_, value) in range(Δslice)) {
        if (f(value)) {
            return true;
        }
    }
    return false;
}

public static bool Every<T>(slice<T> Δslice, Func<T, bool> f) {
    foreach (var (_, value) in range(Δslice)) {
        if (!f(value)) {
            return false;
        }
    }
    return true;
}

public static Func<T, bool> Or<T>(params Span<Func<T, bool>> funcsʗp) {
    var funcs = funcsʗp.slice();

    var funcsʗ1 = funcs;
    return (T input) => {
        foreach (var (_, f) in range(funcsʗ1)) {
            if (f(input)) {
                return true;
            }
        }
        return false;
    };
}

public static T Find<T>(slice<T> Δslice, Func<T, bool> f) {
    foreach (var (_, value) in range(Δslice)) {
        if (f(value)) {
            return value;
        }
    }
    return @new<T>().ValueSlot;
}

public static T FindLast<T>(slice<T> Δslice, Func<T, bool> f) {
    for (nint i = len(Δslice) - 1; i >= 0; i--) {
        var value = Δslice[i];
        if (f(value)) {
            return value;
        }
    }
    return @new<T>().ValueSlot;
}

public static nint FindIndex<T>(slice<T> Δslice, Func<T, bool> f) {
    foreach (var (i, value) in range(Δslice)) {
        if (f(value)) {
            return i;
        }
    }
    return -1;
}

public static nint FindLastIndex<T>(slice<T> Δslice, Func<T, bool> f) {
    for (nint i = len(Δslice) - 1; i >= 0; i--) {
        var value = Δslice[i];
        if (f(value)) {
            return i;
        }
    }
    return -1;
}

public static T FirstOrNil<T>(slice<T> Δslice) {
    if (len(Δslice) != 0) {
        return Δslice[0];
    }
    return @new<T>().ValueSlot;
}

public static T LastOrNil<T>(slice<T> Δslice) {
    if (len(Δslice) != 0) {
        return Δslice[len(Δslice) - 1];
    }
    return @new<T>().ValueSlot;
}

public static T ElementOrNil<T>(slice<T> Δslice, nint index) {
    if (index < len(Δslice)) {
        return Δslice[index];
    }
    return @new<T>().ValueSlot;
}

public static T FirstOrNilSeq<T>(iter.Seq<T> seq) {
    if (seq != nil) {
        foreach (var value in range<T>(seq.Invoke)) {
            return value;
        }
    }
    return @new<T>().ValueSlot;
}

public static U FirstNonNil<T, U>(slice<T> Δslice, Func<T, U> f) {
    foreach (var (_, value) in range(Δslice)) {
        var mapped = f(value);
        if (!AreEqual(mapped, @new<U>().ValueSlot)) {
            return mapped;
        }
    }
    return @new<U>().ValueSlot;
}

public static T FirstNonZero<T>(params Span<T> valuesʗp) {
    var values = valuesʗp.sslice();

    T zero = GoZero<T>();
    foreach (var (_, value) in range(values)) {
        if (!AreEqual(value, zero)) {
            return value;
        }
    }
    return zero;
}

public static slice<T> Concatenate<T>(slice<T> s1, slice<T> s2) {
    if (len(s2) == 0) {
        return s1;
    }
    if (len(s1) == 0) {
        return s2;
    }
    return slices.Concat<slice<T>, T>(s1, s2);
}

public static slice<T> Splice<T>(slice<T> s1, nint start, nint deleteCount, params Span<T> itemsʗp) {
    var items = itemsʗp.slice();

    if (start < 0) {
        start = len(s1) + start;
    }
    if (start < 0) {
        start = 0;
    }
    if (start > len(s1)) {
        start = len(s1);
    }
    if (deleteCount < 0) {
        deleteCount = 0;
    }
    nint end = min(start + max(deleteCount, 0), len(s1));
    if (start == end && len(items) == 0) {
        return s1;
    }
    return slices.Concat<slice<T>, T>(s1.slice(0, start), items, s1.slice(end));
}

public static nint CountWhere<T>(slice<T> Δslice, Func<T, bool> f) {
    nint count = 0;
    foreach (var (_, value) in range(Δslice)) {
        if (f(value)) {
            count++;
        }
    }
    return count;
}

public static slice<T> ReplaceElement<T>(slice<T> Δslice, nint i, T t) {
    var result = slices.Clone<slice<T>, T>(Δslice);
    result[i] = t;
    return result;
}

public static slice<T> InsertSorted<T>(slice<T> Δslice, T element, Func<T, T, nint> cmp) {
    var (i, _) = slices.BinarySearchFunc(Δslice, element, cmp);
    return slices.Insert(Δslice, i, element);
}

public static slice<T> MinAllFunc<T>(slice<T> xs, Func<T, T, nint> cmp) {
    if (len(xs) == 0) {
        return default;
    }

    var m = xs[0];
    var mins = slice<T>([m]);

    foreach (var (_, x) in range(xs[1..])) {
        nint c = cmp(x, m);
        switch (ᐧ) {
        case {} when c is < 0: {
            m = x;
            mins = mins[..0];
            mins = append(mins, x);
            break;
        }
        case {} when c is 0: {
            mins = append(mins, x);
            break;
        }}

    }

    return mins;
}

public static slice<T> AppendIfUnique<T>(slice<T> Δslice, T element) {
    if (slices.Contains(Δslice, element)) {
        return Δslice;
    }
    return append(Δslice, element);
}

public static Func<T> Memoize<T>(Func<T> create) {
    ref var value = ref heap<T>(out var Ꮡvalue);
    return () => {
        if (create != nil) {
            Ꮡvalue.ValueSlot = create();
            create = default;
        }
        return Ꮡvalue.ValueSlot;
    };
}

public static T IfElse<T>(bool b, T whenTrue, T whenFalse) {
    if (b) {
        return whenTrue;
    }
    return whenFalse;
}

public static T OrElse<T>(T value, T defaultValue) {
    if (!AreEqual(value, @new<T>().ValueSlot)) {
        return value;
    }
    return defaultValue;
}

public static ж<U> Coalesce<U>(ж<U> a, ж<U> b)
    /* where T : *U (erased: T renders as ж<U>) */
{
    if (a == nil) {
        return b;
    } else {
        return a;
    }
}

partial struct ECMALineStarts /*[]TextPos*/;

public static ECMALineStarts ComputeECMALineStarts(@string text) {
    var result = new slice<TextPos>(0, strings.Count(text, "\n") + 1);
    return slices.AppendSeq(result, ComputeECMALineStartsSeq(text));
}

public static iter.Seq<TextPos> ComputeECMALineStartsSeq(@string text) {
    return (Func<TextPos, bool> yield) => {
        var textLen = (TextPos)(int32)len(text);
        TextPos pos = default;
        TextPos lineStart = default;
        while (pos < textLen) {
            var b = text[pos];
            if (b < utf8.RuneSelf) {
                pos++;
                var matchᴛ1 = false;
                if (b is (byte)'\r') {
                    if (pos < textLen && text[pos] == '\n') {
                        pos++;
                    }
                    fallthrough = true;
                }
                if (fallthrough || !matchᴛ1 && b is (byte)'\n') { matchᴛ1 = true;
                    if (!yield(lineStart)) {
                        return;
                    }
                    lineStart = pos;
                }

            } else {
                var (ch, size) = utf8.DecodeRuneInString(text.slice(pos));
                pos += ((TextPos)(int32)size);
                if (stringutil.IsLineBreak(ch)) {
                    if (!yield(lineStart)) {
                        return;
                    }
                    lineStart = pos;
                }
            }
        }
        yield(lineStart);
    };
}

public static (nint line, nint byteOffset) PositionToLineAndByteOffset(nint position, slice<TextPos> lineStarts) {
    nint line = default;

    var lineStartsʗ1 = lineStarts;
    line = max(sort.Search(len(lineStarts), (nint i) => (nint)lineStartsʗ1[i] > position) - 1, 0);
    return (line, position - (nint)lineStarts[line]);
}

partial struct UTF16Offset /*num:nint*/;

public static UTF16Offset UTF16Len(@string s) {
    foreach (var i in range(len(s))) {
        if (s[i] >= utf8.RuneSelf) {
            UTF16Offset n = (UTF16Offset)i;
            foreach (var (_, r) in s.slice(i)) {
                n += ((UTF16Offset)utf16.RuneLen(r));
            }
            return n;
        }
    }
    return (UTF16Offset)len(s);
}

public static slice<T> Flatten<T>(slice<slice<T>> Δarray) {
    slice<T> result = default;
    foreach (var (_, subArray) in range(Δarray)) {
        result = appendꓸꓸꓸ(result, subArray);
    }
    return result;
}

public static T Must<T>(T v, error err) {
    if (err != nil) {
        throw panic(err);
    }
    return v;
}

public static T1 FirstResult<T1>(T1 t1, params ꓸꓸꓸany _ʗp) {
    return t1;
}

public static (@string, error) StringifyJson(any input, @string prefix, @string indent) {
    var (output, err) = json.MarshalIndent(input, prefix, indent);
    return ((@string)output, err);
}

public static ScriptKind GetScriptKindFromFileName(@string fileName) {
    nint dotPos = strings.LastIndex(fileName, ".");
    if (dotPos >= 0) {
        var exprᴛ1 = strings.ToLower(fileName.slice(dotPos));
        switch (ᐧ) {
        case {} when exprᴛ1 == tspath.ExtensionJs || exprᴛ1 == tspath.ExtensionCjs || exprᴛ1 == tspath.ExtensionMjs: {
            return ScriptKindJS;
        }
        case {} when exprᴛ1 == tspath.ExtensionJsx: {
            return ScriptKindJSX;
        }
        case {} when exprᴛ1 == tspath.ExtensionTs || exprᴛ1 == tspath.ExtensionCts || exprᴛ1 == tspath.ExtensionMts: {
            return ScriptKindTS;
        }
        case {} when exprᴛ1 == tspath.ExtensionTsx: {
            return ScriptKindTSX;
        }
        case {} when exprᴛ1 == tspath.ExtensionJson: {
            return ScriptKindJSON;
        }}

    }
    return ScriptKindUnknown;
}

public static @string GetDefaultExtensionForScriptKind(ScriptKind scriptKind) {
    switch (scriptKind) {
    case var ᴛ when ᴛ == ScriptKindJS: {
        return tspath.ExtensionJs;
    }
    case var ᴛ when ᴛ == ScriptKindJSX: {
        return tspath.ExtensionJsx;
    }
    case var ᴛ when ᴛ == ScriptKindTSX: {
        return tspath.ExtensionTsx;
    }
    case var ᴛ when ᴛ == ScriptKindJSON: {
        return tspath.ExtensionJson;
    }
    default: {
        return tspath.ExtensionTs;
    }}

}

public static ScriptKind EnsureScriptKindFromFileName(@string fileName) {
    var kind = GetScriptKindFromFileName(fileName); if (kind != ScriptKindUnknown) {
        return kind;
    }
    return ScriptKindTS;
}

public static T GetSpellingSuggestion<T>(@string name, iter.Seq<T> candidates, Func<T, @string> getName, Func<T, T, nint> compare) {
    return getSpellingSuggestion(name, candidates, getName, compare, 0);
}

public static T GetSpellingSuggestionWithMaxCandidateCount<T>(@string name, iter.Seq<T> candidates, Func<T, @string> getName, Func<T, T, nint> compare, nint maxCandidates) {
    return getSpellingSuggestion(name, candidates, getName, compare, maxCandidates);
}

internal static T getSpellingSuggestion<T>(@string name, iter.Seq<T> candidates, Func<T, @string> getName, Func<T, T, nint> compare, nint maxCandidates) {
    GoFrame ᒐ = default;
    try {
        var runeName = slice<rune>(name);
        nint maximumLengthDifference = max(2, (nint)((float64)len(runeName) * 0.34D));
        var bestDistance = math.Floor((float64)len(runeName) * 0.4D) + 0.9D;
        var buffers = ᏑlevenshteinBuffersPool.Get()._<ж<levenshteinBuffers>>();
        defer(ᏑlevenshteinBuffersPool.Put, buffers.OrTypedNil(), ref ᒐ);
        T bestCandidate = GoZero<T>();
        var hasBest = false;
        nint checkedCandidates = 0;
        foreach (var candidate in range<T>(candidates.Invoke)) {
            checkedCandidates++;
            if (maxCandidates > 0 && checkedCandidates > maxCandidates) {
                T zero = GoZero<T>();
                return zero;
            }
            @string candidateName = getName(candidate);
            nint maxLen = max(len(candidateName), len(runeName));
            nint minLen = min(len(candidateName), len(runeName));
            if (candidateName != ""u8 && maxLen - minLen <= maximumLengthDifference) {
                if (candidateName == name) {
                    continue;
                }
                if (len(candidateName) < 3 && !strings.EqualFold(candidateName, name)) {
                    continue;
                }
                var distance = levenshteinWithMax(ref (buffers).DerefOrNull(), runeName, slice<rune>(candidateName), bestDistance);
                if (distance < 0D) {
                    continue;
                }
                debug.Assert(distance <= bestDistance);
                if (distance < bestDistance) {
                    bestDistance = distance;
                    bestCandidate = candidate;
                    hasBest = true;
                } else if (!hasBest || compare(candidate, bestCandidate) < 0) {
                    bestCandidate = candidate;
                    hasBest = true;
                }
            }
        }
        return bestCandidate;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

public static @string GetSpellingSuggestionForStrings(@string name, iter.Seq<@string> candidates) {
    return GetSpellingSuggestion<@string>(name, candidates, Identity<@string>, strings.Compare);
}

partial struct levenshteinBuffers {
    internal slice<float64> previous;
    internal slice<float64> current;
}

internal static readonly ж<sync.Pool> ᏑlevenshteinBuffersPool = new StaticBox<sync.Pool>(new sync.Pool(
    New: () => Ꮡ(new levenshteinBuffers())
));
internal static ref sync.Pool levenshteinBuffersPool => ref ((StaticBox<sync.Pool>)ᏑlevenshteinBuffersPool).ValueSlot;

internal static float64 levenshteinWithMax(ref levenshteinBuffers buffers, slice<rune> s1, slice<rune> s2, float64 maxValue) {
    nint bufferSize = len(s2) + 1;
    buffers.previous = slices.Grow<slice<float64>, float64>(buffers.previous[..0], bufferSize).slice(0, bufferSize);
    buffers.current = slices.Grow<slice<float64>, float64>(buffers.current[..0], bufferSize).slice(0, bufferSize);

    var previous = buffers.previous;
    var current = buffers.current;

    var big = maxValue + 0.01D;
    foreach (var i in range(len(previous))) {
        previous[i] = (float64)i;
    }
    for (nint i = 1; i <= len(s1); i++) {
        var c1 = s1[i - 1];
        nint minJ = max((nint)math.Ceil((float64)i - maxValue), 1);
        nint maxJ = min((nint)math.Floor(maxValue + (float64)i), len(s2));
        var colMin = (float64)i;
        current[0] = colMin;
        for (nint j = 1; j < minJ; j++) {
            current[j] = big;
        }
        for (nint j = minJ; j <= maxJ; j++) {
            float64 substitutionDistance = default;
            float64 dist = default;
            if (unicode.ToLower(s1[i - 1]) == unicode.ToLower(s2[j - 1])) {
                substitutionDistance = previous[j - 1] + 0.1D;
            } else {
                substitutionDistance = previous[j - 1] + 2D;
            }
            if (c1 == s2[j - 1]) {
                dist = previous[j - 1];
            } else {
                dist = math.Min(previous[j] + 1D, math.Min(current[j - 1] + 1D, substitutionDistance));
            }
            current[j] = dist;
            colMin = math.Min(colMin, dist);
        }
        for (nint j = maxJ + 1; j <= len(s2); j++) {
            current[j] = big;
        }
        if (colMin > maxValue) {
            return -1D;
        }
        (previous, current) = (current, previous);
    }
    var res = previous[len(s2)];
    if (res > maxValue) {
        return -1D;
    }
    return res;
}

public static T Identity<T>(T t) {
    return t;
}

public static slice<ж<S>> CheckEachDefined<S>(slice<ж<S>> s, @string msg) {
    foreach (var (_, value) in range(s)) {
        if (value == nil) {
            throw panic(msg);
        }
    }
    return s;
}

public static nint IndexAfter(@string s, @string pattern, nint startIndex) {
    nint matched = strings.Index(s.slice(startIndex), pattern);
    if (matched == -1) {
        return -1;
    } else {
        return matched + startIndex;
    }
}

public static bool ShouldRewriteModuleSpecifier(@string specifier, ж<CompilerOptions> ᏑcompilerOptions) {
    ref var compilerOptions = ref ᏑcompilerOptions.DerefOrNull();

    return compilerOptions.RewriteRelativeImportExtensions.IsTrue() && tspath.PathIsRelative(specifier) && !tspath.IsDeclarationFileName(specifier) && tspath.HasTSFileExtension(specifier);
}

public static slice<ж<T>> SingleElementSlice<T>(ж<T> element) {
    if (element == nil) {
        return default;
    }
    return [element];
}

public static iter.Seq<T> ConcatenateSeq<T>(params Span<iter.Seq<T>> seqsʗp) {
    var seqs = seqsʗp.slice();

    var seqsʗ1 = seqs;
    return (Func<T, bool> yield) => {
        foreach (var (_, seq) in range(seqsʗ1)) {
            if (seq == nil) {
                continue;
            }
            foreach (var e in range<T>(seq.Invoke)) {
                if (!yield(e)) {
                    return;
                }
            }
        }
    };
}

public static iter.Seq2<nint, T> Enumerate<T>(iter.Seq<T> seq) {
    return (Func<nint, T, bool> yield) => {
        nint i = 0;
        foreach (var v in range<T>(seq.Invoke)) {
            if (!yield(i, v)) {
                return;
            }
            i++;
        }
    };
}

internal static bool comparableValuesEqual<T>(T a, T b) {
    return AreEqual(a, b);
}

public static void DiffMaps<K, V>(map<K, V> m1, map<K, V> m2, Action<K, V> onAdded, Action<K, V> onRemoved, Action<K, V, V> onChanged) {
    DiffMapsFunc<K, V, V>(m1, m2, comparableValuesEqual<V>, onAdded, onRemoved, onChanged);
}

public static void DiffMapsFunc<K, V1, V2>(map<K, V1> m1, map<K, V2> m2, Func<V1, V2, bool> equalValues, Action<K, V2> onAdded, Action<K, V1> onRemoved, Action<K, V1, V2> onChanged) {
    if (onAdded != nil) {
        foreach (var (k, v2) in m2) {
            var (_, ok) = m1[k, ꟷ]; if (!ok) {
                onAdded(k, v2);
            }
        }
    }
    if (onChanged == nil && onRemoved == nil) {
        return;
    }
    foreach (var (k, v1) in m1) {
        var (v2, ok) = m2[k, ꟷ]; if (ok) {
            if (onChanged != nil && !equalValues(v1, v2)) {
                onChanged(k, v1, v2);
            }
        } else {
            onRemoved(k, v1);
        }
    }
}

public static map<K, V> CopyMapInto<M1, M2, K, V>(M1 dst, M2 src)
    where M1 : /* ~map[K]V */ IMap<K, V>, ISupportMake<M1>, new()
    where M2 : /* ~map[K]V */ IMap<K, V>, ISupportMake<M2>, new()
{
    if (dst.IsNil) {
        return maps.Clone<M2, K, V>(src).AsMap();
    }
    maps.Copy<M1, M2, K, V>(dst, src);
    return dst.AsMap();
}

public static bool UnorderedEqual<T>(slice<T> s1, slice<T> s2) {
    if (len(s1) != len(s2)) {
        return false;
    }
    var counts = new map<T, nint>();
    foreach (var (_, v) in range(s1)) {
        counts[v]++;
    }
    foreach (var (_, v) in range(s2)) {
        counts[v]--;
        if (counts[v] < 0) {
            return false;
        }
    }
    return true;
}

public static slice<T> Deduplicate<T>(slice<T> Δslice) {
    if (len(Δslice) > 1) {
        foreach (var (iᴛ1, vᴛ1) in range(Δslice)) {
            var i = iᴛ1;
            var value = vᴛ1;

            if (slices.Contains(Δslice.slice(0, i), value)) {
                var result = slices.Clone<slice<T>, T>(Δslice.slice(0, i));
                for (i++; i < len(Δslice); i++) {
                    value = Δslice[i];
                    if (!slices.Contains(result, value)) {
                        result = append(result, value);
                    }
                }
                return result;
            }
        }
    }
    return Δslice;
}

public static slice<T> DeduplicateSorted<T>(slice<T> Δslice, Func<T, T, bool> isEqual) {
    if (len(Δslice) == 0) {
        return Δslice;
    }
    var last = Δslice[0];
    var deduplicated = Δslice[..1];
    for (nint i = 1; i < len(Δslice); i++) {
        var next = Δslice[i];
        if (isEqual(last, next)) {
            continue;
        }

        deduplicated = append(deduplicated, next);
        last = next;
    }

    return deduplicated;
}

public static nint CompareBooleans(bool a, bool b) {
    if (a && !b) {
        return 1;
    } else if (!a && b) {
        return -1;
    }
    return 0;
}

} // end core_package
