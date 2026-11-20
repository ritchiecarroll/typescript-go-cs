namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using utf8 = unicode.utf8_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using stringutil = go.github.com.microsoft.TypeScript.tsc.@internal.stringutil_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using unicode;
using static printer_package;

partial class printer_package {

partial struct ChangeTrackerWriter {
    /*embed*/ internal textWriter textWriter;
    internal nint lastNonTriviaPosition;
    internal map<triviaPositionKey, nint> pos;
    internal map<triviaPositionKey, nint> end;
}

partial interface triviaPositionKey {
    nint Pos();
    nint End();
}

public static ж<ChangeTrackerWriter> NewChangeTrackerWriter(@string newline, nint indentSize) {
    if (indentSize < 0) {
        indentSize = defaultIndentSize;
    }
    var ctw = Ꮡ(new ChangeTrackerWriter(
        textWriter: new textWriter(newLine: newline, indentSize: indentSize),
        lastNonTriviaPosition: 0,
        pos: new map<triviaPositionKey, nint>{},
        end: new map<triviaPositionKey, nint>{}
    ));
    ctw.ᏑtextWriter.Clear();
    return ctw;
}

} partial class printerᴛChangeTrackerWriter {

public static PrintHandlers GetPrintHandlers(this ж<ChangeTrackerWriter> Ꮡct) {
    return new PrintHandlers(
        OnBeforeEmitNode: (ж<ast.Node> nodeOpt) => {
            if (nodeOpt != nil) {
                Ꮡct.Value.setPos(new ast_NodeжtriviaPositionKey(nodeOpt));
            }
        },
        OnAfterEmitNode: (ж<ast.Node> nodeOpt) => {
            if (nodeOpt != nil) {
                Ꮡct.Value.setEnd(new ast_NodeжtriviaPositionKey(nodeOpt));
            }
        },
        OnBeforeEmitNodeList: (ж<ast.NodeList> nodesOpt) => {
            if (nodesOpt != nil) {
                Ꮡct.Value.setPos(new ast_NodeListжtriviaPositionKey(nodesOpt));
            }
        },
        OnAfterEmitNodeList: (ж<ast.NodeList> nodesOpt) => {
            if (nodesOpt != nil) {
                Ꮡct.Value.setEnd(new ast_NodeListжtriviaPositionKey(nodesOpt));
            }
        },
        OnBeforeEmitToken: (ж<astꓸTokenNode> nodeOpt) => {
            if (nodeOpt != nil) {
                Ꮡct.Value.setPos(new ast_NodeжtriviaPositionKey(nodeOpt));
            }
        },
        OnAfterEmitToken: (ж<astꓸTokenNode> nodeOpt) => {
            if (nodeOpt != nil) {
                Ꮡct.Value.setEnd(new ast_NodeжtriviaPositionKey(nodeOpt));
            }
        }
    );
}

internal static void setPos(this ref ChangeTrackerWriter ct, triviaPositionKey node) {
    ct.pos[node] = ct.lastNonTriviaPosition;
}

internal static void setEnd(this ref ChangeTrackerWriter ct, triviaPositionKey node) {
    ct.end[node] = ct.lastNonTriviaPosition;
}

internal static nint getPos(this ref ChangeTrackerWriter ct, triviaPositionKey node) {
    return ct.pos[node];
}

internal static nint getEnd(this ref ChangeTrackerWriter ct, triviaPositionKey node) {
    return ct.end[node];
}

internal static void setLastNonTriviaPosition(this ref ChangeTrackerWriter ct, @string s, bool force) {
    if (force || scanner.SkipTrivia(s, 0) != len(s)) {
        ct.lastNonTriviaPosition = ct.textWriter.GetTextPos();
        nint pos = len(s);
        while (pos > 0) {
            var (r, size) = utf8.DecodeLastRuneInString(s.slice(0, pos));
            if (stringutil.IsWhiteSpaceLike(r)) {
                pos -= size;
            } else {
                break;
            }
        }
        ct.lastNonTriviaPosition -= len(s) - pos;
    }
}

public static ж<ast.Node> AssignPositionsToNode(this ж<ChangeTrackerWriter> Ꮡct, ж<ast.Node> node, ж<ast.NodeFactory> factory) {
    ref var ct = ref Ꮡct.DerefOrNull();

    ref var visitor = ref heap<ж<ast.NodeVisitor>>(out var Ꮡvisitor);
    visitor = Ꮡ(new ast.NodeVisitor(
        Visit: (ж<ast.Node> n) => Ꮡct.Value.assignPositionsToNodeWorker(n, Ꮡvisitor.ValueSlot),
        Factory: factory,
        Hooks: new ast.NodeVisitorHooks(
            VisitNode: Ꮡct.assignPositionsToNodeWorker,
            VisitNodes: Ꮡct.assignPositionsToNodeArray,
            VisitToken: Ꮡct.assignPositionsToNodeWorker,
            VisitModifiers: (ж<ast.ModifierList> modifiers, ж<ast.NodeVisitor> v) => {
                if (modifiers != nil) {
                    var newNodeList = Ꮡct.Value.assignPositionsToNodeArray(modifiers.ᏑNodeList, v);
                    return factory.Value.NewModifierList(newNodeList.Nodes);
                }
                return modifiers;
            }
        )
    ));
    return ct.assignPositionsToNodeWorker(node, visitor);
}

internal static ж<ast.Node> assignPositionsToNodeWorker(this ref ChangeTrackerWriter ct, ж<ast.Node> Ꮡnode, ж<ast.NodeVisitor> Ꮡv) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var v = ref Ꮡv.DerefOrNull();

    if (Ꮡnode == nil) {
        return Ꮡnode;
    }
    var visited = node.VisitEachChild(Ꮡv);
    var newNode = visited;
    if (visited == Ꮡnode) {
        newNode = visited.Clone(new ast.NodeFactoryжNodeFactoryCoercible(v.Factory));
    }
    var newNodeʗ1 = newNode;
    newNode.ForEachChild((ж<ast.Node> child) => {
        child.Parent = newNodeʗ1;
        return true;
    });
    newNode.Loc = core.NewTextRange(ct.getPos(new ast_NodeжtriviaPositionKey(Ꮡnode)), ct.getEnd(new ast_NodeжtriviaPositionKey(Ꮡnode)));
    return newNode;
}

