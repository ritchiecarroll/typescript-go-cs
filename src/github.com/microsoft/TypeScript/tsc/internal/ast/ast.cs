global using NamedMember = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using AnyValidImportOrReExport = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using ValidImportTypeNode = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using TypeOnlyImportDeclaration = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using StringLiteralLike = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using ObjectLiteralLike = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using AnyImportOrRequireStatement = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package.Node;
global using PragmaKindFlags = byte;

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using fmt = fmt_package;
using iter = iter_package;
using strings = strings_package;
using sync = sync_package;
using atomic = go.sync.atomic_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using diagnostics = go.github.com.microsoft.TypeScript.tsc.@internal.diagnostics_package;
using spanmap = go.github.com.microsoft.TypeScript.tsc.@internal.spanmap_package;
using tspath = go.github.com.microsoft.TypeScript.tsc.@internal.tspath_package;
using xxh3 = go.github.com.zeebo.xxh3_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.github.com.zeebo;
using go.sync;
using ast = ast_package;
using static ast_package;

partial class ast_package {

internal static Func<ж<SourceFile>, ж<Node>, slice<ж<Node>>> parseJSDocForNode;

public static void SetParseJSDocForNode(Func<ж<SourceFile>, ж<Node>, slice<ж<Node>>> fn) {
    parseJSDocForNode = fn;
}

// type Visitor is a methodless func type — rendered inline as its base delegate

internal static bool visit(Func<ж<Node>, bool> v, ж<Node> node) {
    if (node != nil) {
        return v(node);
    }
    return false;
}

internal static bool visitNodes(Func<ж<Node>, bool> v, slice<ж<Node>> nodes) {
    foreach (var (_, node) in range(nodes)) {
        if (v(node)) {
            return true;
        }
    }
    return false;
}

internal static bool visitNodeList(Func<ж<Node>, bool> v, ж<NodeList> ᏑnodeList) {
    ref var nodeList = ref ᏑnodeList.DerefOrNull();

    if (ᏑnodeList != nil) {
        return visitNodes(v, nodeList.Nodes);
    }
    return false;
}

internal static bool visitModifiers(Func<ж<Node>, bool> v, ж<ModifierList> Ꮡmodifiers) {
    ref var modifiers = ref Ꮡmodifiers.DerefOrNull();

    if (Ꮡmodifiers != nil) {
        return visitNodes(v, modifiers.Nodes);
    }
    return false;
}

partial struct NodeFactoryHooks {
    public Action<ж<Node>> OnCreate;
    public Action<ж<Node>, ж<Node>> OnUpdate;
    public Action<ж<Node>, ж<Node>> OnClone;
}

partial interface NodeFactoryCoercible {
    ж<NodeFactory> AsNodeFactory();
}

public static ж<NodeFactory> NewNodeFactory(NodeFactoryHooks hooks) {
    return Ꮡ(new NodeFactory(hooks: hooks));
}

internal static ж<Node> newNode(Kind kind, nodeData data, NodeFactoryHooks hooks) {
    var n = data.AsNode();
    n.Loc = core.UndefinedTextRange();
    n.Kind = kind;
    n.data = data;
    if (hooks.OnCreate != nil) {
        hooks.OnCreate(n);
    }
    return n;
}

} partial class astᴛNodeFactory {

internal static ж<Node> newNode(this ref NodeFactory f, Kind kind, nodeData data) {
    f.nodeCount++;
    return ast.newNode(kind, data, f.hooks);
}

public static nint NodeCount(this ref NodeFactory f) {
    return f.nodeCount;
}

public static nint TextCount(this ref NodeFactory f) {
    return f.textCount;
}

public static ж<NodeFactory> AsNodeFactory(this ж<NodeFactory> Ꮡf) {
    return Ꮡf;
}

} partial class ast_package {

internal static ж<Node> updateNode(ж<Node> Ꮡupdated, ж<Node> Ꮡoriginal, NodeFactoryHooks hooks) {
    ref var updated = ref Ꮡupdated.DerefOrNull();
    ref var original = ref Ꮡoriginal.DerefOrNull();

    if (Ꮡupdated != Ꮡoriginal) {
        updated.Flags = original.Flags;
        updated.Loc = original.Loc;
        if (hooks.OnUpdate != nil) {
            hooks.OnUpdate(Ꮡupdated, Ꮡoriginal);
        }
    }
    return Ꮡupdated;
}

internal static ж<Node> cloneNode(ж<Node> updated, ж<Node> original, NodeFactoryHooks hooks) {
    updateNode(updated, original, hooks);
    if (updated != original && hooks.OnClone != nil) {
        hooks.OnClone(updated, original);
    }
    return updated;
}

partial struct NodeList {
    public core.TextRange Loc;
    public slice<ж<Node>> Nodes;
}

} partial class astᴛNodeFactory {

public static ж<NodeList> NewNodeList(this ref NodeFactory f, slice<ж<Node>> nodes) {
    var list = f.nodeListArena.New();
    list.Loc = core.UndefinedTextRange();
    list.Nodes = nodes;
    return list;
}

} partial class astᴛNodeList {

public static nint Pos(this ref NodeList list) { return list.Loc.Pos(); }
public static nint End(this ref NodeList list) { return list.Loc.End(); }

public static bool HasTrailingComma(this ref NodeList list) {
    if (len(list.Nodes) == 0) {
        return false;
    }
    var last = list.Nodes[len(list.Nodes) - 1];
    return last.End() < list.End();
}

public static ж<NodeList> Clone(this ref NodeList list, NodeFactoryCoercible f) {
    var result = f.AsNodeFactory().NewNodeList(list.Nodes);
    result.Loc = list.Loc;
    return result;
}

} partial class ast_package {

partial struct ModifierList {
    /*embed*/ public NodeList NodeList;
    public ModifierFlags ModifierFlags;
}

} partial class astᴛNodeFactory {

public static ж<ModifierList> NewModifierList(this ref NodeFactory f, slice<ж<Node>> nodes) {
    var list = f.modifierListArena.New();
    list.Loc = core.UndefinedTextRange();
    list.Nodes = nodes;
    list.ModifierFlags = ModifiersToFlags(nodes);
    return list;
}

} partial class astᴛModifierList {

public static ж<ModifierList> Clone(this ref ModifierList list, ж<NodeFactory> Ꮡf) {
    ref var f = ref Ꮡf.DerefOrNull();

    var res = f.modifierListArena.New();
    res.Loc = list.Loc;
    res.Nodes = list.Nodes;
    res.ModifierFlags = list.ModifierFlags;
    return res;
}

} partial class ast_package {

partial struct Node {
    public Kind Kind;
    public NodeFlags Flags;
    public core.TextRange Loc;
    internal atomic.Uint64 id;
    public ж<Node> Parent;
    internal nodeData data;
}

} partial class astᴛNode {

public static ж<Node> AsNode(this ж<Node> Ꮡn) { return Ꮡn; }
public static nint Pos(this ref Node n) { return n.Loc.Pos(); }
public static nint End(this ref Node n) { return n.Loc.End(); }
public static iter.Seq<ж<Node>> IterChildren(this ж<Node> Ꮡn) {
    return (Func<ж<Node>, bool> yield) => {
        Ꮡn.Value.ForEachChild((ж<Node> child) => !yield(child));
    };
}
public static ж<Node> Clone(this ref Node n, NodeFactoryCoercible f) { return n.data.Clone(f); }
public static ж<Node> VisitEachChild(this ref Node n, ж<NodeVisitor> v) { return n.data.VisitEachChild(v); }
public static ж<DeclarationName> Name(this ref Node n) { return n.data.Name(); }
public static ж<ModifierList> Modifiers(this ref Node n) { return n.data.Modifiers(); }
public static ж<FlowNodeBase> FlowNodeData(this ref Node n) { return n.data.FlowNodeData(); }
public static ж<DeclarationBase> DeclarationData(this ref Node n) { return n.data.DeclarationData(); }
public static ж<ExportableBase> ExportableData(this ref Node n) { return n.data.ExportableData(); }
public static ж<LocalsContainerBase> LocalsContainerData(this ref Node n) { return n.data.LocalsContainerData(); }
public static ж<FunctionLikeBase> FunctionLikeData(this ref Node n) { return n.data.FunctionLikeData(); }
public static ж<ParameterList> ParameterList(this ref Node n) { return n.data.FunctionLikeData().Parameters; }
public static slice<ж<ParameterDeclarationNode>> Parameters(this ref Node n) { return n.ParameterList().Nodes; }
public static ж<ClassLikeBase> ClassLikeData(this ref Node n) { return n.data.ClassLikeData(); }
public static ж<BodyBase> BodyData(this ref Node n) { return n.data.BodyData(); }
public static SubtreeFacts SubtreeFacts(this ref Node n) { return n.data.SubtreeFacts(); }
internal static SubtreeFacts propagateSubtreeFacts(this ref Node n) { return n.data.propagateSubtreeFacts(); }
public static ж<LiteralLikeNodeBase> LiteralLikeData(this ref Node n) { return n.data.LiteralLikeData(); }
public static ж<TemplateLiteralLikeNodeBase> TemplateLiteralLikeData(this ref Node n) {
    return n.data.TemplateLiteralLikeData();
}
public static @string KindString(this ref Node n) { return n.Kind.String(); }
public static int16 KindValue(this ref Node n) { return (int16)n.Kind; }
public static slice<ж<Node>> Decorators(this ref Node n) {
    if (n.Modifiers() == nil) {
        return default;
    }
    return core.Filter<ж<Node>>(n.Modifiers().Nodes, IsDecorator);
}

} partial class ast_package {

partial struct MutableNode /*Node*/;

} partial class astᴛNode {

public static ж<MutableNode> AsMutable(this ж<Node> Ꮡn) { return Ꮡn.Reinterpret<Node, MutableNode>(); }

} partial class astᴛMutableNode {

public static void SetModifiers(this ref MutableNode n, ж<ModifierList> modifiers) { n.data.setModifiers(modifiers); }

} partial class astᴛNode {

public static ж<Symbol> Symbol(this ref Node n) {
    var data = n.DeclarationData();
    if (data != nil) {
        return data.Symbol;
    }
    return default;
}

public static ж<Symbol> LocalSymbol(this ref Node n) {
    var data = n.ExportableData();
    if (data != nil) {
        return data.LocalSymbol;
    }
    return default;
}

public static SymbolTable Locals(this ref Node n) {
    var data = n.LocalsContainerData();
    if (data != nil) {
        return data.Locals;
    }
    return default;
}

public static ж<Node> Body(this ref Node n) {
    var data = n.BodyData();
    if (data != nil) {
        return data.Body;
    }
    return default;
}

public static @string Text(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindIdentifier: {
        return (~n.AsIdentifier()).Text;
    }
    case var ᴛ when ᴛ == KindPrivateIdentifier: {
        return (~n.AsPrivateIdentifier()).Text;
    }
    case var ᴛ when ᴛ == KindStringLiteral: {
        return (~n.AsStringLiteral()).Text;
    }
    case var ᴛ when ᴛ == KindNumericLiteral: {
        return (~n.AsNumericLiteral()).Text;
    }
    case var ᴛ when ᴛ == KindBigIntLiteral: {
        return (~n.AsBigIntLiteral()).Text;
    }
    case var ᴛ when ᴛ == KindMetaProperty: {
        return n.AsMetaProperty().Name().Text();
    }
    case var ᴛ when ᴛ == KindNoSubstitutionTemplateLiteral: {
        return (~n.AsNoSubstitutionTemplateLiteral()).Text;
    }
    case var ᴛ when ᴛ == KindTemplateHead: {
        return (~n.AsTemplateHead()).Text;
    }
    case var ᴛ when ᴛ == KindTemplateMiddle: {
        return (~n.AsTemplateMiddle()).Text;
    }
    case var ᴛ when ᴛ == KindTemplateTail: {
        return (~n.AsTemplateTail()).Text;
    }
    case var ᴛ when ᴛ == KindJsxNamespacedName: {
        return n.AsJsxNamespacedName().Namespace.Text() + ":"u8 + n.AsJsxNamespacedName().name.Text();
    }
    case var ᴛ when ᴛ == KindRegularExpressionLiteral: {
        return (~n.AsRegularExpressionLiteral()).Text;
    }
    case var ᴛ when ᴛ == KindJSDocText: {
        return strings.Join(n.AsJSDocText().text, "");
    }
    case var ᴛ when ᴛ == KindJSDocLink: {
        return strings.Join(n.AsJSDocLink().text, "");
    }
    case var ᴛ when ᴛ == KindJSDocLinkCode: {
        return strings.Join(n.AsJSDocLinkCode().text, "");
    }
    case var ᴛ when ᴛ == KindJSDocLinkPlain: {
        return strings.Join(n.AsJSDocLinkPlain().text, "");
    }}

    throw panic(fmt.Sprintf("Unhandled case in Node.Text: %T"u8, n.data));
}

public static ж<Node> Expression(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindPropertyAccessExpression: {
        return (~n.AsPropertyAccessExpression()).Expression;
    }
    case var ᴛ when ᴛ == KindElementAccessExpression: {
        return (~n.AsElementAccessExpression()).Expression;
    }
    case var ᴛ when ᴛ == KindParenthesizedExpression: {
        return (~n.AsParenthesizedExpression()).Expression;
    }
    case var ᴛ when ᴛ == KindCallExpression: {
        return (~n.AsCallExpression()).Expression;
    }
    case var ᴛ when ᴛ == KindNewExpression: {
        return (~n.AsNewExpression()).Expression;
    }
    case var ᴛ when ᴛ == KindExpressionWithTypeArguments: {
        return (~n.AsExpressionWithTypeArguments()).Expression;
    }
    case var ᴛ when ᴛ == KindComputedPropertyName: {
        return (~n.AsComputedPropertyName()).Expression;
    }
    case var ᴛ when ᴛ == KindNonNullExpression: {
        return (~n.AsNonNullExpression()).Expression;
    }
    case var ᴛ when ᴛ == KindTypeAssertionExpression: {
        return (~n.AsTypeAssertion()).Expression;
    }
    case var ᴛ when ᴛ == KindAsExpression: {
        return (~n.AsAsExpression()).Expression;
    }
    case var ᴛ when ᴛ == KindSatisfiesExpression: {
        return (~n.AsSatisfiesExpression()).Expression;
    }
    case var ᴛ when ᴛ == KindTypeOfExpression: {
        return (~n.AsTypeOfExpression()).Expression;
    }
    case var ᴛ when ᴛ == KindSpreadAssignment: {
        return (~n.AsSpreadAssignment()).Expression;
    }
    case var ᴛ when ᴛ == KindSpreadElement: {
        return (~n.AsSpreadElement()).Expression;
    }
    case var ᴛ when ᴛ == KindTemplateSpan: {
        return (~n.AsTemplateSpan()).Expression;
    }
    case var ᴛ when ᴛ == KindDeleteExpression: {
        return (~n.AsDeleteExpression()).Expression;
    }
    case var ᴛ when ᴛ == KindVoidExpression: {
        return (~n.AsVoidExpression()).Expression;
    }
    case var ᴛ when ᴛ == KindAwaitExpression: {
        return (~n.AsAwaitExpression()).Expression;
    }
    case var ᴛ when ᴛ == KindYieldExpression: {
        return (~n.AsYieldExpression()).Expression;
    }
    case var ᴛ when ᴛ == KindPartiallyEmittedExpression: {
        return (~n.AsPartiallyEmittedExpression()).Expression;
    }
    case var ᴛ when ᴛ == KindIfStatement: {
        return (~n.AsIfStatement()).Expression;
    }
    case var ᴛ when ᴛ == KindDoStatement: {
        return (~n.AsDoStatement()).Expression;
    }
    case var ᴛ when ᴛ == KindWhileStatement: {
        return (~n.AsWhileStatement()).Expression;
    }
    case var ᴛ when ᴛ == KindWithStatement: {
        return (~n.AsWithStatement()).Expression;
    }
    case var ᴛ when ᴛ == KindForInStatement || ᴛ == KindForOfStatement: {
        return (~n.AsForInOrOfStatement()).Expression;
    }
    case var ᴛ when ᴛ == KindSwitchStatement: {
        return (~n.AsSwitchStatement()).Expression;
    }
    case var ᴛ when ᴛ == KindCaseClause: {
        return (~n.AsCaseOrDefaultClause()).Expression;
    }
    case var ᴛ when ᴛ == KindExpressionStatement: {
        return (~n.AsExpressionStatement()).Expression;
    }
    case var ᴛ when ᴛ == KindReturnStatement: {
        return (~n.AsReturnStatement()).Expression;
    }
    case var ᴛ when ᴛ == KindThrowStatement: {
        return (~n.AsThrowStatement()).Expression;
    }
    case var ᴛ when ᴛ == KindExternalModuleReference: {
        return (~n.AsExternalModuleReference()).Expression;
    }
    case var ᴛ when ᴛ == KindExportAssignment: {
        return (~n.AsExportAssignment()).Expression;
    }
    case var ᴛ when ᴛ == KindDecorator: {
        return (~n.AsDecorator()).Expression;
    }
    case var ᴛ when ᴛ == KindJsxExpression: {
        return (~n.AsJsxExpression()).Expression;
    }
    case var ᴛ when ᴛ == KindJsxSpreadAttribute: {
        return (~n.AsJsxSpreadAttribute()).Expression;
    }}

    throw panic("Unhandled case in Node.Expression: " + n.Kind.String());
}

