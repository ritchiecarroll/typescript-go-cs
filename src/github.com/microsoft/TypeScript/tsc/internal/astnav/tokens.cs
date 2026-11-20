namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using fmt = fmt_package;
using ast = go.github.com.microsoft.TypeScript.tsc.@internal.ast_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using scanner = go.github.com.microsoft.TypeScript.tsc.@internal.scanner_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;

partial class astnav_package {

internal static bool shouldRescanLessThanLessThanToken(ref scanner.Scanner s, ж<ast.Node> containingNode, ast.Kind token) {
    return token == ast.KindLessThanLessThanToken && ast.IsJsxChild(containingNode);
}

internal static ast.Kind scanNavigationToken(ж<scanner.Scanner> Ꮡs, ж<ast.Node> containingNode) {
    ref var s = ref Ꮡs.DerefOrNull();

    var token = s.Token();
    if (shouldRescanLessThanLessThanToken(ref (Ꮡs).DerefOrNull(), containingNode, token)) {
        return Ꮡs.ReScanJsxToken(true);
    }
    return token;
}

public static ж<ast.Node> GetTouchingPropertyName(ж<ast.SourceFile> sourceFile, nint position) {
    return getTokenAtPosition(sourceFile, position, false, (ж<ast.Node> node) => ast.IsPropertyNameLiteral(node) || ast.IsKeywordKind(node.Kind) || ast.IsPrivateIdentifier(node));
}

public static ж<ast.Node> GetTouchingToken(ж<ast.SourceFile> sourceFile, nint position) {
    return getTokenAtPosition(sourceFile, position, false, default);
}

public static ж<ast.Node> GetTokenAtPosition(ж<ast.SourceFile> sourceFile, nint position) {
    return getTokenAtPosition(sourceFile, position, true, default);
}

internal static ж<ast.Node> getTokenAtPosition(ж<ast.SourceFile> sourceFile, nint position, bool allowPositionInLeadingTrivia, Func<ж<ast.Node>, bool> includePrecedingTokenAtEndPosition) {
    ref var next = ref heap<ж<ast.Node>>(out var Ꮡnext);
    ref var prevSubtree = ref heap<ж<ast.Node>>(out var ᏑprevSubtree);
    var current = sourceFile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    nint left = 0;
    ref var nodeAfterLeft = ref heap<ж<ast.Node>>(out var ᏑnodeAfterLeft);

    ж<ast.Node> getIncludedPrecedingToken(ж<ast.Node> subtree) {
        var child = FindPrecedingTokenEx(sourceFile, position, subtree, false);
        if (child != nil && child.End() == position && includePrecedingTokenAtEndPosition(child)) {
            return child;
        }
        return default;
    }

    var getIncludedPrecedingTokenʗ1 = getIncludedPrecedingToken;
    nint testNode(ж<ast.Node> node) {
        if (node.Kind != ast.KindEndOfFile && node.End() == position && includePrecedingTokenAtEndPosition != nil && (ast.NodeFlags)(node.Flags & ast.NodeFlagsReparsed) == 0) {
            if (ᏑprevSubtree.ValueSlot != nil && getIncludedPrecedingTokenʗ1(ᏑprevSubtree.ValueSlot) != nil) {
                return 0;
            }
            ᏑprevSubtree.ValueSlot = node;
        }

        if (node.End() < position || node.End() == position && node.Kind != ast.KindEndOfFile && (!ast.IsJSDocKind(node.Kind) || node.End() != sourceFile.Value.EndOfFileToken.End())) {
            return -1;
        }
        nint nodePos = getPosition(node, sourceFile, allowPositionInLeadingTrivia);
        if (nodePos > position) {
            return 1;
        }
        return 0;
    }

    var testNodeʗ1 = testNode;
    var visitNode = (ж<ast.Node> node, ж<ast.NodeVisitor> _) => {
        if (node == nil || (ast.NodeFlags)(node.Flags & ast.NodeFlagsReparsed) != 0) {
            return default;
        }
        if (ᏑnodeAfterLeft.ValueSlot == nil) {
            ᏑnodeAfterLeft.ValueSlot = node;
        }
        if (Ꮡnext.ValueSlot == nil) {
            nint result = testNodeʗ1(node);
            switch (result) {
            case var ᴛ when ᴛ == -1: {
                if (!ast.IsJSDocKind(node.Kind)) {
                    left = node.End();
                }
                ᏑnodeAfterLeft.ValueSlot = default;
                break;
            }
            case 0: {
                Ꮡnext.ValueSlot = node;
                break;
            }}

        }
        return node;
    };

    var testNodeʗ2 = testNode;
    var visitNodeList = (ж<ast.NodeList> nodeList, ж<ast.NodeVisitor> _) => {
        if (nodeList == nil || len(nodeList.Nodes) == 0) {
            return nodeList;
        }
        if (ᏑnodeAfterLeft.ValueSlot == nil) {
            foreach (var (_, node) in range(nodeList.Nodes)) {
                if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsReparsed) == 0) {
                    ᏑnodeAfterLeft.ValueSlot = node;
                    break;
                }
            }
        }
        if (Ꮡnext.ValueSlot == nil) {
            if (nodeList.End() == position && includePrecedingTokenAtEndPosition != nil) {
                left = nodeList.End();
                ᏑnodeAfterLeft.ValueSlot = default;
                for (nint i = len(nodeList.Nodes) - 1; i >= 0; i--) {
                    if ((ast.NodeFlags)(nodeList.Nodes[i].Flags & ast.NodeFlagsReparsed) == 0) {
                        ᏑprevSubtree.ValueSlot = nodeList.Nodes[i];
                        break;
                    }
                }
            } else if (nodeList.End() <= position) {
                left = nodeList.End();
                ᏑnodeAfterLeft.ValueSlot = default;
            } else if (nodeList.Pos() <= position) {
                ref var nodes = ref heap<slice<ж<ast.Node>>>(nodeList.Nodes, out var Ꮡnodes);
                var testNodeʗ3 = testNodeʗ2;
                var (index, match) = core.BinarySearchUniqueFunc(Ꮡnodes.ValueSlot, (nint middle, ж<ast.Node> node) => {
                    if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsReparsed) != 0) {
                        return 0;
                    }
                    nint cmp = testNodeʗ3(node);
                    if (cmp < 0) {
                        left = node.End();
                        ᏑnodeAfterLeft.ValueSlot = default;
                        for (nint i = middle + 1; i < len(Ꮡnodes.ValueSlot); i++) {
                            if ((ast.NodeFlags)(Ꮡnodes.ValueSlot[i].Flags & ast.NodeFlagsReparsed) == 0) {
                                ᏑnodeAfterLeft.ValueSlot = Ꮡnodes.ValueSlot[i];
                                break;
                            }
                        }
                    }
                    return cmp;
                });
                if (match && (ast.NodeFlags)(Ꮡnodes.ValueSlot[index].Flags & ast.NodeFlagsReparsed) != 0) {
                    Ꮡnodes.ValueSlot = core.Filter(Ꮡnodes.ValueSlot, (ж<ast.Node> node) => (ast.NodeFlags)(node.Flags & ast.NodeFlagsReparsed) == 0);
                    var testNodeʗ4 = testNodeʗ2;
                    (index, match) = core.BinarySearchUniqueFunc(Ꮡnodes.ValueSlot, (nint middle, ж<ast.Node> node) => {
                        nint cmp = testNodeʗ4(node);
                        if (cmp < 0) {
                            left = node.End();
                            if (middle + 1 < len(Ꮡnodes.ValueSlot)) {
                                ᏑnodeAfterLeft.ValueSlot = Ꮡnodes.ValueSlot[middle + 1];
                            } else {
                                ᏑnodeAfterLeft.ValueSlot = default;
                            }
                        }
                        return cmp;
                    });
                }
                if (match) {
                    Ꮡnext.ValueSlot = Ꮡnodes.ValueSlot[index];
                }
            }
        }
        return nodeList;
    };

    for (;;) {
        VisitEachChildAndJSDoc(current, sourceFile, visitNode, visitNodeList);
        if (prevSubtree != nil) {
            var child = getIncludedPrecedingToken(prevSubtree); if (child != nil) {
                return child;
            }
            prevSubtree = default;
        }

        if (next == nil) {
            if (ast.IsTokenKind(current.Kind) || shouldSkipChild(current)) {
                return current;
            }
            var scannerΔ1 = scanner.GetScannerForSourceFile(sourceFile, left);
            nint end = current.End();
            if (nodeAfterLeft != nil) {
                end = nodeAfterLeft.Pos();
            }
            while (left < end) {
                var token = scanNavigationToken(scannerΔ1, current);
                nint tokenFullStart = scannerΔ1.TokenFullStart();
                nint tokenStart = core.IfElse(allowPositionInLeadingTrivia, tokenFullStart, scannerΔ1.TokenStart());
                nint tokenEnd = scannerΔ1.TokenEnd();
                var flags = scannerΔ1.TokenFlags();
                if (tokenEnd > end) {
                    break;
                }
                if (tokenStart <= position && (position < tokenEnd)) {
                    if (token == ast.KindIdentifier || !ast.IsTokenKind(token)) {
                        if (ast.IsJSDocKind(current.Kind)) {
                            return current;
                        }
                        throw panic(fmt.Sprintf("did not expect %s to have %s in its trivia"u8, current.Kind.String(), token.String()));
                    }
                    return sourceFile.GetOrCreateToken(token, tokenFullStart, tokenEnd, current, flags);
                }
                if (includePrecedingTokenAtEndPosition != nil && tokenEnd == position) {
                    var prevToken = sourceFile.GetOrCreateToken(token, tokenFullStart, tokenEnd, current, flags);
                    if (includePrecedingTokenAtEndPosition(prevToken)) {
                        return prevToken;
                    }
                }
                left = tokenEnd;
                scannerΔ1.Scan();
            }
            return current;
        }
        current = next;
        left = current.Pos();
        nodeAfterLeft = default;
        next = default;
    }
}

