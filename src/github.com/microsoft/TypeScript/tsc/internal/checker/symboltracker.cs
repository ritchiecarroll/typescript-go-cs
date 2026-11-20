namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using nodebuilder = go.github.com.microsoft.TypeScript.tsc.@internal.nodebuilder_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static checker_package;

partial class checker_package {

partial struct SymbolTrackerImpl {
    internal ж<NodeBuilderContext> context;
    internal nodebuilder.SymbolTracker inner;
    public bool DisableTrackSymbol;
}

public static ж<SymbolTrackerImpl> NewSymbolTrackerImpl(ж<NodeBuilderContext> context, nodebuilder.SymbolTracker tracker) {
    if (tracker != nil) {
        for (;;) {
            var (t, ok) = tracker._<ж<SymbolTrackerImpl>>(ᐧ);
            if (!ok) {
                break;
            }
            tracker = t.inner;
        }
    }

    return Ꮡ(new SymbolTrackerImpl(context, tracker, false));
}

} partial class checkerᴛSymbolTrackerImpl {

public static bool TrackSymbol(this ref SymbolTrackerImpl @this, ж<ast.Symbol> Ꮡsymbol, ж<ast.Node> enclosingDeclaration, ast.SymbolFlags meaning) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    if (!@this.DisableTrackSymbol) {
        if (@this.inner != nil && @this.inner.TrackSymbol(Ꮡsymbol, enclosingDeclaration, meaning)) {
            @this.onDiagnosticReported();
            return true;
        }
        if ((ast.SymbolFlags)(symbol.Flags & ast.SymbolFlagsTypeParameter) == 0) {
            @this.context.trackedSymbols = append(@this.context.trackedSymbols, Ꮡ(new TrackedSymbolArgs(Ꮡsymbol, enclosingDeclaration, meaning)));
        }
    }
    return false;
}

public static void ReportInaccessibleThisError(this ref SymbolTrackerImpl @this) {
    @this.onDiagnosticReported();
    if (@this.inner == nil) {
        return;
    }
    @this.inner.ReportInaccessibleThisError();
}

public static void ReportPrivateInBaseOfClassExpression(this ref SymbolTrackerImpl @this, @string propertyName) {
    @this.onDiagnosticReported();
    if (@this.inner == nil) {
        return;
    }
    @this.inner.ReportPrivateInBaseOfClassExpression(propertyName);
}

public static void ReportInaccessibleUniqueSymbolError(this ref SymbolTrackerImpl @this) {
    @this.onDiagnosticReported();
    if (@this.inner == nil) {
        return;
    }
    @this.inner.ReportInaccessibleUniqueSymbolError();
}

public static void ReportCyclicStructureError(this ref SymbolTrackerImpl @this) {
    @this.onDiagnosticReported();
    if (@this.inner == nil) {
        return;
    }
    @this.inner.ReportCyclicStructureError();
}

public static void ReportLikelyUnsafeImportRequiredError(this ref SymbolTrackerImpl @this, @string specifier, @string symbolName) {
    @this.onDiagnosticReported();
    if (@this.inner == nil) {
        return;
    }
    @this.inner.ReportLikelyUnsafeImportRequiredError(specifier, symbolName);
}

public static void ReportTruncationError(this ref SymbolTrackerImpl @this) {
    @this.onDiagnosticReported();
    if (@this.inner == nil) {
        return;
    }
    @this.inner.ReportTruncationError();
}

public static void ReportNonlocalAugmentation(this ref SymbolTrackerImpl @this, ж<ast.SourceFile> containingFile, ж<ast.Symbol> parentSymbol, ж<ast.Symbol> augmentingSymbol) {
    @this.onDiagnosticReported();
    if (@this.inner == nil) {
        return;
    }
    @this.inner.ReportNonlocalAugmentation(containingFile, parentSymbol, augmentingSymbol);
}

public static void ReportNonSerializableProperty(this ref SymbolTrackerImpl @this, @string propertyName) {
    @this.onDiagnosticReported();
    if (@this.inner == nil) {
        return;
    }
    @this.inner.ReportNonSerializableProperty(propertyName);
}

internal static void onDiagnosticReported(this ref SymbolTrackerImpl @this) {
    @this.context.reportedDiagnostic = true;
}

public static void ReportInferenceFallback(this ref SymbolTrackerImpl @this, ж<ast.Node> node) {
    if (@this.inner == nil) {
        return;
    }
    @this.inner.ReportInferenceFallback(node);
}

public static void PushErrorFallbackNode(this ref SymbolTrackerImpl @this, ж<ast.Node> node) {
    if (@this.inner == nil) {
        return;
    }
    @this.inner.PushErrorFallbackNode(node);
}

public static void PopErrorFallbackNode(this ref SymbolTrackerImpl @this) {
    if (@this.inner == nil) {
        return;
    }
    @this.inner.PopErrorFallbackNode();
}

} // end checkerᴛSymbolTrackerImpl
