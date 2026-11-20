// Copyright 2023 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

global using element = go.fmt_package.Stringer;

namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using errors = errors_package;
using fmt = fmt_package;
using strings = strings_package;
using utf8 = unicode.utf8_package;
using io = io_package;
using unicode;
using static glob_package;

partial class glob_package {

partial struct Glob {
    internal slice<element> elems;
}

public static (ж<Glob>, error) Parse(@string pattern) {
    var (g, _, err) = parse(pattern, false);
    return (g, err);
}

internal static (ж<Glob>, @string, error) parse(@string pattern, bool nested) {
    var g = @new<Glob>();
    while (len(pattern) > 0) {
        switch (pattern[0]) {
        case (byte)'/': {
            pattern = pattern[1..];
            g.elems = append(g.elems, (element)(new slash()));
            break;
        }
        case (byte)'*': {
            if (len(pattern) > 1 && pattern[1] == '*') {
                if ((len(g.elems) > 0 && !AreEqual(g.elems[len(g.elems) - 1], new slash())) || (len(pattern) > 2 && pattern[2] != '/')) {
                    return (default, "", errors.New("** may only be adjacent to '/'"));
                }
                pattern = pattern[2..];
                g.elems = append(g.elems, (element)(new starStar()));
                break;
            }
            pattern = pattern[1..];
            g.elems = append(g.elems, (element)(new star()));
            break;
        }
        case (byte)'?': {
            pattern = pattern[1..];
            g.elems = append(g.elems, (element)(new anyChar()));
            break;
        }
        case (byte)'{': {
            group gs = default;
            while (pattern[0] != '}') {
                pattern = pattern[1..];
                var (groupG, pat, err) = parse(pattern, true);
                if (err != nil) {
                    return (default, "", err);
                }
                if (len(pat) == 0) {
                    return (default, "", errors.New("unmatched '{'"));
                }
                pattern = pat;
                gs = append(gs, groupG);
            }
            pattern = pattern[1..];
            g.elems = append(g.elems, (element)(gs));
            break;
        }
        case (byte)'}' or (byte)',': {
            if (nested) {
                return (g, pattern, default);
            }
            pattern = g.parseLiteral(pattern, false);
            break;
        }
        case (byte)'[': {
            pattern = pattern[1..];
            if (len(pattern) == 0) {
                return (default, "", errBadRange);
            }
            var negate = false;
            if (pattern[0] == '!') {
                pattern = pattern[1..];
                negate = true;
            }
            var (low, sz, err) = readRangeRune(pattern);
            if (err != nil) {
                return (default, "", err);
            }
            pattern = pattern.slice(sz);
            if (len(pattern) == 0 || pattern[0] != '-') {
                return (default, "", errBadRange);
            }
            pattern = pattern[1..];
            (var high, sz, err) = readRangeRune(pattern);
            if (err != nil) {
                return (default, "", err);
            }
            pattern = pattern.slice(sz);
            if (len(pattern) == 0 || pattern[0] != ']') {
                return (default, "", errBadRange);
            }
            pattern = pattern[1..];
            g.elems = append(g.elems, (element)(new charRange(negate, low, high)));
            break;
        }
        default: {
            pattern = g.parseLiteral(pattern, nested);
            break;
        }}

    }
    return (g, "", default);
}

internal static (rune, nint, error) readRangeRune(@string input) {
    var (r, sz) = utf8.DecodeRuneInString(input);
    error err = default;
    if (r == utf8.RuneError) {
        switch (sz) {
        case 0: {
            err = errBadRange;
            break;
        }
        case 1: {
            err = errInvalidUTF8;
            break;
        }}

    }
    return (r, sz, err);
}

internal static error errBadRange = errors.New("'[' patterns must be of the form [x-y]");
internal static error errInvalidUTF8 = errors.New("invalid UTF-8 encoding");

} partial class globᴛGlob {

internal static @string parseLiteral(this ref Glob g, @string pattern, bool nested) {
    @string specialChars = default;
    if (nested) {
        specialChars = "*?{[/},";
    } else {
        specialChars = "*?{[/";
    }
    nint end = strings.IndexAny(pattern, specialChars);
    if (end == -1) {
        end = len(pattern);
    }
    g.elems = append(g.elems, (element)((literal)(pattern.slice(0, end))));
    return pattern.slice(end);
}

public static @string String(this ref Glob g) {
    ref var b = ref heap(new strings.Builder(), out var Ꮡb);
    foreach (var (_, e) in range(g.elems)) {
        fmt.Fprint(new strings_BuilderжWriter(Ꮡb), e);
    }
    return b.String();
}

} partial class glob_package {

partial struct slash {
}

partial struct literal /*@string*/;

partial struct star {
}

partial struct anyChar {
}

partial struct starStar {
}

partial struct group /*[]ж<Glob>*/;

partial struct charRange {
    internal bool negate;
    internal rune low, high;
}

} partial class globᴛslash {

internal static @string String(this slash s) { return "/"; }

} partial class globᴛliteral {

internal static @string String(this literal l) { return (@string)l; }

} partial class globᴛstar {

internal static @string String(this star s) { return "*"; }

} partial class globᴛanyChar {

internal static @string String(this anyChar a) { return "?"; }

} partial class globᴛstarStar {

internal static @string String(this starStar s) { return "**"; }

} partial class globᴛgroup {

internal static @string String(this group g) {
    var partsᴷ = new stackAppend<@string>();

    slice<@string> parts = default;
    foreach (var (_, gΔ1) in range<ж<Glob>>(g)) {
        parts = append(partsᴷ, parts, gΔ1.String());
    }
    return "{"u8 + strings.Join(parts, ",") + "}"u8;
}

} partial class globᴛcharRange {

internal static @string String(this charRange r) {
    return "["u8 + ((@string)r.low) + "-"u8 + ((@string)r.high) + "]"u8;
}

} partial class globᴛGlob {

public static bool Match(this ref Glob g, @string input) {
    return match(g.elems, input);
}

} partial class glob_package {

internal static bool /*ok*/ match(slice<element> elems, @string input) {
    any elem = default;
    while (len(elems) > 0) {
        (elem, elems) = (elems[0], elems[1..]);
        switch (elem.type()) {
        case slash elemΔ1: {
            if (len(input) == 0 || input[0] != '/') {
                return false;
            }
            while (input[0] == '/') {
                input = input[1..];
            }
            break;
        }
        case starStar elemΔ1: {
            if (len(elems) > 0) {
                elems = elems[1..];
            }

            if (len(elems) == 0) {
                return true;
            }

            while (len(input) != 0) {
                if (match(elems, input)) {
                    return true;
                }
                (_, input) = split(input);
            }
            return false;
        }
        case literal elemΔ1: {
            if (!strings.HasPrefix(input, (@string)elemΔ1)) {
                return false;
            }
            input = input.slice(len(elemΔ1));
            break;
        }
        case star elemΔ1: {
            @string segInput = default;
            (segInput, input) = split(input);

            nint elemEnd = len(elems);
            foreach (var (i, e) in range(elems)) {
                if (AreEqual(e, (new slash()))) {
                    elemEnd = i;
                    break;
                }
            }
            var segElems = elems.slice(0, elemEnd);
            elems = elems.slice(elemEnd);

            if (len(segElems) == 0) {
                break;
            }

            var matched = false;
            foreach (var i in range(len(segInput))) {
                if (match(segElems, segInput.slice(i))) {
                    matched = true;
                    break;
                }
            }
            if (!matched) {
                return false;
            }
            break;
        }
        case anyChar elemΔ1: {
            if (len(input) == 0 || input[0] == '/') {
                return false;
            }
            input = input[1..];
            break;
        }
        case group elemΔ1: {
            slice<element> branch = default;
            foreach (var (_, m) in range<ж<Glob>>(elemΔ1)) {
                branch = branch[..0];
                branch = appendꓸꓸꓸ(branch, m.elems);
                branch = appendꓸꓸꓸ(branch, elems);
                if (match(branch, input)) {
                    return true;
                }
            }
            return false;
        }
        case charRange elemΔ1: {
            if (len(input) == 0 || input[0] == '/') {
                return false;
            }
            var (c, sz) = utf8.DecodeRuneInString(input);
            if (c < elemΔ1.low || c > elemΔ1.high) {
                return false;
            }
            input = input.slice(sz);
            break;
        }
        default: {
            var elemΔ1 = elem;
            throw panic(fmt.Sprintf("segment type %T not implemented"u8, elemΔ1));
            break;
        }}
    }

    return len(input) == 0;
}

internal static (@string first, @string rest) split(@string input) {
    @string first = default;

    nint i = strings.IndexByte(input, (byte)'/');
    if (i < 0) {
        return (input, "");
    }
    first = input.slice(0, i);
    for (nint j = i; j < len(input); j++) {
        if (input[j] != '/') {
            return (first, input.slice(j));
        }
    }
    return (first, "");
}

} // end glob_package