internal static nint getPosition(ж<ast.Node> Ꮡnode, ж<ast.SourceFile> sourceFile, bool allowPositionInLeadingTrivia) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (allowPositionInLeadingTrivia) {
        return node.Pos();
    }
    return scanner.GetTokenPosOfNode(Ꮡnode, sourceFile, true);
}

internal static ж<ast.Node> findRightmostNode(ж<ast.Node> node) {
    ref var next = ref heap<ж<ast.Node>>(out var Ꮡnext);
    var current = node;
    var visitNode = (ж<ast.Node> nodeΔ1, ж<ast.NodeVisitor> _) => {
        if (nodeΔ1 != nil) {
            Ꮡnext.ValueSlot = nodeΔ1;
        }
        return nodeΔ1;
    };
    var visitNodes = (ж<ast.NodeList> nodeList, ж<ast.NodeVisitor> visitorΔ1) => {
        if (nodeList != nil) {
            var rightmost = ast.FindLastVisibleNode(nodeList.Nodes); if (rightmost != nil) {
                Ꮡnext.ValueSlot = rightmost;
            }
        }
        return nodeList;
    };
    var visitor = getNodeVisitor(visitNode, visitNodes);

    for (;;) {
        current.VisitEachChild(visitor);
        if (next == nil) {
            return current;
        }
        current = next;
        next = default;
    }
}

