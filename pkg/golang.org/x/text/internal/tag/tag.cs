// Copyright 2015 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.golang.org.x.text.@internal;

using sort = sort_package;
using static tag_package;

partial class tag_package {

partial struct Index /*@string*/;

} partial class tagᴛIndex {

public static @string Elem(this Index s, nint x) {
    return (@string)(s.slice(x * 4, x * 4 + 4));
}

public static nint Index(this Index s, slice<byte> key) {
    nint n = len(key);
    var keyʗ1 = key;
    nint index = sort.Search(len(s) / 4, (nint iΔ1) => cmp(s.slice(iΔ1 * 4, iΔ1 * 4 + n), keyʗ1) != -1);
    nint i = index * 4;
    if (cmp(s.slice(i, i + len(key)), key) != 0) {
        return -1;
    }
    return index;
}

public static nint Next(this Index s, slice<byte> key, nint x) {
    x++; if (x * 4 < len(s) && cmp(s.slice(x * 4, x * 4 + len(key)), key) == 0) {
        return x;
    }
    return -1;
}

} partial class tag_package {

internal static nint cmp(Index a, slice<byte> b) {
    nint n = len(a);
    if (len(b) < n) {
        n = len(b);
    }
    foreach (var (i, c) in range(b.slice(0, n))) {
        switch (ᐧ) {
        case {} when a[i] > c: {
            return 1;
        }
        case {} when a[i] < c: {
            return -1;
        }}

    }
    switch (ᐧ) {
    case {} when len(a) < len(b): {
        return -1;
    }
    case {} when len(a) > len(b): {
        return 1;
    }}

    return 0;
}

public static nint Compare(@string a, slice<byte> b) {
    return cmp((Index)a, b);
}

public static bool FixCase(@string form, slice<byte> b) {
    if (len(form) != len(b)) {
        return false;
    }
    foreach (var (i, vᴛ1) in range(b)) {
        var c = vᴛ1;

        if (form[i] <= 'Z') {
            if (c >= 'a') {
                c -= (byte)((rune)'z' - (rune)'Z');
            }
            if (c < 'A' || 'Z' < c) {
                return false;
            }
        } else {
            if (c <= 'Z') {
                c += (byte)((rune)'z' - (rune)'Z');
            }
            if (c < 'a' || 'z' < c) {
                return false;
            }
        }
        b[i] = c;
    }
    return true;
}

} // end tag_package
