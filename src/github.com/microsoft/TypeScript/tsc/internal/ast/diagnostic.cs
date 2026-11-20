namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using slices = slices_package;
using strings = strings_package;
using sync = sync_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using locale = go.github.com.microsoft.TypeScript.tsc.@internal.locale_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using spanmap = go.github.com.microsoft.TypeScript.tsc.@internal.spanmap_package;
using ꓸꓸꓸany = Span<any>;
using static ast_package;

partial class ast_package {

partial struct RepopulateDiagnosticKind /*num:nint*/;

public static RepopulateDiagnosticKind RepopulateModeMismatch => 1;
public static RepopulateDiagnosticKind RepopulateModuleNotFound => 2;

partial struct RepopulateDiagnosticInfo {
    public RepopulateDiagnosticKind Kind;
    public @string ModuleReference;
    public coreꓸResolutionMode Mode;
    public @string PackageName;
}

partial struct Diagnostic {
    internal ж<SourceFile> @file;
    internal core.TextRange loc;
    internal int32 code;
    internal diagnostics.Category category;
    internal @string source;
    internal ж<diagnostics.Message> message;
    internal @string messageText;
    internal diagnostics.Key messageKey;
    internal slice<@string> messageArgs;
    internal slice<ж<Diagnostic>> messageChain;
    internal slice<ж<Diagnostic>> relatedInformation;
    internal bool reportsUnnecessary;
    internal bool reportsDeprecated;
    internal bool skippedOnNoEmit;
    internal ж<RepopulateDiagnosticInfo> repopulateInfo;
}

} partial class astᴛDiagnostic {

public static ж<SourceFile> File(this ref Diagnostic d) { return d.@file; }
public static nint Pos(this ref Diagnostic d) { return d.loc.Pos(); }
public static nint End(this ref Diagnostic d) { return d.loc.End(); }
public static nint Len(this ref Diagnostic d) { return d.loc.Len(); }
public static core.TextRange Loc(this ref Diagnostic d) { return d.loc; }
public static int32 Code(this ref Diagnostic d) { return d.code; }
public static diagnostics.Category Category(this ref Diagnostic d) { return d.category; }
public static @string Source(this ref Diagnostic d) { return d.source; }
public static @string MessageText(this ref Diagnostic d) { return d.messageText; }
public static diagnostics.Key MessageKey(this ref Diagnostic d) { return d.messageKey; }
public static slice<@string> MessageArgs(this ref Diagnostic d) { return d.messageArgs; }
public static slice<ж<Diagnostic>> MessageChain(this ref Diagnostic d) { return d.messageChain; }
public static slice<ж<Diagnostic>> RelatedInformation(this ref Diagnostic d) { return d.relatedInformation; }
public static bool ReportsUnnecessary(this ref Diagnostic d) { return d.reportsUnnecessary; }
public static bool ReportsDeprecated(this ref Diagnostic d) { return d.reportsDeprecated; }
public static bool SkippedOnNoEmit(this ref Diagnostic d) { return d.skippedOnNoEmit; }
public static ж<RepopulateDiagnosticInfo> RepopulateInfo(this ref Diagnostic d) { return d.repopulateInfo; }

public static void SetFile(this ref Diagnostic d, ж<SourceFile> Ꮡfile) {
    ref var @file = ref Ꮡfile.DerefOrNull();

    d.@file = Ꮡfile;
}

public static void SetLocation(this ref Diagnostic d, core.TextRange loc) { d.loc = loc; }
public static void SetCategory(this ref Diagnostic d, diagnostics.Category category) { d.category = category; }
public static void SetSkippedOnNoEmit(this ref Diagnostic d) { d.skippedOnNoEmit = true; }
public static void SetRepopulateInfo(this ref Diagnostic d, ж<RepopulateDiagnosticInfo> info) { d.repopulateInfo = info; }

public static ж<Diagnostic> SetExternalData(this ж<Diagnostic> Ꮡd, @string source, @string messageText) {
    ref var d = ref Ꮡd.DerefOrNull();

    d.source = source;
    d.messageText = messageText;
    return Ꮡd;
}

public static ж<Diagnostic> SetMessageChain(this ж<Diagnostic> Ꮡd, slice<ж<Diagnostic>> messageChain) {
    ref var d = ref Ꮡd.DerefOrNull();

    d.messageChain = messageChain;
    return Ꮡd;
}

public static ж<Diagnostic> AddMessageChain(this ж<Diagnostic> Ꮡd, ж<Diagnostic> ᏑmessageChain) {
    ref var d = ref Ꮡd.DerefOrNull();
    ref var messageChain = ref ᏑmessageChain.DerefOrNull();

    if (ᏑmessageChain != nil) {
        d.messageChain = append(d.messageChain, ᏑmessageChain);
    }
    return Ꮡd;
}

public static ж<Diagnostic> SetRelatedInfo(this ж<Diagnostic> Ꮡd, slice<ж<Diagnostic>> relatedInformation) {
    ref var d = ref Ꮡd.DerefOrNull();

    d.relatedInformation = relatedInformation;
    return Ꮡd;
}

public static ж<Diagnostic> AddRelatedInfo(this ж<Diagnostic> Ꮡd, ж<Diagnostic> ᏑrelatedInformation) {
    ref var d = ref Ꮡd.DerefOrNull();
    ref var relatedInformation = ref ᏑrelatedInformation.DerefOrNull();

    if (ᏑrelatedInformation != nil) {
        d.relatedInformation = append(d.relatedInformation, ᏑrelatedInformation);
    }
    return Ꮡd;
}

public static ж<Diagnostic> Clone(this ref Diagnostic d) {
    ref var result = ref heap<Diagnostic>(d, out var Ꮡresult);
    return Ꮡresult;
}

public static @string Localize(this ref Diagnostic d, locale.Locale localeΔ1) {
    if (d.message == nil && d.messageText != ""u8) {
        return d.messageText;
    }
    return diagnostics.Localize(localeΔ1, d.message, d.messageKey, d.displayMessageArgs().ꓸꓸꓸ);
}

public static @string String(this ref Diagnostic d) {
    if (d.message == nil && d.messageText != ""u8) {
        return d.messageText;
    }
    return diagnostics.Localize(locale.Default, d.message, d.messageKey, d.displayMessageArgs().ꓸꓸꓸ);
}

internal static slice<@string> displayMessageArgs(this ref Diagnostic d) {
    if (d.@file == nil || d.source != ""u8) {
        return d.messageArgs;
    }
    var (segment, ok) = d.@file.SpanMap().AliasForVirtualSpan(d.loc);
    if (!ok) {
        return d.messageArgs;
    }
    @string virtualText = d.@file.Text();
    @string originalText = d.@file.OriginalText();
    if (segment.VirtualStart < 0 || segment.VirtualEnd > ((core.TextPos)(int32)len(virtualText)) || segment.OriginalStart < 0 || segment.OriginalEnd > ((core.TextPos)(int32)len(originalText))) {
        return d.messageArgs;
    }
    @string virtualName = virtualText.slice(segment.VirtualStart, segment.VirtualEnd);
    @string originalName = originalText.slice(segment.OriginalStart, segment.OriginalEnd);
    slice<@string> result = default;
    foreach (var (i, arg) in range(d.messageArgs)) {
        if (arg != virtualName) {
            continue;
        }
        if (result == nil) {
            result = slices.Clone<slice<@string>, @string>(d.messageArgs);
        }
        result[i] = originalName;
    }
    if (result != nil) {
        return result;
    }
    return d.messageArgs;
}

} partial class ast_package {

public static ж<Diagnostic> NewDiagnosticFromSerialized(ж<SourceFile> @file, core.TextRange loc, int32 code, diagnostics.Category category, diagnostics.Key messageKey, slice<@string> messageArgs, slice<ж<Diagnostic>> messageChain, slice<ж<Diagnostic>> relatedInformation, bool reportsUnnecessary, bool reportsDeprecated, bool skippedOnNoEmit) {
    return Ꮡ(new Diagnostic(
        @file: @file,
        loc: loc,
        code: code,
        category: category,
        messageKey: messageKey,
        messageArgs: messageArgs,
        messageChain: messageChain,
        relatedInformation: relatedInformation,
        reportsUnnecessary: reportsUnnecessary,
        reportsDeprecated: reportsDeprecated,
        skippedOnNoEmit: skippedOnNoEmit
    ));
}

public static ж<Diagnostic> NewDiagnosticFromText(ж<SourceFile> @file, core.TextRange loc, int32 code, diagnostics.Category category, @string text, slice<ж<Diagnostic>> messageChain, slice<ж<Diagnostic>> relatedInformation, bool reportsUnnecessary, bool reportsDeprecated) {
    return Ꮡ(new Diagnostic(
        @file: @file,
        loc: loc,
        code: code,
        category: category,
        message: diagnostics.NewAdHocMessage(text),
        messageChain: messageChain,
        relatedInformation: relatedInformation,
        reportsUnnecessary: reportsUnnecessary,
        reportsDeprecated: reportsDeprecated
    ));
}

public static ж<Diagnostic> NewDiagnostic(ж<SourceFile> @file, core.TextRange loc, ж<diagnostics.Message> Ꮡmessage, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.slice();
    ref var message = ref Ꮡmessage.DerefOrNull();

    return Ꮡ(new Diagnostic(
        @file: @file,
        loc: loc,
        code: message.Code(),
        category: message.Category(),
        message: Ꮡmessage,
        messageKey: message.Key(),
        messageArgs: diagnostics.StringifyArgs(args),
        reportsUnnecessary: message.ReportsUnnecessary(),
        reportsDeprecated: message.ReportsDeprecated()
    ));
}

public static ж<Diagnostic> NewDiagnosticChain(ж<Diagnostic> Ꮡchain, ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();
    ref var chain = ref Ꮡchain.DerefOrNull();

    if (Ꮡchain != nil) {
        return NewDiagnostic(chain.@file, chain.loc, message, args.ꓸꓸꓸ).AddMessageChain(Ꮡchain).SetRelatedInfo(chain.relatedInformation);
    }
    return NewDiagnostic(nil, new core.TextRange(), message, args.ꓸꓸꓸ);
}

public static ж<Diagnostic> NewCompilerDiagnostic(ж<diagnostics.Message> message, params ꓸꓸꓸany argsʗp) {
    var args = argsʗp.sslice();

    return NewDiagnostic(nil, core.UndefinedTextRange(), message, args.ꓸꓸꓸ);
}

public static ж<Diagnostic> NewExternalDiagnostic(ж<SourceFile> @file, core.TextRange loc, @string source, diagnostics.Category category, int32 code, @string messageText) {
    return Ꮡ(new Diagnostic(
        @file: @file,
        loc: loc,
        code: code,
        category: category,
        source: source,
        messageText: messageText
    ));
}

partial struct DiagnosticsCollection {
    internal sync.Mutex mu;
    internal nint count;
    internal map<tspath.Path, slice<ж<Diagnostic>>> fileDiagnostics;
    internal collections.Set<tspath.Path> fileDiagnosticsSorted;
    internal slice<ж<Diagnostic>> nonFileDiagnostics;
    internal bool nonFileDiagnosticsSorted;
    internal map<diagnosticLocationKey, ж<Diagnostic>> diagnosticIndex;
    internal map<diagnosticLocationKey, slice<ж<Diagnostic>>> diagnosticCollisions;
}

} partial class astᴛDiagnosticsCollection {

public static ж<Diagnostic> Add(this ж<DiagnosticsCollection> Ꮡc, ж<Diagnostic> Ꮡdiagnostic) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var c = ref Ꮡc.DerefOrNull();
        ref var diagnostic = ref Ꮡdiagnostic.DerefOrNull();

        c.mu.Lock();
        ᒐd1 = true;

        var key = getDiagnosticLocationKey(Ꮡdiagnostic);
        var existing = c.diagnosticIndex[key]; if (existing != nil) {
            if (EqualDiagnostics(existing, Ꮡdiagnostic)) {
                return existing;
            }
            foreach (var (_, collision) in range(c.diagnosticCollisions[key])) {
                if (EqualDiagnostics(collision, Ꮡdiagnostic)) {
                    return collision;
                }
            }
        }
        if (c.diagnosticIndex == nil) {
            c.diagnosticIndex = new map<diagnosticLocationKey, ж<Diagnostic>>();
        }
        if (c.diagnosticIndex[key] == nil) {
            c.diagnosticIndex[key] = Ꮡdiagnostic;
        } else {
            if (c.diagnosticCollisions == nil) {
                c.diagnosticCollisions = new map<diagnosticLocationKey, slice<ж<Diagnostic>>>();
            }
            c.diagnosticCollisions[key] = append(c.diagnosticCollisions[key], Ꮡdiagnostic);
        }

        c.count++;

        if (diagnostic.File() != nil) {
            tspath.Path path = diagnostic.File().Path();
            if (c.fileDiagnostics == nil) {
                c.fileDiagnostics = new map<tspath.Path, slice<ж<Diagnostic>>>();
            }
            c.fileDiagnostics[path] = append(c.fileDiagnostics[path], Ꮡdiagnostic);
            c.fileDiagnosticsSorted.Delete(path);
        } else {
            c.nonFileDiagnostics = append(c.nonFileDiagnostics, Ꮡdiagnostic);
            c.nonFileDiagnosticsSorted = false;
        }
        return Ꮡdiagnostic;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡc.DerefOrNull().mu.Unlock(); ᒐ.Run(); }
}

} partial class ast_package {

partial struct diagnosticLocationKey {
    internal tspath.Path path;
    internal core.TextRange loc;
    internal int32 code;
}

internal static diagnosticLocationKey getDiagnosticLocationKey(ж<Diagnostic> Ꮡdiagnostic) {
    ref var diagnostic = ref Ꮡdiagnostic.DerefOrNull();

    tspath.Path path = default;
    if (diagnostic.File() != nil) {
        path = diagnostic.File().Path();
    }
    return new diagnosticLocationKey(
        path: path,
        loc: diagnostic.Loc(),
        code: diagnostic.Code()
    );
}

} partial class astᴛDiagnosticsCollection {

public static ж<Diagnostic> Lookup(this ж<DiagnosticsCollection> Ꮡc, ж<Diagnostic> Ꮡdiagnostic) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var c = ref Ꮡc.DerefOrNull();
        ref var diagnostic = ref Ꮡdiagnostic.DerefOrNull();

        c.mu.Lock();
        ᒐd1 = true;

        slice<ж<Diagnostic>> diagnostics = default;
        if (diagnostic.File() != nil) {
            diagnostics = Ꮡc.getDiagnosticsForFileLocked(diagnostic.File());
        } else {
            diagnostics = c.getGlobalDiagnosticsLocked();
        }
        var (i, ok) = slices.BinarySearchFunc<slice<ж<Diagnostic>>, ж<Diagnostic>, ж<Diagnostic>>(diagnostics, Ꮡdiagnostic, CompareDiagnostics); if (ok) {
            return diagnostics[i];
        }
        return default;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡc.DerefOrNull().mu.Unlock(); ᒐ.Run(); }
}