public static void VisitEachChildAndJSDoc(ж<ast.Node> Ꮡnode, ж<ast.SourceFile> sourceFile, Func<ж<ast.Node>, ж<ast.NodeVisitor>, ж<ast.Node>> visitNode, Func<ж<ast.NodeList>, ж<ast.NodeVisitor>, ж<ast.NodeList>> visitNodes) {
    ref var node = ref Ꮡnode.DerefOrNull();

    var visitor = getNodeVisitor(visitNode, visitNodes);
    foreach (var (_, jsdoc) in range(Ꮡnode.JSDoc(sourceFile))) {
        if (visitor.Hooks.VisitNode != nil) {
            visitor.Hooks.VisitNode(jsdoc, visitor);
        } else {
            visitor.VisitNode(jsdoc);
        }
    }
    node.VisitEachChild(visitor);
}

internal static UntypedInt comparisonLessThan => -1;
internal static UntypedInt comparisonEqualTo => 0;
internal static UntypedInt comparisonGreaterThan => 1;

public static ж<ast.Node> FindPrecedingToken(ж<ast.SourceFile> sourceFile, nint position) {
    return FindPrecedingTokenEx(sourceFile, position, nil, false);
}

public static ж<ast.Node> FindPrecedingTokenEx(ж<ast.SourceFile> sourceFile, nint position, ж<ast.Node> startNode, bool excludeJSDoc) {
    ref var find = ref heap<Func<ж<ast.Node>, ж<ast.Node>>>(out var Ꮡfind);
    find = (ж<ast.Node> n) => {
        if (ast.IsNonWhitespaceToken(n) && n.Kind != ast.KindEndOfFile) {
            return n;
        }

        ref var foundChild = ref heap<ж<ast.Node>>(out var ᏑfoundChild);
        ref var prevChild = ref heap<ж<ast.Node>>(out var ᏑprevChild);
        var visitNode = (ж<ast.Node> nodeΔ1, ж<ast.NodeVisitor> _) => {
            if (nodeΔ1 == nil || (ast.NodeFlags)(nodeΔ1.Flags & ast.NodeFlagsReparsed) != 0) {
                return nodeΔ1;
            }
            if (ᏑfoundChild.ValueSlot != nil) {
                return nodeΔ1;
            }
            if (position < nodeΔ1.End() && (ᏑprevChild.ValueSlot == nil || ᏑprevChild.ValueSlot.End() <= position)) {
                ᏑfoundChild.ValueSlot = nodeΔ1;
            } else {
                ᏑprevChild.ValueSlot = nodeΔ1;
            }
            return nodeΔ1;
        };
        var visitNodes = (ж<ast.NodeList> nodeList, ж<ast.NodeVisitor> _) => {
            if (ᏑfoundChild.ValueSlot != nil) {
                return nodeList;
            }
            if (nodeList != nil && len(nodeList.Nodes) > 0) {
                var nodes = nodeList.Nodes;
                var nodesʗ1 = nodes;
                var (index, match) = core.BinarySearchUniqueFunc(nodes, (nint middle, ж<ast.Node> _) => {
                    if ((ast.NodeFlags)(nodesʗ1[middle].Flags & ast.NodeFlagsReparsed) != 0) {
                        return comparisonLessThan;
                    }
                    if (position < nodesʗ1[middle].End()) {
                        if (middle == 0 || position >= nodesʗ1[middle - 1].End()) {
                            return comparisonEqualTo;
                        }
                        return comparisonGreaterThan;
                    }
                    return comparisonLessThan;
                });

                if (match) {
                    ᏑfoundChild.ValueSlot = nodes[index];
                }

                nint validLookupIndex = core.IfElse(match, index - 1, len(nodes) - 1);
                for (nint i = validLookupIndex; i >= 0; i--) {
                    if ((ast.NodeFlags)(nodes[i].Flags & ast.NodeFlagsReparsed) != 0) {
                        continue;
                    }
                    if (ᏑprevChild.ValueSlot == nil) {
                        ᏑprevChild.ValueSlot = nodes[i];
                    }
                }
            }
            return nodeList;
        };
        VisitEachChildAndJSDoc(n, sourceFile, visitNode, visitNodes);

        if (ᏑfoundChild.ValueSlot != nil) {
            nint start = GetStartOfNode(ᏑfoundChild.ValueSlot, sourceFile, !excludeJSDoc);
            var lookInPreviousChild = start >= position || !isValidPrecedingNode(ᏑfoundChild.ValueSlot, sourceFile);
            if (lookInPreviousChild) {
                if (position >= ᏑfoundChild.ValueSlot.Pos()) {
                    ж<ast.Node> jsDoc = default;
                    var nodeJSDoc = n.JSDoc(sourceFile);
                    for (nint i = len(nodeJSDoc) - 1; i >= 0; i--) {
                        if (nodeJSDoc[i].Pos() >= ᏑfoundChild.ValueSlot.Pos()) {
                            jsDoc = nodeJSDoc[i];
                            break;
                        }
                    }
                    if (jsDoc != nil) {
                        if (!excludeJSDoc && position < jsDoc.End()) {
                            return Ꮡfind.ValueSlot(jsDoc);
                        } else {
                            return findRightmostValidToken(jsDoc.End(), sourceFile, n, position, excludeJSDoc);
                        }
                    }
                    return findRightmostValidToken(ᏑfoundChild.ValueSlot.Pos(), sourceFile, n, -1, excludeJSDoc);
                } else {
                    return findRightmostValidToken(ᏑfoundChild.ValueSlot.Pos(), sourceFile, n, position, excludeJSDoc);
                }
            } else {
                return Ꮡfind.ValueSlot(ᏑfoundChild.ValueSlot);
            }
        }

        if (position >= n.End()) {
            return findRightmostValidToken(n.End(), sourceFile, n, -1, excludeJSDoc);
        } else {
            return findRightmostValidToken(n.End(), sourceFile, n, position, excludeJSDoc);
        }
    };

    ж<ast.Node> node = default;
    if (startNode != nil) {
        node = startNode;
    } else {
        node = sourceFile.of(ast.SourceFile.ᏑNodeBase).of(ast.NodeBase.ᏑNodeDefault).AsNode();
    }
    var result = find(node);
    if (result != nil && ast.IsWhitespaceOnlyJsxText(result)) {
        throw panic("Expected result to be a non-whitespace token.");
    }
    return result;
}

