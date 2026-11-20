namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using math = math_package;
using sync = sync_package;
using atomic = go.sync.atomic_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using MethodImplAttribute = global::System.Runtime.CompilerServices.MethodImplAttribute;
using MethodImplOptions = global::System.Runtime.CompilerServices.MethodImplOptions;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.sync;
using iter = iter_package;
using static core_package;

partial class core_package {

partial struct BreadthFirstSearchResult<N> {
    public bool Stopped;
    public slice<N> Path;
}

partial struct breadthFirstSearchJob<N> {
    internal N node;
    internal ж<breadthFirstSearchJob<N>> parent;
}

partial struct BreadthFirstSearchLevel<K, N> {
    internal ж<collections.OrderedMap<K, ж<breadthFirstSearchJob<N>>>> jobs;
}

} partial class coreᴛBreadthFirstSearchLevel {

public static bool Has<K, N>(this ref BreadthFirstSearchLevel<K, N> l, K key) {
    return l.jobs.Has(key);
}

public static void Delete<K, N>(this ref BreadthFirstSearchLevel<K, N> l, K key) {
    l.jobs.Delete(key);
}

public static void Range<K, N>(this ref BreadthFirstSearchLevel<K, N> l, Func<N, bool> f) {
    foreach (var job in range<ж<breadthFirstSearchJob<N>>>(l.jobs.Values().Invoke)) {
        if (!f(job.node)) {
            return;
        }
    }
}

} partial class core_package {

partial struct BreadthFirstSearchOptions<K, N> {
    public ж<collections.SyncSet<K>> Visited;
    public Action<ж<BreadthFirstSearchLevel<K, N>>> PreprocessLevel;
}

public static BreadthFirstSearchResult<N> BreadthFirstSearchParallel<N>(N start, Func<N, slice<N>> neighbors, Func<N, (bool, bool)> visit) {
    return BreadthFirstSearchParallelEx<N, N>(start, neighbors, visit, new BreadthFirstSearchOptions<N, N>(), Identity<N>);
}

internal partial struct BreadthFirstSearchParallelEx_result<K, N> /*dyn*/ {
    internal bool stop;
    internal ж<breadthFirstSearchJob<N>> job;
    internal ж<collections.OrderedMap<K, ж<breadthFirstSearchJob<N>>>> next;
}

public static BreadthFirstSearchResult<N> BreadthFirstSearchParallelEx<K, N>(N start, Func<N, slice<N>> neighbors, Func<N, (bool, bool)> visit, BreadthFirstSearchOptions<K, N> options, Func<N, K> getKey) {
    var visited = options.Visited;
    if (visited == nil) {
        visited = Ꮡ(new collections.SyncSet<K>());
    }

    ref var fallback = ref heap<ж<breadthFirstSearchJob<N>>>(out var Ꮡfallback);
    var optionsʗ1 = options;
    var visitedʗ1 = visited;
    [MethodImpl(MethodImplOptions.NoInlining)] BreadthFirstSearchParallelEx_result<K, N> processLevel(nint index, ж<collections.OrderedMap<K, ж<breadthFirstSearchJob<N>>>> jobs) {
        ref var lowestFallback = ref heap(new atomic.Int64(), out var ᏑlowestFallback);
        ref var lowestGoal = ref heap(new atomic.Int64(), out var ᏑlowestGoal);
        ref var nextJobCount = ref heap(new atomic.Int64(), out var ᏑnextJobCount);
        ᏑlowestGoal.Store(math.MaxInt64);
        ᏑlowestFallback.Store(math.MaxInt64);
        if (optionsʗ1.PreprocessLevel != nil) {
            optionsʗ1.PreprocessLevel(Ꮡ(new BreadthFirstSearchLevel<K, N>(jobs: jobs)));
        }
        var next = new slice<slice<ж<breadthFirstSearchJob<N>>>>(jobs.Size());
        ref var wg = ref heap(new sync.WaitGroup(), out var Ꮡwg);
        nint i = 0;
        foreach (var j in range<ж<breadthFirstSearchJob<N>>>(jobs.Values().Invoke)) {
            Ꮡwg.Add(1);
            var nextʗ1 = next;
            var visitedʗ2 = visitedʗ1;
            goǃ((nint iΔ1, ж<breadthFirstSearchJob<N>> jΔ1) => {
                GoFrame ᒐ = default;
                try {
                    defer(Ꮡwg.Done, ref ᒐ);
                    if ((int64)iΔ1 >= ᏑlowestGoal.Load()) {
                        return;
                    }

                    if (!visitedʗ2.AddIfAbsent(getKey(jΔ1.node))) {
                        return;
                    }

                    var (isResult, stop) = visit(jΔ1.node);
                    if (isResult) {
                        if (stop) {
                            updateMin(ᏑlowestGoal, (int64)iΔ1);
                            return;
                        }
                        if (Ꮡfallback.ValueSlot == nil) {
                            updateMin(ᏑlowestFallback, (int64)iΔ1);
                        }
                    }

                    if ((int64)iΔ1 >= ᏑlowestGoal.Load()) {
                        return;
                    }
                    var neighborNodes = neighbors(jΔ1.node);
                    if (len(neighborNodes) > 0) {
                        ᏑnextJobCount.Add((int64)len(neighborNodes));
                        nextʗ1[iΔ1] = Map(neighborNodes, ж<breadthFirstSearchJob<N>> (N child) => Ꮡ(new breadthFirstSearchJob<N>(node: child, parent: jΔ1)));
                    }
                }
                catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
                finally { ᒐ.Run(); }
            }, i, j);
            i++;
        }
        Ꮡwg.Wait();
        var indexΔ1 = ᏑlowestGoal.Load(); if (indexΔ1 != math.MaxInt64) {
            var (_, job, _) = jobs.EntryAt((nint)indexΔ1);
            return new BreadthFirstSearchParallelEx_result<K, N>(stop: true, job: job);
        }
        if (Ꮡfallback.ValueSlot == nil) {
            var indexΔ2 = ᏑlowestFallback.Load(); if (indexΔ2 != math.MaxInt64) {
                (_, Ꮡfallback.ValueSlot, _) = jobs.EntryAt((nint)indexΔ2);
            }
        }
        var nextJobs = collections.NewOrderedMapWithSizeHint<K, ж<breadthFirstSearchJob<N>>>((nint)ᏑnextJobCount.Load());
        foreach (var (_, jobsΔ1) in range(next)) {
            foreach (var (_, j) in range(jobsΔ1)) {
                if (!nextJobs.Has(getKey(j.node))) {
                    nextJobs.Set(getKey(j.node), j);
                }
            }
        }
        return new BreadthFirstSearchParallelEx_result<K, N>(next: nextJobs);
    }

    slice<N> createPath(ж<breadthFirstSearchJob<N>> job) {
        slice<N> path = default;
        while (job != nil) {
            path = append(path, job.node);
            job = job.parent;
        }
        return path;
    }

    nint levelIndex = 0;
    var level = collections.NewOrderedMapFromList(slice<collections.MapEntry<K, ж<breadthFirstSearchJob<N>>>>([
        new(Key: getKey(start), Value: Ꮡ(new breadthFirstSearchJob<N>(node: start)))
    ]));
    while (level.Size() > 0) {
        var result = processLevel(levelIndex, level);
        if (result.stop) {
            return new BreadthFirstSearchResult<N>(Stopped: true, Path: createPath(result.job));
        } else if (result.job != nil && fallback == nil) {
            fallback = result.job;
        }
        level = result.next;
        levelIndex++;
    }
    return new BreadthFirstSearchResult<N>(Stopped: false, Path: createPath(fallback));
}

internal static bool updateMin(ж<atomic.Int64> a, int64 candidate) {
    for (;;) {
        var current = a.Load();
        if (current < candidate) {
            return false;
        }
        if (a.CompareAndSwap(current, candidate)) {
            return true;
        }
    }
}

} // end core_package
