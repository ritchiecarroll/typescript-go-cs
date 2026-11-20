namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using static ast_package;

partial class ast_package {

internal static ж<NodeVisitor> getDeepCloneVisitor(ж<NodeFactory> f, bool syntheticLocation) {
    ref var visitor = ref heap<ж<NodeVisitor>>(out var Ꮡvisitor);
    visitor = NewNodeVisitor(
        (ж<Node> node) => {
            var visited = Ꮡvisitor.ValueSlot.VisitEachChild(node);
            if (visited != node) {
                if (syntheticLocation) {
                    visited.Loc = core.NewTextRange(-1, -1);
                }
                return visited;
            }
            var c = node.Clone(new NodeFactoryжNodeFactoryCoercible(f));
            if (syntheticLocation) {
                c.Loc = core.NewTextRange(-1, -1);
            }
            return c;
        },
        f,
        new NodeVisitorHooks(
            VisitNodes: (ж<NodeList> nodes, ж<NodeVisitor> v) => {
                if (nodes == nil) {
                    return default;
                }
                var visited = v.VisitNodes(nodes);
                ж<NodeList> newList = default;
                if (visited != nodes) {
                    newList = visited;
                } else {
                    newList = nodes.Clone(new NodeFactoryжNodeFactoryCoercible(v.Factory));
                }
                if (syntheticLocation) {
                    newList.Loc = core.NewTextRange(-1, -1);
                    if (nodes.HasTrailingComma()) {
                        newList.Nodes[len(newList.Nodes) - 1].Loc = core.NewTextRange(-2, -2);
                    }
                }
                return newList;
            },
            VisitModifiers: (ж<ModifierList> nodes, ж<NodeVisitor> v) => {
                if (nodes == nil) {
                    return default;
                }
                var visited = v.VisitModifiers(nodes);
                ж<ModifierList> newList = default;
                if (visited != nodes) {
                    newList = visited;
                } else {
                    newList = nodes.Clone(v.Factory);
                }
                if (syntheticLocation) {
                    newList.Loc = core.NewTextRange(-1, -1);
                    if (nodes.of(ModifierList.ᏑNodeList).HasTrailingComma()) {
                        newList.Nodes[len(newList.Nodes) - 1].Loc = core.NewTextRange(-2, -2);
                    }
                }
                return newList;
            }
        ));
    return visitor;
}

} partial class astᴛNodeFactory {

public static ж<Node> DeepCloneNode(this ж<NodeFactory> Ꮡf, ж<Node> node) {
    return getDeepCloneVisitor(Ꮡf, true).VisitNode(node);
}

public static ж<Node> DeepCloneReparse(this ж<NodeFactory> Ꮡf, ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode != nil) {
        Ꮡnode = getDeepCloneVisitor(Ꮡf, false).VisitNode(Ꮡnode); node = ref Ꮡnode.DerefOrNull();
        SetParentInChildren(Ꮡnode);
        node.Flags |= (NodeFlags)(NodeFlagsReparsed);
    }
    return Ꮡnode;
}

public static ж<ModifierList> DeepCloneReparseModifiers(this ж<NodeFactory> Ꮡf, ж<ModifierList> modifiers) {
    return getDeepCloneVisitor(Ꮡf, false).VisitModifiers(modifiers);
}

} // end astᴛNodeFactory
