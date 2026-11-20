global using ui = go.uintptr;
global using u8 = byte;
global using u32 = uint;
global using u64 = ulong;
global using u128 = go.github.com.zeebo.xxh3_package.Uint128;

namespace go.github.com.zeebo;

using binary = encoding.binary_package;
using bits = math.bits_package;
using encoding;
using math;
using static xxh3_package;

partial class xxh3_package {

partial struct Uint128 {
    public uint64 Hi, Lo;
}

} partial class xxh3ᴛUint128 {

public static array<byte> Bytes(this Uint128 u) {
    return new byte[]{
        (byte)(u.Hi >> 0x38), (byte)(u.Hi >> 0x30), (byte)(u.Hi >> 0x28), (byte)(u.Hi >> 0x20),
        (byte)(u.Hi >> 0x18), (byte)(u.Hi >> 0x10), (byte)(u.Hi >> 0x08), (byte)u.Hi,
        (byte)(u.Lo >> 0x38), (byte)(u.Lo >> 0x30), (byte)(u.Lo >> 0x28), (byte)(u.Lo >> 0x20),
        (byte)(u.Lo >> 0x18), (byte)(u.Lo >> 0x10), (byte)(u.Lo >> 0x08), (byte)u.Lo
    }.array();
}

} partial class xxh3_package {

partial struct ptr {
    internal slice<byte> b;
    internal ui o;
    internal bool def;
}

} partial class xxh3ᴛptr {

internal static ptr add(this ptr p, ui n) { return new ptr(p.b, p.o + n, p.def); }
internal static ptr sub(this ptr p, ui n) { return new ptr(p.b, p.o - n, p.def); }

} partial class xxh3_package {

partial struct str {
    internal ptr p;
    internal nuint l;
}

internal static str bytesOf(slice<byte> b) { return new str(p: new ptr(b: b), l: (nuint)len(b)); }
internal static str stringOf(@string s) { return new str(p: new ptr(b: slice<byte>(s)), l: (nuint)len(s)); }

internal static uint8 readU8(ptr p, ui o) {
    return p.b[p.o + o];
}

internal static uint16 readU16(ptr p, ui o) {
    return binary.LittleEndian.Uint16(p.b.slice((nint)(p.o + o)));
}

internal static uint32 readU32(ptr p, ui o) {
    return binary.LittleEndian.Uint32(p.b.slice((nint)(p.o + o)));
}

internal static uint64 readU64(ptr p, ui o) {
    return binary.LittleEndian.Uint64(p.b.slice((nint)(p.o + o)));
}

internal static void writeU64(ptr p, ui o, u64 v) {
    binary.LittleEndian.PutUint64(p.b.slice((nint)(p.o + o)), v);
}

internal static UntypedInt secretSize => 192;

internal static void initSecret(ptr secret, u64 seed) {
    for (var i = (ui)0; i < (uintptr)(secretSize / 16); i++) {
        var lo = readU64(key, 16 * i) + seed;
        var hi = readU64(key, 16 * i + 8) - seed;
        writeU64(secret, 16 * i, lo);
        writeU64(secret, 16 * i + 8, hi);
    }
}

internal static u64 xxh64AvalancheSmall(u64 x) {
    x *= prime64_2;
    x ^= (u64)((x >> 29));
    x *= prime64_3;
    x ^= (u64)((x >> 32));
    return x;
}

internal static u64 xxhAvalancheSmall(u64 x) {
    x ^= (u64)((x >> 33));
    x *= prime64_2;
    x ^= (u64)((x >> 29));
    x *= prime64_3;
    x ^= (u64)((x >> 32));
    return x;
}

internal static u64 xxh64AvalancheFull(u64 x) {
    x ^= (u64)((x >> 33));
    x *= prime64_2;
    x ^= (u64)((x >> 29));
    x *= prime64_3;
    x ^= (u64)((x >> 32));
    return x;
}

internal static u64 xxh3Avalanche(u64 x) {
    x ^= (u64)((x >> 37));
    x *= 0x165667919e3779f9UL;
    x ^= (u64)((x >> 32));
    return x;
}

internal static u64 rrmxmx(u64 h64, u64 len) {
    h64 ^= (uint64)((bits.RotateLeft64(h64, 49) ^ bits.RotateLeft64(h64, 24)));
    h64 *= 0x9fb21c651e98df25UL;
    h64 ^= (u64)((h64 >> 35) + len);
    h64 *= 0x9fb21c651e98df25UL;
    h64 ^= (u64)((h64 >> 28));
    return h64;
}

internal static u64 mulFold64(u64 x, u64 y) {
    var (hi, lo) = bits.Mul64(x, y);
    return hi ^ lo;
}

} // end xxh3_package