internal static ж<ast.NodeList> assignPositionsToNodeArray(this ref ChangeTrackerWriter ct, ж<ast.NodeList> nodes, ж<ast.NodeVisitor> Ꮡv) {
    ref var v = ref Ꮡv.DerefOrNull();

    var visited = v.VisitNodes(nodes);
    if (visited == nil) {
        return visited;
    }
    if (nodes == nil) {
        throw panic("if nodes is nil, visited should not be nil");
    }
    var nodeArray = visited;
    if (visited == nodes) {
        nodeArray = visited.Clone(new ast.NodeFactoryжNodeFactoryCoercible(v.Factory));
    }

    nodeArray.Loc = core.NewTextRange(ct.getPos(new ast_NodeListжtriviaPositionKey(nodes)), ct.getEnd(new ast_NodeListжtriviaPositionKey(nodes)));
    return nodeArray;
}

public static void Write(this ж<ChangeTrackerWriter> Ꮡct, @string text) {
    ref var ct = ref Ꮡct.DerefOrNull();

    Ꮡct.ᏑtextWriter.Write(text);
    ct.setLastNonTriviaPosition(text, false);
}

public static void WriteTrailingSemicolon(this ж<ChangeTrackerWriter> Ꮡct, @string text) {
    ref var ct = ref Ꮡct.DerefOrNull();

    Ꮡct.ᏑtextWriter.WriteTrailingSemicolon(text);
    ct.setLastNonTriviaPosition(text, false);
}
public static void WriteComment(this ж<ChangeTrackerWriter> Ꮡct, @string text) { Ꮡct.ᏑtextWriter.WriteComment(text); }
public static void WriteKeyword(this ж<ChangeTrackerWriter> Ꮡct, @string text) {
    ref var ct = ref Ꮡct.DerefOrNull();

    Ꮡct.ᏑtextWriter.WriteKeyword(text);
    ct.setLastNonTriviaPosition(text, false);
}

public static void WriteOperator(this ж<ChangeTrackerWriter> Ꮡct, @string text) {
    ref var ct = ref Ꮡct.DerefOrNull();

    Ꮡct.ᏑtextWriter.WriteOperator(text);
    ct.setLastNonTriviaPosition(text, false);
}