public static @string RawText(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindTemplateHead: {
        return (~n.AsTemplateHead()).RawText;
    }
    case var ᴛ when ᴛ == KindTemplateMiddle: {
        return (~n.AsTemplateMiddle()).RawText;
    }
    case var ᴛ when ᴛ == KindTemplateTail: {
        return (~n.AsTemplateTail()).RawText;
    }}

    throw panic("Unhandled case in Node.RawText: " + n.Kind.String());
}

} partial class astᴛMutableNode {

public static void SetExpression(this ж<MutableNode> Ꮡm, ж<Node> expr) {
    var n = Ꮡm.Reinterpret<MutableNode, Node>();
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindPropertyAccessExpression: {
        n.AsPropertyAccessExpression().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindElementAccessExpression: {
        n.AsElementAccessExpression().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindParenthesizedExpression: {
        n.AsParenthesizedExpression().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindCallExpression: {
        n.AsCallExpression().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindNewExpression: {
        n.AsNewExpression().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindExpressionWithTypeArguments: {
        n.AsExpressionWithTypeArguments().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindComputedPropertyName: {
        n.AsComputedPropertyName().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindNonNullExpression: {
        n.AsNonNullExpression().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindTypeAssertionExpression: {
        n.AsTypeAssertion().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindAsExpression: {
        n.AsAsExpression().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindSatisfiesExpression: {
        n.AsSatisfiesExpression().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindTypeOfExpression: {
        n.AsTypeOfExpression().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindSpreadAssignment: {
        n.AsSpreadAssignment().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindSpreadElement: {
        n.AsSpreadElement().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindTemplateSpan: {
        n.AsTemplateSpan().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindDeleteExpression: {
        n.AsDeleteExpression().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindVoidExpression: {
        n.AsVoidExpression().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindAwaitExpression: {
        n.AsAwaitExpression().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindYieldExpression: {
        n.AsYieldExpression().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindPartiallyEmittedExpression: {
        n.AsPartiallyEmittedExpression().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindIfStatement: {
        n.AsIfStatement().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindDoStatement: {
        n.AsDoStatement().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindWhileStatement: {
        n.AsWhileStatement().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindWithStatement: {
        n.AsWithStatement().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindForInStatement || ᴛ == KindForOfStatement: {
        n.AsForInOrOfStatement().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindSwitchStatement: {
        n.AsSwitchStatement().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindCaseClause: {
        n.AsCaseOrDefaultClause().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindExpressionStatement: {
        n.AsExpressionStatement().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindReturnStatement: {
        n.AsReturnStatement().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindThrowStatement: {
        n.AsThrowStatement().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindExternalModuleReference: {
        n.AsExternalModuleReference().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindExportAssignment: {
        n.AsExportAssignment().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindDecorator: {
        n.AsDecorator().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindJsxExpression: {
        n.AsJsxExpression().Value.Expression = expr;
        break;
    }
    case var ᴛ when ᴛ == KindJsxSpreadAttribute: {
        n.AsJsxSpreadAttribute().Value.Expression = expr;
        break;
    }
    default: {
        throw panic("Unhandled case in mutableNode.SetExpression: " + n.Kind.String());
        break;
    }}

}

} partial class astᴛNode {

public static ж<NodeList> ArgumentList(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindCallExpression: {
        return (~n.AsCallExpression()).Arguments;
    }
    case var ᴛ when ᴛ == KindNewExpression: {
        return (~n.AsNewExpression()).Arguments;
    }}

    throw panic("Unhandled case in Node.Arguments: " + n.Kind.String());
}

public static slice<ж<Node>> Arguments(this ref Node n) {
    var list = n.ArgumentList();
    if (list != nil) {
        return list.Nodes;
    }
    return default;
}

public static ж<NodeList> TypeArgumentList(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindCallExpression: {
        return (~n.AsCallExpression()).TypeArguments;
    }
    case var ᴛ when ᴛ == KindNewExpression: {
        return (~n.AsNewExpression()).TypeArguments;
    }
    case var ᴛ when ᴛ == KindTaggedTemplateExpression: {
        return (~n.AsTaggedTemplateExpression()).TypeArguments;
    }
    case var ᴛ when ᴛ == KindTypeReference: {
        return (~n.AsTypeReferenceNode()).TypeArguments;
    }
    case var ᴛ when ᴛ == KindExpressionWithTypeArguments: {
        return (~n.AsExpressionWithTypeArguments()).TypeArguments;
    }
    case var ᴛ when ᴛ == KindImportType: {
        return (~n.AsImportTypeNode()).TypeArguments;
    }
    case var ᴛ when ᴛ == KindTypeQuery: {
        return (~n.AsTypeQueryNode()).TypeArguments;
    }
    case var ᴛ when ᴛ == KindJsxOpeningElement: {
        return (~n.AsJsxOpeningElement()).TypeArguments;
    }
    case var ᴛ when ᴛ == KindJsxSelfClosingElement: {
        return (~n.AsJsxSelfClosingElement()).TypeArguments;
    }}

    throw panic("Unhandled case in Node.TypeArguments");
}

public static slice<ж<Node>> TypeArguments(this ref Node n) {
    var list = n.TypeArgumentList();
    if (list != nil) {
        return list.Nodes;
    }
    return default;
}

public static ж<NodeList> TypeParameterList(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindClassDeclaration: {
        return (~n.AsClassDeclaration()).TypeParameters;
    }
    case var ᴛ when ᴛ == KindClassExpression: {
        return (~n.AsClassExpression()).TypeParameters;
    }
    case var ᴛ when ᴛ == KindInterfaceDeclaration: {
        return (~n.AsInterfaceDeclaration()).TypeParameters;
    }
    case var ᴛ when ᴛ == KindTypeAliasDeclaration || ᴛ == KindJSTypeAliasDeclaration: {
        return (~n.AsTypeAliasDeclaration()).TypeParameters;
    }
    case var ᴛ when ᴛ == KindJSDocTemplateTag: {
        return (~n.AsJSDocTemplateTag()).TypeParameters;
    }
    default: {
        var funcLike = n.FunctionLikeData();
        if (funcLike != nil) {
            return funcLike.TypeParameters;
        }
        break;
    }}

    throw panic("Unhandled case in Node.TypeParameterList");
}

public static slice<ж<Node>> TypeParameters(this ref Node n) {
    var list = n.TypeParameterList();
    if (list != nil) {
        return list.Nodes;
    }
    return default;
}

public static ж<NodeList> MemberList(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindClassDeclaration: {
        return (~n.AsClassDeclaration()).Members;
    }
    case var ᴛ when ᴛ == KindClassExpression: {
        return (~n.AsClassExpression()).Members;
    }
    case var ᴛ when ᴛ == KindInterfaceDeclaration: {
        return (~n.AsInterfaceDeclaration()).Members;
    }
    case var ᴛ when ᴛ == KindEnumDeclaration: {
        return (~n.AsEnumDeclaration()).Members;
    }
    case var ᴛ when ᴛ == KindTypeLiteral: {
        return (~n.AsTypeLiteralNode()).Members;
    }
    case var ᴛ when ᴛ == KindMappedType: {
        return (~n.AsMappedTypeNode()).Members;
    }}

    throw panic("Unhandled case in Node.MemberList: " + n.Kind.String());
}

public static slice<ж<Node>> Members(this ref Node n) {
    var list = n.MemberList();
    if (list != nil) {
        return list.Nodes;
    }
    return default;
}

public static ж<NodeList> StatementList(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindSourceFile: {
        return (~n.AsSourceFile()).Statements;
    }
    case var ᴛ when ᴛ == KindBlock: {
        return (~n.AsBlock()).Statements;
    }
    case var ᴛ when ᴛ == KindModuleBlock: {
        return (~n.AsModuleBlock()).Statements;
    }
    case var ᴛ when ᴛ == KindCaseClause || ᴛ == KindDefaultClause: {
        return (~n.AsCaseOrDefaultClause()).Statements;
    }}

    throw panic("Unhandled case in Node.StatementList: " + n.Kind.String());
}

public static slice<ж<Node>> Statements(this ref Node n) {
    var list = n.StatementList();
    if (list != nil) {
        return list.Nodes;
    }
    return default;
}

public static bool CanHaveStatements(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindSourceFile || ᴛ == KindBlock || ᴛ == KindModuleBlock || ᴛ == KindCaseClause || ᴛ == KindDefaultClause: {
        return true;
    }
    default: {
        return false;
    }}

}

public static ModifierFlags ModifierFlags(this ref Node n) {
    var modifiers = n.Modifiers();
    if (modifiers != nil) {
        return modifiers.ModifierFlags;
    }
    return ModifierFlagsNone;
}

public static slice<ж<Node>> ModifierNodes(this ref Node n) {
    var modifiers = n.Modifiers();
    if (modifiers != nil) {
        return modifiers.Nodes;
    }
    return default;
}

public static ж<Node> Type(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindVariableDeclaration: {
        return (~n.AsVariableDeclaration()).Type;
    }
    case var ᴛ when ᴛ == KindParameter: {
        return (~n.AsParameterDeclaration()).Type;
    }
    case var ᴛ when ᴛ == KindPropertySignature: {
        return (~n.AsPropertySignatureDeclaration()).Type;
    }
    case var ᴛ when ᴛ == KindPropertyDeclaration: {
        return (~n.AsPropertyDeclaration()).Type;
    }
    case var ᴛ when ᴛ == KindPropertyAssignment: {
        return (~n.AsPropertyAssignment()).Type;
    }
    case var ᴛ when ᴛ == KindShorthandPropertyAssignment: {
        return (~n.AsShorthandPropertyAssignment()).Type;
    }
    case var ᴛ when ᴛ == KindTypePredicate: {
        return (~n.AsTypePredicateNode()).Type;
    }
    case var ᴛ when ᴛ == KindParenthesizedType: {
        return (~n.AsParenthesizedTypeNode()).Type;
    }
    case var ᴛ when ᴛ == KindTypeOperator: {
        return (~n.AsTypeOperatorNode()).Type;
    }
    case var ᴛ when ᴛ == KindMappedType: {
        return (~n.AsMappedTypeNode()).Type;
    }
    case var ᴛ when ᴛ == KindTypeAssertionExpression: {
        return (~n.AsTypeAssertion()).Type;
    }
    case var ᴛ when ᴛ == KindAsExpression: {
        return (~n.AsAsExpression()).Type;
    }
    case var ᴛ when ᴛ == KindSatisfiesExpression: {
        return (~n.AsSatisfiesExpression()).Type;
    }
    case var ᴛ when ᴛ == KindTypeAliasDeclaration || ᴛ == KindJSTypeAliasDeclaration: {
        return (~n.AsTypeAliasDeclaration()).Type;
    }
    case var ᴛ when ᴛ == KindNamedTupleMember: {
        return (~n.AsNamedTupleMember()).Type;
    }
    case var ᴛ when ᴛ == KindOptionalType: {
        return (~n.AsOptionalTypeNode()).Type;
    }
    case var ᴛ when ᴛ == KindRestType: {
        return (~n.AsRestTypeNode()).Type;
    }
    case var ᴛ when ᴛ == KindTemplateLiteralTypeSpan: {
        return (~n.AsTemplateLiteralTypeSpan()).Type;
    }
    case var ᴛ when ᴛ == KindJSDocTypeExpression: {
        return (~n.AsJSDocTypeExpression()).Type;
    }
    case var ᴛ when ᴛ == KindJSDocParameterTag || ᴛ == KindJSDocPropertyTag: {
        return (~n.AsJSDocParameterOrPropertyTag()).TypeExpression;
    }
    case var ᴛ when ᴛ == KindJSDocNullableType: {
        return (~n.AsJSDocNullableType()).Type;
    }
    case var ᴛ when ᴛ == KindJSDocNonNullableType: {
        return (~n.AsJSDocNonNullableType()).Type;
    }
    case var ᴛ when ᴛ == KindJSDocOptionalType: {
        return (~n.AsJSDocOptionalType()).Type;
    }
    case var ᴛ when ᴛ == KindExportAssignment: {
        return (~n.AsExportAssignment()).Type;
    }
    case var ᴛ when ᴛ == KindBinaryExpression: {
        return (~n.AsBinaryExpression()).Type;
    }
    default: {
        var funcLike = n.FunctionLikeData(); if (funcLike != nil) {
            return funcLike.Type;
        }
        break;
    }}

    return default;
}

} partial class astᴛMutableNode {

public static void SetType(this ж<MutableNode> Ꮡm, ж<Node> t) {
    ref var m = ref Ꮡm.DerefOrNull();

    var n = Ꮡm.Reinterpret<MutableNode, Node>();
    switch (m.Kind) {
    case var ᴛ when ᴛ == KindVariableDeclaration: {
        n.AsVariableDeclaration().Value.Type = t;
        break;
    }
    case var ᴛ when ᴛ == KindParameter: {
        n.AsParameterDeclaration().Value.Type = t;
        break;
    }
    case var ᴛ when ᴛ == KindPropertySignature: {
        n.AsPropertySignatureDeclaration().Value.Type = t;
        break;
    }
    case var ᴛ when ᴛ == KindPropertyDeclaration: {
        n.AsPropertyDeclaration().Value.Type = t;
        break;
    }
    case var ᴛ when ᴛ == KindPropertyAssignment: {
        n.AsPropertyAssignment().Value.Type = t;
        break;
    }
    case var ᴛ when ᴛ == KindShorthandPropertyAssignment: {
        n.AsShorthandPropertyAssignment().Value.Type = t;
        break;
    }
    case var ᴛ when ᴛ == KindTypePredicate: {
        n.AsTypePredicateNode().Value.Type = t;
        break;
    }
    case var ᴛ when ᴛ == KindParenthesizedType: {
        n.AsParenthesizedTypeNode().Value.Type = t;
        break;
    }
    case var ᴛ when ᴛ == KindTypeOperator: {
        n.AsTypeOperatorNode().Value.Type = t;
        break;
    }
    case var ᴛ when ᴛ == KindMappedType: {
        n.AsMappedTypeNode().Value.Type = t;
        break;
    }
    case var ᴛ when ᴛ == KindTypeAssertionExpression: {
        n.AsTypeAssertion().Value.Type = t;
        break;
    }
    case var ᴛ when ᴛ == KindAsExpression: {
        n.AsAsExpression().Value.Type = t;
        break;
    }
    case var ᴛ when ᴛ == KindSatisfiesExpression: {
        n.AsSatisfiesExpression().Value.Type = t;
        break;
    }
    case var ᴛ when ᴛ == KindTypeAliasDeclaration || ᴛ == KindJSTypeAliasDeclaration: {
        n.AsTypeAliasDeclaration().Value.Type = t;
        break;
    }
    case var ᴛ when ᴛ == KindNamedTupleMember: {
        n.AsNamedTupleMember().Value.Type = t;
        break;
    }
    case var ᴛ when ᴛ == KindOptionalType: {
        n.AsOptionalTypeNode().Value.Type = t;
        break;
    }
    case var ᴛ when ᴛ == KindRestType: {
        n.AsRestTypeNode().Value.Type = t;
        break;
    }
    case var ᴛ when ᴛ == KindTemplateLiteralTypeSpan: {
        n.AsTemplateLiteralTypeSpan().Value.Type = t;
        break;
    }
    case var ᴛ when ᴛ == KindJSDocTypeExpression: {
        n.AsJSDocTypeExpression().Value.Type = t;
        break;
    }
    case var ᴛ when ᴛ == KindJSDocParameterTag || ᴛ == KindJSDocPropertyTag: {
        n.AsJSDocParameterOrPropertyTag().Value.TypeExpression = t;
        break;
    }
    case var ᴛ when ᴛ == KindJSDocNullableType: {
        n.AsJSDocNullableType().Value.Type = t;
        break;
    }
    case var ᴛ when ᴛ == KindJSDocNonNullableType: {
        n.AsJSDocNonNullableType().Value.Type = t;
        break;
    }
    case var ᴛ when ᴛ == KindJSDocOptionalType: {
        n.AsJSDocOptionalType().Value.Type = t;
        break;
    }
    case var ᴛ when ᴛ == KindExportAssignment: {
        n.AsExportAssignment().Value.Type = t;
        break;
    }
    case var ᴛ when ᴛ == KindBinaryExpression: {
        n.AsBinaryExpression().Value.Type = t;
        break;
    }
    default: {
        var funcLike = n.FunctionLikeData(); if (funcLike != nil) {
            funcLike.Type = t;
        } else {
            throw panic("Unhandled case in mutableNode.SetType: " + n.Kind.String());
        }
        break;
    }}

}

} partial class astᴛNode {

public static ж<Node> Initializer(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindVariableDeclaration: {
        return (~n.AsVariableDeclaration()).Initializer;
    }
    case var ᴛ when ᴛ == KindParameter: {
        return (~n.AsParameterDeclaration()).Initializer;
    }
    case var ᴛ when ᴛ == KindBindingElement: {
        return (~n.AsBindingElement()).Initializer;
    }
    case var ᴛ when ᴛ == KindPropertyDeclaration: {
        return (~n.AsPropertyDeclaration()).Initializer;
    }
    case var ᴛ when ᴛ == KindPropertySignature: {
        return (~n.AsPropertySignatureDeclaration()).Initializer;
    }
    case var ᴛ when ᴛ == KindPropertyAssignment: {
        return (~n.AsPropertyAssignment()).Initializer;
    }
    case var ᴛ when ᴛ == KindEnumMember: {
        return (~n.AsEnumMember()).Initializer;
    }
    case var ᴛ when ᴛ == KindForStatement: {
        return (~n.AsForStatement()).Initializer;
    }
    case var ᴛ when ᴛ == KindForInStatement || ᴛ == KindForOfStatement: {
        return (~n.AsForInOrOfStatement()).Initializer;
    }
    case var ᴛ when ᴛ == KindJsxAttribute: {
        return (~n.AsJsxAttribute()).Initializer;
    }}

    throw panic("Unhandled case in Node.Initializer");
}

} partial class astᴛMutableNode {

public static void SetInitializer(this ж<MutableNode> Ꮡm, ж<Node> initializer) {
    var n = Ꮡm.Reinterpret<MutableNode, Node>();
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindVariableDeclaration: {
        n.AsVariableDeclaration().Value.Initializer = initializer;
        break;
    }
    case var ᴛ when ᴛ == KindParameter: {
        n.AsParameterDeclaration().Value.Initializer = initializer;
        break;
    }
    case var ᴛ when ᴛ == KindBindingElement: {
        n.AsBindingElement().Value.Initializer = initializer;
        break;
    }
    case var ᴛ when ᴛ == KindPropertyDeclaration: {
        n.AsPropertyDeclaration().Value.Initializer = initializer;
        break;
    }
    case var ᴛ when ᴛ == KindPropertySignature: {
        n.AsPropertySignatureDeclaration().Value.Initializer = initializer;
        break;
    }
    case var ᴛ when ᴛ == KindPropertyAssignment: {
        n.AsPropertyAssignment().Value.Initializer = initializer;
        break;
    }
    case var ᴛ when ᴛ == KindEnumMember: {
        n.AsEnumMember().Value.Initializer = initializer;
        break;
    }
    case var ᴛ when ᴛ == KindForStatement: {
        n.AsForStatement().Value.Initializer = initializer;
        break;
    }
    case var ᴛ when ᴛ == KindForInStatement || ᴛ == KindForOfStatement: {
        n.AsForInOrOfStatement().Value.Initializer = initializer;
        break;
    }
    case var ᴛ when ᴛ == KindJsxAttribute: {
        n.AsJsxAttribute().Value.Initializer = initializer;
        break;
    }
    default: {
        throw panic("Unhandled case in mutableNode.SetInitializer");
        break;
    }}

}

} partial class astᴛNode {

public static ж<Node> TagName(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindJsxOpeningElement: {
        return (~n.AsJsxOpeningElement()).TagName;
    }
    case var ᴛ when ᴛ == KindJsxClosingElement: {
        return (~n.AsJsxClosingElement()).TagName;
    }
    case var ᴛ when ᴛ == KindJsxSelfClosingElement: {
        return (~n.AsJsxSelfClosingElement()).TagName;
    }
    case var ᴛ when ᴛ == KindJSDocUnknownTag: {
        return (~n.AsJSDocUnknownTag()).TagName;
    }
    case var ᴛ when ᴛ == KindJSDocAugmentsTag: {
        return (~n.AsJSDocAugmentsTag()).TagName;
    }
    case var ᴛ when ᴛ == KindJSDocImplementsTag: {
        return (~n.AsJSDocImplementsTag()).TagName;
    }
    case var ᴛ when ᴛ == KindJSDocDeprecatedTag: {
        return (~n.AsJSDocDeprecatedTag()).TagName;
    }
    case var ᴛ when ᴛ == KindJSDocPublicTag: {
        return (~n.AsJSDocPublicTag()).TagName;
    }
    case var ᴛ when ᴛ == KindJSDocPrivateTag: {
        return (~n.AsJSDocPrivateTag()).TagName;
    }
    case var ᴛ when ᴛ == KindJSDocProtectedTag: {
        return (~n.AsJSDocProtectedTag()).TagName;
    }
    case var ᴛ when ᴛ == KindJSDocReadonlyTag: {
        return (~n.AsJSDocReadonlyTag()).TagName;
    }
    case var ᴛ when ᴛ == KindJSDocOverrideTag: {
        return (~n.AsJSDocOverrideTag()).TagName;
    }
    case var ᴛ when ᴛ == KindJSDocCallbackTag: {
        return (~n.AsJSDocCallbackTag()).TagName;
    }
    case var ᴛ when ᴛ == KindJSDocOverloadTag: {
        return (~n.AsJSDocOverloadTag()).TagName;
    }
    case var ᴛ when ᴛ == KindJSDocParameterTag || ᴛ == KindJSDocPropertyTag: {
        return (~n.AsJSDocParameterOrPropertyTag()).TagName;
    }
    case var ᴛ when ᴛ == KindJSDocReturnTag: {
        return (~n.AsJSDocReturnTag()).TagName;
    }
    case var ᴛ when ᴛ == KindJSDocThisTag: {
        return (~n.AsJSDocThisTag()).TagName;
    }
    case var ᴛ when ᴛ == KindJSDocTypeTag: {
        return (~n.AsJSDocTypeTag()).TagName;
    }
    case var ᴛ when ᴛ == KindJSDocTemplateTag: {
        return (~n.AsJSDocTemplateTag()).TagName;
    }
    case var ᴛ when ᴛ == KindJSDocTypedefTag: {
        return (~n.AsJSDocTypedefTag()).TagName;
    }
    case var ᴛ when ᴛ == KindJSDocSeeTag: {
        return (~n.AsJSDocSeeTag()).TagName;
    }
    case var ᴛ when ᴛ == KindJSDocSatisfiesTag: {
        return (~n.AsJSDocSatisfiesTag()).TagName;
    }
    case var ᴛ when ᴛ == KindJSDocThrowsTag: {
        return (~n.AsJSDocThrowsTag()).TagName;
    }
    case var ᴛ when ᴛ == KindJSDocImportTag: {
        return (~n.AsJSDocImportTag()).TagName;
    }}

    throw panic("Unhandled case in Node.TagName: " + n.Kind.String());
}

public static ж<Node> PropertyName(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindImportSpecifier: {
        return (~n.AsImportSpecifier()).PropertyName;
    }
    case var ᴛ when ᴛ == KindExportSpecifier: {
        return (~n.AsExportSpecifier()).PropertyName;
    }
    case var ᴛ when ᴛ == KindBindingElement: {
        return (~n.AsBindingElement()).PropertyName;
    }}

    return default;
}

public static ж<Node> PropertyNameOrName(this ref Node n) {
    var name = n.PropertyName();
    if (name == nil) {
        name = n.Name();
    }
    return name;
}

public static bool IsTypeOnly(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindImportEqualsDeclaration: {
        return (~n.AsImportEqualsDeclaration()).IsTypeOnly;
    }
    case var ᴛ when ᴛ == KindImportSpecifier: {
        return (~n.AsImportSpecifier()).IsTypeOnly;
    }
    case var ᴛ when ᴛ == KindImportClause: {
        return n.AsImportClause().PhaseModifier == KindTypeKeyword;
    }
    case var ᴛ when ᴛ == KindExportDeclaration: {
        return (~n.AsExportDeclaration()).IsTypeOnly;
    }
    case var ᴛ when ᴛ == KindExportSpecifier: {
        return (~n.AsExportSpecifier()).IsTypeOnly;
    }}

    return false;
}

public static ж<NodeList> CommentList(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindJSDoc: {
        return n.AsJSDoc().Comment;
    }
    case var ᴛ when ᴛ == KindJSDocUnknownTag: {
        return n.AsJSDocUnknownTag().Comment;
    }
    case var ᴛ when ᴛ == KindJSDocAugmentsTag: {
        return n.AsJSDocAugmentsTag().Comment;
    }
    case var ᴛ when ᴛ == KindJSDocImplementsTag: {
        return n.AsJSDocImplementsTag().Comment;
    }
    case var ᴛ when ᴛ == KindJSDocDeprecatedTag: {
        return n.AsJSDocDeprecatedTag().Comment;
    }
    case var ᴛ when ᴛ == KindJSDocPublicTag: {
        return n.AsJSDocPublicTag().Comment;
    }
    case var ᴛ when ᴛ == KindJSDocPrivateTag: {
        return n.AsJSDocPrivateTag().Comment;
    }
    case var ᴛ when ᴛ == KindJSDocProtectedTag: {
        return n.AsJSDocProtectedTag().Comment;
    }
    case var ᴛ when ᴛ == KindJSDocReadonlyTag: {
        return n.AsJSDocReadonlyTag().Comment;
    }
    case var ᴛ when ᴛ == KindJSDocOverrideTag: {
        return n.AsJSDocOverrideTag().Comment;
    }
    case var ᴛ when ᴛ == KindJSDocCallbackTag: {
        return n.AsJSDocCallbackTag().Comment;
    }
    case var ᴛ when ᴛ == KindJSDocOverloadTag: {
        return n.AsJSDocOverloadTag().Comment;
    }
    case var ᴛ when ᴛ == KindJSDocParameterTag || ᴛ == KindJSDocPropertyTag: {
        return n.AsJSDocParameterOrPropertyTag().Comment;
    }
    case var ᴛ when ᴛ == KindJSDocReturnTag: {
        return n.AsJSDocReturnTag().Comment;
    }
    case var ᴛ when ᴛ == KindJSDocThisTag: {
        return n.AsJSDocThisTag().Comment;
    }
    case var ᴛ when ᴛ == KindJSDocTypeTag: {
        return n.AsJSDocTypeTag().Comment;
    }
    case var ᴛ when ᴛ == KindJSDocTemplateTag: {
        return n.AsJSDocTemplateTag().Comment;
    }
    case var ᴛ when ᴛ == KindJSDocTypedefTag: {
        return n.AsJSDocTypedefTag().Comment;
    }
    case var ᴛ when ᴛ == KindJSDocSeeTag: {
        return n.AsJSDocSeeTag().Comment;
    }
    case var ᴛ when ᴛ == KindJSDocSatisfiesTag: {
        return n.AsJSDocSatisfiesTag().Comment;
    }
    case var ᴛ when ᴛ == KindJSDocThrowsTag: {
        return n.AsJSDocThrowsTag().Comment;
    }
    case var ᴛ when ᴛ == KindJSDocImportTag: {
        return n.AsJSDocImportTag().Comment;
    }}

    throw panic("Unhandled case in Node.CommentList: " + n.Kind.String());
}

public static slice<ж<Node>> Comments(this ref Node n) {
    var list = n.CommentList();
    if (list != nil) {
        return list.Nodes;
    }
    return default;
}

public static ж<Node> Label(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindLabeledStatement: {
        return (~n.AsLabeledStatement()).Label;
    }
    case var ᴛ when ᴛ == KindBreakStatement: {
        return (~n.AsBreakStatement()).Label;
    }
    case var ᴛ when ᴛ == KindContinueStatement: {
        return (~n.AsContinueStatement()).Label;
    }}

    throw panic("Unhandled case in Node.Label: " + n.Kind.String());
}

public static ж<Node> Attributes(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindJsxOpeningElement: {
        return (~n.AsJsxOpeningElement()).Attributes;
    }
    case var ᴛ when ᴛ == KindJsxSelfClosingElement: {
        return (~n.AsJsxSelfClosingElement()).Attributes;
    }
    case var ᴛ when ᴛ == KindModuleDeclaration: {
        return (~n.AsModuleDeclaration()).Attributes;
    }}

    throw panic("Unhandled case in Node.Attributes: " + n.Kind.String());
}

public static ж<NodeList> Children(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindJsxElement: {
        return (~n.AsJsxElement()).Children;
    }
    case var ᴛ when ᴛ == KindJsxFragment: {
        return (~n.AsJsxFragment()).Children;
    }}

    throw panic("Unhandled case in Node.Children: " + n.Kind.String());
}

public static ж<Expression> ModuleSpecifier(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindImportDeclaration || ᴛ == KindJSImportDeclaration: {
        return (~n.AsImportDeclaration()).ModuleSpecifier;
    }
    case var ᴛ when ᴛ == KindExportDeclaration: {
        return (~n.AsExportDeclaration()).ModuleSpecifier;
    }
    case var ᴛ when ᴛ == KindJSDocImportTag: {
        return (~n.AsJSDocImportTag()).ModuleSpecifier;
    }}

    throw panic("Unhandled case in Node.ModuleSpecifier: " + n.Kind.String());
}

public static ж<Node> ImportClause(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindImportDeclaration || ᴛ == KindJSImportDeclaration: {
        return (~n.AsImportDeclaration()).ImportClause;
    }
    case var ᴛ when ᴛ == KindJSDocImportTag: {
        return (~n.AsJSDocImportTag()).ImportClause;
    }}

    throw panic("Unhandled case in Node.ImportClause: " + n.Kind.String());
}

public static ж<Statement> Statement(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindDoStatement: {
        return (~n.AsDoStatement()).Statement;
    }
    case var ᴛ when ᴛ == KindWhileStatement: {
        return (~n.AsWhileStatement()).Statement;
    }
    case var ᴛ when ᴛ == KindForStatement: {
        return (~n.AsForStatement()).Statement;
    }
    case var ᴛ when ᴛ == KindForInStatement || ᴛ == KindForOfStatement: {
        return (~n.AsForInOrOfStatement()).Statement;
    }
    case var ᴛ when ᴛ == KindWithStatement: {
        return (~n.AsWithStatement()).Statement;
    }
    case var ᴛ when ᴛ == KindLabeledStatement: {
        return (~n.AsLabeledStatement()).Statement;
    }}

    throw panic("Unhandled case in Node.Statement: " + n.Kind.String());
}

public static ж<NodeList> PropertyList(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindObjectLiteralExpression: {
        return (~n.AsObjectLiteralExpression()).Properties;
    }
    case var ᴛ when ᴛ == KindJsxAttributes: {
        return (~n.AsJsxAttributes()).Properties;
    }}

    throw panic("Unhandled case in Node.PropertyList: " + n.Kind.String());
}

public static slice<ж<Node>> Properties(this ref Node n) {
    var list = n.PropertyList();
    if (list != nil) {
        return list.Nodes;
    }
    return default;
}

public static ж<NodeList> ElementList(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindNamedImports: {
        return (~n.AsNamedImports()).Elements;
    }
    case var ᴛ when ᴛ == KindNamedExports: {
        return (~n.AsNamedExports()).Elements;
    }
    case var ᴛ when ᴛ == KindObjectBindingPattern || ᴛ == KindArrayBindingPattern: {
        return (~n.AsBindingPattern()).Elements;
    }
    case var ᴛ when ᴛ == KindArrayLiteralExpression: {
        return (~n.AsArrayLiteralExpression()).Elements;
    }
    case var ᴛ when ᴛ == KindTupleType: {
        return (~n.AsTupleTypeNode()).Elements;
    }}

    throw panic("Unhandled case in Node.ElementList: " + n.Kind.String());
}

public static slice<ж<Node>> Elements(this ref Node n) {
    var list = n.ElementList();
    if (list != nil) {
        return list.Nodes;
    }
    return default;
}

public static ж<Node> PostfixToken(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindMethodDeclaration: {
        return (~n.AsMethodDeclaration()).PostfixToken;
    }
    case var ᴛ when ᴛ == KindShorthandPropertyAssignment: {
        return (~n.AsShorthandPropertyAssignment()).PostfixToken;
    }
    case var ᴛ when ᴛ == KindMethodSignature: {
        return (~n.AsMethodSignatureDeclaration()).PostfixToken;
    }
    case var ᴛ when ᴛ == KindPropertySignature: {
        return (~n.AsPropertySignatureDeclaration()).PostfixToken;
    }
    case var ᴛ when ᴛ == KindPropertyAssignment: {
        return (~n.AsPropertyAssignment()).PostfixToken;
    }
    case var ᴛ when ᴛ == KindPropertyDeclaration: {
        return (~n.AsPropertyDeclaration()).PostfixToken;
    }
    case var ᴛ when ᴛ == KindEnumMember: {
        return (~n.AsEnumMember()).PostfixToken;
    }
    case var ᴛ when ᴛ == KindGetAccessor: {
        return (~n.AsGetAccessorDeclaration()).PostfixToken;
    }
    case var ᴛ when ᴛ == KindSetAccessor: {
        return (~n.AsSetAccessorDeclaration()).PostfixToken;
    }}

    return default;
}

public static ж<TokenNode> QuestionToken(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindParameter: {
        return (~n.AsParameterDeclaration()).QuestionToken;
    }
    case var ᴛ when ᴛ == KindConditionalExpression: {
        return (~n.AsConditionalExpression()).QuestionToken;
    }
    case var ᴛ when ᴛ == KindMappedType: {
        return (~n.AsMappedTypeNode()).QuestionToken;
    }
    case var ᴛ when ᴛ == KindNamedTupleMember: {
        return (~n.AsNamedTupleMember()).QuestionToken;
    }}

    var postfix = n.PostfixToken();
    if (postfix != nil && postfix.Kind == KindQuestionToken) {
        return postfix;
    }
    return default;
}

public static ж<Node> QuestionDotToken(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindElementAccessExpression: {
        return (~n.AsElementAccessExpression()).QuestionDotToken;
    }
    case var ᴛ when ᴛ == KindPropertyAccessExpression: {
        return (~n.AsPropertyAccessExpression()).QuestionDotToken;
    }
    case var ᴛ when ᴛ == KindCallExpression: {
        return (~n.AsCallExpression()).QuestionDotToken;
    }
    case var ᴛ when ᴛ == KindTaggedTemplateExpression: {
        return (~n.AsTaggedTemplateExpression()).QuestionDotToken;
    }}

    throw panic("Unhandled case in Node.QuestionDotToken: " + n.Kind.String());
}

public static ж<Node> TypeExpression(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindJSDocParameterTag || ᴛ == KindJSDocPropertyTag: {
        return (~n.AsJSDocParameterOrPropertyTag()).TypeExpression;
    }
    case var ᴛ when ᴛ == KindJSDocReturnTag: {
        return (~n.AsJSDocReturnTag()).TypeExpression;
    }
    case var ᴛ when ᴛ == KindJSDocTypeTag: {
        return (~n.AsJSDocTypeTag()).TypeExpression;
    }
    case var ᴛ when ᴛ == KindJSDocTypedefTag: {
        return (~n.AsJSDocTypedefTag()).TypeExpression;
    }
    case var ᴛ when ᴛ == KindJSDocCallbackTag: {
        return (~n.AsJSDocCallbackTag()).TypeExpression;
    }
    case var ᴛ when ᴛ == KindJSDocSatisfiesTag: {
        return (~n.AsJSDocSatisfiesTag()).TypeExpression;
    }
    case var ᴛ when ᴛ == KindJSDocThrowsTag: {
        return (~n.AsJSDocThrowsTag()).TypeExpression;
    }}

    throw panic("Unhandled case in Node.TypeExpression: " + n.Kind.String());
}

public static ж<Node> ClassName(this ref Node n) {
    switch (n.Kind) {
    case var ᴛ when ᴛ == KindJSDocAugmentsTag: {
        return (~n.AsJSDocAugmentsTag()).ClassName;
    }
    case var ᴛ when ᴛ == KindJSDocImplementsTag: {
        return (~n.AsJSDocImplementsTag()).ClassName;
    }}

    throw panic("Unhandled case in Node.ClassName: " + n.Kind.String());
}

public static bool Contains(this ж<Node> Ꮡn, ж<Node> Ꮡdescendant) {
    ref var descendant = ref Ꮡdescendant.DerefOrNull();

    while (Ꮡdescendant != nil) {
        if (Ꮡdescendant == Ꮡn) {
            return true;
        }
        var parent = descendant.Parent;
        if (parent == nil && !IsSourceFile(Ꮡdescendant)) {
            throw panic("descendant is not parented");
        }
        Ꮡdescendant = parent; descendant = ref Ꮡdescendant.DerefOrNull();
    }
    return false;
}

public static ж<FlowSwitchClauseData> AsFlowSwitchClauseData(this ref Node n) {
    return n.data._<nodeData, ж<FlowSwitchClauseData>>();
}

public static ж<FlowReduceLabelData> AsFlowReduceLabelData(this ref Node n) {
    return n.data._<nodeData, ж<FlowReduceLabelData>>();
}

} partial class ast_package {

partial interface nodeData {
    ж<Node> AsNode();
    bool ForEachChild(Func<ж<Node>, bool> v);
    ж<Node> VisitEachChild(ж<NodeVisitor> v);
    ж<Node> Clone(NodeFactoryCoercible v);
    ж<DeclarationName> Name();
    ж<ModifierList> Modifiers();
    void setModifiers(ж<ModifierList> modifiers);
    ж<FlowNodeBase> FlowNodeData();
    ж<DeclarationBase> DeclarationData();
    ж<ExportableBase> ExportableData();
    ж<LocalsContainerBase> LocalsContainerData();
    ж<FunctionLikeBase> FunctionLikeData();
    ж<ClassLikeBase> ClassLikeData();
    ж<BodyBase> BodyData();
    ж<LiteralLikeNodeBase> LiteralLikeData();
    ж<TemplateLiteralLikeNodeBase> TemplateLiteralLikeData();
    SubtreeFacts SubtreeFacts();
    SubtreeFacts computeSubtreeFacts();
    SubtreeFacts subtreeFactsWorker(nodeData self);
    SubtreeFacts propagateSubtreeFacts();
}

partial struct NodeDefault {
    /*embed*/ public Node Node;
}

} partial class astᴛNodeDefault {

public static ж<Node> AsNode(this ж<NodeDefault> Ꮡnode) { return Ꮡnode.ᏑNode; }
public static bool ForEachChild(this ref NodeDefault node, Func<ж<Node>, bool> v) { return false; }

public static ж<Node> VisitEachChild(this ж<NodeDefault> Ꮡnode, ж<NodeVisitor> v) { return Ꮡnode.AsNode(); }
public static ж<Node> Clone(this ref NodeDefault node, NodeFactoryCoercible v) { return default; }
public static ж<DeclarationName> Name(this ref NodeDefault node) { return default; }
public static ж<ModifierList> Modifiers(this ref NodeDefault node) { return default; }
internal static void setModifiers(this ref NodeDefault node, ж<ModifierList> modifiers) { }
public static ж<FlowNodeBase> FlowNodeData(this ref NodeDefault node) { return default; }
public static ж<DeclarationBase> DeclarationData(this ref NodeDefault node) { return default; }
public static ж<ExportableBase> ExportableData(this ref NodeDefault node) { return default; }
public static ж<LocalsContainerBase> LocalsContainerData(this ref NodeDefault node) { return default; }
public static ж<FunctionLikeBase> FunctionLikeData(this ref NodeDefault node) { return default; }
public static ж<ClassLikeBase> ClassLikeData(this ref NodeDefault node) { return default; }
public static ж<BodyBase> BodyData(this ref NodeDefault node) { return default; }
public static ж<LiteralLikeNodeBase> LiteralLikeData(this ref NodeDefault node) { return default; }
public static ж<TemplateLiteralLikeNodeBase> TemplateLiteralLikeData(this ref NodeDefault node) { return default; }
public static SubtreeFacts SubtreeFacts(this ref NodeDefault node) {
    return node.data.subtreeFactsWorker(node.data);
}

internal static SubtreeFacts subtreeFactsWorker(this ref NodeDefault node, nodeData self) {
    return self.computeSubtreeFacts();
}

internal static SubtreeFacts computeSubtreeFacts(this ref NodeDefault node) {
    return SubtreeFactsNone;
}

internal static SubtreeFacts propagateSubtreeFacts(this ref NodeDefault node) {
    return (SubtreeFacts)(node.data.SubtreeFacts() & ~((SubtreeFacts)SubtreeExclusionsNode));
}

} partial class ast_package {

partial struct NodeBase {
    /*embed*/ public NodeDefault NodeDefault;
}

public static bool IsWriteOnlyAccess(ж<Node> node) {
    return accessKind(node) == AccessKindWrite;
}

public static bool IsWriteAccess(ж<Node> node) {
    return accessKind(node) != AccessKindRead;
}

public static bool IsWriteAccessForReference(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var decl = GetDeclarationFromName(Ꮡnode);
    return (decl != nil && declarationIsWriteAccess(decl)) || node.Kind == KindDefaultKeyword || IsWriteAccess(Ꮡnode);
}

public static ж<Declaration> GetDeclarationFromName(ж<Node> Ꮡname) {
    ref var name = ref Ꮡname.DerefOrNull();

    if (Ꮡname == nil || name.Parent == nil) {
        return default;
    }
    var parent = name.Parent;
    var exprᴛ1 = name.Kind;
    var matchᴛ1 = false;
    if (exprᴛ1 == KindStringLiteral || exprᴛ1 == KindNoSubstitutionTemplateLiteral || exprᴛ1 == KindNumericLiteral) { matchᴛ1 = true;
        if (IsComputedPropertyName(parent)) {
            return parent.Parent;
        }
        fallthrough = true;
    }
    if (fallthrough || !matchᴛ1 && exprᴛ1 == KindIdentifier) {
        if (IsDeclaration(parent)) {
            if (parent.Name() == Ꮡname) {
                return parent;
            }
            return default;
        }
        if (IsQualifiedName(parent)) {
            var tag = parent.Parent;
            if (IsJSDocParameterTag(tag) && tag.Name() == parent) {
                return tag;
            }
            return default;
        }
        var binExp = parent.Parent;
        if (IsBinaryExpression(binExp) && GetAssignmentDeclarationKind(binExp) != JSDeclarationKindNone) {
            var leftHasSymbol = false;
            if (binExp.AsBinaryExpression().Left != nil && binExp.AsBinaryExpression().Left.Symbol() != nil) {
                leftHasSymbol = true;
            }
            if (leftHasSymbol || binExp.Symbol() != nil) {
                if (GetNameOfDeclaration(binExp.AsNode()) == Ꮡname) {
                    return binExp.AsNode();
                }
            }
        }
    }
    else if (exprᴛ1 == KindPrivateIdentifier) { matchᴛ1 = true;
        if (IsDeclaration(parent) && parent.Name() == Ꮡname) {
            return parent;
        }
    }

    return default;
}

internal static bool declarationIsWriteAccess(ж<Node> Ꮡdecl) {
    ref var decl = ref Ꮡdecl.DerefOrNull();

    if (Ꮡdecl == nil) {
        return false;
    }
    if ((NodeFlags)(decl.Flags & NodeFlagsAmbient) != 0) {
        return true;
    }

    switch (decl.Kind) {
    case var ᴛ when ᴛ == KindBinaryExpression || ᴛ == KindBindingElement || ᴛ == KindClassDeclaration || ᴛ == KindClassExpression || ᴛ == KindDefaultKeyword || ᴛ == KindEnumDeclaration || ᴛ == KindEnumMember || ᴛ == KindExportSpecifier || ᴛ == KindImportClause || ᴛ == KindImportEqualsDeclaration || ᴛ == KindImportSpecifier || ᴛ == KindInterfaceDeclaration || ᴛ == KindJSDocCallbackTag || ᴛ == KindJSDocTypedefTag || ᴛ == KindJsxAttribute || ᴛ == KindModuleDeclaration || ᴛ == KindNamespaceExportDeclaration || ᴛ == KindNamespaceImport || ᴛ == KindNamespaceExport || ᴛ == KindParameter || ᴛ == KindShorthandPropertyAssignment || ᴛ == KindTypeAliasDeclaration || ᴛ == KindJSTypeAliasDeclaration || ᴛ == KindTypeParameter: {
        return true;
    }
    case var ᴛ when ᴛ == KindPropertyAssignment: {
        return !IsArrayLiteralOrObjectLiteralDestructuringPattern(decl.Parent);
    }
    case var ᴛ when ᴛ == KindFunctionDeclaration || ᴛ == KindFunctionExpression || ᴛ == KindConstructor || ᴛ == KindMethodDeclaration || ᴛ == KindGetAccessor || ᴛ == KindSetAccessor: {
        switch (decl.Kind) {
        case var ᴛ2 when ᴛ2 == KindFunctionDeclaration: {
            return (~decl.AsFunctionDeclaration()).Body != nil;
        }
        case var ᴛ2 when ᴛ2 == KindFunctionExpression: {
            return (~decl.AsFunctionExpression()).Body != nil;
        }
        case var ᴛ2 when ᴛ2 == KindConstructor: {
            return (~decl.AsConstructorDeclaration()).Body != nil;
        }
        case var ᴛ2 when ᴛ2 == KindMethodDeclaration: {
            return (~decl.AsMethodDeclaration()).Body != nil;
        }
        case var ᴛ2 when ᴛ2 == KindGetAccessor: {
            return (~decl.AsGetAccessorDeclaration()).Body != nil;
        }
        case var ᴛ2 when ᴛ2 == KindSetAccessor: {
            return (~decl.AsSetAccessorDeclaration()).Body != nil;
        }}

        return false;
    }
    case var ᴛ when ᴛ == KindVariableDeclaration || ᴛ == KindPropertyDeclaration: {
        bool hasInit = default;
        switch (decl.Kind) {
        case var ᴛ2 when ᴛ2 == KindVariableDeclaration: {
            hasInit = (~decl.AsVariableDeclaration()).Initializer != nil;
            break;
        }
        case var ᴛ2 when ᴛ2 == KindPropertyDeclaration: {
            hasInit = (~decl.AsPropertyDeclaration()).Initializer != nil;
            break;
        }}

        return hasInit || IsCatchClause(decl.Parent);
    }
    case var ᴛ when ᴛ == KindMethodSignature || ᴛ == KindPropertySignature || ᴛ == KindJSDocPropertyTag || ᴛ == KindJSDocParameterTag: {
        return false;
    }
    default: {
        throw panic("Unhandled case in declarationIsWriteAccess");
        break;
    }}

}

public static bool IsArrayLiteralOrObjectLiteralDestructuringPattern(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (!(IsArrayLiteralExpression(Ꮡnode) || IsObjectLiteralExpression(Ꮡnode))) {
        return false;
    }
    var parent = node.Parent;
    if (IsBinaryExpression(parent) && parent.AsBinaryExpression().Left == Ꮡnode && parent.AsBinaryExpression().OperatorToken.Kind == KindEqualsToken) {
        return true;
    }
    if (IsForOfStatement(parent) && parent.Initializer() == Ꮡnode) {
        return true;
    }
    if (IsPropertyAssignment(parent)) {
        return IsArrayLiteralOrObjectLiteralDestructuringPattern(parent.Parent);
    }
    return IsArrayLiteralOrObjectLiteralDestructuringPattern(parent);
}

internal static AccessKind accessKind(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var parent = node.Parent;
    if (parent == nil) {
        return AccessKindRead;
    }
    switch (parent.Kind) {
    case var ᴛ when ᴛ == KindParenthesizedExpression: {
        return accessKind(parent);
    }
    case var ᴛ when ᴛ == KindPrefixUnaryExpression: {
        var @operator = parent.AsPrefixUnaryExpression().Operator;
        if (@operator == KindPlusPlusToken || @operator == KindMinusMinusToken) {
            return AccessKindReadWrite;
        }
        return AccessKindRead;
    }
    case var ᴛ when ᴛ == KindPostfixUnaryExpression: {
        var @operator = parent.AsPostfixUnaryExpression().Operator;
        if (@operator == KindPlusPlusToken || @operator == KindMinusMinusToken) {
            return AccessKindReadWrite;
        }
        return AccessKindRead;
    }
    case var ᴛ when ᴛ == KindBinaryExpression: {
        if (parent.AsBinaryExpression().Left == Ꮡnode) {
            var @operator = parent.AsBinaryExpression().OperatorToken;
            if (IsAssignmentOperator(@operator.Kind)) {
                if (@operator.Kind == KindEqualsToken) {
                    return AccessKindWrite;
                }
                return AccessKindReadWrite;
            }
        }
        return AccessKindRead;
    }
    case var ᴛ when ᴛ == KindPropertyAccessExpression: {
        if (parent.AsPropertyAccessExpression().Name() != Ꮡnode) {
            return AccessKindRead;
        }
        return accessKind(parent);
    }
    case var ᴛ when ᴛ == KindPropertyAssignment: {
        var parentAccess = accessKind(parent.Parent);
        if (Ꮡnode == parent.AsPropertyAssignment().Name()) {
            return reverseAccessKind(parentAccess);
        }
        return parentAccess;
    }
    case var ᴛ when ᴛ == KindShorthandPropertyAssignment: {
        if (Ꮡnode == parent.AsShorthandPropertyAssignment().ObjectAssignmentInitializer) {
            return AccessKindRead;
        }
        return accessKind(parent.Parent);
    }
    case var ᴛ when ᴛ == KindArrayLiteralExpression: {
        return accessKind(parent);
    }
    case var ᴛ when ᴛ == KindForInStatement || ᴛ == KindForOfStatement: {
        if (Ꮡnode == (~parent.AsForInOrOfStatement()).Initializer) {
            return AccessKindWrite;
        }
        return AccessKindRead;
    }
    default: {
        return AccessKindRead;
    }}

}

internal static AccessKind reverseAccessKind(AccessKind a) {
    switch (a) {
    case var ᴛ when ᴛ == AccessKindRead: {
        return AccessKindWrite;
    }
    case var ᴛ when ᴛ == AccessKindWrite: {
        return AccessKindRead;
    }
    case var ᴛ when ᴛ == AccessKindReadWrite: {
        return AccessKindReadWrite;
    }}

    throw panic("Unhandled case in reverseAccessKind");
}

partial struct AccessKind /*num:int32*/;

public static AccessKind AccessKindRead => /* iota */ 0;
public static AccessKind AccessKindWrite => 1;
public static AccessKind AccessKindReadWrite => 2;

} partial class astᴛDeclarationBase {

public static ж<DeclarationBase> DeclarationData(this ж<DeclarationBase> Ꮡnode) { return Ꮡnode; }

} partial class ast_package {

public static bool IsDeclarationNode(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.DeclarationData() != nil;
}

} partial class astᴛExportableBase {

public static ж<ExportableBase> ExportableData(this ж<ExportableBase> Ꮡnode) { return Ꮡnode; }

} partial class astᴛModifiersBase {

public static ж<ModifierList> Modifiers(this ref ModifiersBase node) { return node.modifiers; }

internal static void setModifiers(this ref ModifiersBase node, ж<ModifierList> Ꮡmodifiers) {
    ref var modifiers = ref Ꮡmodifiers.DerefOrNull();

    node.modifiers = Ꮡmodifiers;
}

} partial class astᴛLocalsContainerBase {

public static ж<LocalsContainerBase> LocalsContainerData(this ж<LocalsContainerBase> Ꮡnode) { return Ꮡnode; }

} partial class ast_package {

public static bool IsLocalsContainer(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.LocalsContainerData() != nil;
}

} partial class astᴛFunctionLikeBase {

public static ж<LocalsContainerBase> LocalsContainerData(this ж<FunctionLikeBase> Ꮡnode) {
    return Ꮡnode.ᏑLocalsContainerBase;
}
public static ж<FunctionLikeBase> FunctionLikeData(this ж<FunctionLikeBase> Ꮡnode) { return Ꮡnode; }

} partial class astᴛBodyBase {

public static ж<BodyBase> BodyData(this ж<BodyBase> Ꮡnode) { return Ꮡnode; }

} partial class astᴛFunctionLikeWithBodyBase {

public static ж<LocalsContainerBase> LocalsContainerData(this ж<FunctionLikeWithBodyBase> Ꮡnode) {
    return Ꮡnode.of(FunctionLikeWithBodyBase.ᏑLocalsContainerBase);
}

public static ж<FunctionLikeBase> FunctionLikeData(this ж<FunctionLikeWithBodyBase> Ꮡnode) {
    return Ꮡnode.ᏑFunctionLikeBase;
}
public static ж<BodyBase> BodyData(this ж<FunctionLikeWithBodyBase> Ꮡnode) { return Ꮡnode.ᏑBodyBase; }

} partial class astᴛFlowNodeBase {

public static ж<FlowNodeBase> FlowNodeData(this ж<FlowNodeBase> Ꮡnode) { return Ꮡnode; }

} partial class astᴛNode {

public static slice<ж<Node>> JSDoc(this ж<Node> Ꮡnode, ж<SourceFile> Ꮡfile) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var @file = ref Ꮡfile.DerefOrNull();

    if ((NodeFlags)(node.Flags & NodeFlagsHasJSDoc) == 0) {
        return default;
    }
    if (Ꮡfile == nil) {
        Ꮡfile = GetSourceFileOfNode(Ꮡnode); @file = ref Ꮡfile.DerefOrNull();
        if (Ꮡfile == nil) {
            return default;
        }
    }
    if (@file.hasLazyJSDoc) {
        return Ꮡfile.resolveJSDoc(Ꮡnode);
    }
    return @file.jsdocCache[Ꮡnode];
}

public static slice<ж<Node>> EagerJSDoc(this ж<Node> Ꮡnode, ж<SourceFile> Ꮡfile) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var @file = ref Ꮡfile.DerefOrNull();

    if ((NodeFlags)(node.Flags & NodeFlagsHasJSDoc) == 0) {
        return default;
    }
    if (Ꮡfile == nil) {
        Ꮡfile = GetSourceFileOfNode(Ꮡnode); @file = ref Ꮡfile.DerefOrNull();
        if (Ꮡfile == nil) {
            return default;
        }
    }
    if (@file.hasLazyJSDoc) {
        Ꮡfile.ᏑjsdocMu.RLock();
        var jsdocs = @file.jsdocCache[Ꮡnode];
        Ꮡfile.ᏑjsdocMu.RUnlock();
        return jsdocs;
    }
    return @file.jsdocCache[Ꮡnode];
}

} partial class astᴛCompositeBase {

internal static SubtreeFacts subtreeFactsWorker(this ж<CompositeBase> Ꮡnode, nodeData self) {
    var facts = (SubtreeFacts)Ꮡnode.Ꮡfacts.Load();
    if ((SubtreeFacts)(facts & SubtreeFactsComputed) == 0) {
        facts |= (SubtreeFacts)((SubtreeFacts)(self.computeSubtreeFacts() | SubtreeFactsComputed));
        Ꮡnode.Ꮡfacts.Store((uint32)facts);
    }
    return (SubtreeFacts)(facts & ~SubtreeFactsComputed);
}

internal static SubtreeFacts computeSubtreeFacts(this ref CompositeBase node) {
    throw panic("not implemented");
}

} partial class astᴛTypeSyntaxBase {

internal static SubtreeFacts computeSubtreeFacts(this ref TypeSyntaxBase node) { return SubtreeContainsTypeScript; }

internal static SubtreeFacts propagateSubtreeFacts(this ref TypeSyntaxBase node) { return SubtreeContainsTypeScript; }

} partial class astᴛToken {

internal static SubtreeFacts computeSubtreeFacts(this ref Token node) {
    switch (node.Kind) {
    case var ᴛ when ᴛ == KindUsingKeyword: {
        return SubtreeContainsUsing;
    }
    case var ᴛ when ᴛ == KindPublicKeyword || ᴛ == KindPrivateKeyword || ᴛ == KindProtectedKeyword || ᴛ == KindReadonlyKeyword || ᴛ == KindAbstractKeyword || ᴛ == KindDeclareKeyword || ᴛ == KindConstKeyword || ᴛ == KindAnyKeyword || ᴛ == KindNumberKeyword || ᴛ == KindBigIntKeyword || ᴛ == KindNeverKeyword || ᴛ == KindObjectKeyword || ᴛ == KindInKeyword || ᴛ == KindOutKeyword || ᴛ == KindOverrideKeyword || ᴛ == KindStringKeyword || ᴛ == KindBooleanKeyword || ᴛ == KindSymbolKeyword || ᴛ == KindVoidKeyword || ᴛ == KindUnknownKeyword || ᴛ == KindUndefinedKeyword || ᴛ == KindExportKeyword: {
        return SubtreeContainsTypeScript;
    }
    case var ᴛ when ᴛ == KindAccessorKeyword: {
        return SubtreeContainsClassFields;
    }
    case var ᴛ when ᴛ == KindAsyncKeyword: {
        return SubtreeContainsAnyAwait;
    }
    case var ᴛ when ᴛ == KindSuperKeyword: {
        return SubtreeContainsLexicalSuper;
    }
    case var ᴛ when ᴛ == KindThisKeyword: {
        return SubtreeContainsLexicalThis;
    }
    case var ᴛ when ᴛ == KindAsteriskAsteriskToken || ᴛ == KindAsteriskAsteriskEqualsToken: {
        return SubtreeContainsExponentiationOperator;
    }
    case var ᴛ when ᴛ == KindQuestionQuestionToken: {
        return SubtreeContainsNullishCoalescing;
    }
    case var ᴛ when ᴛ == KindQuestionDotToken: {
        return SubtreeContainsOptionalChaining;
    }
    case var ᴛ when ᴛ == KindQuestionQuestionEqualsToken || ᴛ == KindBarBarEqualsToken || ᴛ == KindAmpersandAmpersandEqualsToken: {
        return SubtreeContainsLogicalAssignments;
    }}

    return SubtreeFactsNone;
}

} partial class astᴛPrivateIdentifier {

internal static SubtreeFacts computeSubtreeFacts(this ref PrivateIdentifier node) {
    return SubtreeContainsClassFields;
}

} partial class astᴛNodeFactory {

public static ж<Node> NewModifier(this ref NodeFactory f, Kind kind) {
    return f.NewToken(kind);
}

} partial class astᴛDecorator {

internal static SubtreeFacts computeSubtreeFacts(this ref Decorator node) {
    return (SubtreeFacts)((SubtreeFacts)(propagateSubtreeFacts(node.Expression) | SubtreeContainsTypeScript) | SubtreeContainsDecorators);
}

} partial class astᴛForInOrOfStatement {

internal static SubtreeFacts computeSubtreeFacts(this ref ForInOrOfStatement node) {
    return (SubtreeFacts)((SubtreeFacts)((SubtreeFacts)(propagateSubtreeFacts(node.Initializer) | propagateSubtreeFacts(node.Expression)) | propagateSubtreeFacts(node.Statement)) | core.IfElse(node.AwaitModifier != nil, SubtreeContainsForAwaitOrAsyncGenerator, SubtreeFactsNone));
}

} partial class astᴛReturnStatement {

internal static SubtreeFacts computeSubtreeFacts(this ref ReturnStatement node) {
    return (SubtreeFacts)(propagateSubtreeFacts(node.Expression) | SubtreeContainsForAwaitOrAsyncGenerator);
}

} partial class astᴛCatchClause {

internal static SubtreeFacts computeSubtreeFacts(this ref CatchClause node) {
    var res = (SubtreeFacts)(ast.propagateSubtreeFacts(node.VariableDeclaration) | ast.propagateSubtreeFacts(node.Block));
    if (node.VariableDeclaration == nil) {
        res |= (SubtreeFacts)(SubtreeContainsMissingCatchClauseVariable);
    }
    return res;
}

internal static SubtreeFacts propagateSubtreeFacts(this ref CatchClause node) {
    return (SubtreeFacts)(node.NodeBase.NodeDefault.SubtreeFacts() & ~((SubtreeFacts)SubtreeExclusionsCatchClause));
}

} partial class astᴛVariableStatement {

internal static SubtreeFacts computeSubtreeFacts(this ref VariableStatement node) {
    if (node.modifiers != nil && (ModifierFlags)(node.modifiers.ModifierFlags & ModifierFlagsAmbient) != 0) {
        return SubtreeContainsTypeScript;
    } else {
        return (SubtreeFacts)(propagateModifierListSubtreeFacts(node.modifiers) | propagateSubtreeFacts(node.DeclarationList));
    }
}

} partial class astᴛVariableDeclaration {

internal static SubtreeFacts computeSubtreeFacts(this ref VariableDeclaration node) {
    return (SubtreeFacts)((SubtreeFacts)((SubtreeFacts)(propagateSubtreeFacts(node.name) | propagateEraseableSyntaxSubtreeFacts(node.ExclamationToken)) | propagateEraseableSyntaxSubtreeFacts(node.Type)) | propagateSubtreeFacts(node.Initializer));
}

} partial class astᴛVariableDeclarationList {

internal static SubtreeFacts computeSubtreeFacts(this ref VariableDeclarationList node) {
    return (SubtreeFacts)(propagateNodeListSubtreeFacts(node.Declarations, ast.propagateSubtreeFacts) | core.IfElse((NodeFlags)(node.Flags & NodeFlagsUsing) != 0, SubtreeContainsUsing, SubtreeFactsNone));
}

internal static SubtreeFacts propagateSubtreeFacts(this ref VariableDeclarationList node) {
    return (SubtreeFacts)(node.NodeBase.NodeDefault.SubtreeFacts() & ~((SubtreeFacts)SubtreeExclusionsVariableDeclarationList));
}

} partial class astᴛBindingPattern {

internal static SubtreeFacts computeSubtreeFacts(this ref BindingPattern node) {
    switch (node.Kind) {
    case var ᴛ when ᴛ == KindObjectBindingPattern: {
        return propagateNodeListSubtreeFacts(node.Elements, propagateObjectBindingElementSubtreeFacts);
    }
    case var ᴛ when ᴛ == KindArrayBindingPattern: {
        return propagateNodeListSubtreeFacts(node.Elements, propagateBindingElementSubtreeFacts);
    }
    default: {
        return SubtreeFactsNone;
    }}

}

internal static SubtreeFacts propagateSubtreeFacts(this ref BindingPattern node) {
    return (SubtreeFacts)(node.NodeBase.NodeDefault.SubtreeFacts() & ~((SubtreeFacts)SubtreeExclusionsBindingPattern));
}

} partial class astᴛParameterDeclaration {

internal static SubtreeFacts computeSubtreeFacts(this ref ParameterDeclaration node) {
    if (node.name != nil && IsThisIdentifier(node.name)) {
        return SubtreeContainsTypeScript;
    } else {
        return (SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)(propagateModifierListSubtreeFacts(node.modifiers) | ast.propagateSubtreeFacts(node.name)) | propagateEraseableSyntaxSubtreeFacts(node.QuestionToken)) | propagateEraseableSyntaxSubtreeFacts(node.Type)) | ast.propagateSubtreeFacts(node.Initializer));
    }
}

internal static SubtreeFacts propagateSubtreeFacts(this ref ParameterDeclaration node) {
    return (SubtreeFacts)(node.NodeBase.NodeDefault.SubtreeFacts() & ~((SubtreeFacts)SubtreeExclusionsParameter));
}

} partial class astᴛBindingElement {

internal static SubtreeFacts computeSubtreeFacts(this ref BindingElement node) {
    return (SubtreeFacts)((SubtreeFacts)((SubtreeFacts)(propagateSubtreeFacts(node.PropertyName) | propagateSubtreeFacts(node.name)) | propagateSubtreeFacts(node.Initializer)) | core.IfElse(node.DotDotDotToken != nil, SubtreeContainsRestOrSpread, SubtreeFactsNone));
}

} partial class astᴛFunctionDeclaration {

internal static SubtreeFacts computeSubtreeFacts(this ref FunctionDeclaration node) {
    if (node.Body == nil || (ModifierFlags)(node.StatementBase.NodeBase.NodeDefault.Node.ModifierFlags() & ModifierFlagsAmbient) != 0) {
        return SubtreeContainsTypeScript;
    } else {
        var isAsync = (ModifierFlags)(node.StatementBase.NodeBase.NodeDefault.Node.ModifierFlags() & ModifierFlagsAsync) != 0;
        var isGenerator = node.AsteriskToken != nil;
        return (SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)(propagateModifierListSubtreeFacts(node.modifiers) | ast.propagateSubtreeFacts(node.AsteriskToken)) | ast.propagateSubtreeFacts(node.name)) | propagateEraseableSyntaxListSubtreeFacts(node.TypeParameters)) | propagateNodeListSubtreeFacts(node.Parameters, ast.propagateSubtreeFacts)) | propagateEraseableSyntaxSubtreeFacts(node.Type)) | propagateEraseableSyntaxSubtreeFacts(node.FullSignature)) | ast.propagateSubtreeFacts(node.Body)) | core.IfElse(isAsync && isGenerator, SubtreeContainsForAwaitOrAsyncGenerator, SubtreeFactsNone)) | core.IfElse(isAsync && !isGenerator, SubtreeContainsAnyAwait, SubtreeFactsNone));
    }
}

