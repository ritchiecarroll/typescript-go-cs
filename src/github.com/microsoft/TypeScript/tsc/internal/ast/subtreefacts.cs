namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class ast_package {

partial struct SubtreeFacts /*num:uint32*/;

public static SubtreeFacts SubtreeContainsTypeScript => /* 1 << iota */ 1;
public static SubtreeFacts SubtreeContainsJsx => 2;
public static SubtreeFacts SubtreeContainsESDecorators => 4;
public static SubtreeFacts SubtreeContainsUsing => 8;
public static SubtreeFacts SubtreeContainsClassStaticBlocks => 16;
public static SubtreeFacts SubtreeContainsESClassFields => 32;
public static SubtreeFacts SubtreeContainsLogicalAssignments => 64;
public static SubtreeFacts SubtreeContainsNullishCoalescing => 128;
public static SubtreeFacts SubtreeContainsOptionalChaining => 256;
public static SubtreeFacts SubtreeContainsMissingCatchClauseVariable => 512;
public static SubtreeFacts SubtreeContainsESObjectRestOrSpread => 1024;
public static SubtreeFacts SubtreeContainsForAwaitOrAsyncGenerator => 2048;
public static SubtreeFacts SubtreeContainsAnyAwait => 4096;
public static SubtreeFacts SubtreeContainsExponentiationOperator => 8192;
public static SubtreeFacts SubtreeContainsLexicalThis => 16384;
public static SubtreeFacts SubtreeContainsLexicalSuper => 32768;
public static SubtreeFacts SubtreeContainsRestOrSpread => 65536;
public static SubtreeFacts SubtreeContainsObjectRestOrSpread => 131072;
public static SubtreeFacts SubtreeContainsAwait => 262144;
public static SubtreeFacts SubtreeContainsDynamicImport => 524288;
public static SubtreeFacts SubtreeContainsClassFields => 1048576;
public static SubtreeFacts SubtreeContainsDecorators => 2097152;
public static SubtreeFacts SubtreeContainsIdentifier => 4194304;
public static SubtreeFacts SubtreeContainsPrivateIdentifierInExpression => 8388608;
public static SubtreeFacts SubtreeContainsInvalidTemplateEscape => 16777216;
public static SubtreeFacts SubtreeFactsComputed => 33554432;
public static SubtreeFacts SubtreeFactsNone => 0;
public static SubtreeFacts SubtreeContainsESNext => /* SubtreeContainsESDecorators | SubtreeContainsUsing */ 12;
public static SubtreeFacts SubtreeContainsES2022 => /* SubtreeContainsClassStaticBlocks | SubtreeContainsESClassFields */ 48;
public static SubtreeFacts SubtreeContainsES2021 => /* SubtreeContainsLogicalAssignments */ 64;
public static SubtreeFacts SubtreeContainsES2020 => /* SubtreeContainsNullishCoalescing | SubtreeContainsOptionalChaining */ 384;
public static SubtreeFacts SubtreeContainsES2019 => /* SubtreeContainsMissingCatchClauseVariable */ 512;
public static SubtreeFacts SubtreeContainsES2018 => /* SubtreeContainsESObjectRestOrSpread | SubtreeContainsForAwaitOrAsyncGenerator | SubtreeContainsInvalidTemplateEscape */ 16780288;
public static SubtreeFacts SubtreeContainsES2017 => /* SubtreeContainsAnyAwait */ 4096;
public static SubtreeFacts SubtreeContainsES2016 => /* SubtreeContainsExponentiationOperator */ 8192;
public static SubtreeFacts SubtreeExclusionsNode => /* SubtreeFactsComputed */ 33554432;
public static SubtreeFacts SubtreeExclusionsEraseable => /* ^SubtreeContainsTypeScript */ 4294967294;
public static SubtreeFacts SubtreeExclusionsOuterExpression => /* SubtreeExclusionsNode */ 33554432;
public static SubtreeFacts SubtreeExclusionsPropertyAccess => /* SubtreeExclusionsNode */ 33554432;
public static SubtreeFacts SubtreeExclusionsElementAccess => /* SubtreeExclusionsNode */ 33554432;
public static SubtreeFacts SubtreeExclusionsArrowFunction => /* SubtreeExclusionsNode | SubtreeContainsAwait | SubtreeContainsObjectRestOrSpread */ 33947648;
public static SubtreeFacts SubtreeExclusionsFunction => /* SubtreeExclusionsNode | SubtreeContainsLexicalThis | SubtreeContainsLexicalSuper | SubtreeContainsAwait | SubtreeContainsObjectRestOrSpread */ 33996800;
public static SubtreeFacts SubtreeExclusionsConstructor => /* SubtreeExclusionsNode | SubtreeContainsLexicalThis | SubtreeContainsLexicalSuper | SubtreeContainsAwait | SubtreeContainsObjectRestOrSpread */ 33996800;
public static SubtreeFacts SubtreeExclusionsMethod => /* SubtreeExclusionsNode | SubtreeContainsLexicalThis | SubtreeContainsLexicalSuper | SubtreeContainsAwait | SubtreeContainsObjectRestOrSpread */ 33996800;
public static SubtreeFacts SubtreeExclusionsAccessor => /* SubtreeExclusionsNode | SubtreeContainsLexicalThis | SubtreeContainsLexicalSuper | SubtreeContainsAwait | SubtreeContainsObjectRestOrSpread */ 33996800;
public static SubtreeFacts SubtreeExclusionsProperty => /* SubtreeExclusionsNode | SubtreeContainsLexicalThis | SubtreeContainsLexicalSuper */ 33603584;
public static SubtreeFacts SubtreeExclusionsClass => /* SubtreeExclusionsNode */ 33554432;
public static SubtreeFacts SubtreeExclusionsModule => /* SubtreeExclusionsNode | SubtreeContainsLexicalThis | SubtreeContainsLexicalSuper */ 33603584;
public static SubtreeFacts SubtreeExclusionsObjectLiteral => /* SubtreeExclusionsNode | SubtreeContainsObjectRestOrSpread */ 33685504;
public static SubtreeFacts SubtreeExclusionsArrayLiteral => /* SubtreeExclusionsNode */ 33554432;
public static SubtreeFacts SubtreeExclusionsCall => /* SubtreeExclusionsNode */ 33554432;
public static SubtreeFacts SubtreeExclusionsNew => /* SubtreeExclusionsNode */ 33554432;
public static SubtreeFacts SubtreeExclusionsVariableDeclarationList => /* SubtreeExclusionsNode | SubtreeContainsObjectRestOrSpread */ 33685504;
public static SubtreeFacts SubtreeExclusionsParameter => /* SubtreeExclusionsNode */ 33554432;
public static SubtreeFacts SubtreeExclusionsCatchClause => /* SubtreeExclusionsNode | SubtreeContainsObjectRestOrSpread */ 33685504;
public static SubtreeFacts SubtreeExclusionsBindingPattern => /* SubtreeExclusionsNode | SubtreeContainsRestOrSpread */ 33619968;
public static SubtreeFacts SubtreeContainsLexicalThisOrSuper => /* SubtreeContainsLexicalThis | SubtreeContainsLexicalSuper */ 49152;

internal static SubtreeFacts propagateEraseableSyntaxListSubtreeFacts(ж<TypeArgumentList> children) {
    return core.IfElse(children != nil, SubtreeContainsTypeScript, SubtreeFactsNone);
}

internal static SubtreeFacts propagateEraseableSyntaxSubtreeFacts(ж<TypeNode> child) {
    return core.IfElse(child != nil, SubtreeContainsTypeScript, SubtreeFactsNone);
}

internal static SubtreeFacts propagateObjectBindingElementSubtreeFacts(ж<BindingElementNode> child) {
    var facts = propagateSubtreeFacts(child);
    if ((SubtreeFacts)(facts & SubtreeContainsRestOrSpread) != 0) {
        facts &= unchecked((SubtreeFacts)~(SubtreeFacts)(SubtreeContainsRestOrSpread));
        facts |= (SubtreeFacts)((SubtreeFacts)(SubtreeContainsObjectRestOrSpread | SubtreeContainsESObjectRestOrSpread));
    }
    return facts;
}

internal static SubtreeFacts propagateBindingElementSubtreeFacts(ж<BindingElementNode> child) {
    return (SubtreeFacts)(propagateSubtreeFacts(child) & ~((SubtreeFacts)SubtreeContainsRestOrSpread));
}

internal static SubtreeFacts propagateSubtreeFacts(ж<Node> Ꮡchild) {
    ref var child = ref Ꮡchild.DerefOrNull();

    if (Ꮡchild == nil) {
        return SubtreeFactsNone;
    }
    return child.propagateSubtreeFacts();
}

internal static SubtreeFacts propagateNodeListSubtreeFacts(ж<NodeList> Ꮡchildren, Func<ж<Node>, SubtreeFacts> propagate) {
    ref var children = ref Ꮡchildren.DerefOrNull();

    if (Ꮡchildren == nil) {
        return SubtreeFactsNone;
    }
    var facts = SubtreeFactsNone;
    foreach (var (_, child) in range(children.Nodes)) {
        facts |= (SubtreeFacts)(propagate(child));
    }
    return facts;
}

internal static SubtreeFacts propagateModifierListSubtreeFacts(ж<ModifierList> children) {
    if (children == nil) {
        return SubtreeFactsNone;
    }
    return propagateNodeListSubtreeFacts(children.ᏑNodeList, propagateSubtreeFacts);
}

} // end ast_package
