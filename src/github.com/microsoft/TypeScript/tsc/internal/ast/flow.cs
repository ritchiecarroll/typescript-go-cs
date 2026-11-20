global using FlowLabel = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.FlowNode;

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using static ast_package;

partial class ast_package {

partial struct FlowFlags /*num:uint32*/;

public static FlowFlags FlowFlagsUnreachable => /* 1 << 0 */ 1;
public static FlowFlags FlowFlagsStart => /* 1 << 1 */ 2;
public static FlowFlags FlowFlagsBranchLabel => /* 1 << 2 */ 4;
public static FlowFlags FlowFlagsLoopLabel => /* 1 << 3 */ 8;
public static FlowFlags FlowFlagsAssignment => /* 1 << 4 */ 16;
public static FlowFlags FlowFlagsTrueCondition => /* 1 << 5 */ 32;
public static FlowFlags FlowFlagsFalseCondition => /* 1 << 6 */ 64;
public static FlowFlags FlowFlagsSwitchClause => /* 1 << 7 */ 128;
public static FlowFlags FlowFlagsArrayMutation => /* 1 << 8 */ 256;
public static FlowFlags FlowFlagsCall => /* 1 << 9 */ 512;
public static FlowFlags FlowFlagsReduceLabel => /* 1 << 10 */ 1024;
public static FlowFlags FlowFlagsReferenced => /* 1 << 11 */ 2048;
public static FlowFlags FlowFlagsShared => /* 1 << 12 */ 4096;
public static FlowFlags FlowFlagsLabel => /* FlowFlagsBranchLabel | FlowFlagsLoopLabel */ 12;
public static FlowFlags FlowFlagsCondition => /* FlowFlagsTrueCondition | FlowFlagsFalseCondition */ 96;

partial struct FlowNode {
    public FlowFlags Flags;
    public ж<Node> Node;
    public ж<FlowNode> Antecedent;
    public ж<FlowList> Antecedents;
}

partial struct FlowList {
    public ж<FlowNode> Flow;
    public ж<FlowList> Next;
}

partial struct FlowSwitchClauseData {
    /*embed*/ public NodeBase NodeBase;
    public ж<Node> SwitchStatement;
    public int32 ClauseStart;
    public int32 ClauseEnd;
}

public static ж<Node> NewFlowSwitchClauseData(ж<Node> switchStatement, nint clauseStart, nint clauseEnd) {
    var node = Ꮡ(new FlowSwitchClauseData());
    node.SwitchStatement = switchStatement;
    node.ClauseStart = (int32)clauseStart;
    node.ClauseEnd = (int32)clauseEnd;
    return newNode(KindUnknown, new FlowSwitchClauseDataжnodeData(node), new NodeFactoryHooks());
}

} partial class astᴛFlowSwitchClauseData {

public static bool IsEmpty(this ref FlowSwitchClauseData node) {
    return node.ClauseStart == node.ClauseEnd;
}

} partial class ast_package {

partial struct FlowReduceLabelData {
    /*embed*/ public NodeBase NodeBase;
    public ж<FlowLabel> Target;
    public ж<FlowList> Antecedents;
}

public static ж<Node> NewFlowReduceLabelData(ж<FlowLabel> target, ж<FlowList> antecedents) {
    var node = Ꮡ(new FlowReduceLabelData());
    node.Target = target;
    node.Antecedents = antecedents;
    return newNode(KindUnknown, new FlowReduceLabelDataжnodeData(node), new NodeFactoryHooks());
}

} // end ast_package
