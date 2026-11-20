// Copyright 2022 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

global using stringCache = go.array<go.@string>;

namespace go.github.com.go_json_experiment;

using binary = go.encoding.binary_package;
using bits = go.math.bits_package;
using go.encoding;
using go.math;

partial class json_package {

internal static @string makeString(/*[256]*/ ж<stringCache> Ꮡc, slice<byte> b) {
    ref var c = ref Ꮡc.DerefOrNull();

    const nint minCachedLen = 2;
    const nint maxCachedLen = 256;
    if (Ꮡc == nil || len(b) < minCachedLen || len(b) > maxCachedLen) {
        return (@string)b;
    }

    uint32 h = default;
    switch (ᐧ) {
    case {} when len(b) is >= 8: {
        var lo = binary.LittleEndian.Uint64(b[..8]);
        var hi = binary.LittleEndian.Uint64(b.slice(len(b) - 8));
        h = hash64((uint32)lo, (uint32)(lo >> 32)) ^ hash64((uint32)hi, (uint32)(hi >> 32));
        break;
    }
    case {} when len(b) is >= 4: {
        var lo = binary.LittleEndian.Uint32(b[..4]);
        var hi = binary.LittleEndian.Uint32(b.slice(len(b) - 4));
        h = hash64(lo, hi);
        break;
    }
    case {} when len(b) is >= 2: {
        var lo = binary.LittleEndian.Uint16(b[..2]);
        var hi = binary.LittleEndian.Uint16(b.slice(len(b) - 2));
        h = hash64((uint32)lo, (uint32)hi);
        break;
    }}

    var i = h % (uint32)len(c);
    @string sΔ1 = (c)[i]; if (sΔ1 == ((sstring)b)) {
        return sΔ1;
    }
    @string s = (@string)b;
    (c)[i] = s;
    return s;
}

internal static uint32 hash64(uint32 lo, uint32 hi) {
    UntypedInt prime1 = 0x9e3779b1;
    const uint32 prime2 = 0x85ebca77;
    const uint32 prime3 = 0xc2b2ae3d;
    const uint32 prime4 = 0x27d4eb2f;
    UntypedInt prime5 = 0x165667b1;
    var h = (uint32)prime5 + (uint32)8;
    h += lo * prime3;
    h = bits.RotateLeft32(h, 17) * prime4;
    h += hi * prime3;
    h = bits.RotateLeft32(h, 17) * prime4;
    const bool avalanche = false;
    if (avalanche) {
        h ^= (uint32)((h >> 15));
        h *= prime2;
        h ^= (uint32)((h >> 13));
        h *= prime3;
        h ^= (uint32)((h >> 16));
    }
    return h;
}

} // end json_package
