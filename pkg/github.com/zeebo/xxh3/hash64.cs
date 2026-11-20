namespace go.github.com.zeebo;

using bits = math.bits_package;
using math;

partial class xxh3_package {

public static uint64 Hash(slice<byte> b) {
    return hashAny(bytesOf(b));
}

public static uint64 HashString(@string s) {
    return hashAny(stringOf(s));
}

internal static u64 /*acc*/ hashAny(str s) {
    u64 acc = default;

    var p = s.p;
    nuint l = s.l;

    switch (ᐧ) {
    case {} when l is <= 16: {
        switch (ᐧ) {
        case {} when l is > 8: {
            var inputlo = (uint64)(readU64(p, 0) ^ ((u64)(key64_024 ^ key64_032)));
            var inputhi = (uint64)(readU64(p, ((ui)l) - 8) ^ ((u64)(key64_040 ^ key64_048)));
            var folded = mulFold64(inputlo, inputhi);
            return xxh3Avalanche(((u64)l) + bits.ReverseBytes64(inputlo) + inputhi + folded);
        }
        case {} when l is > 3: {
            var input1 = readU32(p, 0);
            var input2 = readU32(p, ((ui)l) - 4);
            var input64 = ((u64)input2) + (((u64)input1) << 32);
            var keyed = (u64)(input64 ^ ((u64)(key64_008 ^ key64_016)));
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
            return 0x2d06800538d394c2UL;
        }}

        acc ^= (u64)(((u64)((u32)(key32_000 ^ key32_004))));
        return xxhAvalancheSmall(acc);
    }
    case {} when l is <= 128: {
        acc = ((u64)l) * (uint64)prime64_1;

        if (l > 32) {
            if (l > 64) {
                if (l > 96) {
                    acc += mulFold64(readU64(p, 6 * 8) ^ key64_096, readU64(p, 7 * 8) ^ key64_104);
                    acc += mulFold64(readU64(p, ((ui)l) - 8 * 8) ^ key64_112, readU64(p, ((ui)l) - 7 * 8) ^ key64_120);
                }
                acc += mulFold64(readU64(p, 4 * 8) ^ key64_064, readU64(p, 5 * 8) ^ key64_072);
                acc += mulFold64(readU64(p, ((ui)l) - 6 * 8) ^ key64_080, readU64(p, ((ui)l) - 5 * 8) ^ key64_088);
            }
            acc += mulFold64(readU64(p, 2 * 8) ^ key64_032, readU64(p, 3 * 8) ^ key64_040);
            acc += mulFold64(readU64(p, ((ui)l) - 4 * 8) ^ key64_048, readU64(p, ((ui)l) - 3 * 8) ^ key64_056);
        }
        acc += mulFold64(readU64(p, 0 * 8) ^ key64_000, readU64(p, 1 * 8) ^ key64_008);
        acc += mulFold64(readU64(p, ((ui)l) - 2 * 8) ^ key64_016, readU64(p, ((ui)l) - 1 * 8) ^ key64_024);

        return xxh3Avalanche(acc);
    }
    case {} when l is <= 240: {
        acc = ((u64)l) * (uint64)prime64_1;

        acc += mulFold64(readU64(p, 0 * 16 + 0) ^ key64_000, readU64(p, 0 * 16 + 8) ^ key64_008);
        acc += mulFold64(readU64(p, 1 * 16 + 0) ^ key64_016, readU64(p, 1 * 16 + 8) ^ key64_024);
        acc += mulFold64(readU64(p, 2 * 16 + 0) ^ key64_032, readU64(p, 2 * 16 + 8) ^ key64_040);
        acc += mulFold64(readU64(p, 3 * 16 + 0) ^ key64_048, readU64(p, 3 * 16 + 8) ^ key64_056);
        acc += mulFold64(readU64(p, 4 * 16 + 0) ^ key64_064, readU64(p, 4 * 16 + 8) ^ key64_072);
        acc += mulFold64(readU64(p, 5 * 16 + 0) ^ key64_080, readU64(p, 5 * 16 + 8) ^ key64_088);
        acc += mulFold64(readU64(p, 6 * 16 + 0) ^ key64_096, readU64(p, 6 * 16 + 8) ^ key64_104);
        acc += mulFold64(readU64(p, 7 * 16 + 0) ^ key64_112, readU64(p, 7 * 16 + 8) ^ key64_120);

        acc = xxh3Avalanche(acc);

        var top = (ui)(((ui)l) & ~(ui)15);
        for (var i = (ui)(8 * 16); i < top; i += 16) {
            acc += mulFold64(readU64(p, i + 0) ^ readU64(key, i - 125), readU64(p, i + 8) ^ readU64(key, i - 117));
        }

        acc += mulFold64(readU64(p, ((ui)l) - 16) ^ key64_119, readU64(p, ((ui)l) - 8) ^ key64_127);

        return xxh3Avalanche(acc);
    }
    default: {
        acc = ((u64)l) * (uint64)prime64_1;

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

        acc += mulFold64((u64)(accs[0] ^ (uint64)key64_011), (u64)(accs[1] ^ (uint64)key64_019));
        acc += mulFold64((u64)(accs[2] ^ (uint64)key64_027), (u64)(accs[3] ^ (uint64)key64_035));
        acc += mulFold64((u64)(accs[4] ^ (uint64)key64_043), (u64)(accs[5] ^ (uint64)key64_051));
        acc += mulFold64((u64)(accs[6] ^ (uint64)key64_059), (u64)(accs[7] ^ (uint64)key64_067));

        return xxh3Avalanche(acc);
    }}

}

} // end xxh3_package
