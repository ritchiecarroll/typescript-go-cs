namespace go.github.com.zeebo;

using bits = math.bits_package;
using math;

partial class xxh3_package {

public static Uint128 Hash128(slice<byte> b) {
    return hashAny128(bytesOf(b));
}

public static Uint128 HashString128(@string s) {
    return hashAny128(stringOf(s));
}

internal static u128 /*acc*/ hashAny128(str s) {
    u128 acc = default;

    var p = s.p;
    nuint l = s.l;

    switch (ᐧ) {
    case {} when l is <= 16: {
        switch (ᐧ) {
        case {} when l is > 8: {
            const u64 bitflipl = /* key64_032 ^ key64_040 */ 6455697860950631241;
            const u64 bitfliph = /* key64_048 ^ key64_056 */ 13979869743488056664;

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
            const u64 bitflip = /* key64_016 ^ key64_024 */ 14190881133394760876;

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
            return new u128(0x99aa06d3014798d8UL, 0x6001c324468d497fUL);
        }}

        acc.Hi = (uint64)bits.RotateLeft32(bits.ReverseBytes32((uint32)acc.Lo), 13);
        acc.Lo ^= (uint64)((uint64)((u32)(key32_000 ^ key32_004)));
        acc.Hi ^= (uint64)((uint64)((u32)(key32_008 ^ key32_012)));

        acc.Lo = xxh64AvalancheSmall(acc.Lo);
        acc.Hi = xxh64AvalancheSmall(acc.Hi);

        return acc;
    }
    case {} when l is <= 128: {
        acc.Lo = ((u64)l) * (uint64)prime64_1;

        if (l > 32) {
            if (l > 64) {
                if (l > 96) {
                    var (in8, in7) = (readU64(p, ((ui)l) - 8 * 8), readU64(p, ((ui)l) - 7 * 8));
                    var (i6, i7) = (readU64(p, 6 * 8), readU64(p, 7 * 8));

                    acc.Hi += mulFold64(in8 ^ key64_112, in7 ^ key64_120);
                    acc.Hi ^= (uint64)(i6 + i7);
                    acc.Lo += mulFold64(i6 ^ key64_096, i7 ^ key64_104);
                    acc.Lo ^= (uint64)(in8 + in7);
                }

                var (in6, in5) = (readU64(p, ((ui)l) - 6 * 8), readU64(p, ((ui)l) - 5 * 8));
                var (i4, i5) = (readU64(p, 4 * 8), readU64(p, 5 * 8));

                acc.Hi += mulFold64(in6 ^ key64_080, in5 ^ key64_088);
                acc.Hi ^= (uint64)(i4 + i5);
                acc.Lo += mulFold64(i4 ^ key64_064, i5 ^ key64_072);
                acc.Lo ^= (uint64)(in6 + in5);
            }

            var (in4, in3) = (readU64(p, ((ui)l) - 4 * 8), readU64(p, ((ui)l) - 3 * 8));
            var (i2, i3) = (readU64(p, 2 * 8), readU64(p, 3 * 8));

            acc.Hi += mulFold64(in4 ^ key64_048, in3 ^ key64_056);
            acc.Hi ^= (uint64)(i2 + i3);
            acc.Lo += mulFold64(i2 ^ key64_032, i3 ^ key64_040);
            acc.Lo ^= (uint64)(in4 + in3);
        }

        var (in2, in1) = (readU64(p, ((ui)l) - 2 * 8), readU64(p, ((ui)l) - 1 * 8));
        var (i0, i1) = (readU64(p, 0 * 8), readU64(p, 1 * 8));

        acc.Hi += mulFold64(in2 ^ key64_016, in1 ^ key64_024);
        acc.Hi ^= (uint64)(i0 + i1);
        acc.Lo += mulFold64(i0 ^ key64_000, i1 ^ key64_008);
        acc.Lo ^= (uint64)(in2 + in1);

        (acc.Hi, acc.Lo) = ((acc.Lo * (uint64)prime64_1) + (acc.Hi * (uint64)prime64_4) + (((u64)l) * (uint64)prime64_2), acc.Hi + acc.Lo);

        acc.Hi = ((u64)0 - xxh3Avalanche(acc.Hi));
        acc.Lo = xxh3Avalanche(acc.Lo);

        return acc;
    }
    case {} when l is <= 240: {
        acc.Lo = ((u64)l) * (uint64)prime64_1;

        {
            var (i0, i1, i2, i3) = (readU64(p, 0 * 8), readU64(p, 1 * 8), readU64(p, 2 * 8), readU64(p, 3 * 8));

            acc.Hi += mulFold64(i2 ^ key64_016, i3 ^ key64_024);
            acc.Hi ^= (uint64)(i0 + i1);
            acc.Lo += mulFold64(i0 ^ key64_000, i1 ^ key64_008);
            acc.Lo ^= (uint64)(i2 + i3);
        }

        {
            var (i0, i1, i2, i3) = (readU64(p, 4 * 8), readU64(p, 5 * 8), readU64(p, 6 * 8), readU64(p, 7 * 8));

            acc.Hi += mulFold64(i2 ^ key64_048, i3 ^ key64_056);
            acc.Hi ^= (uint64)(i0 + i1);
            acc.Lo += mulFold64(i0 ^ key64_032, i1 ^ key64_040);
            acc.Lo ^= (uint64)(i2 + i3);
        }

        {
            var (i0, i1, i2, i3) = (readU64(p, 8 * 8), readU64(p, 9 * 8), readU64(p, 10 * 8), readU64(p, 11 * 8));

            acc.Hi += mulFold64(i2 ^ key64_080, i3 ^ key64_088);
            acc.Hi ^= (uint64)(i0 + i1);
            acc.Lo += mulFold64(i0 ^ key64_064, i1 ^ key64_072);
            acc.Lo ^= (uint64)(i2 + i3);
        }

        {
            var (i0, i1, i2, i3) = (readU64(p, 12 * 8), readU64(p, 13 * 8), readU64(p, 14 * 8), readU64(p, 15 * 8));

            acc.Hi += mulFold64(i2 ^ key64_112, i3 ^ key64_120);
            acc.Hi ^= (uint64)(i0 + i1);
            acc.Lo += mulFold64(i0 ^ key64_096, i1 ^ key64_104);
            acc.Lo ^= (uint64)(i2 + i3);
        }

        acc.Hi = xxh3Avalanche(acc.Hi);
        acc.Lo = xxh3Avalanche(acc.Lo);

        var top = (ui)(((ui)l) & ~(ui)31);
        for (var i = (ui)(4 * 32); i < top; i += 32) {
            var (i0, i1, i2, i3) = (readU64(p, i + 0), readU64(p, i + 8), readU64(p, i + 16), readU64(p, i + 24));
            var (k0, k1, k2, k3) = (readU64(key, i - 125), readU64(key, i - 117), readU64(key, i - 109), readU64(key, i - 101));

            acc.Hi += mulFold64(i2 ^ k2, i3 ^ k3);
            acc.Hi ^= (uint64)(i0 + i1);
            acc.Lo += mulFold64(i0 ^ k0, i1 ^ k1);
            acc.Lo ^= (uint64)(i2 + i3);
        }

        {
            var (i0, i1, i2, i3) = (readU64(p, ((ui)l) - 32), readU64(p, ((ui)l) - 24), readU64(p, ((ui)l) - 16), readU64(p, ((ui)l) - 8));

            acc.Hi += mulFold64(i0 ^ key64_119, i1 ^ key64_127);
            acc.Hi ^= (uint64)(i2 + i3);
            acc.Lo += mulFold64(i2 ^ key64_103, i3 ^ key64_111);
            acc.Lo ^= (uint64)(i0 + i1);
        }

        (acc.Hi, acc.Lo) = ((acc.Lo * (uint64)prime64_1) + (acc.Hi * (uint64)prime64_4) + (((u64)l) * (uint64)prime64_2), acc.Hi + acc.Lo);

        acc.Hi = ((u64)0 - xxh3Avalanche(acc.Hi));
        acc.Lo = xxh3Avalanche(acc.Lo);

        return acc;
    }
    default: {
        acc.Lo = ((u64)l) * (uint64)prime64_1;
        acc.Hi = ~(((u64)l) * (uint64)prime64_2);

        var accs = new u64[]{
            prime32_3, prime64_1, prime64_2, prime64_3,
            prime64_4, prime32_2, prime64_5, prime32_1
        }.array();

        if (hasAVX512 && l >= avx512Switch) {
            accumAVX512(ref accs, p, key, (u64)l);
        } else if (hasAVX2) {
            accumAVX2(ref accs, p, key, (u64)l);
        } else if (hasSSE2) {
            accumSSE(ref accs, p, key, (u64)l);
        } else if (hasNEON) {
            accumNEON(ref accs, p, key, (u64)l);
        } else {
            accumScalar(ref accs, p, key, (u64)l);
        }

        acc.Lo += mulFold64((u64)(accs[0] ^ (uint64)key64_011), (u64)(accs[1] ^ (uint64)key64_019));
        acc.Hi += mulFold64((u64)(accs[0] ^ (uint64)key64_117), (u64)(accs[1] ^ (uint64)key64_125));

        acc.Lo += mulFold64((u64)(accs[2] ^ (uint64)key64_027), (u64)(accs[3] ^ (uint64)key64_035));
        acc.Hi += mulFold64((u64)(accs[2] ^ (uint64)key64_133), (u64)(accs[3] ^ (uint64)key64_141));

        acc.Lo += mulFold64((u64)(accs[4] ^ (uint64)key64_043), (u64)(accs[5] ^ (uint64)key64_051));
        acc.Hi += mulFold64((u64)(accs[4] ^ (uint64)key64_149), (u64)(accs[5] ^ (uint64)key64_157));

        acc.Lo += mulFold64((u64)(accs[6] ^ (uint64)key64_059), (u64)(accs[7] ^ (uint64)key64_067));
        acc.Hi += mulFold64((u64)(accs[6] ^ (uint64)key64_165), (u64)(accs[7] ^ (uint64)key64_173));

        acc.Lo = xxh3Avalanche(acc.Lo);
        acc.Hi = xxh3Avalanche(acc.Hi);

        return acc;
    }}

}

} // end xxh3_package
