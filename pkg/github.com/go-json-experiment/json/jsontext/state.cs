// Copyright 2020 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.github.com.go_json_experiment.json;

using errors = errors_package;
using iter = iter_package;
using math = math_package;
using strconv = strconv_package;
using strings = strings_package;
using utf8 = unicode.utf8_package;
using jsonwire = go.github.com.go_json_experiment.json.@internal.jsonwire_package;
using go.github.com.go_json_experiment.json.@internal;
using unicode;
using static jsontext_package;

partial class jsontext_package {

public static error ErrDuplicateName = errors.New("duplicate object member name");

public static error ErrNonStringName = errors.New("object member name must be a string");

internal static error errMissingValue = errors.New("missing value after object name");
internal static error errMismatchDelim = errors.New("mismatching structural token for object or array");
internal static error errMaxDepth = errors.New("exceeded max depth");
internal static error errInvalidNamespace = errors.New("object namespace is in an invalid state");

internal static UntypedInt maxNestingDepth => 10000;

partial struct state {
    public stateMachine Tokens;
    public objectNameStack Names;
    public objectNamespaceStack Namespaces;
}

} partial class jsontextᴛstate {

internal static bool needObjectValue(this ref state s) {
    return s.Tokens.Last.needObjectValue();
}

internal static void reset(this ref state s) {
    s.Tokens.reset();
    s.Names.reset();
    s.Namespaces.reset();
}

} partial class jsontext_package {

partial struct Pointer /*@string*/;

} partial class jsontextᴛPointer {

public static bool IsValid(this Pointer p) {
    foreach (var (i, r) in p) {
        switch (ᐧ) {
        case {} when r == '~' && (i + 1 == len(p) || (p[i + 1] != (byte)'0' && p[i + 1] != (byte)'1')): {
            return false;
        }
        case {} when r == '\ufffd' && !strings.HasPrefix((@string)(p.slice(i)), "\ufffd"): {
            return false;
        }}

    }
    return len(p) == 0 || p[0] == '/';
}

public static bool Contains(this Pointer p, Pointer pc) {
    var (suffix, ok) = strings.CutPrefix((@string)pc, (@string)p);
    return ok && (suffix == ""u8 || suffix[0] == '/');
}

public static Pointer Parent(this Pointer p) {
    return p.slice(0, max(strings.LastIndexByte((@string)p, (byte)'/'), 0));
}

public static @string LastToken(this Pointer p) {
    Pointer last = p.slice(max(strings.LastIndexByte((@string)p, (byte)'/'), 0));
    return unescapePointerToken(strings.TrimPrefix((@string)last, "/"));
}

public static Pointer AppendToken(this Pointer p, @string tok) {
    return (Pointer)appendEscapePointerName(slice<byte>((@string)(p + "/"u8)), slice<byte>(tok));
}

public static iter.Seq<@string> Tokens(this Pointer p) {
    return (Func<@string, bool> yield) => {
        while (len(p) > 0) {
            p = (Pointer)strings.TrimPrefix((@string)p, "/");
            nuint i = min((nuint)strings.IndexByte((@string)p, (byte)'/'), (nuint)len(p));
            if (!yield(unescapePointerToken(((@string)p).slice(0, (nint)(i))))) {
                return;
            }
            p = p.slice((nint)(i));
        }
    };
}

} partial class jsontext_package {

internal static @string unescapePointerToken(@string token) {
    if (strings.Contains(token, "~")) {
        token = strings.ReplaceAll(token, "~1", "/");
        token = strings.ReplaceAll(token, "~0", "~");
    }
    return token;
}

} partial class jsontextᴛstate {

internal static slice<byte> appendStackPointer(this state sʗp, slice<byte> b, nint where) {
    ref var s = ref heap(sʗp, out var Ꮡs);

    nint objectDepth = default;
    for (nint i = 1; i < s.Tokens.Depth(); i++) {
        var e = Ꮡs.ᏑTokens.index(i);
        nint arrayDelta = -1;
        var isLast = i == s.Tokens.Depth() - 1; if (isLast) {
            switch (ᐧ) {
            case {} when where < 0 && e.Length() == 0 || where == 0 && !e.needObjectValue() || where > 0 && e.NeedObjectName(): {
                return b;
            }
            case {} when where > 0 && e.isArray(): {
                arrayDelta = 0;
                break;
            }}

        }
        switch (ᐧ) {
        case {} when e.isObject(): {
            b = appendEscapePointerName(append(b, (byte)'/'), s.Names.getUnquoted(objectDepth));
            objectDepth++;
            break;
        }
        case {} when e.isArray(): {
            b = strconv.AppendUint(append(b, (byte)'/'), (uint64)(e.Length() + (int64)arrayDelta), 10);
            break;
        }}

    }
    return b;
}

} partial class jsontext_package {

internal static slice<byte> appendEscapePointerName(slice<byte> b, slice<byte> name) {
    foreach (var (_, r) in ((@string)name)) {
        switch (r) {
        case '~': {
            b = append(b, ((@string)"~0").ꓸꓸꓸ);
            break;
        }
        case '/': {
            b = append(b, ((@string)"~1").ꓸꓸꓸ);
            break;
        }
        default: {
            b = utf8.AppendRune(b, r);
            break;
        }}

    }
    return b;
}

public partial struct stateMachine {
    public slice<stateEntry> Stack;
    public stateEntry Last;
}

} partial class jsontextᴛstateMachine {

internal static void reset(this ref stateMachine m) {
    m.Stack = m.Stack[..0];
    if (cap(m.Stack) > 1 << 10) {
        m.Stack = default;
    }
    m.Last = stateTypeArray;
}

public static nint Depth(this stateMachine m) {
    return len(m.Stack) + 1;
}

internal static ж<stateEntry> index(this ж<stateMachine> Ꮡm, nint i) {
    ref var m = ref Ꮡm.DerefOrNull();

    if (i == len(m.Stack)) {
        return Ꮡm.ᏑLast;
    }
    return Ꮡ(m.Stack, i);
}

public static (nint, int64) DepthLength(this stateMachine m) {
    return (m.Depth(), m.Last.Length());
}

internal static error appendLiteral(this ref stateMachine m) {
    switch (ᐧ) {
    case {} when m.Last.NeedObjectName(): {
        return ErrNonStringName;
    }
    case {} when !m.Last.isValidNamespace(): {
        return errInvalidNamespace;
    }
    default: {
        m.Last.Increment();
        return default;
    }}

}

internal static error appendString(this ref stateMachine m) {
    switch (ᐧ) {
    case {} when !m.Last.isValidNamespace(): {
        return errInvalidNamespace;
    }
    default: {
        m.Last.Increment();
        return default;
    }}

}

internal static error appendNumber(this ref stateMachine m) {
    return m.appendLiteral();
}

internal static error pushObject(this ref stateMachine m) {
    switch (ᐧ) {
    case {} when m.Last.NeedObjectName(): {
        return ErrNonStringName;
    }
    case {} when !m.Last.isValidNamespace(): {
        return errInvalidNamespace;
    }
    case {} when len(m.Stack) == maxNestingDepth: {
        return errMaxDepth;
    }
    default: {
        m.Last.Increment();
        m.Stack = append(m.Stack, m.Last);
        m.Last = stateTypeObject;
        return default;
    }}

}

internal static error popObject(this ref stateMachine m) {
    switch (ᐧ) {
    case {} when !m.Last.isObject(): {
        return errMismatchDelim;
    }
    case {} when m.Last.needObjectValue(): {
        return errMissingValue;
    }
    case {} when !m.Last.isValidNamespace(): {
        return errInvalidNamespace;
    }
    default: {
        m.Last = m.Stack[len(m.Stack) - 1];
        m.Stack = m.Stack.slice(0, len(m.Stack) - 1);
        return default;
    }}

}

internal static error pushArray(this ref stateMachine m) {
    switch (ᐧ) {
    case {} when m.Last.NeedObjectName(): {
        return ErrNonStringName;
    }
    case {} when !m.Last.isValidNamespace(): {
        return errInvalidNamespace;
    }
    case {} when len(m.Stack) == maxNestingDepth: {
        return errMaxDepth;
    }
    default: {
        m.Last.Increment();
        m.Stack = append(m.Stack, m.Last);
        m.Last = stateTypeArray;
        return default;
    }}

}

internal static error popArray(this ref stateMachine m) {
    switch (ᐧ) {
    case {} when !m.Last.isArray() || len(m.Stack) == 0: {
        return errMismatchDelim;
    }
    case {} when !m.Last.isValidNamespace(): {
        return errInvalidNamespace;
    }
    default: {
        m.Last = m.Stack[len(m.Stack) - 1];
        m.Stack = m.Stack.slice(0, len(m.Stack) - 1);
        return default;
    }}

}

public static nint /*n*/ NeedIndent(this stateMachine m, Kind next) {
    var willEnd = next == (rune)'}' || next == (rune)']';
    switch (ᐧ) {
    case {} when m.Depth() is 1: {
        return 0;
    }
    case {} when m.Last.Length() == 0 && willEnd: {
        return 0;
    }
    case {} when m.Last.Length() == 0 || m.Last.needImplicitComma(next): {
        return m.Depth();
    }
    case {} when willEnd: {
        return m.Depth() - 1;
    }
    default: {
        return 0;
    }}

}

public static slice<byte> MayAppendDelim(this stateMachine m, slice<byte> b, Kind next) {
    switch (ᐧ) {
    case {} when m.Last.needImplicitColon(): {
        return append(b, (byte)':');
    }
    case {} when m.Last.needImplicitComma(next) && len(m.Stack) != 0: {
        return append(b, (byte)',');
    }
    default: {
        return b;
    }}

}

internal static byte /*delim*/ needDelim(this stateMachine m, Kind next) {
    switch (ᐧ) {
    case {} when m.Last.needImplicitColon(): {
        return (byte)':';
    }
    case {} when m.Last.needImplicitComma(next) && len(m.Stack) != 0: {
        return (byte)',';
    }
    default: {
        return 0;
    }}

}

public static void InvalidateDisabledNamespaces(this ж<stateMachine> Ꮡm) {
    foreach (var i in range(Ꮡm.Depth())) {
        var e = Ꮡm.index(i);
        if (!e.isActiveNamespace()) {
            e.invalidateNamespace();
        }
    }
}

} partial class jsontext_package {

public partial struct stateEntry /*num:uint64*/;

internal static stateEntry stateTypeMask => unchecked((stateEntry)0x8000_0000_0000_0000);
internal static stateEntry stateTypeObject => unchecked((stateEntry)0x8000_0000_0000_0000);
internal static stateEntry stateTypeArray => 0x0000_0000_0000_0000;
internal static stateEntry stateNamespaceMask => unchecked((stateEntry)0x6000_0000_0000_0000);
internal static stateEntry stateDisableNamespace => unchecked((stateEntry)0x4000_0000_0000_0000);
internal static stateEntry stateInvalidNamespace => unchecked((stateEntry)0x2000_0000_0000_0000);
internal static stateEntry stateCountMask => unchecked((stateEntry)0x1fff_ffff_ffff_ffff);
internal static stateEntry stateCountLSBMask => 0x0000_0000_0000_0001;
internal static stateEntry stateCountOdd => 0x0000_0000_0000_0001;
internal static stateEntry stateCountEven => 0x0000_0000_0000_0000;

} partial class jsontextᴛstateEntry {

public static int64 Length(this stateEntry e) {
    return (int64)((stateEntry)(e & stateCountMask));
}

internal static bool isObject(this stateEntry e) {
    return (stateEntry)(e & stateTypeMask) == stateTypeObject;
}

internal static bool isArray(this stateEntry e) {
    return (stateEntry)(e & stateTypeMask) == stateTypeArray;
}

public static bool NeedObjectName(this stateEntry e) {
    return (stateEntry)(e & ((stateEntry)(stateTypeMask | stateCountLSBMask))) == (stateEntry)(stateTypeObject | stateCountEven);
}

internal static bool needImplicitColon(this stateEntry e) {
    return e.needObjectValue();
}

internal static bool needObjectValue(this stateEntry e) {
    return (stateEntry)(e & ((stateEntry)(stateTypeMask | stateCountLSBMask))) == (stateEntry)(stateTypeObject | stateCountOdd);
}

internal static bool needImplicitComma(this stateEntry e, Kind next) {
    return !e.needObjectValue() && e.Length() > 0 && next != (rune)'}' && next != (rune)']';
}

public static void Increment(this ref stateEntry e) {
    (e)++;
}

internal static void decrement(this ref stateEntry e) {
    (e)--;
}

public static void DisableNamespace(this ref stateEntry e) {
    e |= (stateEntry)(stateDisableNamespace);
}

internal static bool isActiveNamespace(this stateEntry e) {
    return (stateEntry)(e & (stateDisableNamespace)) == 0;
}

internal static void invalidateNamespace(this ref stateEntry e) {
    e |= (stateEntry)(stateInvalidNamespace);
}

internal static bool isValidNamespace(this stateEntry e) {
    return (stateEntry)(e & (stateInvalidNamespace)) == 0;
}

} partial class jsontext_package {

public partial struct objectNameStack {
    internal slice<nint> offsets;
    internal slice<byte> unquotedNames;
}

} partial class jsontextᴛobjectNameStack {

internal static void reset(this ref objectNameStack ns) {
    ns.offsets = ns.offsets[..0];
    ns.unquotedNames = ns.unquotedNames[..0];
    if (cap(ns.offsets) > 1 << 6) {
        ns.offsets = default;
    }
    if (cap(ns.unquotedNames) > 1 << 10) {
        ns.unquotedNames = default;
    }
}

internal static nint length(this ref objectNameStack ns) {
    return len(ns.offsets);
}

internal static slice<byte> getUnquoted(this ref objectNameStack ns, nint i) {
    ns.ensureCopiedBuffer();
    if (i == 0) {
        return ns.unquotedNames.slice(0, ns.offsets[0]);
    } else {
        return ns.unquotedNames.slice(ns.offsets[i - 1], ns.offsets[i]);
    }
}

} partial class jsontext_package {

internal static UntypedInt invalidOffset => /* math.MinInt */ -9223372036854775808;

} partial class jsontextᴛobjectNameStack {

internal static void push(this ref objectNameStack ns) {
    ns.offsets = append(ns.offsets, (nint)(invalidOffset));
}

public static void ReplaceLastQuotedOffset(this ref objectNameStack ns, nint i) {
    ns.offsets[len(ns.offsets) - 1] = ~i;
}

internal static void replaceLastUnquotedName(this ref objectNameStack ns, @string s) {
    ns.ensureCopiedBuffer();
    nint startOffset = default;
    if (len(ns.offsets) > 1) {
        startOffset = ns.offsets[len(ns.offsets) - 2];
    }
    ns.unquotedNames = append(ns.unquotedNames.slice(0, startOffset), s.ꓸꓸꓸ);
    ns.offsets[len(ns.offsets) - 1] = len(ns.unquotedNames);
}

internal static void clearLast(this ref objectNameStack ns) {
    ns.offsets[len(ns.offsets) - 1] = invalidOffset;
}

internal static void pop(this ref objectNameStack ns) {
    ns.offsets = ns.offsets.slice(0, len(ns.offsets) - 1);
}

internal static void copyQuotedBuffer(this ref objectNameStack ns, slice<byte> b) {
    nint i = default;
    for (i = len(ns.offsets) - 1; i >= 0 && ns.offsets[i] < 0; i--) {
        continue;
    }

    for (i = i + 1; i < len(ns.offsets); i++) {
        if (i == len(ns.offsets) - 1 && ns.offsets[i] == invalidOffset) {
            if (i == 0) {
                ns.offsets[i] = 0;
            } else {
                ns.offsets[i] = ns.offsets[i - 1];
            }
            break;
        }

        var quotedName = b.slice(~ns.offsets[i]);
        if (quotedName[0] == invalidateBufferByte) {
            quotedName[0] = (byte)'"';
        }

        nint startOffset = default;
        if (i > 0) {
            startOffset = ns.offsets[i - 1];
        }
        nint n = jsonwire.ConsumeSimpleString(quotedName); if (n > 0) {
            ns.unquotedNames = appendꓸꓸꓸ(ns.unquotedNames.slice(0, startOffset), quotedName.slice(len(@""""), n - len(@"""")));
        } else {
            (ns.unquotedNames, _) = jsonwire.AppendUnquote(ns.unquotedNames.slice(0, startOffset), quotedName);
        }
        ns.offsets[i] = len(ns.unquotedNames);
    }
}

internal static void ensureCopiedBuffer(this ref objectNameStack ns) {
    if (len(ns.offsets) > 0 && ns.offsets[len(ns.offsets) - 1] < 0) {
        throw panic("BUG: copyQuotedBuffer not called beforehand");
    }
}

} partial class jsontext_package {

public partial struct objectNamespaceStack /*[]objectNamespace*/;

} partial class jsontextᴛobjectNamespaceStack {

internal static void reset(this ref objectNamespaceStack nss) {
    if (cap(nss) > 1 << 10) {
        nss = default;
    }
    nss = (nss)[..0];
}

internal static void push(this ж<objectNamespaceStack> Ꮡnss) {
    ref var nss = ref Ꮡnss.DerefOrNull();

    if (cap(nss) > len(nss)) {
        nss = (nss).slice(0, len(nss) + 1);
        Ꮡnss.Last().reset();
    } else {
        nss = append(nss, new objectNamespace());
    }
}

public static ж<objectNamespace> Last(this objectNamespaceStack nss) {
    return Ꮡ(nss, len(nss) - 1);
}

internal static void pop(this ref objectNamespaceStack nss) {
    nss = (nss).slice(0, len(nss) - 1);
}

} partial class jsontext_package {

public partial struct objectNamespace {
    internal slice<nuint> endOffsets;
    internal slice<byte> allUnquotedNames;
    internal map<@string, EmptyStruct> mapNames;
}

} partial class jsontextᴛobjectNamespace {

internal static void reset(this ref objectNamespace ns) {
    ns.endOffsets = ns.endOffsets[..0];
    ns.allUnquotedNames = ns.allUnquotedNames[..0];
    ns.mapNames = default;
    if (cap(ns.endOffsets) > 1 << 6) {
        ns.endOffsets = default;
    }
    if (cap(ns.allUnquotedNames) > 1 << 10) {
        ns.allUnquotedNames = default;
    }
}

internal static nint length(this ref objectNamespace ns) {
    return len(ns.endOffsets);
}

internal static slice<byte> getUnquoted(this ref objectNamespace ns, nint i) {
    if (i == 0) {
        return ns.allUnquotedNames.slice(0, (nint)(ns.endOffsets[0]));
    } else {
        return ns.allUnquotedNames.slice((nint)(ns.endOffsets[i - 1]), (nint)(ns.endOffsets[i]));
    }
}

internal static slice<byte> lastUnquoted(this ref objectNamespace ns) {
    return ns.getUnquoted(ns.length() - 1);
}

internal static bool insertQuoted(this ref objectNamespace ns, slice<byte> name, bool isVerbatim) {
    if (isVerbatim) {
        name = name.slice(len(@""""), len(name) - len(@""""));
    }
    return ns.insert(name, !isVerbatim);
}
public static bool InsertUnquoted(this ref objectNamespace ns, slice<byte> name) {
    return ns.insert(name, false);
}
internal static bool insert(this ref objectNamespace ns, slice<byte> name, bool quoted) {
    slice<byte> allNames = default;
    if (quoted) {
        (allNames, _) = jsonwire.AppendUnquote(ns.allUnquotedNames, name);
    } else {
        allNames = appendꓸꓸꓸ(ns.allUnquotedNames, name);
    }
    name = allNames.slice(len(ns.allUnquotedNames));

    if (ns.mapNames == nil && (ns.length() > 64 || len(ns.allUnquotedNames) > 1024)) {
        ns.mapNames = new map<@string, EmptyStruct>();
        nuint startOffset = default;
        foreach (var (_, endOffset) in range(ns.endOffsets)) {
            var nameΔ1 = ns.allUnquotedNames.slice((nint)(startOffset), (nint)(endOffset));
            ns.mapNames[((@string)nameΔ1)] = new EmptyStruct();
            startOffset = endOffset;
        }
    }

    if (ns.mapNames == nil) {
        nuint startOffset = default;
        foreach (var (_, endOffset) in range(ns.endOffsets)) {
            if (((sstring)(ns.allUnquotedNames.slice((nint)(startOffset), (nint)(endOffset)))) == ((sstring)name)) {
                return false;
            }
            startOffset = endOffset;
        }
    } else {
        var (_, ok) = ns.mapNames[tmpstring(name), ꟷ]; if (ok) {
            return false;
        }
        ns.mapNames[((@string)name)] = new EmptyStruct();
    }

    ns.allUnquotedNames = allNames;
    ns.endOffsets = append(ns.endOffsets, (nuint)len(ns.allUnquotedNames));
    return true;
}

internal static void removeLast(this ref objectNamespace ns) {
    if (ns.mapNames != nil) {
        delete(ns.mapNames, (@string)ns.lastUnquoted());
    }
    if (ns.length() - 1 == 0) {
        ns.endOffsets = ns.endOffsets[..0];
        ns.allUnquotedNames = ns.allUnquotedNames[..0];
    } else {
        ns.endOffsets = ns.endOffsets.slice(0, ns.length() - 1);
        ns.allUnquotedNames = ns.allUnquotedNames.slice(0, (nint)(ns.endOffsets[ns.length() - 1]));
    }
}

} // end jsontextᴛobjectNamespace
