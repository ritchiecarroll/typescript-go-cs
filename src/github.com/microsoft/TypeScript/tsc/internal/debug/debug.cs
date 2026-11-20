namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using fmt = fmt_package;
using ꓸꓸꓸany = Span<any>;

partial class debug_package {

public static void Fail(@string reason) {
    if (len(reason) == 0) {
        reason = "Debug failure.";
    } else {
        reason = "Debug failure. "u8 + reason;
    }
    throw panic(reason);
}

public partial interface FailBadSyntaxKind_node /*dyn*/ {
    @string KindString();
}

public static void FailBadSyntaxKind(FailBadSyntaxKind_node node, params ꓸꓸꓸany messageʗp) {
    var message = messageʗp.sslice();

    @string msg = default;
    if (len(message) == 0) {
        msg = "Unexpected node.";
    } else {
        msg = fmt.Sprint(message.ꓸꓸꓸ);
    }
    Fail(fmt.Sprintf("%s\nNode %s was unexpected."u8, msg, node.KindString()));
}

public static void AssertNever(any member, params ꓸꓸꓸany messageʗp) {
    var message = messageʗp.sslice();

    @string msg = default;
    if (len(message) == 0) {
        msg = "Illegal value:";
    } else {
        msg = fmt.Sprint(message.ꓸꓸꓸ);
    }
    @string detail = default;
    var (m, ok) = member._<FailBadSyntaxKind_node>(ᐧ); if (ok) {
        detail = m.KindString();
    } else if (member._<fmt.Stringer>(out var mΔ1)) {
        detail = mΔ1.String();
    } else {
        detail = fmt.Sprintf("%v"u8, member);
    }
    Fail(fmt.Sprintf("%s %s"u8, msg, detail));
}

public static void Assert(bool value, params ꓸꓸꓸany messageʗp) {
    var message = messageʗp.sslice();

    if (value) {
        return;
    }
    assertSlow(message.ꓸꓸꓸ);
}

internal static void assertSlow(params ꓸꓸꓸany messageʗp) {
    var message = messageʗp.sslice();

    @string msg = default;
    if (len(message) > 0) {
        msg = "False expression: "u8 + fmt.Sprint(message.ꓸꓸꓸ);
    } else {
        msg = "False expression.";
    }
    Fail(msg);
}

} // end debug_package
