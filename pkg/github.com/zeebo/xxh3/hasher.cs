namespace go.github.com.zeebo;

using binary = encoding.binary_package;
using hash = hash_package;
using encoding;
using static xxh3_package;

partial class xxh3_package {

partial struct Hasher {
    internal array<u64> acc = new(8);
    internal u64 blk;
    internal u64 len;
    internal ptr key;
    internal array<byte> buf = new(_block + _stripe);
    internal u64 seed;
}

internal static hash.Hash _ᴛ1ʗ = new HasherжHash(((ж<Hasher>)nil));
internal static hash.Hash64 _ᴛ2ʗ = new HasherжHash64(((ж<Hasher>)nil));

public static ж<Hasher> New() {
    return @new<Hasher>();
}

public static ж<Hasher> NewSeed(uint64 seed) {
    ref var h = ref heap(new Hasher(), out var Ꮡh);
    h.Reset();
    h.seed = seed;
    h.key = key;

    if (seed != 0) {
        h.key = new ptr(b: new slice<byte>(secretSize));
        initSecret(h.key, seed);
    }
    return Ꮡh;
}

} partial class xxh3ᴛHasher {

public static void Reset(this ref Hasher h) {
    h.acc.Assign(new u64[]{
        prime32_3, prime64_1, prime64_2, prime64_3,
        prime64_4, prime32_2, prime64_5, prime32_1
    }.array());
    h.blk = 0;
    h.len = 0;
}

public static void ResetSeed(this ref Hasher h, uint64 seed) {
    h.Reset();
    if (h.seed == 0 && seed != 0) {
        h.key = new ptr(b: new slice<byte>(secretSize));
    }
    if (seed == 0) {
        h.key = new ptr();
    } else if (seed != h.seed) {
        initSecret(h.key, seed);
    }
    h.seed = seed;
}

public static nint BlockSize(this ref Hasher h) { return _stripe; }

public static nint Size(this ref Hasher h) { return 8; }

public static slice<byte> Sum(this ref Hasher h, slice<byte> b) {
    array<byte> tmp = new(8);
    binary.BigEndian.PutUint64(tmp[..], h.Sum64());
    return appendꓸꓸꓸ(b, tmp[..]);
}

public static (nint, error) Write(this ж<Hasher> Ꮡh, slice<byte> buf) {
    Ꮡh.update(buf);
    return (len(buf), default);
}

public static (nint, error) WriteString(this ж<Hasher> Ꮡh, @string buf) {
    Ꮡh.updateString(buf);
    return (len(buf), default);
}

internal static void update(this ж<Hasher> Ꮡh, slice<byte> buf) {
    Ꮡh.updateString((@string)buf);
}

internal static void updateString(this ж<Hasher> Ꮡh, @string buf) {
    ref var h = ref Ꮡh.DerefOrNull();

    if (h.key.b == nil) {
        h.key = key;
        h.Reset();
    }

    while (h.len == 0 && len(buf) > len(h.buf)) {
        if (hasAVX512) {
            accumBlockAVX512(ref nonnil(ref h).acc, new ptr(b: slice<byte>(buf[..(int)(_block)])), h.key);
        } else if (hasAVX2) {
            accumBlockAVX2(ref nonnil(ref h).acc, new ptr(b: slice<byte>(buf[..(int)(_block)])), h.key);
        } else if (hasSSE2) {
            accumBlockSSE(ref nonnil(ref h).acc, new ptr(b: slice<byte>(buf[..(int)(_block)])), h.key);
        } else if (hasNEON) {
            accumBlockNEON(ref nonnil(ref h).acc, new ptr(b: slice<byte>(buf[..(int)(_block)])), h.key);
        } else {
            accumBlockScalar(ref nonnil(ref h).acc, new ptr(b: slice<byte>(buf[..(int)(_block)])), h.key);
        }
        buf = buf[(int)(_block)..];
        h.blk++;
    }

    while (len(buf) > 0) {
        if (h.len < ((u64)len(h.buf))) {
            nint n = copy(h.buf.slice((nint)(h.len)), buf);
            h.len += ((u64)n);
            buf = buf.slice(n);
            continue;
        }

        if (hasAVX512) {
            accumBlockAVX512(ref nonnil(ref h).acc, new ptr(b: h.buf[..]), h.key);
        } else if (hasAVX2) {
            accumBlockAVX2(ref nonnil(ref h).acc, new ptr(b: h.buf[..]), h.key);
        } else if (hasSSE2) {
            accumBlockSSE(ref nonnil(ref h).acc, new ptr(b: h.buf[..]), h.key);
        } else if (hasNEON) {
            accumBlockNEON(ref nonnil(ref h).acc, new ptr(b: h.buf[..]), h.key);
        } else {
            accumBlockScalar(ref nonnil(ref h).acc, new ptr(b: h.buf[..]), h.key);
        }

        h.blk++;
        h.len = _stripe;
        copy(h.buf[..(int)(_stripe)], h.buf[(int)(_block)..]);
    }
}

public static uint64 Sum64(this ref Hasher h) {
    if (h.key.b == nil) {
        h.key = key;
        h.Reset();
    }

    if (h.blk == 0) {
        if (h.seed == 0) {
            return Hash(h.buf.slice(0, (nint)(h.len)));
        }
        return HashSeed(h.buf.slice(0, (nint)(h.len)), h.seed);
    }

    var l = h.blk * (uint64)_block + h.len;
    var acc = l * (uint64)prime64_1;
    var accs = h.acc.Clone();

    if (h.len > 0) {
        if (hasAVX2) {
            accumAVX2(ref accs, new ptr(b: h.buf[..]), h.key, h.len);
        } else if (hasSSE2) {
            accumSSE(ref accs, new ptr(b: h.buf[..]), h.key, h.len);
        } else if (hasNEON) {
            accumNEON(ref accs, new ptr(b: h.buf[..]), h.key, h.len);
        } else {
            accumScalar(ref accs, new ptr(b: h.buf[..]), h.key, h.len);
        }
    }

    if (h.seed == 0) {
        acc += mulFold64((u64)(accs[0] ^ (uint64)key64_011), (u64)(accs[1] ^ (uint64)key64_019));
        acc += mulFold64((u64)(accs[2] ^ (uint64)key64_027), (u64)(accs[3] ^ (uint64)key64_035));
        acc += mulFold64((u64)(accs[4] ^ (uint64)key64_043), (u64)(accs[5] ^ (uint64)key64_051));
        acc += mulFold64((u64)(accs[6] ^ (uint64)key64_059), (u64)(accs[7] ^ (uint64)key64_067));
    } else {
        var secret = h.key;
        acc += mulFold64((u64)(accs[0] ^ readU64(secret, 11)), (u64)(accs[1] ^ readU64(secret, 19)));
        acc += mulFold64((u64)(accs[2] ^ readU64(secret, 27)), (u64)(accs[3] ^ readU64(secret, 35)));
        acc += mulFold64((u64)(accs[4] ^ readU64(secret, 43)), (u64)(accs[5] ^ readU64(secret, 51)));
        acc += mulFold64((u64)(accs[6] ^ readU64(secret, 59)), (u64)(accs[7] ^ readU64(secret, 67)));
    }

    acc = xxh3Avalanche(acc);

    return acc;
}

public static Uint128 Sum128(this ref Hasher h) {
    if (h.key.b == nil) {
        h.key = key;
        h.Reset();
    }

    if (h.blk == 0) {
        if (h.seed == 0) {
            return Hash128(h.buf.slice(0, (nint)(h.len)));
        }
        return Hash128Seed(h.buf.slice(0, (nint)(h.len)), h.seed);
    }

    var l = h.blk * (uint64)_block + h.len;
    var acc = new Uint128(Lo: l * (uint64)prime64_1, Hi: ~(l * (uint64)prime64_2));
    var accs = h.acc.Clone();

    if (h.len > 0) {
        if (hasAVX2) {
            accumAVX2(ref accs, new ptr(b: h.buf[..]), h.key, h.len);
        } else if (hasSSE2) {
            accumSSE(ref accs, new ptr(b: h.buf[..]), h.key, h.len);
        } else if (hasNEON) {
            accumNEON(ref accs, new ptr(b: h.buf[..]), h.key, h.len);
        } else {
            accumScalar(ref accs, new ptr(b: h.buf[..]), h.key, h.len);
        }
    }

    if (h.seed == 0) {
        acc.Lo += mulFold64((u64)(accs[0] ^ (uint64)key64_011), (u64)(accs[1] ^ (uint64)key64_019));
        acc.Hi += mulFold64((u64)(accs[0] ^ (uint64)key64_117), (u64)(accs[1] ^ (uint64)key64_125));

        acc.Lo += mulFold64((u64)(accs[2] ^ (uint64)key64_027), (u64)(accs[3] ^ (uint64)key64_035));
        acc.Hi += mulFold64((u64)(accs[2] ^ (uint64)key64_133), (u64)(accs[3] ^ (uint64)key64_141));

        acc.Lo += mulFold64((u64)(accs[4] ^ (uint64)key64_043), (u64)(accs[5] ^ (uint64)key64_051));
        acc.Hi += mulFold64((u64)(accs[4] ^ (uint64)key64_149), (u64)(accs[5] ^ (uint64)key64_157));

        acc.Lo += mulFold64((u64)(accs[6] ^ (uint64)key64_059), (u64)(accs[7] ^ (uint64)key64_067));
        acc.Hi += mulFold64((u64)(accs[6] ^ (uint64)key64_165), (u64)(accs[7] ^ (uint64)key64_173));
    } else {
        var secret = h.key;
        UntypedInt hi_off = /* 117 - 11 */ 106;

        acc.Lo += mulFold64((u64)(accs[0] ^ readU64(secret, 11)), (u64)(accs[1] ^ readU64(secret, 19)));
        acc.Hi += mulFold64((u64)(accs[0] ^ readU64(secret, 11 + hi_off)), (u64)(accs[1] ^ readU64(secret, 19 + hi_off)));

        acc.Lo += mulFold64((u64)(accs[2] ^ readU64(secret, 27)), (u64)(accs[3] ^ readU64(secret, 35)));
        acc.Hi += mulFold64((u64)(accs[2] ^ readU64(secret, 27 + hi_off)), (u64)(accs[3] ^ readU64(secret, 35 + hi_off)));

        acc.Lo += mulFold64((u64)(accs[4] ^ readU64(secret, 43)), (u64)(accs[5] ^ readU64(secret, 51)));
        acc.Hi += mulFold64((u64)(accs[4] ^ readU64(secret, 43 + hi_off)), (u64)(accs[5] ^ readU64(secret, 51 + hi_off)));

        acc.Lo += mulFold64((u64)(accs[6] ^ readU64(secret, 59)), (u64)(accs[7] ^ readU64(secret, 67)));
        acc.Hi += mulFold64((u64)(accs[6] ^ readU64(secret, 59 + hi_off)), (u64)(accs[7] ^ readU64(secret, 67 + hi_off)));
    }

    acc.Lo = xxh3Avalanche(acc.Lo);
    acc.Hi = xxh3Avalanche(acc.Hi);

    return acc;
}

} // end xxh3ᴛHasher