public static void WritePunctuation(this ж<ChangeTrackerWriter> Ꮡct, @string text) {
    ref var ct = ref Ꮡct.DerefOrNull();

    Ꮡct.ᏑtextWriter.WritePunctuation(text);
    ct.setLastNonTriviaPosition(text, false);
}

public static void WriteSpace(this ж<ChangeTrackerWriter> Ꮡct, @string text) {
    ref var ct = ref Ꮡct.DerefOrNull();

    Ꮡct.ᏑtextWriter.WriteSpace(text);
    ct.setLastNonTriviaPosition(text, false);
}

public static void WriteStringLiteral(this ж<ChangeTrackerWriter> Ꮡct, @string text) {
    ref var ct = ref Ꮡct.DerefOrNull();

    Ꮡct.ᏑtextWriter.WriteStringLiteral(text);
    ct.setLastNonTriviaPosition(text, false);
}

public static void WriteParameter(this ж<ChangeTrackerWriter> Ꮡct, @string text) {
    ref var ct = ref Ꮡct.DerefOrNull();

    Ꮡct.ᏑtextWriter.WriteParameter(text);
    ct.setLastNonTriviaPosition(text, false);
}

public static void WriteProperty(this ж<ChangeTrackerWriter> Ꮡct, @string text) {
    ref var ct = ref Ꮡct.DerefOrNull();

    Ꮡct.ᏑtextWriter.WriteProperty(text);
    ct.setLastNonTriviaPosition(text, false);
}

public static void WriteSymbol(this ж<ChangeTrackerWriter> Ꮡct, @string text, ж<ast.Symbol> symbol) {
    ref var ct = ref Ꮡct.DerefOrNull();

    Ꮡct.ᏑtextWriter.WriteSymbol(text, symbol);
    ct.setLastNonTriviaPosition(text, false);
}
public static void WriteLine(this ж<ChangeTrackerWriter> Ꮡct) { Ꮡct.ᏑtextWriter.WriteLine(); }
public static void WriteLineForce(this ж<ChangeTrackerWriter> Ꮡct, bool force) { Ꮡct.ᏑtextWriter.WriteLineForce(force); }
public static void IncreaseIndent(this ref ChangeTrackerWriter ct) { ct.textWriter.IncreaseIndent(); }
public static void DecreaseIndent(this ref ChangeTrackerWriter ct) { ct.textWriter.DecreaseIndent(); }

public static void Clear(this ref ChangeTrackerWriter ct) {
    ct.textWriter.Clear();
    ct.lastNonTriviaPosition = 0;
}

public static @string String(this ref ChangeTrackerWriter ct) { return ct.textWriter.String(); }
public static void RawWrite(this ж<ChangeTrackerWriter> Ꮡct, @string s) {
    ref var ct = ref Ꮡct.DerefOrNull();

    Ꮡct.ᏑtextWriter.RawWrite(s);
    ct.setLastNonTriviaPosition(s, false);
}

public static void WriteLiteral(this ж<ChangeTrackerWriter> Ꮡct, @string s) {
    ref var ct = ref Ꮡct.DerefOrNull();

    Ꮡct.ᏑtextWriter.WriteLiteral(s);
    ct.setLastNonTriviaPosition(s, true);
}
public static nint GetTextPos(this ref ChangeTrackerWriter ct) { return ct.textWriter.GetTextPos(); }
public static nint GetLine(this ref ChangeTrackerWriter ct) { return ct.textWriter.GetLine(); }
public static core.UTF16Offset GetColumn(this ref ChangeTrackerWriter ct) { return ct.textWriter.GetColumn(); }
public static nint GetIndent(this ref ChangeTrackerWriter ct) { return ct.textWriter.GetIndent(); }
public static bool IsAtStartOfLine(this ref ChangeTrackerWriter ct) { return ct.textWriter.IsAtStartOfLine(); }

public static bool HasTrailingComment(this ref ChangeTrackerWriter ct) { return ct.textWriter.HasTrailingComment(); }

public static bool HasTrailingWhitespace(this ref ChangeTrackerWriter ct) {
    return ct.textWriter.HasTrailingWhitespace();
}

} // end printerᴛChangeTrackerWriter
