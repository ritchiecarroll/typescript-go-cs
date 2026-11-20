namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using context = context_package;
using math = math_package;
using slices = slices_package;
using sort = sort_package;
using sync = sync_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using checker = go.github.com.microsoft.TypeScript.tsc.@internal.checker_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using tracing = go.github.com.microsoft.TypeScript.tsc.@internal.tracing_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using module = go.github.com.microsoft.TypeScript.tsc.@internal.module_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using static compiler_package;

partial class compiler_package {

partial interface CheckerPool {
    (ж<checker.Checker>, Action) GetChecker(context.Context ctx, ж<ast.SourceFile> @file);
}

partial struct checkerPool {
    internal ж<Program> program;
    internal ж<tracing.Tracing> tracing;
    internal sync.Once createCheckersOnce;
    internal slice<ж<checker.Checker>> checkers;
    internal slice<ж<sync.Mutex>> locks;
    internal map<ж<ast.SourceFile>, ж<checker.Checker>> fileAssociations;
}

internal static CheckerPool _ᴛ1ʗ = new checkerPoolжCheckerPool(((ж<checkerPool>)nil));

internal static UntypedInt checkerAssociationTextWeightDivisor => 100;
internal static UntypedInt checkerAssociationSourceFileWeightMultiplier => 4;
internal static UntypedInt checkerAssociationBalancePenaltyMultiplier => 16;
internal static UntypedInt checkerAssociationPrioritizedSourcePenalty => 12;
internal static UntypedInt checkerAssociationStrongBalanceMinCheckerCount => 4;

partial struct checkerAssociationPolicy {
    internal bool prioritizeSourceFiles;
    internal nint sourceFileWeightMultiplier;
    internal nint balancePenaltyMultiplier;
}

internal static checkerAssociationPolicy getCheckerAssociationPolicy(nint totalWeight, nint declarationWeight, nint checkerCount) {
    if (shouldPrioritizeSourceFiles(totalWeight, declarationWeight, checkerCount)) {
        return new checkerAssociationPolicy(
            prioritizeSourceFiles: true,
            sourceFileWeightMultiplier: 1,
            balancePenaltyMultiplier: checkerAssociationPrioritizedSourcePenalty
        );
    }
    if (checkerCount >= checkerAssociationStrongBalanceMinCheckerCount) {
        return new checkerAssociationPolicy(
            sourceFileWeightMultiplier: checkerAssociationSourceFileWeightMultiplier,
            balancePenaltyMultiplier: checkerAssociationBalancePenaltyMultiplier
        );
    }
    return new checkerAssociationPolicy(
        sourceFileWeightMultiplier: 1,
        balancePenaltyMultiplier: 1
    );
}

internal static slice<nint> getCheckerAssociationsInOrder(slice<nint> fileWeights, slice<slice<nint>> adjacentFiles, slice<nint> fileOrder, nint checkerCount, nint penaltyMultiplier) {
    if (len(fileWeights) == 0) {
        return default;
    }

    nint totalWeight = 0;
    nint maxFileWeight = 0;
    nint edgeCount = 0;
    foreach (var (i, weight) in range(fileWeights)) {
        totalWeight += weight;
        maxFileWeight = max(maxFileWeight, weight);
        edgeCount += len(adjacentFiles[i]);
    }

    var associations = new slice<nint>(len(fileWeights));
    foreach (var i in range(len(associations))) {
        associations[i] = -1;
    }
    var checkerWeights = new slice<nint>(checkerCount);
    nint averageCheckerWeight = quo((totalWeight + checkerCount - 1), checkerCount);
    nint maxCheckerWeight = max(maxFileWeight, averageCheckerWeight + averageCheckerWeight / 100);
    var totalWeightFloat = (float64)totalWeight;
    var alpha = (float64)penaltyMultiplier * (float64)(edgeCount / 2) * math.Sqrt((float64)checkerCount) / (totalWeightFloat * math.Sqrt(totalWeightFloat));
    var neighborCounts = new slice<nint>(checkerCount);

    foreach (var position in range(len(fileWeights))) {
        nint fileIndex = position;
        if (fileOrder != nil) {
            fileIndex = fileOrder[position];
        }

        clear(neighborCounts);
        foreach (var (_, adjacentFile) in range(adjacentFiles[fileIndex])) {
            nint checkerIndex = associations[adjacentFile]; if (checkerIndex >= 0) {
                neighborCounts[checkerIndex]++;
            }
        }

        nint bestChecker = -1;
        var bestScore = math.Inf(-1);
        foreach (var (checkerIndex, checkerWeight) in range(checkerWeights)) {
            if (checkerWeight + fileWeights[fileIndex] > maxCheckerWeight) {
                continue;
            }
            var oldWeight = (float64)checkerWeight;
            var newWeight = (float64)(checkerWeight + fileWeights[fileIndex]);
            var penalty = alpha * (newWeight * math.Sqrt(newWeight) - oldWeight * math.Sqrt(oldWeight));
            var score = (float64)neighborCounts[checkerIndex] - penalty;
            if (score > bestScore || score == bestScore && (bestChecker < 0 || checkerWeight < checkerWeights[bestChecker])) {
                bestChecker = checkerIndex;
                bestScore = score;
            }
        }
        if (bestChecker < 0) {
            bestChecker = 0;
            foreach (var (checkerIndex, checkerWeight) in range(checkerWeights[1..])) {
                if (checkerWeight < checkerWeights[bestChecker]) {
                    bestChecker = checkerIndex + 1;
                }
            }
        }
        associations[fileIndex] = bestChecker;
        checkerWeights[bestChecker] += fileWeights[fileIndex];
    }
    return associations;
}

internal static slice<nint> getCheckerAssociationOrder(slice<nint> fileWeights, slice<bool> isDeclarationFile, bool prioritizeSourceFiles) {
    if (!prioritizeSourceFiles) {
        return default;
    }
    var fileOrder = new slice<nint>(len(fileWeights));
    foreach (var i in range(len(fileOrder))) {
        fileOrder[i] = i;
    }
    var fileOrderʗ1 = fileOrder;
    var fileWeightsʗ1 = fileWeights;
    var isDeclarationFileʗ1 = isDeclarationFile;
    sort.Slice(fileOrder, (nint i, nint j) => {
        nint left = fileOrderʗ1[i];
        nint right = fileOrderʗ1[j];
        if (isDeclarationFileʗ1[left] != isDeclarationFileʗ1[right]) {
            return !isDeclarationFileʗ1[left];
        }
        if (fileWeightsʗ1[left] != fileWeightsʗ1[right]) {
            return fileWeightsʗ1[left] > fileWeightsʗ1[right];
        }
        return left < right;
    });
    return fileOrder;
}

internal static nint getCheckerAssociationBaseWeight(nint nodeCount, nint textLength) {
    return max(nodeCount + textLength / (nint)checkerAssociationTextWeightDivisor, 1);
}

internal static bool shouldPrioritizeSourceFiles(nint totalWeight, nint declarationWeight, nint checkerCount) {
    return declarationWeight * checkerCount * 2 <= totalWeight;
}

internal static slice<nint> getCheckerAssociationWeights(slice<nint> baseWeights, slice<nint> importCounts) {
    nint totalBaseWeight = 0;
    nint totalImports = 0;
    foreach (var (i, baseWeight) in range(baseWeights)) {
        totalBaseWeight += baseWeight;
        totalImports += importCounts[i];
    }
    nint importWeight = 0;
    if (totalImports > 0) {
        importWeight = max(quo(totalBaseWeight, totalImports), 1);
    }
    var fileWeights = new slice<nint>(len(baseWeights));
    foreach (var (i, baseWeight) in range(baseWeights)) {
        fileWeights[i] = baseWeight + importCounts[i] * importWeight;
    }
    return fileWeights;
}

internal static ж<checkerPool> newCheckerPool(ж<Program> program) {
    return newCheckerPoolWithTracing(program, nil);
}

internal static ж<checkerPool> newCheckerPoolWithTracing(ж<Program> Ꮡprogram, ж<tracing.Tracing> tr) {
    ref var program = ref Ꮡprogram.DerefOrNull();

    nint checkerCount = 4;
    if (program.SingleThreaded()) {
        checkerCount = 1;
    } else if (program.Options().Checkers is var c && c != nil) {
        checkerCount = c.Value;
    }

    checkerCount = max(min(checkerCount, len(program.files), 256), 1);

    var pool = Ꮡ(new checkerPool(
        program: Ꮡprogram,
        checkers: new slice<ж<checker.Checker>>(checkerCount),
        locks: new slice<ж<sync.Mutex>>(checkerCount),
        tracing: tr
    ));

    return pool;
}

} partial class compilerᴛcheckerPool {

internal static (ж<checker.Checker>, Action) GetChecker(this ж<checkerPool> Ꮡp, context.Context ctx, ж<ast.SourceFile> @file) {
    ref var p = ref Ꮡp.DerefOrNull();

    if (@file != nil) {
        return Ꮡp.getCheckerForFileExclusive(ctx, @file);
    }
    Ꮡp.createCheckers();
    var c = p.checkers[0];
    p.locks[0].Lock();
    return (c, sync.OnceFunc(() => {
        Ꮡp.Value.locks[0].Unlock();
    }));
}

internal static (ж<checker.Checker>, Action) getCheckerForFileNonExclusive(this ж<checkerPool> Ꮡp, ж<ast.SourceFile> @file) {
    ref var p = ref Ꮡp.DerefOrNull();

    Ꮡp.createCheckers();
    return (p.fileAssociations[@file], noop);
}

internal static (ж<checker.Checker>, Action) getCheckerForFileExclusive(this ж<checkerPool> Ꮡp, context.Context ctx, ж<ast.SourceFile> @file) {
    ref var p = ref Ꮡp.DerefOrNull();

    Ꮡp.createCheckers();
    var c = p.fileAssociations[@file];
    nint idx = slices.Index(p.checkers, c);
    p.locks[idx].Lock();
    return (c, sync.OnceFunc(() => {
        Ꮡp.Value.locks[idx].Unlock();
    }));
}

internal static (ж<checker.Checker>, Action) getCheckerNonExclusive(this ж<checkerPool> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    Ꮡp.createCheckers();
    return (p.checkers[0], noop);
}

internal static void createCheckers(this ж<checkerPool> Ꮡp) {
    Ꮡp.ᏑcreateCheckersOnce.Do(() => {
        nint checkerCount = len(Ꮡp.Value.checkers);
        var wg = core.NewWorkGroup(Ꮡp.Value.program.SingleThreaded());
        foreach (var i in range(checkerCount)) {
            wg.Queue(() => {
                ж<checker.Tracer> tracer = default;
                if (Ꮡp.Value.tracing != nil) {
                    tracer = checker.NewTracer(Ꮡp.Value.tracing, i);
                }
                (Ꮡp.Value.checkers[i], Ꮡp.Value.locks[i]) = checker.NewChecker(new ProgramжProgram(Ꮡp.Value.program), tracer);
            });
        }

        wg.RunAndWait();

        var associations = new slice<nint>(len(Ꮡp.Value.program.files));
        if (checkerCount > 1) {
            var baseWeights = new slice<nint>(len(Ꮡp.Value.program.files));
            var importCounts = new slice<nint>(len(Ꮡp.Value.program.files));
            var isDeclarationFile = new slice<bool>(len(Ꮡp.Value.program.files));
            nint totalBaseWeight = 0;
            nint declarationBaseWeight = 0;
            foreach (var (i, @file) in range(Ꮡp.Value.program.files)) {
                nint baseWeight = getCheckerAssociationBaseWeight(@file.NodeCount, len(@file.Text()));
                totalBaseWeight += baseWeight;
                if (@file.IsDeclarationFile) {
                    declarationBaseWeight += baseWeight;
                }
                baseWeights[i] = baseWeight;
                importCounts[i] = len(@file.Imports());
                isDeclarationFile[i] = @file.IsDeclarationFile;
            }
            var policy = getCheckerAssociationPolicy(totalBaseWeight, declarationBaseWeight, checkerCount);
            if (policy.sourceFileWeightMultiplier != 1) {
                foreach (var (i, declaration) in range(isDeclarationFile)) {
                    if (!declaration) {
                        baseWeights[i] *= policy.sourceFileWeightMultiplier;
                    }
                }
            }
            var fileWeights = getCheckerAssociationWeights(baseWeights, importCounts);
            var adjacentFiles = Ꮡp.Value.getImportAdjacency();
            var fileOrder = getCheckerAssociationOrder(fileWeights, isDeclarationFile, policy.prioritizeSourceFiles);
            associations = getCheckerAssociationsInOrder(fileWeights, adjacentFiles, fileOrder, checkerCount, policy.balancePenaltyMultiplier);
        }
        Ꮡp.Value.fileAssociations = new map<ж<ast.SourceFile>, ж<checker.Checker>>(len(Ꮡp.Value.program.files));
        foreach (var (i, @file) in range(Ꮡp.Value.program.files)) {
            Ꮡp.Value.fileAssociations[@file] = Ꮡp.Value.checkers[associations[i]];
        }
    });
}

internal static slice<slice<nint>> getImportAdjacency(this ref checkerPool p) {
    var fileIndices = new map<ж<ast.SourceFile>, nint>(len(p.program.files));
    foreach (var (i, @file) in range(p.program.files)) {
        fileIndices[@file] = i;
    }
    var adjacentFiles = new slice<slice<nint>>(len(p.program.files));
    foreach (var (fileIndex, @file) in range(p.program.files)) {
        var resolvedModules = p.program.resolvedModules[@file.Path()];
        foreach (var (_, resolved) in resolvedModules) {
            if (resolved == nil || !resolved.IsResolved()) {
                continue;
            }
            var importedFile = p.program.GetSourceFileForResolvedModule(resolved.ResolvedFileName);
            var (importedIndex, ok) = fileIndices[importedFile, ꟷ];
            if (!ok || importedIndex == fileIndex) {
                continue;
            }
            adjacentFiles[fileIndex] = append(adjacentFiles[fileIndex], importedIndex);
            adjacentFiles[importedIndex] = append(adjacentFiles[importedIndex], fileIndex);
        }
    }
    return adjacentFiles;
}

internal static void forEachCheckerParallel(this ж<checkerPool> Ꮡp, Action<nint, ж<checker.Checker>> cb) {
    ref var p = ref Ꮡp.DerefOrNull();

    Ꮡp.createCheckers();
    var wg = core.NewWorkGroup(p.program.SingleThreaded());
    foreach (var (idx, checkerΔ1) in range(p.checkers)) {
        var checkerʗ1 = checkerΔ1;
        wg.Queue(() => {
            GoFrame ᒐ = default;
            try {
                Ꮡp.Value.locks[idx].Lock();
                defer(Ꮡp.Value.locks[idx].Unlock, ref ᒐ);
                cb(idx, checkerʗ1);
            }
            catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
            finally { ᒐ.Run(); }
        });
    }
    wg.RunAndWait();
}

internal static slice<ж<ast.Diagnostic>> GetGlobalDiagnostics(this ж<checkerPool> Ꮡp) {
    ref var p = ref Ꮡp.DerefOrNull();

    Ꮡp.createCheckers();
    var globalDiagnostics = new slice<slice<ж<ast.Diagnostic>>>(len(p.checkers));
    var globalDiagnosticsʗ1 = globalDiagnostics;
    Ꮡp.forEachCheckerParallel((nint idx, ж<checker.Checker> checkerΔ1) => {
        globalDiagnosticsʗ1[idx] = checkerΔ1.GetGlobalDiagnostics();
    });
    return SortAndDeduplicateDiagnostics(slices.Concat<slice<ж<ast.Diagnostic>>, ж<ast.Diagnostic>>(globalDiagnostics.ꓸꓸꓸ));
}

internal static void forEachCheckerGroupDo(this ж<checkerPool> Ꮡp, context.Context ctx, slice<ж<ast.SourceFile>> files, bool singleThreaded, Action<ж<checker.Checker>, nint, ж<ast.SourceFile>> cb) {
    ref var p = ref Ꮡp.DerefOrNull();

    Ꮡp.createCheckers();

    nint checkerCount = len(p.checkers);
    var wg = core.NewWorkGroup(singleThreaded);
    foreach (var checkerIdx in range(checkerCount)) {
        var filesʗ1 = files;
        wg.Queue(() => {
            GoFrame ᒐ = default;
            try {
                Ꮡp.Value.locks[checkerIdx].Lock();
                defer(Ꮡp.Value.locks[checkerIdx].Unlock, ref ᒐ);
                foreach (var (i, @file) in range(filesʗ1)) {
                    var checkerΔ1 = Ꮡp.Value.checkers[checkerIdx]; if (checkerΔ1 == Ꮡp.Value.fileAssociations[@file]) {
                        cb(checkerΔ1, i, @file);
                    }
                }
            }
            catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); }
            finally { ᒐ.Run(); }
        });
    }
    wg.RunAndWait();
}

} partial class compiler_package {

internal static void noop() { }

} // end compiler_package