internal static bool isValidPrecedingNode(ж<ast.Node> Ꮡnode, ж<ast.SourceFile> sourceFile) {
    ref var node = ref Ꮡnode.DerefOrNull();

    if (node.Kind == ast.KindEndOfFile) {
        return len(Ꮡnode.JSDoc(sourceFile)) > 0;
    }
    nint start = GetStartOfNode(Ꮡnode, sourceFile, false);
    nint width = node.End() - start;
    return !(ast.IsWhitespaceOnlyJsxText(Ꮡnode) || width == 0);
}

public static nint GetStartOfNode(ж<ast.Node> node, ж<ast.SourceFile> @file, bool includeJSDoc) {
    return scanner.GetTokenPosOfNode(node, @file, includeJSDoc);
}

internal static ж<ast.Node> findRightmostValidToken(nint endPos, ж<ast.SourceFile> sourceFile, ж<ast.Node> ᏑcontainingNode, nint position, bool excludeJSDoc) {
    ref var containingNode = ref ᏑcontainingNode.DerefOrNull();

    if (position == -1) {
        position = containingNode.End();
    }
    ref var find = ref heap<Func<ж<ast.Node>, nint, ж<ast.Node>>>(out var Ꮡfind);
    find = (ж<ast.Node> n, nint endPosΔ1) => {
        var tokensᴷ = new stackAppend<ж<ast.Node>>();
        if (n == nil) {
            return default;
        }
        if (ast.IsNonWhitespaceToken(n)) {
            return n;
        }

        ref var rightmostValidNode = ref heap<ж<ast.Node>>(out var ᏑrightmostValidNode);
        ref var rightmostVisitedNodes = ref heap<slice<ж<ast.Node>>>(new slice<ж<ast.Node>>(0, 1), out var ᏑrightmostVisitedNodes);
        var hasChildren = false;
        bool shouldVisitNode(ж<ast.Node> node) => !((ast.NodeFlags)(node.Flags & ast.NodeFlagsReparsed) != 0 || node.End() > endPosΔ1 || GetStartOfNode(node, sourceFile, !excludeJSDoc) >= position);
        var shouldVisitNodeʗ1 = shouldVisitNode;
        var visitNode = (ж<ast.Node> node, ж<ast.NodeVisitor> _Δp1) => {
            if (node == nil || (ast.NodeFlags)(node.Flags & ast.NodeFlagsReparsed) != 0) {
                return node;
            }
            hasChildren = true;
            if (!shouldVisitNodeʗ1(node)) {
                return node;
            }
            ᏑrightmostVisitedNodes.ValueSlot = append(ᏑrightmostVisitedNodes.ValueSlot, node);
            if (isValidPrecedingNode(node, sourceFile)) {
                ᏑrightmostValidNode.ValueSlot = node;
                ᏑrightmostVisitedNodes.ValueSlot = ᏑrightmostVisitedNodes.ValueSlot[..0];
            }
            return node;
        };
        var shouldVisitNodeʗ2 = shouldVisitNode;
        var visitNodes = (ж<ast.NodeList> nodeList, ж<ast.NodeVisitor> _Δp1) => {
            if (nodeList != nil && len(nodeList.Nodes) > 0) {
                hasChildren = true;
                var (index, _) = core.BinarySearchUniqueFunc(nodeList.Nodes, (nint middle, ж<ast.Node> node) => {
                    if (node.End() > endPosΔ1) {
                        return comparisonGreaterThan;
                    }
                    return comparisonLessThan;
                });
                nint validIndex = -1;
                for (nint i = index - 1; i >= 0; i--) {
                    if (!shouldVisitNodeʗ2(nodeList.Nodes[i])) {
                        continue;
                    }
                    if (isValidPrecedingNode(nodeList.Nodes[i], sourceFile)) {
                        validIndex = i;
                        ᏑrightmostValidNode.ValueSlot = nodeList.Nodes[i];
                        break;
                    }
                }
                for (nint i = validIndex + 1; i < index; i++) {
                    if (!shouldVisitNodeʗ2(nodeList.Nodes[i])) {
                        continue;
                    }
                    ᏑrightmostVisitedNodes.ValueSlot = append(ᏑrightmostVisitedNodes.ValueSlot, nodeList.Nodes[i]);
                }
            }
            return nodeList;
        };
        VisitEachChildAndJSDoc(n, sourceFile, visitNode, visitNodes);

        if (!shouldSkipChild(n)) {
            nint startPos = default;
            if (ᏑrightmostValidNode.ValueSlot != nil) {
                startPos = ᏑrightmostValidNode.ValueSlot.End();
            } else {
                startPos = n.Pos();
            }
            var scannerΔ1 = scanner.GetScannerForSourceFile(sourceFile, startPos);
            slice<ж<ast.Node>> tokens = default;
            foreach (var (_, visitedNode) in range(ᏑrightmostVisitedNodes.ValueSlot)) {
                while (startPos < min(visitedNode.Pos(), position)) {
                    var token = scanNavigationToken(scannerΔ1, n);
                    nint tokenStart = scannerΔ1.TokenStart();
                    if (tokenStart >= min(visitedNode.Pos(), position)) {
                        break;
                    }
                    nint tokenFullStart = scannerΔ1.TokenFullStart();
                    nint tokenEnd = scannerΔ1.TokenEnd();
                    startPos = tokenEnd;
                    var flags = scannerΔ1.TokenFlags();
                    tokens = append(tokensᴷ, tokens, sourceFile.GetOrCreateToken(token, tokenFullStart, tokenEnd, n, flags));
                    scannerΔ1.Scan();
                }
                startPos = visitedNode.End();
                scannerΔ1.ResetPos(startPos);
                scannerΔ1.Scan();
            }
            while (startPos < min(endPosΔ1, position)) {
                var token = scanNavigationToken(scannerΔ1, n);
                nint tokenStart = scannerΔ1.TokenStart();
                if (tokenStart >= min(endPosΔ1, position)) {
                    break;
                }
                nint tokenFullStart = scannerΔ1.TokenFullStart();
                nint tokenEnd = scannerΔ1.TokenEnd();
                startPos = tokenEnd;
                var flags = scannerΔ1.TokenFlags();
                tokens = append(tokens, sourceFile.GetOrCreateToken(token, tokenFullStart, tokenEnd, n, flags));
                scannerΔ1.Scan();
            }

            nint lastToken = len(tokens) - 1;
            for (nint i = lastToken; i >= 0; i--) {
                if (!ast.IsWhitespaceOnlyJsxText(tokens[i])) {
                    return tokens[i];
                }
            }
        }

        if (!hasChildren) {
            if (n != ᏑcontainingNode) {
                return n;
            }
            return default;
        }
        if (ᏑrightmostValidNode.ValueSlot != nil) {
            endPosΔ1 = ᏑrightmostValidNode.ValueSlot.End();
        }
        return Ꮡfind.ValueSlot(ᏑrightmostValidNode.ValueSlot, endPosΔ1);
    };

    return find(ᏑcontainingNode, endPos);
}

