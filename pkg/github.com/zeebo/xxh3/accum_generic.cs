namespace go.github.com.zeebo;

partial class xxh3_package {

internal static UntypedInt avx512Switch => /* 1 << 10 */ 1024;

internal static void accumScalar(ref array<u64> accs, ptr p, ptr secret, u64 l) {
    if (!secret.def) {
        accumScalarSeed(ref accs, p, secret, l);
        return;
    }
    while (l > _block) {
        var k = secret;

        for (nint i = 0; i < 16; i++) {
            {
                UntypedInt off = 0;
                var dv0 = readU64(p, 8 * off);
                var dk0 = dv0 ^ readU64(k, 8 * off);
                var ac1 = dv0;
                var ac0 = (uint64)(uint32)dk0 * (dk0 >> 32);

                var dv1 = readU64(p, 8 * off + 8);
                var dk1 = dv1 ^ readU64(k, 8 * off + 8);
                ac0 += dv1;
                ac1 += (uint64)(uint32)dk1 * (dk1 >> 32);

                accs[off] += ac0;
                accs[off + 1] += ac1;
            }
            {
                UntypedInt off = 2;
                var dv0 = readU64(p, 8 * off);
                var dk0 = dv0 ^ readU64(k, 8 * off);
                var ac1 = dv0;
                var ac0 = (uint64)(uint32)dk0 * (dk0 >> 32);

                var dv1 = readU64(p, 8 * off + 8);
                var dk1 = dv1 ^ readU64(k, 8 * off + 8);
                ac0 += dv1;
                ac1 += (uint64)(uint32)dk1 * (dk1 >> 32);

                accs[off] += ac0;
                accs[off + 1] += ac1;
            }
            {
                UntypedInt off = 4;
                var dv0 = readU64(p, 8 * off);
                var dk0 = dv0 ^ readU64(k, 8 * off);
                var ac1 = dv0;
                var ac0 = (uint64)(uint32)dk0 * (dk0 >> 32);

                var dv1 = readU64(p, 8 * off + 8);
                var dk1 = dv1 ^ readU64(k, 8 * off + 8);
                ac0 += dv1;
                ac1 += (uint64)(uint32)dk1 * (dk1 >> 32);

                accs[off] += ac0;
                accs[off + 1] += ac1;
            }
            {
                UntypedInt off = 6;
                var dv0 = readU64(p, 8 * off);
                var dk0 = dv0 ^ readU64(k, 8 * off);
                var ac1 = dv0;
                var ac0 = (uint64)(uint32)dk0 * (dk0 >> 32);

                var dv1 = readU64(p, 8 * off + 8);
                var dk1 = dv1 ^ readU64(k, 8 * off + 8);
                ac0 += dv1;
                ac1 += (uint64)(uint32)dk1 * (dk1 >> 32);

                accs[off] += ac0;
                accs[off + 1] += ac1;
            }

            l -= _stripe;
            if (l > 0) {
                (p, k) = (p.add(_stripe), k.add(8));
            }
        }

        accs[0] ^= (u64)((accs[0] >> 47));
        accs[0] ^= (u64)(key64_128);
        accs[0] *= prime32_1;

        accs[1] ^= (u64)((accs[1] >> 47));
        accs[1] ^= (u64)(key64_136);
        accs[1] *= prime32_1;

        accs[2] ^= (u64)((accs[2] >> 47));
        accs[2] ^= (u64)(key64_144);
        accs[2] *= prime32_1;

        accs[3] ^= (u64)((accs[3] >> 47));
        accs[3] ^= (u64)(key64_152);
        accs[3] *= prime32_1;

        accs[4] ^= (u64)((accs[4] >> 47));
        accs[4] ^= (u64)(key64_160);
        accs[4] *= prime32_1;

        accs[5] ^= (u64)((accs[5] >> 47));
        accs[5] ^= (u64)(key64_168);
        accs[5] *= prime32_1;

        accs[6] ^= (u64)((accs[6] >> 47));
        accs[6] ^= (u64)(key64_176);
        accs[6] *= prime32_1;

        accs[7] ^= (u64)((accs[7] >> 47));
        accs[7] ^= (u64)(key64_184);
        accs[7] *= prime32_1;
    }

    if (l > 0) {
        var (t, k) = ((l - 1) / (uint64)_stripe, secret);

        for (var i = (u64)0; i < t; i++) {
            {
                UntypedInt off = 0;
                var dv0 = readU64(p, 8 * off);
                var dk0 = dv0 ^ readU64(k, 8 * off);
                var ac1 = dv0;
                var ac0 = (uint64)(uint32)dk0 * (dk0 >> 32);

                var dv1 = readU64(p, 8 * off + 8);
                var dk1 = dv1 ^ readU64(k, 8 * off + 8);
                ac0 += dv1;
                ac1 += (uint64)(uint32)dk1 * (dk1 >> 32);

                accs[off] += ac0;
                accs[off + 1] += ac1;
            }
            {
                UntypedInt off = 2;
                var dv0 = readU64(p, 8 * off);
                var dk0 = dv0 ^ readU64(k, 8 * off);
                var ac1 = dv0;
                var ac0 = (uint64)(uint32)dk0 * (dk0 >> 32);

                var dv1 = readU64(p, 8 * off + 8);
                var dk1 = dv1 ^ readU64(k, 8 * off + 8);
                ac0 += dv1;
                ac1 += (uint64)(uint32)dk1 * (dk1 >> 32);

                accs[off] += ac0;
                accs[off + 1] += ac1;
            }
            {
                UntypedInt off = 4;
                var dv0 = readU64(p, 8 * off);
                var dk0 = dv0 ^ readU64(k, 8 * off);
                var ac1 = dv0;
                var ac0 = (uint64)(uint32)dk0 * (dk0 >> 32);

                var dv1 = readU64(p, 8 * off + 8);
                var dk1 = dv1 ^ readU64(k, 8 * off + 8);
                ac0 += dv1;
                ac1 += (uint64)(uint32)dk1 * (dk1 >> 32);

                accs[off] += ac0;
                accs[off + 1] += ac1;
            }
            {
                UntypedInt off = 6;
                var dv0 = readU64(p, 8 * off);
                var dk0 = dv0 ^ readU64(k, 8 * off);
                var ac1 = dv0;
                var ac0 = (uint64)(uint32)dk0 * (dk0 >> 32);

                var dv1 = readU64(p, 8 * off + 8);
                var dk1 = dv1 ^ readU64(k, 8 * off + 8);
                ac0 += dv1;
                ac1 += (uint64)(uint32)dk1 * (dk1 >> 32);

                accs[off] += ac0;
                accs[off + 1] += ac1;
            }

            l -= _stripe;
            if (l > 0) {
                (p, k) = (p.add(_stripe), k.add(8));
            }
        }

        if (l > 0) {
            p = p.sub((ui)((uint64)_stripe - l));

            var dv0 = readU64(p, 8 * 0);
            var dk0 = dv0 ^ key64_121;
            accs[1] += dv0;
            accs[0] += (uint64)(uint32)dk0 * (dk0 >> 32);

            var dv1 = readU64(p, 8 * 1);
            var dk1 = dv1 ^ key64_129;
            accs[0] += dv1;
            accs[1] += (uint64)(uint32)dk1 * (dk1 >> 32);

            var dv2 = readU64(p, 8 * 2);
            var dk2 = dv2 ^ key64_137;
            accs[3] += dv2;
            accs[2] += (uint64)(uint32)dk2 * (dk2 >> 32);

            var dv3 = readU64(p, 8 * 3);
            var dk3 = dv3 ^ key64_145;
            accs[2] += dv3;
            accs[3] += (uint64)(uint32)dk3 * (dk3 >> 32);

            var dv4 = readU64(p, 8 * 4);
            var dk4 = dv4 ^ key64_153;
            accs[5] += dv4;
            accs[4] += (uint64)(uint32)dk4 * (dk4 >> 32);

            var dv5 = readU64(p, 8 * 5);
            var dk5 = dv5 ^ key64_161;
            accs[4] += dv5;
            accs[5] += (uint64)(uint32)dk5 * (dk5 >> 32);

            var dv6 = readU64(p, 8 * 6);
            var dk6 = dv6 ^ key64_169;
            accs[7] += dv6;
            accs[6] += (uint64)(uint32)dk6 * (dk6 >> 32);

            var dv7 = readU64(p, 8 * 7);
            var dk7 = dv7 ^ key64_177;
            accs[6] += dv7;
            accs[7] += (uint64)(uint32)dk7 * (dk7 >> 32);
        }
    }
}

internal static void accumBlockScalar(ref array<u64> accs, ptr p, ptr secret) {
    if (!secret.def) {
        accumBlockScalarSeed(ref accs, p, secret);
        return;
    }
    for (nint i = 0; i < 16; i++) {
        {
            UntypedInt off = 0;
            var dv0 = readU64(p, 8 * off);
            var dk0 = dv0 ^ readU64(secret, 8 * off);
            var ac1 = dv0;
            var ac0 = (uint64)(uint32)dk0 * (dk0 >> 32);

            var dv1 = readU64(p, 8 * off + 8);
            var dk1 = dv1 ^ readU64(secret, 8 * off + 8);
            ac0 += dv1;
            ac1 += (uint64)(uint32)dk1 * (dk1 >> 32);

            accs[off] += ac0;
            accs[off + 1] += ac1;
        }
        {
            UntypedInt off = 2;
            var dv0 = readU64(p, 8 * off);
            var dk0 = dv0 ^ readU64(secret, 8 * off);
            var ac1 = dv0;
            var ac0 = (uint64)(uint32)dk0 * (dk0 >> 32);

            var dv1 = readU64(p, 8 * off + 8);
            var dk1 = dv1 ^ readU64(secret, 8 * off + 8);
            ac0 += dv1;
            ac1 += (uint64)(uint32)dk1 * (dk1 >> 32);

            accs[off] += ac0;
            accs[off + 1] += ac1;
        }
        {
            UntypedInt off = 4;
            var dv0 = readU64(p, 8 * off);
            var dk0 = dv0 ^ readU64(secret, 8 * off);
            var ac1 = dv0;
            var ac0 = (uint64)(uint32)dk0 * (dk0 >> 32);

            var dv1 = readU64(p, 8 * off + 8);
            var dk1 = dv1 ^ readU64(secret, 8 * off + 8);
            ac0 += dv1;
            ac1 += (uint64)(uint32)dk1 * (dk1 >> 32);

            accs[off] += ac0;
            accs[off + 1] += ac1;
        }
        {
            UntypedInt off = 6;
            var dv0 = readU64(p, 8 * off);
            var dk0 = dv0 ^ readU64(secret, 8 * off);
            var ac1 = dv0;
            var ac0 = (uint64)(uint32)dk0 * (dk0 >> 32);

            var dv1 = readU64(p, 8 * off + 8);
            var dk1 = dv1 ^ readU64(secret, 8 * off + 8);
            ac0 += dv1;
            ac1 += (uint64)(uint32)dk1 * (dk1 >> 32);

            accs[off] += ac0;
            accs[off + 1] += ac1;
        }

        (p, secret) = (p.add(_stripe), secret.add(8));
    }

    accs[0] ^= (u64)((accs[0] >> 47));
    accs[0] ^= (u64)(key64_128);
    accs[0] *= prime32_1;

    accs[1] ^= (u64)((accs[1] >> 47));
    accs[1] ^= (u64)(key64_136);
    accs[1] *= prime32_1;

    accs[2] ^= (u64)((accs[2] >> 47));
    accs[2] ^= (u64)(key64_144);
    accs[2] *= prime32_1;

    accs[3] ^= (u64)((accs[3] >> 47));
    accs[3] ^= (u64)(key64_152);
    accs[3] *= prime32_1;

    accs[4] ^= (u64)((accs[4] >> 47));
    accs[4] ^= (u64)(key64_160);
    accs[4] *= prime32_1;

    accs[5] ^= (u64)((accs[5] >> 47));
    accs[5] ^= (u64)(key64_168);
    accs[5] *= prime32_1;

    accs[6] ^= (u64)((accs[6] >> 47));
    accs[6] ^= (u64)(key64_176);
    accs[6] *= prime32_1;

    accs[7] ^= (u64)((accs[7] >> 47));
    accs[7] ^= (u64)(key64_184);
    accs[7] *= prime32_1;
}

internal static void accumScalarSeed(ref array<u64> accs, ptr p, ptr secret, u64 l) {
    while (l > _block) {
        var k = secret;

        for (nint i = 0; i < 16; i++) {
            {
                UntypedInt off = 0;
                var dv0 = readU64(p, 8 * off);
                var dk0 = dv0 ^ readU64(k, 8 * off);
                var ac1 = dv0;
                var ac0 = (uint64)(uint32)dk0 * (dk0 >> 32);

                var dv1 = readU64(p, 8 * off + 8);
                var dk1 = dv1 ^ readU64(k, 8 * off + 8);
                ac0 += dv1;
                ac1 += (uint64)(uint32)dk1 * (dk1 >> 32);

                accs[off] += ac0;
                accs[off + 1] += ac1;
            }
            {
                UntypedInt off = 2;
                var dv0 = readU64(p, 8 * off);
                var dk0 = dv0 ^ readU64(k, 8 * off);
                var ac1 = dv0;
                var ac0 = (uint64)(uint32)dk0 * (dk0 >> 32);

                var dv1 = readU64(p, 8 * off + 8);
                var dk1 = dv1 ^ readU64(k, 8 * off + 8);
                ac0 += dv1;
                ac1 += (uint64)(uint32)dk1 * (dk1 >> 32);

                accs[off] += ac0;
                accs[off + 1] += ac1;
            }
            {
                UntypedInt off = 4;
                var dv0 = readU64(p, 8 * off);
                var dk0 = dv0 ^ readU64(k, 8 * off);
                var ac1 = dv0;
                var ac0 = (uint64)(uint32)dk0 * (dk0 >> 32);

                var dv1 = readU64(p, 8 * off + 8);
                var dk1 = dv1 ^ readU64(k, 8 * off + 8);
                ac0 += dv1;
                ac1 += (uint64)(uint32)dk1 * (dk1 >> 32);

                accs[off] += ac0;
                accs[off + 1] += ac1;
            }
            {
                UntypedInt off = 6;
                var dv0 = readU64(p, 8 * off);
                var dk0 = dv0 ^ readU64(k, 8 * off);
                var ac1 = dv0;
                var ac0 = (uint64)(uint32)dk0 * (dk0 >> 32);

                var dv1 = readU64(p, 8 * off + 8);
                var dk1 = dv1 ^ readU64(k, 8 * off + 8);
                ac0 += dv1;
                ac1 += (uint64)(uint32)dk1 * (dk1 >> 32);

                accs[off] += ac0;
                accs[off + 1] += ac1;
            }

            l -= _stripe;
            if (l > 0) {
                (p, k) = (p.add(_stripe), k.add(8));
            }
        }

        accs[0] ^= (u64)((accs[0] >> 47));
        accs[0] ^= (uint64)(readU64(secret, 128));
        accs[0] *= prime32_1;

        accs[1] ^= (u64)((accs[1] >> 47));
        accs[1] ^= (uint64)(readU64(secret, 136));
        accs[1] *= prime32_1;

        accs[2] ^= (u64)((accs[2] >> 47));
        accs[2] ^= (uint64)(readU64(secret, 144));
        accs[2] *= prime32_1;

        accs[3] ^= (u64)((accs[3] >> 47));
        accs[3] ^= (uint64)(readU64(secret, 152));
        accs[3] *= prime32_1;

        accs[4] ^= (u64)((accs[4] >> 47));
        accs[4] ^= (uint64)(readU64(secret, 160));
        accs[4] *= prime32_1;

        accs[5] ^= (u64)((accs[5] >> 47));
        accs[5] ^= (uint64)(readU64(secret, 168));
        accs[5] *= prime32_1;

        accs[6] ^= (u64)((accs[6] >> 47));
        accs[6] ^= (uint64)(readU64(secret, 176));
        accs[6] *= prime32_1;

        accs[7] ^= (u64)((accs[7] >> 47));
        accs[7] ^= (uint64)(readU64(secret, 184));
        accs[7] *= prime32_1;
    }

    if (l > 0) {
        var (t, k) = ((l - 1) / (uint64)_stripe, secret);

        for (var i = (u64)0; i < t; i++) {
            {
                UntypedInt off = 0;
                var dv0 = readU64(p, 8 * off);
                var dk0 = dv0 ^ readU64(k, 8 * off);
                var ac1 = dv0;
                var ac0 = (uint64)(uint32)dk0 * (dk0 >> 32);

                var dv1 = readU64(p, 8 * off + 8);
                var dk1 = dv1 ^ readU64(k, 8 * off + 8);
                ac0 += dv1;
                ac1 += (uint64)(uint32)dk1 * (dk1 >> 32);

                accs[off] += ac0;
                accs[off + 1] += ac1;
            }
            {
                UntypedInt off = 2;
                var dv0 = readU64(p, 8 * off);
                var dk0 = dv0 ^ readU64(k, 8 * off);
                var ac1 = dv0;
                var ac0 = (uint64)(uint32)dk0 * (dk0 >> 32);

                var dv1 = readU64(p, 8 * off + 8);
                var dk1 = dv1 ^ readU64(k, 8 * off + 8);
                ac0 += dv1;
                ac1 += (uint64)(uint32)dk1 * (dk1 >> 32);

                accs[off] += ac0;
                accs[off + 1] += ac1;
            }
            {
                UntypedInt off = 4;
                var dv0 = readU64(p, 8 * off);
                var dk0 = dv0 ^ readU64(k, 8 * off);
                var ac1 = dv0;
                var ac0 = (uint64)(uint32)dk0 * (dk0 >> 32);

                var dv1 = readU64(p, 8 * off + 8);
                var dk1 = dv1 ^ readU64(k, 8 * off + 8);
                ac0 += dv1;
                ac1 += (uint64)(uint32)dk1 * (dk1 >> 32);

                accs[off] += ac0;
                accs[off + 1] += ac1;
            }
            {
                UntypedInt off = 6;
                var dv0 = readU64(p, 8 * off);
                var dk0 = dv0 ^ readU64(k, 8 * off);
                var ac1 = dv0;
                var ac0 = (uint64)(uint32)dk0 * (dk0 >> 32);

                var dv1 = readU64(p, 8 * off + 8);
                var dk1 = dv1 ^ readU64(k, 8 * off + 8);
                ac0 += dv1;
                ac1 += (uint64)(uint32)dk1 * (dk1 >> 32);

                accs[off] += ac0;
                accs[off + 1] += ac1;
            }

            l -= _stripe;
            if (l > 0) {
                (p, k) = (p.add(_stripe), k.add(8));
            }
        }

        if (l > 0) {
            p = p.sub((ui)((uint64)_stripe - l));

            var dv0 = readU64(p, 8 * 0);
            var dk0 = dv0 ^ readU64(secret, 121);
            accs[1] += dv0;
            accs[0] += (uint64)(uint32)dk0 * (dk0 >> 32);

            var dv1 = readU64(p, 8 * 1);
            var dk1 = dv1 ^ readU64(secret, 129);
            accs[0] += dv1;
            accs[1] += (uint64)(uint32)dk1 * (dk1 >> 32);

            var dv2 = readU64(p, 8 * 2);
            var dk2 = dv2 ^ readU64(secret, 137);
            accs[3] += dv2;
            accs[2] += (uint64)(uint32)dk2 * (dk2 >> 32);

            var dv3 = readU64(p, 8 * 3);
            var dk3 = dv3 ^ readU64(secret, 145);
            accs[2] += dv3;
            accs[3] += (uint64)(uint32)dk3 * (dk3 >> 32);

            var dv4 = readU64(p, 8 * 4);
            var dk4 = dv4 ^ readU64(secret, 153);
            accs[5] += dv4;
            accs[4] += (uint64)(uint32)dk4 * (dk4 >> 32);

            var dv5 = readU64(p, 8 * 5);
            var dk5 = dv5 ^ readU64(secret, 161);
            accs[4] += dv5;
            accs[5] += (uint64)(uint32)dk5 * (dk5 >> 32);

            var dv6 = readU64(p, 8 * 6);
            var dk6 = dv6 ^ readU64(secret, 169);
            accs[7] += dv6;
            accs[6] += (uint64)(uint32)dk6 * (dk6 >> 32);

            var dv7 = readU64(p, 8 * 7);
            var dk7 = dv7 ^ readU64(secret, 177);
            accs[6] += dv7;
            accs[7] += (uint64)(uint32)dk7 * (dk7 >> 32);
        }
    }
}

internal static void accumBlockScalarSeed(ref array<u64> accs, ptr p, ptr secret) {
    {
        var secretΔ1 = secret;
        for (nint i = 0; i < 16; i++) {
            {
                UntypedInt off = 0;
                var dv0 = readU64(p, 8 * off);
                var dk0 = dv0 ^ readU64(secretΔ1, 8 * off);
                var ac1 = dv0;
                var ac0 = (uint64)(uint32)dk0 * (dk0 >> 32);

                var dv1 = readU64(p, 8 * off + 8);
                var dk1 = dv1 ^ readU64(secretΔ1, 8 * off + 8);
                ac0 += dv1;
                ac1 += (uint64)(uint32)dk1 * (dk1 >> 32);

                accs[off] += ac0;
                accs[off + 1] += ac1;
            }
            {
                UntypedInt off = 2;
                var dv0 = readU64(p, 8 * off);
                var dk0 = dv0 ^ readU64(secretΔ1, 8 * off);
                var ac1 = dv0;
                var ac0 = (uint64)(uint32)dk0 * (dk0 >> 32);

                var dv1 = readU64(p, 8 * off + 8);
                var dk1 = dv1 ^ readU64(secretΔ1, 8 * off + 8);
                ac0 += dv1;
                ac1 += (uint64)(uint32)dk1 * (dk1 >> 32);

                accs[off] += ac0;
                accs[off + 1] += ac1;
            }
            {
                UntypedInt off = 4;
                var dv0 = readU64(p, 8 * off);
                var dk0 = dv0 ^ readU64(secretΔ1, 8 * off);
                var ac1 = dv0;
                var ac0 = (uint64)(uint32)dk0 * (dk0 >> 32);

                var dv1 = readU64(p, 8 * off + 8);
                var dk1 = dv1 ^ readU64(secretΔ1, 8 * off + 8);
                ac0 += dv1;
                ac1 += (uint64)(uint32)dk1 * (dk1 >> 32);

                accs[off] += ac0;
                accs[off + 1] += ac1;
            }
            {
                UntypedInt off = 6;
                var dv0 = readU64(p, 8 * off);
                var dk0 = dv0 ^ readU64(secretΔ1, 8 * off);
                var ac1 = dv0;
                var ac0 = (uint64)(uint32)dk0 * (dk0 >> 32);

                var dv1 = readU64(p, 8 * off + 8);
                var dk1 = dv1 ^ readU64(secretΔ1, 8 * off + 8);
                ac0 += dv1;
                ac1 += (uint64)(uint32)dk1 * (dk1 >> 32);

                accs[off] += ac0;
                accs[off + 1] += ac1;
            }

            (p, secretΔ1) = (p.add(_stripe), secretΔ1.add(8));
        }
    }

    accs[0] ^= (u64)((accs[0] >> 47));
    accs[0] ^= (uint64)(readU64(secret, 128));
    accs[0] *= prime32_1;

    accs[1] ^= (u64)((accs[1] >> 47));
    accs[1] ^= (uint64)(readU64(secret, 136));
    accs[1] *= prime32_1;

    accs[2] ^= (u64)((accs[2] >> 47));
    accs[2] ^= (uint64)(readU64(secret, 144));
    accs[2] *= prime32_1;

    accs[3] ^= (u64)((accs[3] >> 47));
    accs[3] ^= (uint64)(readU64(secret, 152));
    accs[3] *= prime32_1;

    accs[4] ^= (u64)((accs[4] >> 47));
    accs[4] ^= (uint64)(readU64(secret, 160));
    accs[4] *= prime32_1;

    accs[5] ^= (u64)((accs[5] >> 47));
    accs[5] ^= (uint64)(readU64(secret, 168));
    accs[5] *= prime32_1;

    accs[6] ^= (u64)((accs[6] >> 47));
    accs[6] ^= (uint64)(readU64(secret, 176));
    accs[6] *= prime32_1;

    accs[7] ^= (u64)((accs[7] >> 47));
    accs[7] ^= (uint64)(readU64(secret, 184));
    accs[7] *= prime32_1;
}

} // end xxh3_package
