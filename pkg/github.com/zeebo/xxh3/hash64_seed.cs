namespace go.github.com.zeebo;

using bits = math.bits_package;
using math;

partial class xxh3_package {

public static uint64 HashSeed(slice<byte> b, uint64 seed) {
    return hashAnySeed(bytesOf(b), seed);
}

public static uint64 HashStringSeed(@string s, uint64 seed) {
    return hashAnySeed(stringOf(s), seed);
}

internal static u64 /*acc*/ hashAnySeed(str s, uint64 seed) {
    u64 acc = default;

    var p = s.p;
    nuint l = s.l;

    switch (ᐧ) {
    case {} when l is <= 16: {
        switch (ᐧ) {
        case {} when l is > 8: {
            var inputlo = readU64(p, 0) ^ ((u64)(key64_024 ^ key64_032) + seed);
            var inputhi = readU64(p, ((ui)l) - 8) ^ ((u64)(key64_040 ^ key64_048) - seed);
            var folded = mulFold64(inputlo, inputhi);
            return xxh3Avalanche(((u64)l) + bits.ReverseBytes64(inputlo) + inputhi + folded);
        }
        case {} when l is > 3: {
            seed ^= (u64)((((u64)bits.ReverseBytes32((u32)seed)) << 32));
            var input1 = readU32(p, 0);
            var input2 = readU32(p, ((ui)l) - 4);
            var input64 = ((u64)input2) + (((u64)input1) << 32);
            var keyed = (u64)(input64 ^ ((u64)(key64_008 ^ key64_016) - seed));
            return rrmxmx(keyed, (u64)l);
        }
        case {} when l is 3: {
            var c12 = (u64)readU16(p, 0);
            var c3 = (u64)readU8(p, 2);
            acc = (c12 << 16) + c3 + ((uint64)3 << 8);
            break;
        }
        case {} when l is > 1: {
            var c12 = (u64)readU16(p, 0);
            acc = (c12 * ((1 << 24) + 1) >> 8) + ((uint64)2 << 8);
            break;
        }
        case {} when l is 1: {
            var c1 = (u64)readU8(p, 0);
            acc = c1 * ((1 << 24) + (1 << 16) + 1) + ((uint64)1 << 8);
            break;
        }
        default: {
            return xxhAvalancheSmall(seed ^ key64_056 ^ key64_064);
        }}

        acc ^= (u64)(((u64)((u32)(key32_000 ^ key32_004))) + seed);
        return xxhAvalancheSmall(acc);
    }
    case {} when l is <= 128: {
        acc = ((u64)l) * (uint64)prime64_1;

        if (l > 32) {
            if (l > 64) {
                if (l > 96) {
                    acc += mulFold64(readU64(p, 6 * 8) ^ (key64_096 + seed), readU64(p, 7 * 8) ^ (key64_104 - seed));
                    acc += mulFold64(readU64(p, ((ui)l) - 8 * 8) ^ (key64_112 + seed), readU64(p, ((ui)l) - 7 * 8) ^ (key64_120 - seed));
                }
                acc += mulFold64(readU64(p, 4 * 8) ^ (key64_064 + seed), readU64(p, 5 * 8) ^ (key64_072 - seed));
                acc += mulFold64(readU64(p, ((ui)l) - 6 * 8) ^ (key64_080 + seed), readU64(p, ((ui)l) - 5 * 8) ^ (key64_088 - seed));
            }
            acc += mulFold64(readU64(p, 2 * 8) ^ (key64_032 + seed), readU64(p, 3 * 8) ^ (key64_040 - seed));
            acc += mulFold64(readU64(p, ((ui)l) - 4 * 8) ^ (key64_048 + seed), readU64(p, ((ui)l) - 3 * 8) ^ (key64_056 - seed));
        }
        acc += mulFold64(readU64(p, 0 * 8) ^ (key64_000 + seed), readU64(p, 1 * 8) ^ (key64_008 - seed));
        acc += mulFold64(readU64(p, ((ui)l) - 2 * 8) ^ (key64_016 + seed), readU64(p, ((ui)l) - 1 * 8) ^ (key64_024 - seed));

        return xxh3Avalanche(acc);
    }
    case {} when l is <= 240: {
        acc = ((u64)l) * (uint64)prime64_1;

        acc += mulFold64(readU64(p, 0 * 16 + 0) ^ (key64_000 + seed), readU64(p, 0 * 16 + 8) ^ (key64_008 - seed));
        acc += mulFold64(readU64(p, 1 * 16 + 0) ^ (key64_016 + seed), readU64(p, 1 * 16 + 8) ^ (key64_024 - seed));
        acc += mulFold64(readU64(p, 2 * 16 + 0) ^ (key64_032 + seed), readU64(p, 2 * 16 + 8) ^ (key64_040 - seed));
        acc += mulFold64(readU64(p, 3 * 16 + 0) ^ (key64_048 + seed), readU64(p, 3 * 16 + 8) ^ (key64_056 - seed));
        acc += mulFold64(readU64(p, 4 * 16 + 0) ^ (key64_064 + seed), readU64(p, 4 * 16 + 8) ^ (key64_072 - seed));
        acc += mulFold64(readU64(p, 5 * 16 + 0) ^ (key64_080 + seed), readU64(p, 5 * 16 + 8) ^ (key64_088 - seed));
        acc += mulFold64(readU64(p, 6 * 16 + 0) ^ (key64_096 + seed), readU64(p, 6 * 16 + 8) ^ (key64_104 - seed));
        acc += mulFold64(readU64(p, 7 * 16 + 0) ^ (key64_112 + seed), readU64(p, 7 * 16 + 8) ^ (key64_120 - seed));

        acc = xxh3Avalanche(acc);

        var top = (ui)(((ui)l) & ~(ui)15);
        for (var i = (ui)(8 * 16); i < top; i += 16) {
            acc += mulFold64(readU64(p, i + 0) ^ (readU64(key, i - 125) + seed), readU64(p, i + 8) ^ (readU64(key, i - 117) - seed));
        }

        acc += mulFold64(readU64(p, ((ui)l) - 16) ^ (key64_119 + seed), readU64(p, ((ui)l) - 8) ^ (key64_127 - seed));

        return xxh3Avalanche(acc);
    }
    default: {
        acc = ((u64)l) * (uint64)prime64_1;

        var secret = key;
        if (seed != 0) {
            secret = new ptr(b: new slice<byte>(secretSize));
            initSecret(secret, seed);
        }

        var accs = new u64[]{
            prime32_3, prime64_1, prime64_2, prime64_3,
            prime64_4, prime32_2, prime64_5, prime32_1
        }.array();

        if (hasAVX512 && l >= avx512Switch) {
            accumAVX512(ref accs, p, secret, (u64)l);
        } else if (hasAVX2) {
            accumAVX2(ref accs, p, secret, (u64)l);
        } else if (hasSSE2) {
            accumSSE(ref accs, p, secret, (u64)l);
        } else if (hasNEON) {
            accumNEON(ref accs, p, secret, (u64)l);
        } else {
            accumScalarSeed(ref accs, p, secret, (u64)l);
        }

        acc += mulFold64((u64)(accs[0] ^ readU64(secret, 11)), (u64)(accs[1] ^ readU64(secret, 19)));
        acc += mulFold64((u64)(accs[2] ^ readU64(secret, 27)), (u64)(accs[3] ^ readU64(secret, 35)));
        acc += mulFold64((u64)(accs[4] ^ readU64(secret, 43)), (u64)(accs[5] ^ readU64(secret, 51)));
        acc += mulFold64((u64)(accs[6] ^ readU64(secret, 59)), (u64)(accs[7] ^ readU64(secret, 67)));

        return xxh3Avalanche(acc);
    }}

}

} // end xxh3_package
