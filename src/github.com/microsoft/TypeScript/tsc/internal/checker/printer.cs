namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strings = strings_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using nodebuilder = go.github.com.microsoft.TypeScript.tsc.@internal.nodebuilder_package;
using printer = go.github.com.microsoft.TypeScript.tsc.@internal.printer_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using sourcemap = go.github.com.microsoft.TypeScript.tsc.@internal.sourcemap_package;
using static checker_package;

partial class checker_package {

internal static ж<printer.Printer> createPrinterWithDefaults(ж<printer.EmitContext> emitContext) {
    return printer.NewPrinter(new printer.PrinterOptions(), new printer.PrintHandlers(), emitContext);
}

internal static ж<printer.Printer> createPrinterWithRemoveComments(ж<printer.EmitContext> emitContext) {
    return printer.NewPrinter(new printer.PrinterOptions(RemoveComments: true), new printer.PrintHandlers(), emitContext);
}

internal static ж<printer.Printer> createPrinterWithRemoveCommentsOmitTrailingSemicolon(ж<printer.EmitContext> emitContext) {
    return printer.NewPrinter(new printer.PrinterOptions(
        RemoveComments: true,
        OmitTrailingSemicolon: true
    ), new printer.PrintHandlers(), emitContext);
}

internal static ж<printer.Printer> createPrinterWithRemoveCommentsOmitTrailingSemicolonNeverAsciiEscape(ж<printer.EmitContext> emitContext) {
    return printer.NewPrinter(new printer.PrinterOptions(
        RemoveComments: true,
        OmitTrailingSemicolon: true,
        NeverAsciiEscape: true
    ), new printer.PrintHandlers(), emitContext);
}

internal static ж<printer.Printer> createPrinterWithRemoveCommentsNeverAsciiEscape(ж<printer.EmitContext> emitContext) {
    return printer.NewPrinter(new printer.PrinterOptions(
        RemoveComments: true,
        NeverAsciiEscape: true
    ), new printer.PrintHandlers(), emitContext);
}

} partial class checkerᴛChecker {

public static @string TypeToString(this ж<Checker> Ꮡc, ж<Type> t) {
    return Ꮡc.typeToString(t, nil);
}

internal static @string typeToString(this ж<Checker> Ꮡc, ж<Type> t, ж<ast.Node> enclosingDeclaration) {
    return Ꮡc.typeToStringEx(t, enclosingDeclaration, (TypeFormatFlags)(TypeFormatFlagsAllowUniqueESSymbolType | TypeFormatFlagsUseAliasDefinedOutsideCurrentScope), nil);
}

} partial class checker_package {

internal static nodebuilder.Flags toNodeBuilderFlags(TypeFormatFlags flags) {
    return (nodebuilder.Flags)(uint32)((TypeFormatFlags)(flags & TypeFormatFlagsNodeBuilderFlagsMask));
}

} partial class checkerᴛChecker {

public static @string TypeToStringEx(this ж<Checker> Ꮡc, ж<Type> t, ж<ast.Node> enclosingDeclaration, TypeFormatFlags flags, ж<VerbosityContext> vc) {
    return Ꮡc.typeToStringEx(t, enclosingDeclaration, flags, vc);
}

internal static @string typeToStringEx(this ж<Checker> Ꮡc, ж<Type> t, ж<ast.Node> enclosingDeclaration, TypeFormatFlags flags, ж<VerbosityContext> Ꮡvc) {
    GoFrame ᒐ = default;
    try {
        ref var c = ref Ꮡc.DerefOrNull();
        ref var vc = ref Ꮡvc.DerefOrNull();

        if (c.serializationLevel >= maxSerializationLevel) {
            return "?";
        }
        @string newLine = "";
        if ((TypeFormatFlags)(flags & TypeFormatFlagsMultilineObjectLiterals) != 0) {
            newLine = "\n";
        }
        var writer = printer.NewTextWriter(newLine, 0);
        var noTruncation = ((Ꮡvc == nil || vc.MaxTruncationLength == 0) && c.compilerOptions.NoErrorTruncation == core.TSTrue) || ((TypeFormatFlags)(flags & TypeFormatFlagsNoTruncation) != 0);
        var combinedFlags = (nodebuilder.Flags)(toNodeBuilderFlags(flags) | nodebuilder.FlagsIgnoreErrors);
        if (noTruncation) {
            combinedFlags = (nodebuilder.Flags)(combinedFlags | nodebuilder.FlagsNoTruncation);
        }
        var (nodeBuilder, release) = Ꮡc.getNodeBuilder();
        var releaseʗ1 = release;
        defer(releaseʗ1, ref ᒐ);
        var oldVerbosity = nodeBuilder.verbosity;
        nodeBuilder.verbosity = Ꮡvc;
        var nodeBuilderʗ1 = nodeBuilder;
        var oldVerbosityʗ1 = oldVerbosity;
        defer(() => {
            nodeBuilderʗ1.verbosity = oldVerbosityʗ1;
        }, ref ᒐ);
        c.serializationLevel++;
        var typeNode = nodeBuilder.TypeToTypeNode(t, enclosingDeclaration, combinedFlags, nodebuilder.InternalFlagsNone, default);
        c.serializationLevel--;
        if (typeNode == nil) {
            throw panic("should always get typenode");
        }
        ж<printer.Printer> p = default;
        if (t == c.unresolvedType) {
            p = createPrinterWithDefaults(nodeBuilder.EmitContext());
        } else {
            p = createPrinterWithRemoveComments(nodeBuilder.EmitContext());
        }
        ж<ast.SourceFile> sourceFile = default;
        if (enclosingDeclaration != nil) {
            sourceFile = ast.GetSourceFileOfNode(enclosingDeclaration);
        }
        p.Write(typeNode, sourceFile, writer, nil);
        @string result = writer.String();

        nint maxLength = defaultMaximumTruncationLength * 2;
        if (Ꮡvc != nil && vc.MaxTruncationLength > 0) {
            maxLength = vc.MaxTruncationLength * 10;
        }
        if (noTruncation) {
            maxLength = noTruncationMaximumTruncationLength * 2;
        }
        if (maxLength > 0 && result != ""u8 && len(result) >= maxLength) {
            if (Ꮡvc != nil) {
                vc.Truncated = true;
            }
            return result.slice(0, maxLength - len("...")) + "...";
        }
        return result;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

public static @string SymbolToString(this ж<Checker> Ꮡc, ж<ast.Symbol> s) {
    return Ꮡc.symbolToString(s);
}

internal static @string symbolToString(this ж<Checker> Ꮡc, ж<ast.Symbol> symbol) {
    return Ꮡc.symbolToStringEx(symbol, nil, ast.SymbolFlagsAll, SymbolFormatFlagsAllowAnyNodeKind);
}

public static @string SymbolToStringEx(this ж<Checker> Ꮡc, ж<ast.Symbol> symbol, ж<ast.Node> enclosingDeclaration, ast.SymbolFlags meaning, SymbolFormatFlags flags) {
    return Ꮡc.symbolToStringEx(symbol, enclosingDeclaration, meaning, flags);
}

internal static @string symbolToStringEx(this ж<Checker> Ꮡc, ж<ast.Symbol> symbol, ж<ast.Node> ᏑenclosingDeclaration, ast.SymbolFlags meaning, SymbolFormatFlags flags) {
    GoFrame ᒐ = default;
    try {
        ref var enclosingDeclaration = ref ᏑenclosingDeclaration.DerefOrNull();

        var (writer, putWriter) = printer.GetSingleLineStringWriter();
        var putWriterʗ1 = putWriter;
        defer(putWriterʗ1, ref ᒐ);

        var nodeFlags = nodebuilder.FlagsIgnoreErrors;
        var internalNodeFlags = nodebuilder.InternalFlagsNone;
        if ((SymbolFormatFlags)(flags & SymbolFormatFlagsUseOnlyExternalAliasing) != 0) {
            nodeFlags |= (nodebuilder.Flags)(nodebuilder.FlagsUseOnlyExternalAliasing);
        }
        if ((SymbolFormatFlags)(flags & SymbolFormatFlagsWriteTypeParametersOrArguments) != 0) {
            nodeFlags |= (nodebuilder.Flags)(nodebuilder.FlagsWriteTypeParametersInQualifiedName);
        }
        if ((SymbolFormatFlags)(flags & SymbolFormatFlagsUseAliasDefinedOutsideCurrentScope) != 0) {
            nodeFlags |= (nodebuilder.Flags)(nodebuilder.FlagsUseAliasDefinedOutsideCurrentScope);
        }
        if ((SymbolFormatFlags)(flags & SymbolFormatFlagsDoNotIncludeSymbolChain) != 0) {
            internalNodeFlags |= (nodebuilder.InternalFlags)(nodebuilder.InternalFlagsDoNotIncludeSymbolChain);
        }
        if ((SymbolFormatFlags)(flags & SymbolFormatFlagsWriteComputedProps) != 0) {
            internalNodeFlags |= (nodebuilder.InternalFlags)(nodebuilder.InternalFlagsWriteComputedProps);
        }

        var (nodeBuilder, release) = Ꮡc.getNodeBuilder();
        var releaseʗ1 = release;
        defer(releaseʗ1, ref ᒐ);
        ж<ast.SourceFile> sourceFile = default;
        if (ᏑenclosingDeclaration != nil) {
            sourceFile = ast.GetSourceFileOfNode(ᏑenclosingDeclaration);
        }
        ж<printer.Printer> printer_ = default;
        if (ᏑenclosingDeclaration != nil && enclosingDeclaration.Kind == ast.KindSourceFile) {
            printer_ = createPrinterWithRemoveCommentsOmitTrailingSemicolonNeverAsciiEscape(nodeBuilder.EmitContext());
        } else {
            printer_ = createPrinterWithRemoveCommentsOmitTrailingSemicolon(nodeBuilder.EmitContext());
        }

        Func<ж<ast.Symbol>, ast.SymbolFlags, ж<ast.Node>, nodebuilder.Flags, nodebuilder.InternalFlags, nodebuilder.SymbolTracker, ж<ast.Node>> builder = default;
        if ((SymbolFormatFlags)(flags & SymbolFormatFlagsAllowAnyNodeKind) != 0) {
            var nodeBuilderʗ1 = nodeBuilder;
                        builder = (ж<ast.Symbol> p1, ast.SymbolFlags p2, ж<ast.Node> p3, nodebuilder.Flags p4, nodebuilder.InternalFlags p5, nodebuilder.SymbolTracker p6) => nodeBuilderʗ1.SymbolToNode(p1, p2, p3, p4, p5, p6);
        } else {
            var nodeBuilderʗ2 = nodeBuilder;
                        builder = (ж<ast.Symbol> p1, ast.SymbolFlags p2, ж<ast.Node> p3, nodebuilder.Flags p4, nodebuilder.InternalFlags p5, nodebuilder.SymbolTracker p6) => nodeBuilderʗ2.SymbolToEntityName(p1, p2, p3, p4, p5, p6);
        }
        var entity = builder(symbol, meaning, ᏑenclosingDeclaration, nodeFlags, internalNodeFlags, default);
        printer_.Write(entity, sourceFile, writer, nil);
        return writer.String();
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static @string signatureToString(this ж<Checker> Ꮡc, ж<Signature> signature) {
    return Ꮡc.signatureToStringEx(signature, nil, TypeFormatFlagsNone, nil);
}

public static @string SignatureToStringEx(this ж<Checker> Ꮡc, ж<Signature> signature, ж<ast.Node> enclosingDeclaration, TypeFormatFlags flags, ж<VerbosityContext> vc) {
    return Ꮡc.signatureToStringEx(signature, enclosingDeclaration, flags, vc);
}

internal static @string signatureToStringEx(this ж<Checker> Ꮡc, ж<Signature> Ꮡsignature, ж<ast.Node> enclosingDeclaration, TypeFormatFlags flags, ж<VerbosityContext> vc) {
    GoFrame ᒐ = default;
    try {
        ref var signature = ref Ꮡsignature.DerefOrNull();

        var isConstructor = (SignatureFlags)(signature.flags & SignatureFlagsConstruct) != 0 && (TypeFormatFlags)(flags & TypeFormatFlagsWriteCallStyleSignature) == 0;
        ast.Kind sigOutput = default;
        if ((TypeFormatFlags)(flags & TypeFormatFlagsWriteArrowStyleSignature) != 0) {
            if (isConstructor) {
                sigOutput = ast.KindConstructorType;
            } else {
                sigOutput = ast.KindFunctionType;
            }
        } else {
            if (isConstructor) {
                sigOutput = ast.KindConstructSignature;
            } else {
                sigOutput = ast.KindCallSignature;
            }
        }

        var (nodeBuilder, release) = Ꮡc.getNodeBuilder();
        var releaseʗ1 = release;
        defer(releaseʗ1, ref ᒐ);
        var oldVerbosity = nodeBuilder.verbosity;
        nodeBuilder.verbosity = vc;
        var nodeBuilderʗ1 = nodeBuilder;
        var oldVerbosityʗ1 = oldVerbosity;
        defer(() => {
            nodeBuilderʗ1.verbosity = oldVerbosityʗ1;
        }, ref ᒐ);
        var combinedFlags = (nodebuilder.Flags)((nodebuilder.Flags)(toNodeBuilderFlags(flags) | nodebuilder.FlagsIgnoreErrors) | nodebuilder.FlagsWriteTypeParametersInQualifiedName);
        var sig = nodeBuilder.SignatureToSignatureDeclaration(Ꮡsignature, sigOutput, enclosingDeclaration, combinedFlags, nodebuilder.InternalFlagsNone, default);
        var p = createPrinterWithRemoveCommentsOmitTrailingSemicolonNeverAsciiEscape(nodeBuilder.EmitContext());
        ж<ast.SourceFile> sourceFile = default;
        if (enclosingDeclaration != nil) {
            sourceFile = ast.GetSourceFileOfNode(enclosingDeclaration);
        }
        if ((TypeFormatFlags)(flags & TypeFormatFlagsMultilineObjectLiterals) != 0) {
            var writerΔ1 = printer.NewTextWriter("\n", 0);
            p.Write(sig, sourceFile, writerΔ1, nil);
            return writerΔ1.String();
        }
        var (writer, putWriter) = printer.GetSingleLineStringWriter();
        var putWriterʗ1 = putWriter;
        defer(putWriterʗ1, ref ᒐ);
        p.Write(sig, sourceFile, writer, nil);
        return writer.String();
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static @string typePredicateToString(this ж<Checker> Ꮡc, ж<TypePredicate> typePredicate) {
    return Ꮡc.typePredicateToStringEx(typePredicate, nil, TypeFormatFlagsUseAliasDefinedOutsideCurrentScope);
}

internal static @string typePredicateToStringEx(this ж<Checker> Ꮡc, ж<TypePredicate> typePredicate, ж<ast.Node> enclosingDeclaration, TypeFormatFlags flags) {
    GoFrame ᒐ = default;
    try {
        var (writer, putWriter) = printer.GetSingleLineStringWriter();
        var putWriterʗ1 = putWriter;
        defer(putWriterʗ1, ref ᒐ);
        var (nodeBuilder, release) = Ꮡc.getNodeBuilder();
        var releaseʗ1 = release;
        defer(releaseʗ1, ref ᒐ);
        var combinedFlags = (nodebuilder.Flags)((nodebuilder.Flags)(toNodeBuilderFlags(flags) | nodebuilder.FlagsIgnoreErrors) | nodebuilder.FlagsWriteTypeParametersInQualifiedName);
        var predicate = nodeBuilder.TypePredicateToTypePredicateNode(typePredicate, enclosingDeclaration, combinedFlags, nodebuilder.InternalFlagsNone, default);
        var printer_ = createPrinterWithRemoveComments(nodeBuilder.EmitContext());
        ж<ast.SourceFile> sourceFile = default;
        if (enclosingDeclaration != nil) {
            sourceFile = ast.GetSourceFileOfNode(enclosingDeclaration);
        }
        printer_.Write(predicate, sourceFile, writer, nil);
        return writer.String();
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

internal static @string valueToString(this ref Checker c, any value) {
    return ValueToString(value);
}

internal static slice<ж<Type>> formatUnionTypes(this ж<Checker> Ꮡc, slice<ж<Type>> types, bool expandingEnum) {
    ref var c = ref Ꮡc.DerefOrNull();

    slice<ж<Type>> result = default;
    TypeFlags flags = default;
    for (nint i = 0; i < len(types); i++) {
        var t = types[i];
        flags |= (TypeFlags)(t.flags);
        if ((TypeFlags)(t.flags & TypeFlagsNullable) == 0) {
            if ((TypeFlags)(t.flags & TypeFlagsBooleanLiteral) != 0 || (!expandingEnum && (TypeFlags)(t.flags & TypeFlagsEnumLike) != 0)) {
                ж<Type> baseType = default;
                if ((TypeFlags)(t.flags & TypeFlagsBooleanLiteral) != 0) {
                    baseType = c.booleanType;
                } else {
                    baseType = Ꮡc.getBaseTypeOfEnumLikeType(t);
                }
                if ((TypeFlags)(baseType.flags & TypeFlagsUnion) != 0) {
                    nint count = len(baseType.AsUnionType().types);
                    if (i + count <= len(types) && Ꮡc.getRegularTypeOfLiteralType(types[i + count - 1]) == Ꮡc.getRegularTypeOfLiteralType(baseType.AsUnionType().types[count - 1])) {
                        result = append(result, baseType);
                        i += count - 1;
                        continue;
                    }
                }
            }
            result = append(result, t);
        }
    }
    if ((TypeFlags)(flags & TypeFlagsNull) != 0) {
        result = append(result, c.nullType);
    }
    if ((TypeFlags)(flags & TypeFlagsUndefined) != 0) {
        result = append(result, c.undefinedType);
    }
    return result;
}

public static ж<astꓸTypeNode> TypeToTypeNode(this ж<Checker> Ꮡc, ж<Type> t, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, map<ж<astꓸIdentifierNode>, ж<ast.Symbol>> idToSymbol) {
    var nodeBuilder = Ꮡc.getNodeBuilderEx(idToSymbol);
    return nodeBuilder.TypeToTypeNode(t, enclosingDeclaration, flags, nodebuilder.InternalFlagsNone, default);
}

public static ж<ast.Node> SignatureToSignatureDeclaration(this ж<Checker> Ꮡc, ж<Signature> signature, ast.Kind kind, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags) {
    GoFrame ᒐ = default;
    try {
        var (nodeBuilder, release) = Ꮡc.getNodeBuilder();
        var releaseʗ1 = release;
        defer(releaseʗ1, ref ᒐ);
        return nodeBuilder.SignatureToSignatureDeclaration(signature, kind, enclosingDeclaration, flags, nodebuilder.InternalFlagsNone, default);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

public static @string ExpandSymbolForHover(this ж<Checker> Ꮡc, ж<ast.Symbol> Ꮡsymbol, ast.SymbolFlags meaning, ж<VerbosityContext> vc) {
    GoFrame ᒐ = default;
    try {
        ref var symbol = ref Ꮡsymbol.DerefOrNull();

        var (nodeBuilder, release) = Ꮡc.getNodeBuilder();
        var releaseʗ1 = release;
        defer(releaseʗ1, ref ᒐ);
        var oldVerbosity = nodeBuilder.verbosity;
        nodeBuilder.verbosity = vc;
        var nodeBuilderʗ1 = nodeBuilder;
        var oldVerbosityʗ1 = oldVerbosity;
        defer(() => {
            nodeBuilderʗ1.verbosity = oldVerbosityʗ1;
        }, ref ᒐ);
        var nodes = nodeBuilder.ExpandSymbolForHover(Ꮡsymbol, meaning);
        if (len(nodes) == 0) {
            return "";
        }
        var p = createPrinterWithRemoveComments(nodeBuilder.EmitContext());
        ж<ast.SourceFile> sourceFile = default;
        if (symbol.ValueDeclaration != nil) {
            sourceFile = ast.GetSourceFileOfNode(symbol.ValueDeclaration);
        }
        ref var b = ref heap(new strings.Builder(), out var Ꮡb);
        foreach (var (i, node) in range(nodes)) {
            if (i > 0) {
                Ꮡb.WriteString("\n");
            }
            Ꮡb.WriteString(p.Emit(node, sourceFile));
        }
        return b.String();
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

public static @string TypeParameterToStringEx(this ж<Checker> Ꮡc, ж<Type> t, ж<ast.Node> enclosingDeclaration, ж<VerbosityContext> vc) {
    GoFrame ᒐ = default;
    try {
        var (nodeBuilder, release) = Ꮡc.getNodeBuilder();
        var releaseʗ1 = release;
        defer(releaseʗ1, ref ᒐ);
        var oldVerbosity = nodeBuilder.verbosity;
        nodeBuilder.verbosity = vc;
        var nodeBuilderʗ1 = nodeBuilder;
        var oldVerbosityʗ1 = oldVerbosity;
        defer(() => {
            nodeBuilderʗ1.verbosity = oldVerbosityʗ1;
        }, ref ᒐ);
        var typeParamNode = nodeBuilder.TypeParameterToDeclaration(t, enclosingDeclaration, nodebuilder.FlagsIgnoreErrors, nodebuilder.InternalFlagsNone, default);
        if (typeParamNode == nil) {
            return Ꮡc.TypeToString(t);
        }
        var p = createPrinterWithRemoveComments(nodeBuilder.EmitContext());
        ж<ast.SourceFile> sourceFile = default;
        if (enclosingDeclaration != nil) {
            sourceFile = ast.GetSourceFileOfNode(enclosingDeclaration);
        }
        return p.Emit(typeParamNode, sourceFile);
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

public static ж<astꓸTypeNode> TypeToTypeNodeEx(this ж<Checker> Ꮡc, ж<Type> t, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, nodebuilder.InternalFlags internalFlags, map<ж<astꓸIdentifierNode>, ж<ast.Symbol>> idToSymbol) {
    var nodeBuilder = Ꮡc.getNodeBuilderEx(idToSymbol);
    return nodeBuilder.TypeToTypeNode(t, enclosingDeclaration, flags, internalFlags, default);
}

public static ж<astꓸTypePredicateNodeNode> TypePredicateToTypePredicateNode(this ж<Checker> Ꮡc, ж<TypePredicate> t, ж<ast.Node> enclosingDeclaration, nodebuilder.Flags flags, map<ж<astꓸIdentifierNode>, ж<ast.Symbol>> idToSymbol) {
    var nodeBuilder = Ꮡc.getNodeBuilderEx(idToSymbol);
    return nodeBuilder.TypePredicateToTypePredicateNode(t, enclosingDeclaration, flags, nodebuilder.InternalFlagsNone, default);
}

} // end checkerᴛChecker