public static slice<ж<Diagnostic>> GetGlobalDiagnostics(this ж<DiagnosticsCollection> Ꮡc) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var c = ref Ꮡc.DerefOrNull();

        c.mu.Lock();
        ᒐd1 = true;

        return c.getGlobalDiagnosticsLocked();
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡc.DerefOrNull().mu.Unlock(); ᒐ.Run(); }
}

internal static slice<ж<Diagnostic>> getGlobalDiagnosticsLocked(this ref DiagnosticsCollection c) {
    if (!c.nonFileDiagnosticsSorted) {
        slices.SortStableFunc<slice<ж<Diagnostic>>, ж<Diagnostic>>(c.nonFileDiagnostics, CompareDiagnostics);
        c.nonFileDiagnosticsSorted = true;
    }
    return slices.Clone<slice<ж<Diagnostic>>, ж<Diagnostic>>(c.nonFileDiagnostics);
}

public static slice<ж<Diagnostic>> GetDiagnosticsForFile(this ж<DiagnosticsCollection> Ꮡc, ж<SourceFile> @file) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var c = ref Ꮡc.DerefOrNull();

        c.mu.Lock();
        ᒐd1 = true;

        return Ꮡc.getDiagnosticsForFileLocked(@file);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡc.DerefOrNull().mu.Unlock(); ᒐ.Run(); }
}

