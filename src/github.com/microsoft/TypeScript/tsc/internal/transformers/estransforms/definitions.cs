namespace go.github.com.microsoft.TypeScript.tsc.@internal.transformers;

using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using transformers = go.github.com.microsoft.TypeScript.tsc.@internal.transformers_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class estransforms_package {

internal static transformersꓸTransformerFactory esDecoratorAndClassFields = transformers.Chain(newESDecoratorTransformer, newClassFieldsTransformer);
public static readonly ж<transformersꓸTransformerFactory> ᏑNewESNextTransformer = new StaticBox<transformersꓸTransformerFactory>(transformers.Chain(newUsingDeclarationTransformer, esDecoratorAndClassFields));
public static ref transformersꓸTransformerFactory NewESNextTransformer => ref ((StaticBox<transformersꓸTransformerFactory>)ᏑNewESNextTransformer).ValueSlot;
public static readonly ж<transformersꓸTransformerFactory> ᏑNewES2021Transformer = new StaticBox<transformersꓸTransformerFactory>(transformers.Chain(NewESNextTransformer, newLogicalAssignmentTransformer));
public static ref transformersꓸTransformerFactory NewES2021Transformer => ref ((StaticBox<transformersꓸTransformerFactory>)ᏑNewES2021Transformer).ValueSlot;
public static readonly ж<transformersꓸTransformerFactory> ᏑNewES2020Transformer = new StaticBox<transformersꓸTransformerFactory>(transformers.Chain(NewES2021Transformer, newNullishCoalescingTransformer, newOptionalChainTransformer));
public static ref transformersꓸTransformerFactory NewES2020Transformer => ref ((StaticBox<transformersꓸTransformerFactory>)ᏑNewES2020Transformer).ValueSlot;
public static readonly ж<transformersꓸTransformerFactory> ᏑNewES2019Transformer = new StaticBox<transformersꓸTransformerFactory>(transformers.Chain(NewES2020Transformer, newOptionalCatchTransformer));
public static ref transformersꓸTransformerFactory NewES2019Transformer => ref ((StaticBox<transformersꓸTransformerFactory>)ᏑNewES2019Transformer).ValueSlot;
public static readonly ж<transformersꓸTransformerFactory> ᏑNewES2018Transformer = new StaticBox<transformersꓸTransformerFactory>(default(transformersꓸTransformerFactory));
public static ref transformersꓸTransformerFactory NewES2018Transformer => ref ((StaticBox<transformersꓸTransformerFactory>)ᏑNewES2018Transformer).ValueSlot;
internal static void initᴛNewES2018Transformer() { NewES2018Transformer = transformers.Chain(NewES2019Transformer, newObjectRestSpreadTransformer, newforawaitTransformer, newTaggedTemplateLiftRestrictionTransformer); }
public static readonly ж<transformersꓸTransformerFactory> ᏑNewES2017Transformer = new StaticBox<transformersꓸTransformerFactory>(default(transformersꓸTransformerFactory));
public static ref transformersꓸTransformerFactory NewES2017Transformer => ref ((StaticBox<transformersꓸTransformerFactory>)ᏑNewES2017Transformer).ValueSlot;
internal static void initᴛNewES2017Transformer() { NewES2017Transformer = transformers.Chain(NewES2018Transformer, newAsyncTransformer); }
public static readonly ж<transformersꓸTransformerFactory> ᏑNewES2016Transformer = new StaticBox<transformersꓸTransformerFactory>(default(transformersꓸTransformerFactory));
public static ref transformersꓸTransformerFactory NewES2016Transformer => ref ((StaticBox<transformersꓸTransformerFactory>)ᏑNewES2016Transformer).ValueSlot;
internal static void initᴛNewES2016Transformer() { NewES2016Transformer = transformers.Chain(NewES2017Transformer, newExponentiationTransformer); }

public static ж<transformers.Transformer> GetESTransformer(ж<transformers.TransformOptions> Ꮡopts) {
    ref var opts = ref Ꮡopts.DerefOrNull();

    var options = opts.CompilerOptions;
    switch (options.GetEmitScriptTarget()) {
    case var ᴛ when ᴛ == core.ScriptTargetESNext: {
        return esDecoratorAndClassFields(Ꮡopts);
    }
    case var ᴛ when ᴛ == core.ScriptTargetES2025 || ᴛ == core.ScriptTargetES2024 || ᴛ == core.ScriptTargetES2023 || ᴛ == core.ScriptTargetES2022 || ᴛ == core.ScriptTargetES2021: {
        return NewESNextTransformer(Ꮡopts);
    }
    case var ᴛ when ᴛ == core.ScriptTargetES2020: {
        return NewES2021Transformer(Ꮡopts);
    }
    case var ᴛ when ᴛ == core.ScriptTargetES2019: {
        return NewES2020Transformer(Ꮡopts);
    }
    case var ᴛ when ᴛ == core.ScriptTargetES2018: {
        return NewES2019Transformer(Ꮡopts);
    }
    case var ᴛ when ᴛ == core.ScriptTargetES2017: {
        return NewES2018Transformer(Ꮡopts);
    }
    case var ᴛ when ᴛ == core.ScriptTargetES2016: {
        return NewES2017Transformer(Ꮡopts);
    }
    default: {
        return NewES2016Transformer(Ꮡopts);
    }}

}

} // end estransforms_package