internal static SubtreeFacts propagateSubtreeFacts(this ref FunctionDeclaration node) {
    return (SubtreeFacts)(node.StatementBase.NodeBase.NodeDefault.SubtreeFacts() & ~((SubtreeFacts)SubtreeExclusionsFunction));
}

} partial class astᴛClassLikeBase {

public static ж<DeclarationName> Name(this ref ClassLikeBase node) { return node.name; }

public static ж<ClassLikeBase> ClassLikeData(this ж<ClassLikeBase> Ꮡnode) { return Ꮡnode; }

internal static SubtreeFacts computeSubtreeFacts(this ref ClassLikeBase node) {
    if (node.modifiers != nil && (ModifierFlags)(node.modifiers.ModifierFlags & ModifierFlagsAmbient) != 0) {
        return SubtreeContainsTypeScript;
    } else {
        return (SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)(propagateModifierListSubtreeFacts(node.modifiers) | propagateSubtreeFacts(node.name)) | propagateEraseableSyntaxListSubtreeFacts(node.TypeParameters)) | propagateNodeListSubtreeFacts(node.HeritageClauses, propagateSubtreeFacts)) | propagateNodeListSubtreeFacts(node.Members, propagateSubtreeFacts));
    }
}

} partial class astᴛClassDeclaration {

internal static SubtreeFacts propagateSubtreeFacts(this ref ClassDeclaration node) {
    return (SubtreeFacts)(node.StatementBase.NodeBase.NodeDefault.SubtreeFacts() & ~((SubtreeFacts)SubtreeExclusionsClass));
}

} partial class astᴛClassExpression {

internal static SubtreeFacts propagateSubtreeFacts(this ref ClassExpression node) {
    return (SubtreeFacts)(node.PrimaryExpressionBase.MemberExpressionBase.LeftHandSideExpressionBase.UpdateExpressionBase.UnaryExpressionBase.ExpressionBase.NodeBase.NodeDefault.SubtreeFacts() & ~((SubtreeFacts)SubtreeExclusionsClass));
}

} partial class astᴛHeritageClause {

internal static SubtreeFacts computeSubtreeFacts(this ref HeritageClause node) {
    switch (node.Token) {
    case var ᴛ when ᴛ == KindExtendsKeyword: {
        return propagateNodeListSubtreeFacts(node.Types, propagateSubtreeFacts);
    }
    case var ᴛ when ᴛ == KindImplementsKeyword: {
        return SubtreeContainsTypeScript;
    }
    default: {
        return SubtreeFactsNone;
    }}

}

} partial class ast_package {

public static bool IsTypeOrJSTypeAliasDeclaration(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == KindTypeAliasDeclaration || node.Kind == KindJSTypeAliasDeclaration;
}

} partial class astᴛEnumMember {

internal static SubtreeFacts computeSubtreeFacts(this ref EnumMember node) {
    return (SubtreeFacts)((SubtreeFacts)(propagateSubtreeFacts(node.name) | propagateSubtreeFacts(node.Initializer)) | SubtreeContainsTypeScript);
}

} partial class astᴛEnumDeclaration {

internal static SubtreeFacts computeSubtreeFacts(this ref EnumDeclaration node) {
    if (node.modifiers != nil && (ModifierFlags)(node.modifiers.ModifierFlags & ModifierFlagsAmbient) != 0) {
        return SubtreeContainsTypeScript;
    } else {
        return (SubtreeFacts)((SubtreeFacts)((SubtreeFacts)(propagateModifierListSubtreeFacts(node.modifiers) | propagateSubtreeFacts(node.name)) | propagateNodeListSubtreeFacts(node.Members, propagateSubtreeFacts)) | SubtreeContainsTypeScript);
    }
}

} partial class astᴛModuleDeclaration {

internal static SubtreeFacts computeSubtreeFacts(this ref ModuleDeclaration node) {
    if ((ModifierFlags)(node.StatementBase.NodeBase.NodeDefault.Node.ModifierFlags() & ModifierFlagsAmbient) != 0) {
        return SubtreeContainsTypeScript;
    } else {
        return (SubtreeFacts)((SubtreeFacts)((SubtreeFacts)(propagateModifierListSubtreeFacts(node.modifiers) | ast.propagateSubtreeFacts(node.name)) | ast.propagateSubtreeFacts(node.Body)) | SubtreeContainsTypeScript);
    }
}

internal static SubtreeFacts propagateSubtreeFacts(this ref ModuleDeclaration node) {
    return (SubtreeFacts)(node.StatementBase.NodeBase.NodeDefault.SubtreeFacts() & ~((SubtreeFacts)SubtreeExclusionsModule));
}

} partial class astᴛImportEqualsDeclaration {

internal static SubtreeFacts computeSubtreeFacts(this ref ImportEqualsDeclaration node) {
    if (node.IsTypeOnly || !IsExternalModuleReference(node.ModuleReference)) {
        return SubtreeContainsTypeScript;
    } else {
        return (SubtreeFacts)((SubtreeFacts)(propagateModifierListSubtreeFacts(node.modifiers) | propagateSubtreeFacts(node.name)) | propagateSubtreeFacts(node.ModuleReference));
    }
}

} partial class ast_package {

public static bool IsImportDeclarationOrJSImportDeclaration(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == KindImportDeclaration || node.Kind == KindJSImportDeclaration;
}

} partial class astᴛImportSpecifier {

internal static SubtreeFacts computeSubtreeFacts(this ref ImportSpecifier node) {
    if (node.IsTypeOnly) {
        return SubtreeContainsTypeScript;
    } else {
        return (SubtreeFacts)(propagateSubtreeFacts(node.PropertyName) | propagateSubtreeFacts(node.name));
    }
}

} partial class astᴛImportClause {

internal static SubtreeFacts computeSubtreeFacts(this ref ImportClause node) {
    if (node.PhaseModifier == KindTypeKeyword) {
        return SubtreeContainsTypeScript;
    } else {
        return (SubtreeFacts)(propagateSubtreeFacts(node.name) | propagateSubtreeFacts(node.NamedBindings));
    }
}

} partial class astᴛExportAssignment {

internal static SubtreeFacts computeSubtreeFacts(this ref ExportAssignment node) {
    return (SubtreeFacts)((SubtreeFacts)((SubtreeFacts)(propagateModifierListSubtreeFacts(node.modifiers) | propagateSubtreeFacts(node.Type)) | propagateSubtreeFacts(node.Expression)) | core.IfElse(node.IsExportEquals, SubtreeContainsTypeScript, SubtreeFactsNone));
}

} partial class ast_package {

public static bool IsAnyExportAssignment(ж<Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == KindExportAssignment;
}

} partial class astᴛExportDeclaration {

internal static SubtreeFacts computeSubtreeFacts(this ref ExportDeclaration node) {
    return (SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)(propagateModifierListSubtreeFacts(node.modifiers) | propagateSubtreeFacts(node.ExportClause)) | propagateSubtreeFacts(node.ModuleSpecifier)) | propagateSubtreeFacts(node.Attributes)) | core.IfElse(node.IsTypeOnly, SubtreeContainsTypeScript, SubtreeFactsNone));
}

} partial class astᴛExportSpecifier {

internal static SubtreeFacts computeSubtreeFacts(this ref ExportSpecifier node) {
    if (node.IsTypeOnly) {
        return SubtreeContainsTypeScript;
    } else {
        return (SubtreeFacts)(propagateSubtreeFacts(node.PropertyName) | propagateSubtreeFacts(node.name));
    }
}

} partial class astᴛNamedMemberBase {

public static ж<ModifierList> Modifiers(this ref NamedMemberBase node) { return node.modifiers; }

internal static void setModifiers(this ref NamedMemberBase node, ж<ModifierList> Ꮡmodifiers) {
    ref var modifiers = ref Ꮡmodifiers.DerefOrNull();

    node.modifiers = Ꮡmodifiers;
}

public static ж<DeclarationName> Name(this ref NamedMemberBase node) { return node.name; }

} partial class astᴛConstructorDeclaration {

internal static SubtreeFacts computeSubtreeFacts(this ref ConstructorDeclaration node) {
    if (node.Body == nil) {
        return SubtreeContainsTypeScript;
    } else {
        return (SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)(propagateModifierListSubtreeFacts(node.modifiers) | propagateEraseableSyntaxListSubtreeFacts(node.TypeParameters)) | propagateNodeListSubtreeFacts(node.Parameters, ast.propagateSubtreeFacts)) | propagateEraseableSyntaxSubtreeFacts(node.Type)) | propagateEraseableSyntaxSubtreeFacts(node.FullSignature)) | ast.propagateSubtreeFacts(node.Body));
    }
}