public static ж<ast.Node> FindNextToken(ж<ast.Node> previousToken, ж<ast.Node> parent, ж<ast.SourceFile> @file) {
    ref var find = ref heap<Func<ж<ast.Node>, ж<ast.Node>>>(out var Ꮡfind);
    find = (ж<ast.Node> n) => {
        if (ast.IsTokenKind(n.Kind) && n.Pos() == previousToken.Value.End()) {
            return n;
        }
        ref var foundNode = ref heap<ж<ast.Node>>(out var ᏑfoundNode);
        var visitNode = (ж<ast.Node> node, ж<ast.NodeVisitor> _) => {
            if (node != nil && (ast.NodeFlags)(node.Flags & ast.NodeFlagsReparsed) == 0 && node.Pos() <= previousToken.Value.End() && node.End() > previousToken.Value.End()) {
                ᏑfoundNode.ValueSlot = node;
            }
            return node;
        };
        var visitNodes = (ж<ast.NodeList> nodeList, ж<ast.NodeVisitor> _) => {
            if (nodeList != nil && len(nodeList.Nodes) > 0 && ᏑfoundNode.ValueSlot == nil) {
                var nodes = nodeList.Nodes;
                var (index, match) = core.BinarySearchUniqueFunc(nodes, (nint _, ж<ast.Node> node) => {
                    if ((ast.NodeFlags)(node.Flags & ast.NodeFlagsReparsed) != 0) {
                        return comparisonLessThan;
                    }
                    if (node.Pos() > previousToken.Value.End()) {
                        return comparisonGreaterThan;
                    }
                    if (node.End() <= previousToken.Value.Pos()) {
                        return comparisonLessThan;
                    }
                    return comparisonEqualTo;
                });
                if (match) {
                    ᏑfoundNode.ValueSlot = nodes[index];
                }
            }
            return nodeList;
        };
        VisitEachChildAndJSDoc(n, @file, visitNode, visitNodes);

        if (ᏑfoundNode.ValueSlot != nil) {
            return Ꮡfind.ValueSlot(ᏑfoundNode.ValueSlot);
        }
        nint startPos = previousToken.Value.End();
        if (startPos >= n.Pos() && startPos < n.End()) {
            var scannerΔ1 = scanner.GetScannerForSourceFile(@file, startPos);
            var token = scannerΔ1.Token();
            nint tokenFullStart = scannerΔ1.TokenFullStart();
            nint tokenEnd = scannerΔ1.TokenEnd();
            var flags = scannerΔ1.TokenFlags();
            if (tokenFullStart == previousToken.Value.End()) {
                return @file.GetOrCreateToken(token, tokenFullStart, tokenEnd, n, flags);
            }
            throw panic(fmt.Sprintf("Expected to find next token at %d, got token %s at %d"u8, previousToken.Value.End(), token, tokenFullStart));
        }
        return default;
    };
    return find(parent);
}