internal static slice<ж<Diagnostic>> getDiagnosticsForFileLocked(this ж<DiagnosticsCollection> Ꮡc, ж<SourceFile> Ꮡfile) {
    ref var c = ref Ꮡc.DerefOrNull();
    ref var @file = ref Ꮡfile.DerefOrNull();

    tspath.Path path = @file.Path();
    if (!Ꮡc.ᏑfileDiagnosticsSorted.Has(path)) {
        slices.SortStableFunc<slice<ж<Diagnostic>>, ж<Diagnostic>>(c.fileDiagnostics[path], CompareDiagnostics);
        c.fileDiagnosticsSorted.Add(path);
    }
    return slices.Clone<slice<ж<Diagnostic>>, ж<Diagnostic>>(c.fileDiagnostics[path]);
}

public static slice<ж<Diagnostic>> GetDiagnostics(this ж<DiagnosticsCollection> Ꮡc) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var c = ref Ꮡc.DerefOrNull();

        c.mu.Lock();
        ᒐd1 = true;

        var diagnostics = new slice<ж<Diagnostic>>(0, c.count);
        diagnostics = appendꓸꓸꓸ(diagnostics, c.nonFileDiagnostics);
        foreach (var (_, diags) in c.fileDiagnostics) {
            diagnostics = appendꓸꓸꓸ(diagnostics, diags);
        }
        slices.SortFunc<slice<ж<Diagnostic>>, ж<Diagnostic>>(diagnostics, CompareDiagnostics);
        return diagnostics;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡc.DerefOrNull().mu.Unlock(); ᒐ.Run(); }
}

} partial class ast_package {

internal static @string getDiagnosticPath(ж<Diagnostic> Ꮡd) {
    ref var d = ref Ꮡd.DerefOrNull();

    if (d.File() != nil) {
        return d.File().FileName();
    }
    return "";
}

public static bool EqualDiagnostics(ж<Diagnostic> Ꮡd1, ж<Diagnostic> Ꮡd2) {
    ref var d1 = ref Ꮡd1.DerefOrNull();
    ref var d2 = ref Ꮡd2.DerefOrNull();

    if (Ꮡd1 == Ꮡd2) {
        return true;
    }
    return EqualDiagnosticsNoRelatedInfo(Ꮡd1, Ꮡd2) && slices.EqualFunc<slice<ж<Diagnostic>>, slice<ж<Diagnostic>>, ж<Diagnostic>, ж<Diagnostic>>(d1.RelatedInformation(), d2.RelatedInformation(), EqualDiagnostics);
}

public static bool EqualDiagnosticsNoRelatedInfo(ж<Diagnostic> Ꮡd1, ж<Diagnostic> Ꮡd2) {
    ref var d1 = ref Ꮡd1.DerefOrNull();
    ref var d2 = ref Ꮡd2.DerefOrNull();

    if (Ꮡd1 == Ꮡd2) {
        return true;
    }
    return getDiagnosticPath(Ꮡd1) == getDiagnosticPath(Ꮡd2) && d1.Loc() == d2.Loc() && d1.Code() == d2.Code() && d1.Category() == d2.Category() && d1.Source() == d2.Source() && getDiagnosticMessageIdentity(Ꮡd1) == getDiagnosticMessageIdentity(Ꮡd2) && slices.Equal<slice<@string>, @string>(d1.MessageArgs(), d2.MessageArgs()) && slices.EqualFunc<slice<ж<Diagnostic>>, slice<ж<Diagnostic>>, ж<Diagnostic>, ж<Diagnostic>>(d1.MessageChain(), d2.MessageChain(), equalMessageChain);
}

internal static @string getDiagnosticMessageIdentity(ж<Diagnostic> Ꮡdiagnostic) {
    ref var diagnostic = ref Ꮡdiagnostic.DerefOrNull();

    if (diagnostic.MessageText() != ""u8) {
        return diagnostic.MessageText();
    }
    if (diagnostic.message != nil && diagnostic.Code() == -1) {
        return diagnostic.message.String();
    }
    return (@string)diagnostic.MessageKey();
}

internal static bool equalMessageChain(ж<Diagnostic> Ꮡc1, ж<Diagnostic> Ꮡc2) {
    ref var c1 = ref Ꮡc1.DerefOrNull();
    ref var c2 = ref Ꮡc2.DerefOrNull();

    if (Ꮡc1 == Ꮡc2) {
        return true;
    }
    return c1.Code() == c2.Code() && slices.Equal<slice<@string>, @string>(c1.MessageArgs(), c2.MessageArgs()) && slices.EqualFunc<slice<ж<Diagnostic>>, slice<ж<Diagnostic>>, ж<Diagnostic>, ж<Diagnostic>>(c1.MessageChain(), c2.MessageChain(), equalMessageChain);
}

internal static nint compareMessageChainSize(slice<ж<Diagnostic>> c1, slice<ж<Diagnostic>> c2) {
    nint c = len(c2) - len(c1);
    if (c != 0) {
        return c;
    }
    foreach (var i in range(len(c1))) {
        c = compareMessageChainSize(c1[i].MessageChain(), c2[i].MessageChain());
        if (c != 0) {
            return c;
        }
    }
    return 0;
}

internal static nint compareMessageChainContent(slice<ж<Diagnostic>> c1, slice<ж<Diagnostic>> c2) {
    foreach (var i in range(len(c1))) {
        nint c = slices.Compare<slice<@string>, @string>(c1[i].MessageArgs(), c2[i].MessageArgs());
        if (c != 0) {
            return c;
        }
        if (c1[i].MessageChain() != nil) {
            c = compareMessageChainContent(c1[i].MessageChain(), c2[i].MessageChain());
            if (c != 0) {
                return c;
            }
        }
    }
    return 0;
}

internal static nint compareRelatedInfo(slice<ж<Diagnostic>> r1, slice<ж<Diagnostic>> r2) {
    nint c = len(r2) - len(r1);
    if (c != 0) {
        return c;
    }
    foreach (var i in range(len(r1))) {
        c = CompareDiagnostics(r1[i], r2[i]);
        if (c != 0) {
            return c;
        }
    }
    return 0;
}

public static nint CompareDiagnostics(ж<Diagnostic> Ꮡd1, ж<Diagnostic> Ꮡd2) {
    ref var d1 = ref Ꮡd1.DerefOrNull();
    ref var d2 = ref Ꮡd2.DerefOrNull();

    if (Ꮡd1 == Ꮡd2) {
        return 0;
    }
    nint c = strings.Compare(getDiagnosticPath(Ꮡd1), getDiagnosticPath(Ꮡd2));
    if (c != 0) {
        return c;
    }
    c = d1.Loc().Pos() - d2.Loc().Pos();
    if (c != 0) {
        return c;
    }
    c = d1.Loc().End() - d2.Loc().End();
    if (c != 0) {
        return c;
    }
    c = (nint)d1.Code() - (nint)d2.Code();
    if (c != 0) {
        return c;
    }
    c = (nint)d1.Category() - (nint)d2.Category();
    if (c != 0) {
        return c;
    }
    c = strings.Compare(d1.Source(), d2.Source());
    if (c != 0) {
        return c;
    }
    c = strings.Compare(getDiagnosticMessageIdentity(Ꮡd1), getDiagnosticMessageIdentity(Ꮡd2));
    if (c != 0) {
        return c;
    }
    c = slices.Compare<slice<@string>, @string>(d1.MessageArgs(), d2.MessageArgs());
    if (c != 0) {
        return c;
    }
    c = compareMessageChainSize(d1.MessageChain(), d2.MessageChain());
    if (c != 0) {
        return c;
    }
    c = compareMessageChainContent(d1.MessageChain(), d2.MessageChain());
    if (c != 0) {
        return c;
    }
    return compareRelatedInfo(d1.RelatedInformation(), d2.RelatedInformation());
}

} // end ast_package