internal static SubtreeFacts propagateSubtreeFacts(this ref ConstructorDeclaration node) {
    return (SubtreeFacts)(node.NodeBase.NodeDefault.SubtreeFacts() & ~((SubtreeFacts)SubtreeExclusionsConstructor));
}

} partial class astᴛAccessorDeclarationBase {

public static void IsAccessorDeclaration(this ref AccessorDeclarationBase node) { }

internal static SubtreeFacts computeSubtreeFacts(this ref AccessorDeclarationBase node) {
    if (node.Body == nil) {
        return SubtreeContainsTypeScript;
    } else {
        return (SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)(propagateModifierListSubtreeFacts(node.modifiers) | ast.propagateSubtreeFacts(node.name)) | propagateEraseableSyntaxListSubtreeFacts(node.TypeParameters)) | propagateNodeListSubtreeFacts(node.Parameters, ast.propagateSubtreeFacts)) | propagateEraseableSyntaxSubtreeFacts(node.Type)) | propagateEraseableSyntaxSubtreeFacts(node.FullSignature)) | ast.propagateSubtreeFacts(node.Body));
    }
}

internal static SubtreeFacts propagateSubtreeFacts(this ref AccessorDeclarationBase node) {
    return (SubtreeFacts)((SubtreeFacts)(node.NodeBase.NodeDefault.SubtreeFacts() & ~((SubtreeFacts)SubtreeExclusionsAccessor)) | ast.propagateSubtreeFacts(node.name));
}

} partial class astᴛMethodDeclaration {

internal static SubtreeFacts computeSubtreeFacts(this ref MethodDeclaration node) {
    if (node.Body == nil) {
        return SubtreeContainsTypeScript;
    } else {
        var isAsync = node.modifiers != nil && (ModifierFlags)(node.modifiers.ModifierFlags & ModifierFlagsAsync) != 0;
        var isGenerator = node.AsteriskToken != nil;
        return (SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)(propagateModifierListSubtreeFacts(node.modifiers) | ast.propagateSubtreeFacts(node.AsteriskToken)) | ast.propagateSubtreeFacts(node.name)) | propagateEraseableSyntaxSubtreeFacts(node.PostfixToken)) | propagateEraseableSyntaxListSubtreeFacts(node.TypeParameters)) | propagateNodeListSubtreeFacts(node.Parameters, ast.propagateSubtreeFacts)) | ast.propagateSubtreeFacts(node.Body)) | propagateEraseableSyntaxSubtreeFacts(node.Type)) | propagateEraseableSyntaxSubtreeFacts(node.FullSignature)) | core.IfElse(isAsync && isGenerator, SubtreeContainsForAwaitOrAsyncGenerator, SubtreeFactsNone)) | core.IfElse(isAsync && !isGenerator, SubtreeContainsAnyAwait, SubtreeFactsNone));
    }
}