internal static ж<ast.NodeVisitor> getNodeVisitor(Func<ж<ast.Node>, ж<ast.NodeVisitor>, ж<ast.Node>> visitNode, Func<ж<ast.NodeList>, ж<ast.NodeVisitor>, ж<ast.NodeList>> visitNodes) {
    Func<ж<ast.Node>, ж<ast.NodeVisitor>, ж<ast.Node>> wrappedVisitNode = default;
    Func<ж<ast.NodeList>, ж<ast.NodeVisitor>, ж<ast.NodeList>> wrappedVisitNodes = default;
    if (visitNode != nil) {
        wrappedVisitNode = (ж<ast.Node> n, ж<ast.NodeVisitor> v) => {
            if (ast.IsJSDocSingleCommentNodeComment(n)) {
                return n;
            }
            return visitNode(n, v);
        };
    }

    if (visitNodes != nil) {
        wrappedVisitNodes = (ж<ast.NodeList> n, ж<ast.NodeVisitor> v) => {
            if (ast.IsJSDocSingleCommentNodeList(n)) {
                return n;
            }
            return visitNodes(n, v);
        };
    }

        var wrappedVisitNodesʗ1 = wrappedVisitNodes;
    return ast.NewNodeVisitor(core.Identity<ж<ast.Node>>, nil, new ast.NodeVisitorHooks(
        VisitNode: wrappedVisitNode,
        VisitToken: wrappedVisitNode,
        VisitNodes: wrappedVisitNodes,
        VisitModifiers: (ж<ast.ModifierList> modifiers, ж<ast.NodeVisitor> visitor) => {
            if (modifiers != nil) {
                wrappedVisitNodesʗ1(modifiers.ᏑNodeList, visitor);
            }
            return modifiers;
        }
    ));
}

