namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using slices = slices_package;
using static ast_package;

partial class ast_package {

partial struct NodeVisitor {
    public Func<ж<Node>, ж<Node>> Visit;
    public ж<NodeFactory> Factory;
    public NodeVisitorHooks Hooks;
}

partial struct NodeVisitorHooks {
    public Func<ж<Node>, ж<NodeVisitor>, ж<Node>> VisitNode;
    public Func<ж<TokenNode>, ж<NodeVisitor>, ж<Node>> VisitToken;
    public Func<ж<NodeList>, ж<NodeVisitor>, ж<NodeList>> VisitNodes;
    public Func<ж<ModifierList>, ж<NodeVisitor>, ж<ModifierList>> VisitModifiers;
    public Func<ж<Statement>, ж<NodeVisitor>, ж<Statement>> VisitEmbeddedStatement;
    public Func<ж<Statement>, ж<NodeVisitor>, ж<Statement>> VisitIterationBody;
    public Func<ж<ParameterList>, ж<NodeVisitor>, ж<ParameterList>> VisitParameters;
    public Func<ж<BlockOrExpression>, ж<NodeVisitor>, ж<BlockOrExpression>> VisitFunctionBody;
    public Func<ж<StatementList>, ж<NodeVisitor>, ж<StatementList>> VisitTopLevelStatements;
}

public static ж<NodeVisitor> NewNodeVisitor(Func<ж<Node>, ж<Node>> visit, ж<NodeFactory> Ꮡfactory, NodeVisitorHooks hooks) {
    ref var factory = ref Ꮡfactory.DerefOrNull();

    if (Ꮡfactory == nil) {
        Ꮡfactory = Ꮡ(new NodeFactory()); factory = ref Ꮡfactory.DerefOrNull();
    }
    return Ꮡ(new NodeVisitor(Visit: visit, Factory: Ꮡfactory, Hooks: hooks));
}

} partial class astᴛNodeVisitor {

public static ж<SourceFile> VisitSourceFile(this ref NodeVisitor v, ж<SourceFile> node) {
    return v.VisitNode(node.of(SourceFile.ᏑNodeBase).of(NodeBase.ᏑNodeDefault).AsNode()).AsSourceFile();
}

public static ж<Node> VisitNode(this ref NodeVisitor v, ж<Node> node) {
    if (node == nil || v.Visit == nil) {
        return node;
    }

    if (v.Visit != nil) {
        var visited = v.Visit(node);
        if (visited != nil && visited.Kind == KindSyntaxList) {
            var nodes = visited.AsSyntaxList().Value.Children;
            if (len(nodes) != 1) {
                throw panic("Expected only a single node to be written to output");
            }
            visited = nodes[0];
            if (visited != nil && visited.Kind == KindSyntaxList) {
                throw panic("The result of visiting and lifting a Node may not be SyntaxList");
            }
        }
        return visited;
    }

    return node;
}

public static ж<Statement> VisitEmbeddedStatement(this ref NodeVisitor v, ж<Statement> node) {
    if (node == nil || v.Visit == nil) {
        return node;
    }

    var visited = v.Visit(node);
    if (visited == nil) {
        return default;
    }
    return v.liftToBlock(visited);
}

public static ж<NodeList> VisitNodes(this ref NodeVisitor v, ж<NodeList> Ꮡnodes) {
    ref var nodes = ref Ꮡnodes.DerefOrNull();

    if (Ꮡnodes == nil || v.Visit == nil) {
        return Ꮡnodes;
    }

    var (result, changed) = v.VisitSlice(nodes.Nodes); if (changed) {
        var list = v.Factory.NewNodeList(result);
        list.Loc = nodes.Loc;
        return list;
    }

    return Ꮡnodes;
}

public static ж<ModifierList> VisitModifiers(this ref NodeVisitor v, ж<ModifierList> Ꮡnodes) {
    ref var nodes = ref Ꮡnodes.DerefOrNull();

    if (Ꮡnodes == nil || v.Visit == nil) {
        return Ꮡnodes;
    }

    var (result, changed) = v.VisitSlice(nodes.Nodes); if (changed) {
        var list = v.Factory.NewModifierList(result);
        list.Loc = nodes.Loc;
        return list;
    }

    return Ꮡnodes;
}

public static (slice<ж<Node>> result, bool changed) VisitSlice(this ref NodeVisitor v, slice<ж<Node>> nodes) {
    if (nodes == nil || v.Visit == nil) {
        return (nodes, false);
    }

    for (nint i = 0; i < len(nodes); i++) {
        var node = nodes[i];
        if (v.Visit == nil) {
            break;
        }

        var visited = v.Visit(node);
        if (visited == nil || visited != node) {
            var updated = slices.Clone<slice<ж<Node>>, ж<Node>>(nodes.slice(0, i));

            for (;;) {
                switch (ᐧ) {
                case {} when visited == nil: {
                    break;
                }
                case {} when visited.Kind == KindSyntaxList: {
                    updated = appendꓸꓸꓸ(updated, (~visited.AsSyntaxList()).Children);
                    break;
                }
                default: {
                    updated = append(updated, visited);
                    break;
                }}

                i++;

                if (i >= len(nodes)) {
                    break;
                }

                if (v.Visit != nil) {
                    node = nodes[i];
                    visited = v.Visit(node);
                } else {
                    updated = appendꓸꓸꓸ(updated, nodes.slice(i));
                    break;
                }
            }

            return (updated, true);
        }
    }

    return (nodes, false);
}

public static ж<Node> VisitEachChild(this ж<NodeVisitor> Ꮡv, ж<Node> Ꮡnode) {
    ref var v = ref Ꮡv.DerefOrNull();
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil || v.Visit == nil) {
        return Ꮡnode;
    }

    return node.VisitEachChild(Ꮡv);
}