internal static SubtreeFacts propagateSubtreeFacts(this ref MethodDeclaration node) {
    return (SubtreeFacts)((SubtreeFacts)(node.NodeBase.NodeDefault.SubtreeFacts() & ~((SubtreeFacts)SubtreeExclusionsMethod)) | ast.propagateSubtreeFacts(node.name));
}

} partial class astᴛPropertyDeclaration {

internal static SubtreeFacts computeSubtreeFacts(this ref PropertyDeclaration node) {
    return (SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)(propagateModifierListSubtreeFacts(node.modifiers) | ast.propagateSubtreeFacts(node.name)) | propagateEraseableSyntaxSubtreeFacts(node.PostfixToken)) | propagateEraseableSyntaxSubtreeFacts(node.Type)) | ast.propagateSubtreeFacts(node.Initializer)) | SubtreeContainsClassFields);
}

internal static SubtreeFacts propagateSubtreeFacts(this ref PropertyDeclaration node) {
    return (SubtreeFacts)((SubtreeFacts)(node.NodeBase.NodeDefault.SubtreeFacts() & ~((SubtreeFacts)SubtreeExclusionsProperty)) | ast.propagateSubtreeFacts(node.name));
}

} partial class astᴛClassStaticBlockDeclaration {

internal static SubtreeFacts computeSubtreeFacts(this ref ClassStaticBlockDeclaration node) {
    return (SubtreeFacts)((SubtreeFacts)(propagateModifierListSubtreeFacts(node.modifiers) | propagateSubtreeFacts(node.Body)) | SubtreeContainsClassFields);
}

} partial class astᴛKeywordExpression {

internal static SubtreeFacts computeSubtreeFacts(this ref KeywordExpression node) {
    switch (node.Kind) {
    case var ᴛ when ᴛ == KindThisKeyword: {
        return SubtreeContainsLexicalThis;
    }
    case var ᴛ when ᴛ == KindSuperKeyword: {
        return SubtreeContainsLexicalSuper;
    }}

    return SubtreeFactsNone;
}

} partial class astᴛLiteralLikeNodeBase {

public static ж<LiteralLikeNodeBase> LiteralLikeData(this ж<LiteralLikeNodeBase> Ꮡnode) { return Ꮡnode; }

} partial class astᴛBigIntLiteral {

internal static SubtreeFacts computeSubtreeFacts(this ref BigIntLiteral node) {
    return SubtreeFactsNone;
}

} partial class astᴛIdentifier {

internal static SubtreeFacts computeSubtreeFacts(this ref Identifier node) {
    return SubtreeContainsIdentifier;
}

} partial class astᴛNoSubstitutionTemplateLiteral {

internal static SubtreeFacts computeSubtreeFacts(this ref NoSubstitutionTemplateLiteral node) {
    if ((TokenFlags)(node.TemplateFlags & TokenFlagsContainsInvalidEscape) != 0) {
        return SubtreeContainsInvalidTemplateEscape;
    }
    return SubtreeFactsNone;
}

} partial class astᴛBinaryExpression {

internal static SubtreeFacts computeSubtreeFacts(this ref BinaryExpression node) {
    var facts = (SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)(propagateModifierListSubtreeFacts(node.modifiers) | propagateSubtreeFacts(node.Left)) | propagateSubtreeFacts(node.Type)) | propagateSubtreeFacts(node.OperatorToken)) | propagateSubtreeFacts(node.Right)) | core.IfElse(node.OperatorToken.Kind == KindInKeyword && IsPrivateIdentifier(node.Left), (SubtreeFacts)(SubtreeContainsClassFields | SubtreeContainsPrivateIdentifierInExpression), SubtreeFactsNone));
    if (node.OperatorToken.Kind == KindEqualsToken) {
        if ((IsObjectLiteralExpression(node.Left) || IsArrayLiteralExpression(node.Left)) && ContainsObjectRestOrSpread(node.Left)) {
            facts |= (SubtreeFacts)(SubtreeContainsObjectRestOrSpread);
        }
    }
    return facts;
}

