namespace go.github.com.zeebo;

using bits = math.bits_package;
using math;

partial class xxh3_package {

public static Uint128 Hash128Seed(slice<byte> b, uint64 seed) {
    return hashAny128Seed(bytesOf(b), seed);
}

public static Uint128 HashString128Seed(@string s, uint64 seed) {
    return hashAny128Seed(stringOf(s), seed);
}

internal static u128 /*acc*/ hashAny128Seed(str s, uint64 seed) {
    u128 acc = default;

    var p = s.p;
    nuint l = s.l;

    switch (ᐧ) {
    case {} when l is <= 16: {
        switch (ᐧ) {
        case {} when l is > 8: {
            var bitflipl = ((u64)(key64_032 ^ key64_040)) - seed;
            var bitfliph = ((u64)(key64_048 ^ key64_056)) + seed;

            var input_lo = readU64(p, 0);
            var input_hi = readU64(p, ((ui)l) - 8);

            var (m128_h, m128_l) = bits.Mul64(input_lo ^ input_hi ^ bitflipl, prime64_1);

            m128_l += ((uint64)(l - 1) << 54);
            input_hi ^= (u64)(bitfliph);

            m128_h += input_hi + (uint64)(uint32)input_hi * (uint64)(prime32_2 - 1);

            m128_l ^= (uint64)(bits.ReverseBytes64(m128_h));

            (acc.Hi, acc.Lo) = bits.Mul64(m128_l, prime64_2);
            acc.Hi += m128_h * (uint64)prime64_2;

            acc.Lo = xxh3Avalanche(acc.Lo);
            acc.Hi = xxh3Avalanche(acc.Hi);

            return acc;
        }
        case {} when l is > 3: {
            seed ^= (u64)((((u64)bits.ReverseBytes32((u32)seed)) << 32));
            var bitflip = ((u64)(key64_016 ^ key64_024)) + seed;
            var input_lo = readU32(p, 0);
            var input_hi = readU32(p, ((ui)l) - 4);
            var input_64 = ((u64)input_lo) + (((u64)input_hi) << 32);
            var keyed = (u64)(input_64 ^ bitflip);

            (acc.Hi, acc.Lo) = bits.Mul64(keyed, (uint64)prime64_1 + ((uint64)l << 2));

            acc.Hi += (acc.Lo << 1);
            acc.Lo ^= (uint64)((acc.Hi >> 3));

            acc.Lo ^= (uint64)((acc.Lo >> 35));
            acc.Lo *= 0x9fb21c651e98df25UL;
            acc.Lo ^= (uint64)((acc.Lo >> 28));
            acc.Hi = xxh3Avalanche(acc.Hi);

            return acc;
        }
        case {} when l is 3: {
            var c12 = (u64)readU16(p, 0);
            var c3 = (u64)readU8(p, 2);
            acc.Lo = (c12 << 16) + c3 + ((uint64)3 << 8);
            break;
        }
        case {} when l is > 1: {
            var c12 = (u64)readU16(p, 0);
            acc.Lo = (c12 * ((1 << 24) + 1) >> 8) + ((uint64)2 << 8);
            break;
        }
        case {} when l is 1: {
            var c1 = (u64)readU8(p, 0);
            acc.Lo = c1 * ((1 << 24) + (1 << 16) + 1) + ((uint64)1 << 8);
            break;
        }
        default: {
            var bitflipl = (u64)((u64)(key64_064 ^ key64_072) ^ seed);
            var bitfliph = (u64)((u64)(key64_080 ^ key64_088) ^ seed);
            return new u128(Lo: xxh64AvalancheFull(bitflipl), Hi: xxh64AvalancheFull(bitfliph));
        }}

        acc.Hi = (uint64)bits.RotateLeft32(bits.ReverseBytes32((uint32)acc.Lo), 13);
        acc.Lo ^= (uint64)((uint64)((u32)(key32_000 ^ key32_004)) + seed);
        acc.Hi ^= (uint64)((uint64)((u32)(key32_008 ^ key32_012)) - seed);

        acc.Lo = xxh64AvalancheFull(acc.Lo);
        acc.Hi = xxh64AvalancheFull(acc.Hi);

        return acc;
    }
    case {} when l is <= 128: {
        acc.Lo = ((u64)l) * (uint64)prime64_1;

        if (l > 32) {
            if (l > 64) {
                if (l > 96) {
                    var (in8, in7) = (readU64(p, ((ui)l) - 8 * 8), readU64(p, ((ui)l) - 7 * 8));
                    var (i6, i7) = (readU64(p, 6 * 8), readU64(p, 7 * 8));

                    acc.Hi += mulFold64(in8 ^ (key64_112 + seed), in7 ^ (key64_120 - seed));
                    acc.Hi ^= (uint64)(i6 + i7);
                    acc.Lo += mulFold64(i6 ^ (key64_096 + seed), i7 ^ (key64_104 - seed));
                    acc.Lo ^= (uint64)(in8 + in7);
                }

                var (in6, in5) = (readU64(p, ((ui)l) - 6 * 8), readU64(p, ((ui)l) - 5 * 8));
                var (i4, i5) = (readU64(p, 4 * 8), readU64(p, 5 * 8));

                acc.Hi += mulFold64(in6 ^ (key64_080 + seed), in5 ^ (key64_088 - seed));
                acc.Hi ^= (uint64)(i4 + i5);
                acc.Lo += mulFold64(i4 ^ (key64_064 + seed), i5 ^ (key64_072 - seed));
                acc.Lo ^= (uint64)(in6 + in5);
            }

            var (in4, in3) = (readU64(p, ((ui)l) - 4 * 8), readU64(p, ((ui)l) - 3 * 8));
            var (i2, i3) = (readU64(p, 2 * 8), readU64(p, 3 * 8));

            acc.Hi += mulFold64(in4 ^ (key64_048 + seed), in3 ^ (key64_056 - seed));
            acc.Hi ^= (uint64)(i2 + i3);
            acc.Lo += mulFold64(i2 ^ (key64_032 + seed), i3 ^ (key64_040 - seed));
            acc.Lo ^= (uint64)(in4 + in3);
        }

        var (in2, in1) = (readU64(p, ((ui)l) - 2 * 8), readU64(p, ((ui)l) - 1 * 8));
        var (i0, i1) = (readU64(p, 0 * 8), readU64(p, 1 * 8));

        acc.Hi += mulFold64(in2 ^ (key64_016 + seed), in1 ^ (key64_024 - seed));
        acc.Hi ^= (uint64)(i0 + i1);
        acc.Lo += mulFold64(i0 ^ (key64_000 + seed), i1 ^ (key64_008 - seed));
        acc.Lo ^= (uint64)(in2 + in1);

        (acc.Hi, acc.Lo) = ((acc.Lo * (uint64)prime64_1) + (acc.Hi * (uint64)prime64_4) + ((((u64)l) - seed) * (uint64)prime64_2), acc.Hi + acc.Lo);

        acc.Hi = ((u64)0 - xxh3Avalanche(acc.Hi));
        acc.Lo = xxh3Avalanche(acc.Lo);

        return acc;
    }
    case {} when l is <= 240: {
        acc.Lo = ((u64)l) * (uint64)prime64_1;

        {
            var (i0, i1, i2, i3) = (readU64(p, 0 * 8), readU64(p, 1 * 8), readU64(p, 2 * 8), readU64(p, 3 * 8));

            acc.Hi += mulFold64(i2 ^ (key64_016 + seed), i3 ^ (key64_024 - seed));
            acc.Hi ^= (uint64)(i0 + i1);
            acc.Lo += mulFold64(i0 ^ (key64_000 + seed), i1 ^ (key64_008 - seed));
            acc.Lo ^= (uint64)(i2 + i3);
        }

        {
            var (i0, i1, i2, i3) = (readU64(p, 4 * 8), readU64(p, 5 * 8), readU64(p, 6 * 8), readU64(p, 7 * 8));

            acc.Hi += mulFold64(i2 ^ (key64_048 + seed), i3 ^ (key64_056 - seed));
            acc.Hi ^= (uint64)(i0 + i1);
            acc.Lo += mulFold64(i0 ^ (key64_032 + seed), i1 ^ (key64_040 - seed));
            acc.Lo ^= (uint64)(i2 + i3);
        }

        {
            var (i0, i1, i2, i3) = (readU64(p, 8 * 8), readU64(p, 9 * 8), readU64(p, 10 * 8), readU64(p, 11 * 8));

            acc.Hi += mulFold64(i2 ^ (key64_080 + seed), i3 ^ (key64_088 - seed));
            acc.Hi ^= (uint64)(i0 + i1);
            acc.Lo += mulFold64(i0 ^ (key64_064 + seed), i1 ^ (key64_072 - seed));
            acc.Lo ^= (uint64)(i2 + i3);
        }

        {
            var (i0, i1, i2, i3) = (readU64(p, 12 * 8), readU64(p, 13 * 8), readU64(p, 14 * 8), readU64(p, 15 * 8));

            acc.Hi += mulFold64(i2 ^ (key64_112 + seed), i3 ^ (key64_120 - seed));
            acc.Hi ^= (uint64)(i0 + i1);
            acc.Lo += mulFold64(i0 ^ (key64_096 + seed), i1 ^ (key64_104 - seed));
            acc.Lo ^= (uint64)(i2 + i3);
        }

        acc.Hi = xxh3Avalanche(acc.Hi);
        acc.Lo = xxh3Avalanche(acc.Lo);

        var top = (ui)(((ui)l) & ~(ui)31);
        for (var i = (ui)(4 * 32); i < top; i += 32) {
            var (i0, i1, i2, i3) = (readU64(p, i + 0), readU64(p, i + 8), readU64(p, i + 16), readU64(p, i + 24));
            var (k0, k1, k2, k3) = (readU64(key, i - 125) + seed, readU64(key, i - 117) - seed, readU64(key, i - 109) + seed, readU64(key, i - 101) - seed);

            acc.Hi += mulFold64(i2 ^ k2, i3 ^ k3);
            acc.Hi ^= (uint64)(i0 + i1);
            acc.Lo += mulFold64(i0 ^ k0, i1 ^ k1);
            acc.Lo ^= (uint64)(i2 + i3);
        }

        {
            var (i0, i1, i2, i3) = (readU64(p, ((ui)l) - 32), readU64(p, ((ui)l) - 24), readU64(p, ((ui)l) - 16), readU64(p, ((ui)l) - 8));

            var seedΔ2 = 0 - seed;
            acc.Hi += mulFold64(i0 ^ (key64_119 + seedΔ2), i1 ^ (key64_127 - seedΔ2));
            acc.Hi ^= (uint64)(i2 + i3);
            acc.Lo += mulFold64(i2 ^ (key64_103 + seedΔ2), i3 ^ (key64_111 - seedΔ2));
            acc.Lo ^= (uint64)(i0 + i1);
        }

        (acc.Hi, acc.Lo) = ((acc.Lo * (uint64)prime64_1) + (acc.Hi * (uint64)prime64_4) + ((((u64)l) - seed) * (uint64)prime64_2), acc.Hi + acc.Lo);

        acc.Hi = ((u64)0 - xxh3Avalanche(acc.Hi));
        acc.Lo = xxh3Avalanche(acc.Lo);

        return acc;
    }
    default: {
        acc.Lo = ((u64)l) * (uint64)prime64_1;
        acc.Hi = ~(((u64)l) * (uint64)prime64_2);

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
            accumScalar(ref accs, p, secret, (u64)l);
        }

        UntypedInt hi_off = /* 117 - 11 */ 106;

        acc.Lo += mulFold64((u64)(accs[0] ^ readU64(secret, 11)), (u64)(accs[1] ^ readU64(secret, 19)));
        acc.Hi += mulFold64((u64)(accs[0] ^ readU64(secret, 11 + hi_off)), (u64)(accs[1] ^ readU64(secret, 19 + hi_off)));

        acc.Lo += mulFold64((u64)(accs[2] ^ readU64(secret, 27)), (u64)(accs[3] ^ readU64(secret, 35)));
        acc.Hi += mulFold64((u64)(accs[2] ^ readU64(secret, 27 + hi_off)), (u64)(accs[3] ^ readU64(secret, 35 + hi_off)));

        acc.Lo += mulFold64((u64)(accs[4] ^ readU64(secret, 43)), (u64)(accs[5] ^ readU64(secret, 51)));
        acc.Hi += mulFold64((u64)(accs[4] ^ readU64(secret, 43 + hi_off)), (u64)(accs[5] ^ readU64(secret, 51 + hi_off)));

        acc.Lo += mulFold64((u64)(accs[6] ^ readU64(secret, 59)), (u64)(accs[7] ^ readU64(secret, 67)));
        acc.Hi += mulFold64((u64)(accs[6] ^ readU64(secret, 59 + hi_off)), (u64)(accs[7] ^ readU64(secret, 67 + hi_off)));

        acc.Lo = xxh3Avalanche(acc.Lo);
        acc.Hi = xxh3Avalanche(acc.Hi);

        return acc;
    }}

}

} // end xxh3_package