internal static bool shouldSkipChild(ж<ast.Node> Ꮡnode) {
    ref var node = ref Ꮡnode.DerefOrNull();

    return node.Kind == ast.KindJSDoc || node.Kind == ast.KindJSDocText || node.Kind == ast.KindJSDocTypeLiteral || node.Kind == ast.KindJSDocSignature || ast.IsJSDocLinkLike(Ꮡnode) || ast.IsJSDocTag(Ꮡnode);
}

public static ж<ast.Node> FindChildOfKind(ж<ast.Node> ᏑcontainingNode, ast.Kind kind, ж<ast.SourceFile> sourceFile) {
    ref var containingNode = ref ᏑcontainingNode.DerefOrNull();

    nint lastNodePos = containingNode.Pos();
    var scan = scanner.GetScannerForSourceFile(sourceFile, lastNodePos);

    ref var foundChild = ref heap<ж<ast.Node>>(out var ᏑfoundChild);
    var scanʗ1 = scan;
    var visitNode = (ж<ast.Node> node) => {
        if (node == nil || (ast.NodeFlags)(node.Flags & ast.NodeFlagsReparsed) != 0) {
            return false;
        }
        nint startPosΔ1 = lastNodePos;
        while (startPosΔ1 < node.Pos()) {
            var tokenKind = scanʗ1.Token();
            nint tokenEnd = scanʗ1.TokenEnd();
            if (tokenKind == kind) {
                nint tokenFullStart = scanʗ1.TokenFullStart();
                var flags = scanʗ1.TokenFlags();
                ᏑfoundChild.ValueSlot = sourceFile.GetOrCreateToken(tokenKind, tokenFullStart, tokenEnd, ᏑcontainingNode, flags);
                return true;
            }
            startPosΔ1 = tokenEnd;
            scanʗ1.Scan();
        }

        if (node.Kind == kind) {
            ᏑfoundChild.ValueSlot = node;
            return true;
        }

        lastNodePos = node.End();
        scanʗ1.ResetPos(lastNodePos);
        return false;
    };

    ast.ForEachChildAndJSDoc(ᏑcontainingNode, sourceFile, visitNode);

    if (foundChild != nil) {
        return foundChild;
    }

    nint startPos = lastNodePos;
    while (startPos < containingNode.End()) {
        var tokenKind = scan.Token();
        nint tokenEnd = scan.TokenEnd();
        if (tokenKind == kind) {
            nint tokenFullStart = scan.TokenFullStart();
            var flags = scan.TokenFlags();
            var token = sourceFile.GetOrCreateToken(tokenKind, tokenFullStart, tokenEnd, ᏑcontainingNode, flags);
            return token;
        }
        startPos = tokenEnd;
        scan.Scan();
    }
    return default;
}

} // end astnav_package