internal static void setModifiers(this ref BinaryExpression node, ж<ModifierList> Ꮡmodifiers) {
    ref var modifiers = ref Ꮡmodifiers.DerefOrNull();

    node.modifiers = Ꮡmodifiers;
}

} partial class astᴛYieldExpression {

internal static SubtreeFacts computeSubtreeFacts(this ref YieldExpression node) {
    return (SubtreeFacts)(propagateSubtreeFacts(node.Expression) | SubtreeContainsForAwaitOrAsyncGenerator);
}

} partial class astᴛArrowFunction {

internal static SubtreeFacts computeSubtreeFacts(this ref ArrowFunction node) {
    return (SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)(propagateModifierListSubtreeFacts(node.modifiers) | propagateEraseableSyntaxListSubtreeFacts(node.TypeParameters)) | propagateNodeListSubtreeFacts(node.Parameters, ast.propagateSubtreeFacts)) | propagateEraseableSyntaxSubtreeFacts(node.Type)) | propagateEraseableSyntaxSubtreeFacts(node.FullSignature)) | ast.propagateSubtreeFacts(node.Body)) | core.IfElse((ModifierFlags)(node.ExpressionBase.NodeBase.NodeDefault.Node.ModifierFlags() & ModifierFlagsAsync) != 0, SubtreeContainsAnyAwait, SubtreeFactsNone));
}

internal static SubtreeFacts propagateSubtreeFacts(this ref ArrowFunction node) {
    return (SubtreeFacts)(node.ExpressionBase.NodeBase.NodeDefault.SubtreeFacts() & ~((SubtreeFacts)SubtreeExclusionsArrowFunction));
}

} partial class astᴛFunctionExpression {

internal static SubtreeFacts computeSubtreeFacts(this ref FunctionExpression node) {
    var isAsync = node.modifiers != nil && (ModifierFlags)(node.modifiers.ModifierFlags & ModifierFlagsAsync) != 0;
    var isGenerator = node.AsteriskToken != nil;
    return (SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)(propagateModifierListSubtreeFacts(node.modifiers) | ast.propagateSubtreeFacts(node.AsteriskToken)) | ast.propagateSubtreeFacts(node.name)) | propagateEraseableSyntaxListSubtreeFacts(node.TypeParameters)) | propagateNodeListSubtreeFacts(node.Parameters, ast.propagateSubtreeFacts)) | propagateEraseableSyntaxSubtreeFacts(node.Type)) | propagateEraseableSyntaxSubtreeFacts(node.FullSignature)) | ast.propagateSubtreeFacts(node.Body)) | core.IfElse(isAsync && isGenerator, SubtreeContainsForAwaitOrAsyncGenerator, SubtreeFactsNone)) | core.IfElse(isAsync && !isGenerator, SubtreeContainsAnyAwait, SubtreeFactsNone));
}

