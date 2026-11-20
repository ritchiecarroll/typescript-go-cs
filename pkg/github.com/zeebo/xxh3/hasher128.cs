namespace go.github.com.zeebo;

using hash = hash_package;
using static xxh3_package;

partial class xxh3_package {

partial struct Hasher128 {
    internal Hasher h;
}

internal static hash.Hash _ᴛ3ʗ = new Hasher128жHash(((ж<Hasher128>)nil));

public static ж<Hasher128> New128() {
    return Ꮡ(new Hasher128());
}

public static ж<Hasher128> NewSeed128(uint64 seed) {
    ref var h = ref heap(new Hasher128(), out var Ꮡh);
    h.ResetSeed(seed);
    return Ꮡh;
}

} partial class xxh3ᴛHasher128 {

public static void Reset(this ref Hasher128 h) {
    h.h.Reset();
}

public static void ResetSeed(this ref Hasher128 h, uint64 seed) {
    h.h.ResetSeed(seed);
}

public static nint BlockSize(this ref Hasher128 h) { return _stripe; }

public static nint Size(this ref Hasher128 h) { return 16; }

public static slice<byte> Sum(this ref Hasher128 h, slice<byte> b) {
    var sum = h.h.Sum128().Bytes();
    return appendꓸꓸꓸ(b, sum[..]);
}

public static (nint, error) Write(this ж<Hasher128> Ꮡh, slice<byte> buf) {
    Ꮡh.Ꮡh.update(buf);
    return (len(buf), default);
}

public static (nint, error) WriteString(this ж<Hasher128> Ꮡh, @string buf) {
    Ꮡh.Ꮡh.updateString(buf);
    return (len(buf), default);
}

public static Uint128 Sum128(this ref Hasher128 h) {
    return h.h.Sum128();
}

} // end xxh3ᴛHasher128
