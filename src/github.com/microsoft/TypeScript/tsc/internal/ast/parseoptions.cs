namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class ast_package {

partial struct SourceFileParseOptions {
    public @string FileName;
    public tspath.Path Path;
    public ExternalModuleIndicatorOptions ExternalModuleIndicatorOptions;
}

partial struct ExternalModuleIndicatorOptions {
    public bool JSX;
    public bool Force;
}

public static ExternalModuleIndicatorOptions GetExternalModuleIndicatorOptions(@string fileName, ж<core.CompilerOptions> Ꮡoptions, SourceFileMetaData metadata) {
    ref var options = ref Ꮡoptions.DerefOrNull();

    if (tspath.IsDeclarationFileName(fileName)) {
        return new ExternalModuleIndicatorOptions();
    }

    switch (options.GetEmitModuleDetectionKind()) {
    case var ᴛ when ᴛ == core.ModuleDetectionKindForce: {
        return new ExternalModuleIndicatorOptions(Force: true);
    }
    case var ᴛ when ᴛ == core.ModuleDetectionKindLegacy: {
        return new ExternalModuleIndicatorOptions();
    }
    case var ᴛ when ᴛ == core.ModuleDetectionKindAuto: {
        return new ExternalModuleIndicatorOptions(
            JSX: options.Jsx == core.JsxEmitReactJSX || options.Jsx == core.JsxEmitReactJSXDev,
            Force: isFileForcedToBeModuleByFormat(fileName, Ꮡoptions, metadata)
        );
    }
    default: {
        return new ExternalModuleIndicatorOptions();
    }}

}

internal static slice<@string> isFileForcedToBeModuleByFormatExtensions = [tspath.ExtensionCjs, tspath.ExtensionCts, tspath.ExtensionMjs, tspath.ExtensionMts];

internal static bool isFileForcedToBeModuleByFormat(@string fileName, ж<core.CompilerOptions> Ꮡoptions, SourceFileMetaData metadata) {
    ref var options = ref Ꮡoptions.DerefOrNull();

    if (GetImpliedNodeFormatForEmitWorker(fileName, options.GetEmitModuleKind(), metadata) == core.ModuleKindESNext || tspath.FileExtensionIsOneOf(fileName, isFileForcedToBeModuleByFormatExtensions)) {
        return true;
    }
    return false;
}

public static void SetExternalModuleIndicator(ж<SourceFile> Ꮡfile, ExternalModuleIndicatorOptions opts) {
    ref var @file = ref Ꮡfile.DerefOrNull();

    @file.ExternalModuleIndicator = getExternalModuleIndicator(Ꮡfile, opts);
}

internal static ж<Node> getExternalModuleIndicator(ж<SourceFile> Ꮡfile, ExternalModuleIndicatorOptions opts) {
    ref var @file = ref Ꮡfile.DerefOrNull();

    if (@file.ScriptKind == core.ScriptKindJSON) {
        return default;
    }

    {
        var node = isFileProbablyExternalModule(Ꮡfile); if (node != nil) {
            return node;
        }
    }

    if (@file.IsDeclarationFile) {
        return default;
    }

    if (opts.JSX) {
        var node = isFileModuleFromUsingJSXTag(Ꮡfile); if (node != nil) {
            return node;
        }
    }

    if (opts.Force) {
        return Ꮡfile.of(SourceFile.ᏑNodeBase).of(NodeBase.ᏑNodeDefault).AsNode();
    }

    return default;
}

internal static ж<Node> isFileProbablyExternalModule(ж<SourceFile> ᏑsourceFile) {
    ref var sourceFile = ref ᏑsourceFile.DerefOrNull();

    foreach (var (_, statement) in range(sourceFile.Statements.Nodes)) {
        if (isAnExternalModuleIndicatorNode(statement)) {
            return statement;
        }
    }
    return getImportMetaIfNecessary(ᏑsourceFile);
}

internal static bool isAnExternalModuleIndicatorNode(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return HasSyntacticModifier(Ꮡnode, ModifierFlagsExport) || IsImportEqualsDeclaration(Ꮡnode) && IsExternalModuleReference(node.AsImportEqualsDeclaration().ModuleReference) || IsImportDeclaration(Ꮡnode) || IsExportAssignment(Ꮡnode) || IsExportDeclaration(Ꮡnode);
}

internal static ж<Node> getImportMetaIfNecessary(ж<SourceFile> sourceFile) {
    if ((NodeFlags)(sourceFile.of(SourceFile.ᏑNodeBase).of(NodeBase.ᏑNodeDefault).AsNode().Flags & NodeFlagsPossiblyContainsImportMeta) != 0) {
        return findChildNode(sourceFile.of(SourceFile.ᏑNodeBase).of(NodeBase.ᏑNodeDefault).AsNode(), IsImportMeta);
    }
    return default;
}

internal static ж<Node> findChildNode(ж<Node> root, Func<ж<Node>, bool> check) {
    ref var result = ref heap<ж<Node>>(out var Ꮡresult);
    ref var visit = ref heap<Func<ж<Node>, bool>>(out var Ꮡvisit);
    visit = (ж<Node> node) => {
        if (check(node)) {
            Ꮡresult.ValueSlot = node;
            return true;
        }
        return node.ForEachChild(Ꮡvisit.ValueSlot);
    };
    visit(root);
    return result;
}

internal static ж<Node> isFileModuleFromUsingJSXTag(ж<SourceFile> @file) {
    return walkTreeForJSXTags(@file.of(SourceFile.ᏑNodeBase).of(NodeBase.ᏑNodeDefault).AsNode());
}

internal static ж<Node> walkTreeForJSXTags(ж<Node> node) {
    ref var found = ref heap<ж<Node>>(out var Ꮡfound);

    ref var visitor = ref heap<Func<ж<Node>, bool>>(out var Ꮡvisitor);
    visitor = (ж<Node> nodeΔ1) => {
        if (Ꮡfound.ValueSlot != nil) {
            return true;
        }
        if ((SubtreeFacts)(nodeΔ1.SubtreeFacts() & SubtreeContainsJsx) == 0) {
            return false;
        }
        if (IsJsxOpeningLikeElement(nodeΔ1) || IsJsxFragment(nodeΔ1)) {
            Ꮡfound.ValueSlot = nodeΔ1;
            return true;
        }
        return nodeΔ1.ForEachChild(Ꮡvisitor.ValueSlot);
    };
    visitor(node);

    return found;
}

} // end ast_package