internal static SubtreeFacts propagateSubtreeFacts(this ref FunctionExpression node) {
    return (SubtreeFacts)(node.PrimaryExpressionBase.MemberExpressionBase.LeftHandSideExpressionBase.UpdateExpressionBase.UnaryExpressionBase.ExpressionBase.NodeBase.NodeDefault.SubtreeFacts() & ~((SubtreeFacts)SubtreeExclusionsFunction));
}

} partial class astᴛAsExpression {

internal static SubtreeFacts computeSubtreeFacts(this ref AsExpression node) {
    return (SubtreeFacts)(ast.propagateSubtreeFacts(node.Expression) | SubtreeContainsTypeScript);
}

internal static SubtreeFacts propagateSubtreeFacts(this ref AsExpression node) {
    return (SubtreeFacts)(node.ExpressionBase.NodeBase.NodeDefault.SubtreeFacts() & ~((SubtreeFacts)SubtreeExclusionsOuterExpression));
}

} partial class astᴛSatisfiesExpression {

internal static SubtreeFacts computeSubtreeFacts(this ref SatisfiesExpression node) {
    return (SubtreeFacts)(ast.propagateSubtreeFacts(node.Expression) | SubtreeContainsTypeScript);
}

internal static SubtreeFacts propagateSubtreeFacts(this ref SatisfiesExpression node) {
    return (SubtreeFacts)(node.ExpressionBase.NodeBase.NodeDefault.SubtreeFacts() & ~((SubtreeFacts)SubtreeExclusionsOuterExpression));
}

} partial class astᴛPropertyAccessExpression {

internal static SubtreeFacts computeSubtreeFacts(this ref PropertyAccessExpression node) {
    var privateName = SubtreeFactsNone;
    if (!IsIdentifier(node.name)) {
        privateName = SubtreeContainsPrivateIdentifierInExpression;
    }
    return (SubtreeFacts)((SubtreeFacts)((SubtreeFacts)(ast.propagateSubtreeFacts(node.Expression) | ast.propagateSubtreeFacts(node.QuestionDotToken)) | ast.propagateSubtreeFacts(node.name)) | privateName);
}

internal static SubtreeFacts propagateSubtreeFacts(this ref PropertyAccessExpression node) {
    return (SubtreeFacts)(node.MemberExpressionBase.LeftHandSideExpressionBase.UpdateExpressionBase.UnaryExpressionBase.ExpressionBase.NodeBase.NodeDefault.SubtreeFacts() & ~((SubtreeFacts)SubtreeExclusionsPropertyAccess));
}

} partial class astᴛElementAccessExpression {

internal static SubtreeFacts propagateSubtreeFacts(this ref ElementAccessExpression node) {
    return (SubtreeFacts)(node.MemberExpressionBase.LeftHandSideExpressionBase.UpdateExpressionBase.UnaryExpressionBase.ExpressionBase.NodeBase.NodeDefault.SubtreeFacts() & ~((SubtreeFacts)SubtreeExclusionsElementAccess));
}

} partial class astᴛCallExpression {

internal static SubtreeFacts computeSubtreeFacts(this ref CallExpression node) {
    return (SubtreeFacts)((SubtreeFacts)((SubtreeFacts)((SubtreeFacts)(ast.propagateSubtreeFacts(node.Expression) | ast.propagateSubtreeFacts(node.QuestionDotToken)) | propagateEraseableSyntaxListSubtreeFacts(node.TypeArguments)) | propagateNodeListSubtreeFacts(node.Arguments, ast.propagateSubtreeFacts)) | core.IfElse(node.Expression.Kind == KindImportKeyword, SubtreeContainsDynamicImport, SubtreeFactsNone));
}

internal static SubtreeFacts propagateSubtreeFacts(this ref CallExpression node) {
    return (SubtreeFacts)(node.LeftHandSideExpressionBase.UpdateExpressionBase.UnaryExpressionBase.ExpressionBase.NodeBase.NodeDefault.SubtreeFacts() & ~((SubtreeFacts)SubtreeExclusionsCall));
}

} partial class astᴛNewExpression {

internal static SubtreeFacts computeSubtreeFacts(this ref NewExpression node) {
    return (SubtreeFacts)((SubtreeFacts)(ast.propagateSubtreeFacts(node.Expression) | propagateEraseableSyntaxListSubtreeFacts(node.TypeArguments)) | propagateNodeListSubtreeFacts(node.Arguments, ast.propagateSubtreeFacts));
}

internal static SubtreeFacts propagateSubtreeFacts(this ref NewExpression node) {
    return (SubtreeFacts)(node.PrimaryExpressionBase.MemberExpressionBase.LeftHandSideExpressionBase.UpdateExpressionBase.UnaryExpressionBase.ExpressionBase.NodeBase.NodeDefault.SubtreeFacts() & ~((SubtreeFacts)SubtreeExclusionsNew));
}

} partial class astᴛMetaProperty {

internal static SubtreeFacts computeSubtreeFacts(this ref MetaProperty node) {
    return (SubtreeFacts)(propagateSubtreeFacts(node.name) & ~SubtreeContainsIdentifier);
}

} partial class astᴛNonNullExpression {

internal static SubtreeFacts computeSubtreeFacts(this ref NonNullExpression node) {
    return (SubtreeFacts)(propagateSubtreeFacts(node.Expression) | SubtreeContainsTypeScript);
}

} partial class astᴛSpreadElement {

internal static SubtreeFacts computeSubtreeFacts(this ref SpreadElement node) {
    return (SubtreeFacts)(propagateSubtreeFacts(node.Expression) | SubtreeContainsRestOrSpread);
}

} partial class astᴛTaggedTemplateExpression {

internal static SubtreeFacts computeSubtreeFacts(this ref TaggedTemplateExpression node) {
    return (SubtreeFacts)((SubtreeFacts)((SubtreeFacts)(propagateSubtreeFacts(node.Tag) | propagateSubtreeFacts(node.QuestionDotToken)) | propagateEraseableSyntaxListSubtreeFacts(node.TypeArguments)) | propagateSubtreeFacts(node.Template));
}

} partial class astᴛArrayLiteralExpression {

internal static SubtreeFacts propagateSubtreeFacts(this ref ArrayLiteralExpression node) {
    return (SubtreeFacts)(node.PrimaryExpressionBase.MemberExpressionBase.LeftHandSideExpressionBase.UpdateExpressionBase.UnaryExpressionBase.ExpressionBase.NodeBase.NodeDefault.SubtreeFacts() & ~((SubtreeFacts)SubtreeExclusionsArrayLiteral));
}

} partial class astᴛObjectLiteralExpression {

internal static SubtreeFacts propagateSubtreeFacts(this ref ObjectLiteralExpression node) {
    return (SubtreeFacts)(node.PrimaryExpressionBase.MemberExpressionBase.LeftHandSideExpressionBase.UpdateExpressionBase.UnaryExpressionBase.ExpressionBase.NodeBase.NodeDefault.SubtreeFacts() & ~((SubtreeFacts)SubtreeExclusionsObjectLiteral));
}

} partial class astᴛSpreadAssignment {

internal static SubtreeFacts computeSubtreeFacts(this ref SpreadAssignment node) {
    return (SubtreeFacts)((SubtreeFacts)(propagateSubtreeFacts(node.Expression) | SubtreeContainsESObjectRestOrSpread) | SubtreeContainsObjectRestOrSpread);
}

} partial class astᴛPropertyAssignment {

internal static SubtreeFacts computeSubtreeFacts(this ref PropertyAssignment node) {
    return (SubtreeFacts)((SubtreeFacts)(propagateSubtreeFacts(node.name) | propagateSubtreeFacts(node.Type)) | propagateSubtreeFacts(node.Initializer));
}

} partial class astᴛShorthandPropertyAssignment {

internal static SubtreeFacts computeSubtreeFacts(this ref ShorthandPropertyAssignment node) {
    return (SubtreeFacts)((SubtreeFacts)((SubtreeFacts)(propagateSubtreeFacts(node.name) | propagateSubtreeFacts(node.Type)) | propagateSubtreeFacts(node.ObjectAssignmentInitializer)) | SubtreeContainsTypeScript);
}

} partial class astᴛAwaitExpression {

internal static SubtreeFacts computeSubtreeFacts(this ref AwaitExpression node) {
    return (SubtreeFacts)((SubtreeFacts)((SubtreeFacts)(propagateSubtreeFacts(node.Expression) | SubtreeContainsAwait) | SubtreeContainsAnyAwait) | SubtreeContainsForAwaitOrAsyncGenerator);
}

} partial class astᴛTypeAssertion {

internal static SubtreeFacts computeSubtreeFacts(this ref TypeAssertion node) {
    return (SubtreeFacts)(ast.propagateSubtreeFacts(node.Expression) | SubtreeContainsTypeScript);
}

internal static SubtreeFacts propagateSubtreeFacts(this ref TypeAssertion node) {
    return (SubtreeFacts)(node.UnaryExpressionBase.ExpressionBase.NodeBase.NodeDefault.SubtreeFacts() & ~((SubtreeFacts)SubtreeExclusionsOuterExpression));
}

} partial class astᴛExpressionWithTypeArguments {

internal static SubtreeFacts computeSubtreeFacts(this ref ExpressionWithTypeArguments node) {
    return (SubtreeFacts)(propagateSubtreeFacts(node.Expression) | propagateEraseableSyntaxListSubtreeFacts(node.TypeArguments));
}

} partial class astᴛNode {

public static (coreꓸResolutionMode, bool) GetResolutionModeOverride(this ж<ImportAttributesNode> Ꮡnode, Funcꓸꓸꓸ<ж<Node>, ж<diagnostics.Message>, any, bool> grammarErrorOnNode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (Ꮡnode == nil) {
        return (core.ResolutionModeNone, false);
    }

    var attributes = node.AsImportAttributes().Value.Attributes;

    var attribute = core.Find(attributes.Nodes, (ж<Node> attributeΔ1) => attributeΔ1.Name().Text() == "resolution-mode"u8);
    if (attribute == nil) {
        return (core.ResolutionModeNone, false);
    }

    var elem = attribute.AsImportAttribute();
    if (!IsStringLiteralLike((~elem).Value)) {
        return (core.ResolutionModeNone, false);
    }
    if ((~elem).Value.Text() != "import"u8 && (~elem).Value.Text() != "require"u8) {
        if (grammarErrorOnNode != nil) {
            grammarErrorOnNode((~elem).Value, diagnostics.X_resolution_mode_should_be_either_require_or_import);
        }
        return (core.ResolutionModeNone, false);
    }
    if ((~elem).Value.Text() == "import"u8) {
        return (core.ResolutionModeESM, true);
    } else {
        return (core.ModuleKindCommonJS, true);
    }
}

} partial class astᴛTemplateLiteralLikeNodeBase {

public static ж<LiteralLikeNodeBase> LiteralLikeData(this ж<TemplateLiteralLikeNodeBase> Ꮡnode) {
    return Ꮡnode.ᏑLiteralLikeNodeBase;
}

public static ж<TemplateLiteralLikeNodeBase> TemplateLiteralLikeData(this ж<TemplateLiteralLikeNodeBase> Ꮡnode) {
    return Ꮡnode;
}

} partial class astᴛTemplateHead {

internal static SubtreeFacts computeSubtreeFacts(this ref TemplateHead node) {
    if ((TokenFlags)(node.TemplateFlags & TokenFlagsContainsInvalidEscape) != 0) {
        return SubtreeContainsInvalidTemplateEscape;
    }
    return SubtreeFactsNone;
}

} partial class astᴛTemplateMiddle {

internal static SubtreeFacts computeSubtreeFacts(this ref TemplateMiddle node) {
    if ((TokenFlags)(node.TemplateFlags & TokenFlagsContainsInvalidEscape) != 0) {
        return SubtreeContainsInvalidTemplateEscape;
    }
    return SubtreeFactsNone;
}

} partial class astᴛTemplateTail {

internal static SubtreeFacts computeSubtreeFacts(this ref TemplateTail node) {
    if ((TokenFlags)(node.TemplateFlags & TokenFlagsContainsInvalidEscape) != 0) {
        return SubtreeContainsInvalidTemplateEscape;
    }
    return SubtreeFactsNone;
}

} partial class astᴛJsxElement {

internal static SubtreeFacts computeSubtreeFacts(this ref JsxElement node) {
    return (SubtreeFacts)((SubtreeFacts)((SubtreeFacts)(propagateSubtreeFacts(node.OpeningElement) | propagateNodeListSubtreeFacts(node.Children, propagateSubtreeFacts)) | propagateSubtreeFacts(node.ClosingElement)) | SubtreeContainsJsx);
}

} partial class astᴛJsxAttributes {

internal static SubtreeFacts computeSubtreeFacts(this ref JsxAttributes node) {
    return (SubtreeFacts)(propagateNodeListSubtreeFacts(node.Properties, propagateSubtreeFacts) | SubtreeContainsJsx);
}

} partial class astᴛJsxNamespacedName {

internal static SubtreeFacts computeSubtreeFacts(this ref JsxNamespacedName node) {
    return (SubtreeFacts)((SubtreeFacts)(propagateSubtreeFacts(node.Namespace) | propagateSubtreeFacts(node.name)) | SubtreeContainsJsx);
}

} partial class astᴛJsxOpeningElement {

internal static SubtreeFacts computeSubtreeFacts(this ref JsxOpeningElement node) {
    return (SubtreeFacts)((SubtreeFacts)((SubtreeFacts)(propagateSubtreeFacts(node.TagName) | propagateEraseableSyntaxListSubtreeFacts(node.TypeArguments)) | propagateSubtreeFacts(node.Attributes)) | SubtreeContainsJsx);
}

} partial class astᴛJsxSelfClosingElement {

internal static SubtreeFacts computeSubtreeFacts(this ref JsxSelfClosingElement node) {
    return (SubtreeFacts)((SubtreeFacts)((SubtreeFacts)(propagateSubtreeFacts(node.TagName) | propagateEraseableSyntaxListSubtreeFacts(node.TypeArguments)) | propagateSubtreeFacts(node.Attributes)) | SubtreeContainsJsx);
}

} partial class astᴛJsxFragment {

internal static SubtreeFacts computeSubtreeFacts(this ref JsxFragment node) {
    return (SubtreeFacts)(propagateNodeListSubtreeFacts(node.Children, propagateSubtreeFacts) | SubtreeContainsJsx);
}

} partial class astᴛJsxOpeningFragment {

internal static SubtreeFacts computeSubtreeFacts(this ref JsxOpeningFragment node) {
    return SubtreeContainsJsx;
}

} partial class astᴛJsxClosingFragment {

internal static SubtreeFacts computeSubtreeFacts(this ref JsxClosingFragment node) {
    return SubtreeContainsJsx;
}

} partial class astᴛJsxAttribute {

internal static SubtreeFacts computeSubtreeFacts(this ref JsxAttribute node) {
    return (SubtreeFacts)((SubtreeFacts)(propagateSubtreeFacts(node.name) | propagateSubtreeFacts(node.Initializer)) | SubtreeContainsJsx);
}

} partial class astᴛJsxSpreadAttribute {

internal static SubtreeFacts computeSubtreeFacts(this ref JsxSpreadAttribute node) {
    return (SubtreeFacts)(propagateSubtreeFacts(node.Expression) | SubtreeContainsJsx);
}

} partial class astᴛJsxClosingElement {

internal static SubtreeFacts computeSubtreeFacts(this ref JsxClosingElement node) {
    return (SubtreeFacts)(propagateSubtreeFacts(node.TagName) | SubtreeContainsJsx);
}

} partial class astᴛJsxExpression {

internal static SubtreeFacts computeSubtreeFacts(this ref JsxExpression node) {
    return (SubtreeFacts)(propagateSubtreeFacts(node.Expression) | SubtreeContainsJsx);
}

} partial class astᴛJsxText {

internal static SubtreeFacts computeSubtreeFacts(this ref JsxText node) {
    return SubtreeContainsJsx;
}

} partial class astᴛNode {

public static bool IsJSDoc(this ref Node node) {
    return node.Kind == KindJSDoc;
}

} partial class ast_package {

partial struct PatternAmbientModule {
    public core.Pattern Pattern;
    public ж<Symbol> Symbol;
}

partial struct CommentDirectiveKind /*num:int32*/;

public static CommentDirectiveKind CommentDirectiveKindUnknown => /* iota */ 0;
public static CommentDirectiveKind CommentDirectiveKindExpectError => 1;
public static CommentDirectiveKind CommentDirectiveKindIgnore => 2;

partial struct CommentDirective {
    public core.TextRange Loc;
    public CommentDirectiveKind Kind;
}

partial struct SourceFileMetaData {
    public @string PackageJsonType;
    public @string PackageJsonDirectory;
    public coreꓸResolutionMode ImpliedNodeFormat;
}

partial struct SourceFileDataKey<T> {
    internal sourceFileDataKey key;
    internal array<T> _ = new(0);
}

partial struct sourceFileDataKey /*num:uint64*/;

internal static readonly ж<atomic.Uint64> ᏑsourceFileDataKeyCounter = new StaticBox<atomic.Uint64>(default(atomic.Uint64));
internal static ref atomic.Uint64 sourceFileDataKeyCounter => ref ((StaticBox<atomic.Uint64>)ᏑsourceFileDataKeyCounter).ValueSlot;

partial struct sourceFileDataCell<T> {
    internal sync.Once once;
    internal T value;
}

public static ж<SourceFileDataKey<T>> NewSourceFileDataKey<T>() {
    return Ꮡ(new SourceFileDataKey<T>(key: ((sourceFileDataKey)ᏑsourceFileDataKeyCounter.Add(1))));
}

public static T GetOrComputeSourceFileData<T>(ж<SourceFile> @file, ж<SourceFileDataKey<T>> key, Func<ж<SourceFile>, T> compute) {
    var cell = getSourceFileDataCell(@file, key);
    var cellʗ1 = cell;
    cell.Ꮡonce.Do(() => {
        cellʗ1.value = compute(@file);
    });
    return cell.value;
}

internal static ж<sourceFileDataCell<T>> getSourceFileDataCell<T>(ж<SourceFile> Ꮡfile, ж<SourceFileDataKey<T>> Ꮡkey) {
    GoFrame ᒐ = default;
    try {
        ref var @file = ref Ꮡfile.DerefOrNull();
        ref var key = ref Ꮡkey.DerefOrNull();

        if (Ꮡkey == nil || key.key == 0) {
            throw panic("invalid SourceFileDataKey; use NewSourceFileDataKey");
        }

        @file.dataMu.Lock();
        defer(Ꮡfile.ᏑdataMu.Unlock, ref ᒐ);

        if (@file.data == nil) {
            @file.data = new map<sourceFileDataKey, any>();
        }
        var (cellΔ1, ok) = @file.data[key.key, ꟷ]; if (ok) {
            return cellΔ1._<ж<sourceFileDataCell<T>>>();
        }
        var cell = Ꮡ(new sourceFileDataCell<T>());
        @file.data[key.key] = cell.OrTypedNil();
        return cell;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { ᒐ.Run(); }
}

partial struct CheckJsDirective {
    public bool Enabled;
    public CommentRange Range;
}

partial interface HasFileName {
    @string FileName();
    tspath.Path Path();
}

partial struct TokenCacheKey {
    internal ж<Node> parent;
    internal core.TextRange loc;
}

partial struct SourceFile {
    /*embed*/ public NodeBase NodeBase;
    /*embed*/ public DeclarationBase DeclarationBase;
    /*embed*/ public LocalsContainerBase LocalsContainerBase;
    /*embed*/ public CompositeBase CompositeBase;
    internal @string fileName;
    internal SourceFileParseOptions parseOptions;
    internal @string text;
    internal ж<ContentMapperSourceFileInfo> contentMapperInfo;
    public ж<NodeList> Statements;
    public ж<TokenNode> EndOfFileToken;
    internal sync.Mutex dataMu;
    internal map<sourceFileDataKey, any> data;
    internal slice<ж<Diagnostic>> diagnostics;
    internal slice<ж<Diagnostic>> jsDiagnostics;
    internal slice<ж<Diagnostic>> jsdocDiagnostics;
    public core.LanguageVariant LanguageVariant;
    public core.ScriptKind ScriptKind;
    public bool IsDeclarationFile;
    public core.Tristate UsesUriStyleNodeCoreModules;
    public nint IdentifierCount;
    internal slice<ж<LiteralLikeNode>> imports;
    public slice<ж<ModuleName>> ModuleAugmentations;
    public slice<@string> AmbientModuleNames;
    public slice<CommentDirective> CommentDirectives;
    internal map<ж<Node>, slice<ж<Node>>> jsdocCache;
    internal sync.RWMutex jsdocMu;
    internal bool hasLazyJSDoc;
    internal sync.Once identifiersOnce;
    internal collections.Set<@string> identifiers;
    public slice<ж<Node>> ReparsedClones;
    public slice<Pragma> Pragmas;
    public slice<ж<FileReference>> ReferencedFiles;
    public slice<ж<FileReference>> TypeReferenceDirectives;
    public slice<ж<FileReference>> LibReferenceDirectives;
    public ж<CheckJsDirective> CheckJsDirective;
    public nint NodeCount;
    public nint TextCount;
    public ж<Node> CommonJSModuleIndicator;
    public ж<Node> ExternalModuleIndicator;
    internal atomic.Bool isBound;
    internal sync.Once bindOnce;
    internal slice<ж<Diagnostic>> bindDiagnostics;
    public nint SymbolCount;
    public slice<ж<PatternAmbientModule>> PatternAmbientModules;
    public SymbolTable GlobalExports;
    internal sync.RWMutex ecmaLineMapMu;
    internal slice<core.TextPos> ecmaLineMap;
    public xxh3.Uint128 Hash;
    internal sync.Mutex tokenCacheMu;
    internal map<TokenCacheKey, ж<Node>> tokenCache;
    internal ж<NodeFactory> tokenFactory;
    internal sync.Mutex declarationMapMu;
    internal map<@string, slice<ж<Node>>> declarationMap;
    internal sync.Once nameTableOnce;
    internal map<@string, nint> nameTable;
    internal sync.Once positionMapOnce;
    internal ж<PositionMap> positionMap;
}

} partial class astᴛNodeFactory {

public static ж<Node> NewSourceFile(this ref NodeFactory f, SourceFileParseOptions opts, @string text, ж<NodeList> statements, ж<TokenNode> endOfFileToken) {
    if (tspath.GetEncodedRootLength(opts.FileName) == 0 || opts.FileName != tspath.NormalizePath(opts.FileName)) {
        throw panic(fmt.Sprintf("fileName should be normalized and absolute: %q"u8, opts.FileName));
    }
    var data = Ꮡ(new SourceFile());
    data.fileName = opts.FileName;
    data.parseOptions = opts;
    data.text = text;
    data.Value.Statements = statements;
    data.EndOfFileToken = endOfFileToken;
    return f.newNode(KindSourceFile, new SourceFileжnodeData(data));
}

} partial class astᴛSourceFile {

public static SourceFileParseOptions ParseOptions(this ref SourceFile node) {
    return node.parseOptions;
}

public static @string Text(this ref SourceFile node) {
    return node.text;
}

public static @string OriginalText(this ref SourceFile node) {
    if (node.ContentMapper() != ""u8) {
        return node.contentMapperInfo.OriginalText;
    }
    return node.text;
}

public static @string OriginalFileName(this ref SourceFile node) {
    var canonical = node.CanonicalSourceFile(); if (canonical != nil) {
        return canonical.FileName();
    }
    return node.FileName();
}

public static ж<spanmap.SpanMap> SpanMap(this ref SourceFile node) {
    if (node.contentMapperInfo == nil) {
        return default;
    }
    return node.contentMapperInfo.SpanMap;
}

public static @string ContentMapper(this ref SourceFile node) {
    if (node.contentMapperInfo == nil) {
        return "";
    }
    return node.contentMapperInfo.ContentMapper;
}

public static bool IsContentMapperFailureStub(this ref SourceFile node) {
    return node.ContentMapper() != ""u8 && node.SpanMap() == nil;
}

public static @string ContentMapperTransformIdentity(this ref SourceFile node) {
    if (node.contentMapperInfo == nil) {
        return "";
    }
    return node.contentMapperInfo.TransformIdentity;
}

public static @string VirtualFileName(this ref SourceFile node) {
    if (node.contentMapperInfo == nil) {
        return "";
    }
    return node.contentMapperInfo.VirtualFileName;
}

} partial class ast_package {

partial struct MappedDiagnosticDirectivePolicy /*num:uint8*/;

public static MappedDiagnosticDirectivePolicy MappedDiagnosticDirectivePolicyIgnore => /* iota */ 0;
public static MappedDiagnosticDirectivePolicy MappedDiagnosticDirectivePolicyExpect => 1;

partial struct MappedDiagnosticDirective {
    public core.TextRange OriginalRange;
    public core.TextRange VirtualRange;
    public MappedDiagnosticDirectivePolicy Policy;
    public int32 UnusedCode;
    public @string UnusedMessageText;
    public @string Source;
}

partial struct ContentMapperSourceFileInfo {
    public @string ContentMapper;
    public @string TransformIdentity;
    public SourceFileParseOptions ParseOptions;
    public @string VirtualFileName;
    public @string OriginalText;
    public ж<spanmap.SpanMap> SpanMap;
    public slice<MappedDiagnosticDirective> DiagnosticDirectives;
    public slice<ж<SourceFile>> SupplementalSourceFiles;
    public ж<SourceFile> CanonicalSourceFile;
}

} partial class astᴛSourceFile {

public static SourceFileParseOptions ContentMapperParseOptions(this ref SourceFile node) {
    if (node.contentMapperInfo == nil) {
        return new SourceFileParseOptions();
    }
    return node.contentMapperInfo.ParseOptions;
}

public static void SetContentMapperInfo(this ref SourceFile node, ContentMapperSourceFileInfo infoʗp) {
    ref var info = ref heap(infoʗp, out var Ꮡinfo);

    if (node.contentMapperInfo != nil) {
        throw panic("content mapper source file info already set");
    }
    node.contentMapperInfo = Ꮡinfo;
}

public static slice<MappedDiagnosticDirective> DiagnosticDirectives(this ref SourceFile node) {
    if (node.contentMapperInfo == nil) {
        return default;
    }
    return node.contentMapperInfo.DiagnosticDirectives;
}

public static slice<ж<SourceFile>> SupplementalSourceFiles(this ref SourceFile node) {
    if (node.contentMapperInfo == nil) {
        return default;
    }
    return node.contentMapperInfo.SupplementalSourceFiles;
}

public static ж<SourceFile> CanonicalSourceFile(this ref SourceFile node) {
    if (node.contentMapperInfo == nil) {
        return default;
    }
    return node.contentMapperInfo.CanonicalSourceFile;
}

public static bool IsContentMapperSupplemental(this ref SourceFile node) {
    return node.CanonicalSourceFile() != nil;
}

public static bool HasIdentifier(this ж<SourceFile> Ꮡfile, @string name) {
    Ꮡfile.ᏑidentifiersOnce.Do(() => {
        Ꮡfile.Value.identifiers = collectIdentifiersForSourceFile(Ꮡfile);
    });
    return Ꮡfile.Ꮡidentifiers.Has(name);
}

} partial class ast_package {

internal static collections.Set<@string> collectIdentifiersForSourceFile(ж<SourceFile> sourceFile) {
    collections.Set<@string> identifiers = default;
    ref var collect = ref heap<Func<ж<Node>, bool>>(out var Ꮡcollect);
    collect = (ж<Node> node) => {
        switch (node.Kind) {
        case var ᴛ when ᴛ == KindIdentifier || ᴛ == KindPrivateIdentifier || ᴛ == KindStringLiteral || ᴛ == KindNumericLiteral || ᴛ == KindBigIntLiteral || ᴛ == KindNoSubstitutionTemplateLiteral: {
            identifiers.Add(node.Text());
            break;
        }}

        node.ForEachChild(Ꮡcollect.ValueSlot);
        return false;
    };
    collect(sourceFile.of(SourceFile.ᏑNodeBase).of(NodeBase.ᏑNodeDefault).AsNode());
    return identifiers;
}

} partial class astᴛSourceFile {

public static @string FileName(this ref SourceFile node) {
    return node.parseOptions.FileName;
}

public static tspath.Path Path(this ref SourceFile node) {
    return node.parseOptions.Path;
}

public static slice<ж<LiteralLikeNode>> Imports(this ref SourceFile node) {
    return node.imports;
}

public static slice<ж<Diagnostic>> Diagnostics(this ref SourceFile node) {
    return node.diagnostics;
}

public static void SetDiagnostics(this ref SourceFile node, slice<ж<Diagnostic>> diags) {
    node.diagnostics = diags;
}

public static slice<ж<Diagnostic>> JSDiagnostics(this ref SourceFile node) {
    return node.jsDiagnostics;
}

public static void SetJSDiagnostics(this ref SourceFile node, slice<ж<Diagnostic>> diags) {
    node.jsDiagnostics = diags;
}

public static slice<ж<Diagnostic>> JSDocDiagnostics(this ref SourceFile node) {
    return node.jsdocDiagnostics;
}

public static void SetJSDocDiagnostics(this ref SourceFile node, slice<ж<Diagnostic>> diags) {
    node.jsdocDiagnostics = diags;
}

public static void SetJSDocCache(this ref SourceFile node, map<ж<Node>, slice<ж<Node>>> cache) {
    node.jsdocCache = cache;
}

public static void SetHasLazyJSDoc(this ref SourceFile node, bool lazy) {
    node.hasLazyJSDoc = lazy;
}

internal static slice<ж<Node>> resolveJSDoc(this ж<SourceFile> Ꮡnode, ж<Node> n) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var node = ref Ꮡnode.DerefOrNull();

        if (parseJSDocForNode == nil) {
            throw panic("resolveJSDoc called but parseJSDocForNode is not registered; ensure the parser package is imported");
        }
        Ꮡnode.ᏑjsdocMu.RLock();
        if (node.jsdocCache[n, ꟷ] is (var jsdocsΔ1, true)) {
            Ꮡnode.ᏑjsdocMu.RUnlock();
            return jsdocsΔ1;
        }
        Ꮡnode.ᏑjsdocMu.RUnlock();

        Ꮡnode.ᏑjsdocMu.Lock();
        ᒐd1 = true;
        if (node.jsdocCache[n, ꟷ] is (var jsdocsΔ2, true)) {
            return jsdocsΔ2;
        }
        var jsdocs = parseJSDocForNode(Ꮡnode, n);
        if (node.jsdocCache == nil) {
            node.jsdocCache = new map<ж<Node>, slice<ж<Node>>>();
        }
        node.jsdocCache[n] = jsdocs;
        return jsdocs;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡnode.ᏑjsdocMu.Unlock(); ᒐ.Run(); }
}

public static slice<ж<Diagnostic>> BindDiagnostics(this ref SourceFile node) {
    return node.bindDiagnostics;
}

public static void SetBindDiagnostics(this ref SourceFile node, slice<ж<Diagnostic>> diags) {
    node.bindDiagnostics = diags;
}

public static bool ForEachChild(this ref SourceFile node, Func<ж<Node>, bool> v) {
    return visitNodeList(v, node.Statements) || visit(v, node.EndOfFileToken);
}

public static ж<Node> VisitEachChild(this ж<SourceFile> Ꮡnode, ж<NodeVisitor> Ꮡv) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var v = ref Ꮡv.DerefOrNull();

    return v.Factory.UpdateSourceFile(Ꮡnode, Ꮡv.visitTopLevelStatements(node.Statements), Ꮡv.visitToken(node.EndOfFileToken));
}

public static bool IsJS(this ж<SourceFile> Ꮡnode) {
    return IsSourceFileJS(Ꮡnode);
}

internal static void copyFrom(this ref SourceFile node, ж<SourceFile> Ꮡother) {
    ref var other = ref Ꮡother.DerefOrNull();

    if (other.contentMapperInfo != nil) {
        node.SetContentMapperInfo(other.contentMapperInfo.Value);
    }
    node.LanguageVariant = other.LanguageVariant;
    node.ScriptKind = other.ScriptKind;
    node.IsDeclarationFile = other.IsDeclarationFile;
    node.UsesUriStyleNodeCoreModules = other.UsesUriStyleNodeCoreModules;
    node.imports = other.imports;
    node.ModuleAugmentations = other.ModuleAugmentations;
    node.AmbientModuleNames = other.AmbientModuleNames;
    node.CommentDirectives = other.CommentDirectives;
    node.Pragmas = other.Pragmas;
    node.ReferencedFiles = other.ReferencedFiles;
    node.TypeReferenceDirectives = other.TypeReferenceDirectives;
    node.LibReferenceDirectives = other.LibReferenceDirectives;
    node.CommonJSModuleIndicator = other.CommonJSModuleIndicator;
    node.ExternalModuleIndicator = other.ExternalModuleIndicator;
    node.Flags |= (NodeFlags)(other.Flags);
}