internal static ж<Node> visitNode(this ж<NodeVisitor> Ꮡv, ж<Node> node) {
    ref var v = ref Ꮡv.DerefOrNull();

    if (v.Hooks.VisitNode != nil) {
        return v.Hooks.VisitNode(node, Ꮡv);
    }
    return v.VisitNode(node);
}

internal static ж<Node> visitEmbeddedStatement(this ж<NodeVisitor> Ꮡv, ж<Node> node) {
    ref var v = ref Ꮡv.DerefOrNull();

    if (v.Hooks.VisitEmbeddedStatement != nil) {
        return v.Hooks.VisitEmbeddedStatement(node, Ꮡv);
    }
    if (v.Hooks.VisitNode != nil) {
        return v.liftToBlock(v.Hooks.VisitNode(node, Ꮡv));
    }
    return v.VisitEmbeddedStatement(node);
}

internal static ж<Statement> visitIterationBody(this ж<NodeVisitor> Ꮡv, ж<Statement> node) {
    ref var v = ref Ꮡv.DerefOrNull();

    if (v.Hooks.VisitIterationBody != nil) {
        return v.Hooks.VisitIterationBody(node, Ꮡv);
    }
    return Ꮡv.visitEmbeddedStatement(node);
}

internal static ж<BlockOrExpression> visitFunctionBody(this ж<NodeVisitor> Ꮡv, ж<BlockOrExpression> node) {
    ref var v = ref Ꮡv.DerefOrNull();

    if (v.Hooks.VisitFunctionBody != nil) {
        return v.Hooks.VisitFunctionBody(node, Ꮡv);
    }
    return Ꮡv.visitNode(node);
}

internal static ж<Node> visitToken(this ж<NodeVisitor> Ꮡv, ж<Node> node) {
    ref var v = ref Ꮡv.DerefOrNull();

    if (v.Hooks.VisitToken != nil) {
        return v.Hooks.VisitToken(node, Ꮡv);
    }
    return v.VisitNode(node);
}

internal static ж<NodeList> visitNodes(this ж<NodeVisitor> Ꮡv, ж<NodeList> nodes) {
    ref var v = ref Ꮡv.DerefOrNull();

    if (v.Hooks.VisitNodes != nil) {
        return v.Hooks.VisitNodes(nodes, Ꮡv);
    }
    return v.VisitNodes(nodes);
}

internal static ж<ModifierList> visitModifiers(this ж<NodeVisitor> Ꮡv, ж<ModifierList> nodes) {
    ref var v = ref Ꮡv.DerefOrNull();

    if (v.Hooks.VisitModifiers != nil) {
        return v.Hooks.VisitModifiers(nodes, Ꮡv);
    }
    return v.VisitModifiers(nodes);
}

internal static ж<ParameterList> visitParameters(this ж<NodeVisitor> Ꮡv, ж<ParameterList> nodes) {
    ref var v = ref Ꮡv.DerefOrNull();

    if (v.Hooks.VisitParameters != nil) {
        return v.Hooks.VisitParameters(nodes, Ꮡv);
    }
    return Ꮡv.visitNodes(nodes);
}

internal static ж<StatementList> visitTopLevelStatements(this ж<NodeVisitor> Ꮡv, ж<StatementList> nodes) {
    ref var v = ref Ꮡv.DerefOrNull();

    if (v.Hooks.VisitTopLevelStatements != nil) {
        return v.Hooks.VisitTopLevelStatements(nodes, Ꮡv);
    }
    return Ꮡv.visitNodes(nodes);
}

internal static ж<Statement> liftToBlock(this ref NodeVisitor v, ж<Statement> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    slice<ж<Node>> nodes = default;
    if (Ꮡnode != nil) {
        if (node.Kind == KindSyntaxList) {
            nodes = node.AsSyntaxList().Value.Children;
        } else {
            nodes = [Ꮡnode];
        }
    }
    if (len(nodes) == 1) {
        Ꮡnode = nodes[0]; node = ref Ꮡnode.DerefOrNull();
    } else {
        Ꮡnode = v.Factory.NewBlock(v.Factory.NewNodeList(nodes), true); node = ref Ꮡnode.DerefOrNull();
    }
    if (node.Kind == KindSyntaxList) {
        throw panic("The result of visiting and lifting a Node may not be SyntaxList");
    }
    return Ꮡnode;
}

} // end astᴛNodeVisitor