public static ж<Node> Clone(this ж<SourceFile> Ꮡnode, NodeFactoryCoercible f) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var updated = f.AsNodeFactory().NewSourceFile(node.parseOptions, node.text, node.Statements, node.EndOfFileToken);
    var newFile = updated.AsSourceFile();
    newFile.copyFrom(Ꮡnode);
    return cloneNode(updated, Ꮡnode.of(SourceFile.ᏑNodeBase).of(NodeBase.ᏑNodeDefault).AsNode(), f.AsNodeFactory().hooks);
}

internal static SubtreeFacts computeSubtreeFacts(this ref SourceFile node) {
    return propagateNodeListSubtreeFacts(node.Statements, propagateSubtreeFacts);
}

} partial class astᴛNodeFactory {

public static ж<Node> UpdateSourceFile(this ref NodeFactory f, ж<SourceFile> Ꮡnode, ж<StatementList> statements, ж<TokenNode> endOfFileToken) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (statements != node.Statements || endOfFileToken != node.EndOfFileToken) {
        var updated = f.NewSourceFile(node.parseOptions, node.text, statements, endOfFileToken).AsSourceFile();
        updated.copyFrom(Ꮡnode);
        return updateNode(updated.of(SourceFile.ᏑNodeBase).of(NodeBase.ᏑNodeDefault).AsNode(), Ꮡnode.of(SourceFile.ᏑNodeBase).of(NodeBase.ᏑNodeDefault).AsNode(), f.hooks);
    }
    return Ꮡnode.of(SourceFile.ᏑNodeBase).of(NodeBase.ᏑNodeDefault).AsNode();
}

} partial class astᴛSourceFile {

public static slice<core.TextPos> ECMALineMap(this ж<SourceFile> Ꮡnode) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var node = ref Ꮡnode.DerefOrNull();

        Ꮡnode.ᏑecmaLineMapMu.RLock();
        var lineMap = node.ecmaLineMap;
        Ꮡnode.ᏑecmaLineMapMu.RUnlock();
        if (lineMap == nil) {
            Ꮡnode.ᏑecmaLineMapMu.Lock();
            ᒐd1 = true;
            lineMap = node.ecmaLineMap;
            if (lineMap == nil) {
                lineMap = core.ComputeECMALineStarts(node.Text());
                node.ecmaLineMap = lineMap;
            }
        }
        return lineMap;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡnode.ᏑecmaLineMapMu.Unlock(); ᒐ.Run(); }
}

public static map<@string, nint> GetNameTable(this ж<SourceFile> Ꮡfile) {
    ref var @file = ref Ꮡfile.DerefOrNull();

    Ꮡfile.ᏑnameTableOnce.Do(() => {
        var nameTable = new map<@string, nint>(Ꮡfile.Value.IdentifierCount);

        ref var walk = ref heap<Func<ж<Node>, bool>>(out var Ꮡwalk);
        var nameTableʗ1 = nameTable;
        Ꮡwalk.ValueSlot = (ж<Node> node) => {
            if (IsIdentifier(node) && !IsTagName(node) && node.Text() != ""u8 || IsStringOrNumericLiteralLike(node) && literalIsName(node) || IsPrivateIdentifier(node)) {
                @string text = node.Text();
                var (_, ok) = nameTableʗ1[text, ꟷ]; if (ok) {
                    nameTableʗ1[text] = -1;
                } else {
                    nameTableʗ1[text] = node.Pos();
                }
            }

            node.ForEachChild(Ꮡwalk.ValueSlot);
            var jsdocNodes = node.JSDoc(Ꮡfile);
            foreach (var (_, jsdoc) in range(jsdocNodes)) {
                jsdoc.ForEachChild(Ꮡwalk.ValueSlot);
            }
            return false;
        };
        Ꮡfile.Value.ForEachChild(Ꮡwalk.ValueSlot);

        Ꮡfile.Value.nameTable = nameTable;
    });
    return @file.nameTable;
}

public static bool IsBound(this ж<SourceFile> Ꮡnode) {
    return Ꮡnode.ᏑisBound.Load();
}

public static ж<PositionMap> GetPositionMap(this ж<SourceFile> Ꮡfile) {
    ref var @file = ref Ꮡfile.DerefOrNull();

    Ꮡfile.ᏑpositionMapOnce.Do(() => {
        Ꮡfile.Value.positionMap = ComputePositionMap(Ꮡfile.Value.Text());
    });
    return @file.positionMap;
}

public static void BindOnce(this ж<SourceFile> Ꮡnode, Action bind) {
    Ꮡnode.ᏑbindOnce.Do(() => {
        bind();
        Ꮡnode.ᏑisBound.Store(true);
    });
}

public static ж<TokenNode> GetOrCreateToken(this ж<SourceFile> Ꮡnode, Kind kind, nint pos, nint end, ж<Node> Ꮡparent, TokenFlags flags) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var node = ref Ꮡnode.DerefOrNull();
        ref var parent = ref Ꮡparent.DerefOrNull();

        node.tokenCacheMu.Lock();
        ᒐd1 = true;
        var loc = core.NewTextRange(pos, end);
        var key = new TokenCacheKey(Ꮡparent, loc);
        var (tokenΔ1, ok) = node.tokenCache[key, ꟷ]; if (ok) {
            if (tokenΔ1.Kind != kind) {
                throw panic(fmt.Sprintf("Token cache mismatch: %v != %v"u8, tokenΔ1.Kind, kind));
            }
            return tokenΔ1;
        }
        if ((NodeFlags)(parent.Flags & NodeFlagsReparsed) != 0) {
            throw panic(fmt.Sprintf("Cannot create token from reparsed node of kind %v"u8, parent.Kind));
        }
        if (node.tokenCache == nil) {
            node.tokenCache = new map<TokenCacheKey, ж<Node>>();
        }
        var token = createToken(kind, ref (Ꮡnode).DerefOrNull(), pos, end, flags);
        token.Loc = loc;
        token.Parent = Ꮡparent;
        node.tokenCache[key] = token;
        return token;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡnode.DerefOrNull().tokenCacheMu.Unlock(); ᒐ.Run(); }
}

} partial class ast_package {

internal static ж<Node> createToken(Kind kind, ref SourceFile @file, nint pos, nint end, TokenFlags flags) {
    if (@file.tokenFactory == nil) {
        @file.tokenFactory = NewNodeFactory(new NodeFactoryHooks());
    }
    @string text = @file.text.slice(pos, end);
    switch (kind) {
    case var ᴛ when ᴛ == KindNumericLiteral: {
        return @file.tokenFactory.NewNumericLiteral(text, flags);
    }
    case var ᴛ when ᴛ == KindBigIntLiteral: {
        return @file.tokenFactory.NewBigIntLiteral(text, flags);
    }
    case var ᴛ when ᴛ == KindStringLiteral: {
        return @file.tokenFactory.NewStringLiteral(text, flags);
    }
    case var ᴛ when ᴛ == KindJsxText || ᴛ == KindJsxTextAllWhiteSpaces: {
        return @file.tokenFactory.NewJsxText(text, kind == KindJsxTextAllWhiteSpaces);
    }
    case var ᴛ when ᴛ == KindRegularExpressionLiteral: {
        return @file.tokenFactory.NewRegularExpressionLiteral(text, flags);
    }
    case var ᴛ when ᴛ == KindNoSubstitutionTemplateLiteral: {
        return @file.tokenFactory.NewNoSubstitutionTemplateLiteral(text, flags);
    }
    case var ᴛ when ᴛ == KindTemplateHead: {
        return @file.tokenFactory.NewTemplateHead(text, "", flags);
    }
    case var ᴛ when ᴛ == KindTemplateMiddle: {
        return @file.tokenFactory.NewTemplateMiddle(text, "", flags);
    }
    case var ᴛ when ᴛ == KindTemplateTail: {
        return @file.tokenFactory.NewTemplateTail(text, "", flags);
    }
    case var ᴛ when ᴛ == KindIdentifier: {
        return @file.tokenFactory.NewIdentifier(text);
    }
    case var ᴛ when ᴛ == KindPrivateIdentifier: {
        return @file.tokenFactory.NewPrivateIdentifier(text);
    }
    default: {
        return @file.tokenFactory.NewToken(kind);
    }}

}

} partial class astᴛSourceFile {

public static map<@string, slice<ж<Node>>> GetDeclarationMap(this ж<SourceFile> Ꮡnode) {
    GoFrame ᒐ = default;
    bool ᒐd1 = false;
    try {
        ref var node = ref Ꮡnode.DerefOrNull();

        node.declarationMapMu.Lock();
        ᒐd1 = true;
        if (node.declarationMap == nil) {
            node.declarationMap = Ꮡnode.computeDeclarationMap();
        }
        return node.declarationMap;
    }
    catch (Exception ᒐex) when (GoFrame.IsPanic(ᒐex, out PanicException? ᒐp)) { GoFrame.Capture(ᒐp); return default; }
    finally { if (ᒐd1) Ꮡnode.DerefOrNull().declarationMapMu.Unlock(); ᒐ.Run(); }
}

internal static map<@string, slice<ж<Node>>> computeDeclarationMap(this ж<SourceFile> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var result = new map<@string, slice<ж<Node>>>();

    var resultʗ1 = result;
    void addDeclaration(ж<Node> declaration) {
        @string name = GetDeclarationName(declaration);
        if (name != ""u8) {
            resultʗ1[name] = append(resultʗ1[name], declaration);
        }
    }

    ref var visit = ref heap<Func<ж<Node>, bool>>(out var Ꮡvisit);
    var addDeclarationʗ1 = addDeclaration;
    var resultʗ2 = result;
    visit = (ж<Node> nodeΔ1) => {
        var exprᴛ1 = nodeΔ1.Kind;
        var matchᴛ1 = false;
        if (exprᴛ1 == KindFunctionDeclaration || exprᴛ1 == KindFunctionExpression || exprᴛ1 == KindMethodDeclaration || exprᴛ1 == KindMethodSignature) { matchᴛ1 = true;
            @string declarationName = GetDeclarationName(nodeΔ1);
            if (declarationName != ""u8) {
                var declarations = resultʗ2[declarationName];
                ж<Node> lastDeclaration = default;
                if (len(declarations) != 0) {
                    lastDeclaration = declarations[len(declarations) - 1];
                }
                if (lastDeclaration != nil && nodeΔ1.Parent == lastDeclaration.Parent && nodeΔ1.Symbol() == lastDeclaration.Symbol()) {
                    if (nodeΔ1.Body() != nil && lastDeclaration.Body() == nil) {
                        declarations[len(declarations) - 1] = nodeΔ1;
                    }
                } else {
                    resultʗ2[declarationName] = append(resultʗ2[declarationName], nodeΔ1);
                }
            }
            nodeΔ1.ForEachChild(Ꮡvisit.ValueSlot);
        }
        else if (exprᴛ1 == KindClassDeclaration || exprᴛ1 == KindClassExpression || exprᴛ1 == KindInterfaceDeclaration || exprᴛ1 == KindTypeAliasDeclaration || exprᴛ1 == KindEnumDeclaration || exprᴛ1 == KindModuleDeclaration || exprᴛ1 == KindImportEqualsDeclaration || exprᴛ1 == KindImportClause || exprᴛ1 == KindNamespaceImport || exprᴛ1 == KindGetAccessor || exprᴛ1 == KindSetAccessor || exprᴛ1 == KindTypeLiteral) { matchᴛ1 = true;
            addDeclarationʗ1(nodeΔ1);
            nodeΔ1.ForEachChild(Ꮡvisit.ValueSlot);
        }
        else if (exprᴛ1 == KindImportSpecifier || exprᴛ1 == KindExportSpecifier) { matchᴛ1 = true;
            if (nodeΔ1.PropertyName() != nil) {
                addDeclarationʗ1(nodeΔ1);
            }
        }
        else if (exprᴛ1 == KindParameter) { matchᴛ1 = true;
            do {
                if (!HasSyntacticModifier(nodeΔ1, ModifierFlagsParameterPropertyModifier)) {
                    break;
                }
                fallthrough = true;
            } while (false);
        }
        if (fallthrough || !matchᴛ1 && (exprᴛ1 == KindVariableDeclaration || exprᴛ1 == KindBindingElement)) { matchᴛ1 = true;
            var name = nodeΔ1.Name();
            if (name != nil) {
                if (IsBindingPattern(name)) {
                    nodeΔ1.Name().ForEachChild(Ꮡvisit.ValueSlot);
                } else {
                    if (nodeΔ1.Initializer() != nil) {
                        Ꮡvisit.ValueSlot(nodeΔ1.Initializer());
                    }
                    addDeclarationʗ1(nodeΔ1);
                }
            }
        }
        else if (exprᴛ1 == KindEnumMember || exprᴛ1 == KindPropertyDeclaration || exprᴛ1 == KindPropertySignature) { matchᴛ1 = true;
            addDeclarationʗ1(nodeΔ1);
        }
        else if (exprᴛ1 == KindExportDeclaration) { matchᴛ1 = true;
            var exportClause = nodeΔ1.AsExportDeclaration().ExportClause;
            if (exportClause != nil) {
                if (IsNamedExports(exportClause)) {
                    foreach (var (_, element) in range(exportClause.Elements())) {
                        Ꮡvisit.ValueSlot(element);
                    }
                } else {
                    Ꮡvisit.ValueSlot(exportClause.AsNamespaceExport().Name());
                }
            }
        }
        else if (exprᴛ1 == KindImportDeclaration) { matchᴛ1 = true;
            var importClause = nodeΔ1.AsImportDeclaration().Value.ImportClause;
            if (importClause != nil) {
                if (importClause.Name() != nil) {
                    addDeclarationʗ1(importClause.Name());
                }
                var namedBindings = importClause.AsImportClause().NamedBindings;
                if (namedBindings != nil) {
                    if (namedBindings.Kind == KindNamespaceImport) {
                        addDeclarationʗ1(namedBindings);
                    } else {
                        foreach (var (_, element) in range(namedBindings.Elements())) {
                            Ꮡvisit.ValueSlot(element);
                        }
                    }
                }
            }
        }
        else if (exprᴛ1 == KindBinaryExpression) {
            switch (GetAssignmentDeclarationKind(nodeΔ1)) {
            case var ᴛ when ᴛ == JSDeclarationKindExportsProperty || ᴛ == JSDeclarationKindThisProperty || ᴛ == JSDeclarationKindProperty: {
                addDeclarationʗ1(nodeΔ1);
                break;
            }}

            nodeΔ1.ForEachChild(Ꮡvisit.ValueSlot);
        }
        else if (!matchᴛ1) { /* default: */
            nodeΔ1.ForEachChild(Ꮡvisit.ValueSlot);
        }

        return false;
    };
    node.ForEachChild(visit);
    return result;
}

} partial class ast_package {

public static @string GetDeclarationName(ж<Node> declaration) {
    var name = GetNonAssignedNameOfDeclaration(declaration);
    if (name != nil) {
        if (IsComputedPropertyName(name)) {
            if (IsStringOrNumericLiteralLike(name.Expression())) {
                return name.Expression().Text();
            }
            if (IsPropertyAccessExpression(name.Expression())) {
                return name.Expression().Name().Text();
            }
        } else if (IsPropertyName(name)) {
            return name.Text();
        }
    }
    return "";
}

partial interface SourceFileLike {
    @string Text();
    slice<core.TextPos> ECMALineMap();
}

partial struct CommentRange {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.core_package.TextRange TextRange;
    public Kind Kind;
    public bool HasTrailingNewLine;
}

} partial class astᴛNodeFactory {

public static CommentRange NewCommentRange(this ref NodeFactory f, Kind kind, nint pos, nint end, bool hasTrailingNewLine) {
    return new CommentRange(
        TextRange: core.NewTextRange(pos, end),
        Kind: kind,
        HasTrailingNewLine: hasTrailingNewLine
    );
}

} partial class ast_package {

partial struct FileReference {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.core_package.TextRange TextRange;
    public @string FileName;
    public coreꓸResolutionMode ResolutionMode;
    public bool Preserve;
}

partial struct PragmaArgument {
    /*embed*/ public go.github.com.microsoft.TypeScript.tsc.@internal.core_package.TextRange TextRange;
    public @string Name;
    public @string Value;
}

partial struct Pragma {
    /*embed*/ public CommentRange CommentRange;
    public @string Name;
    public map<@string, PragmaArgument> Args;
}

public const PragmaKindFlags PragmaKindTripleSlashXML = /* 1 << iota */ 1;
public const PragmaKindFlags PragmaKindSingleLine = 2;
public const PragmaKindFlags PragmaKindMultiLine = 4;
public const PragmaKindFlags PragmaKindFlagsNone = 0;
public const PragmaKindFlags PragmaKindAll = /* PragmaKindTripleSlashXML | PragmaKindSingleLine | PragmaKindMultiLine */ 7;
public const PragmaKindFlags PragmaKindDefault = /* PragmaKindAll */ 7;

partial struct PragmaArgumentSpecification {
    public @string Name;
    public bool Optional;
    public bool CaptureSpan;
}
partial struct PragmaSpecification {
    public slice<PragmaArgumentSpecification> Args;
    public PragmaKindFlags Kind;
}

} partial class astᴛPragmaSpecification {

public static bool IsTripleSlash(this ref PragmaSpecification spec) {
    return ((PragmaKindFlags)(spec.Kind & PragmaKindTripleSlashXML)) > 0;
}

} partial class ast_package {

internal static bool forEachChild_JSDocParameterOrPropertyTag(ref JSDocParameterOrPropertyTag node, Func<ж<Node>, bool> v) {
    return visit(v, node.TagName) || (node.IsNameFirst && (visit(v, node.name) || visit(v, node.TypeExpression))) || (!node.IsNameFirst && (visit(v, node.TypeExpression) || visit(v, node.name))) || visitNodeList(v, node.Comment);
}

internal static ж<Node> visitEachChild_JSDocParameterOrPropertyTag(ж<JSDocParameterOrPropertyTag> Ꮡnode, ж<NodeVisitor> Ꮡv) {
    ref var node = ref Ꮡnode.DerefOrNull();
    ref var v = ref Ꮡv.DerefOrNull();

    return v.Factory.UpdateJSDocParameterOrPropertyTag(Ꮡnode, Ꮡv.visitNode(node.TagName), Ꮡv.visitNode(node.name), node.IsBracketed, Ꮡv.visitNode(node.TypeExpression), node.IsNameFirst, Ꮡv.visitNodes(node.Comment));
}

} partial class astᴛNodeFactory {

public static void ReleaseArenas(this ref NodeFactory f) {
    f = new NodeFactory(
        hooks: f.hooks,
        textCount: f.textCount,
        nodeCount: f.nodeCount
    );
}

} // end astᴛNodeFactory
