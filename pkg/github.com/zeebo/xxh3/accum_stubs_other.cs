namespace go.github.com.zeebo;

partial class xxh3_package {

internal const bool hasAVX2 = false;
internal const bool hasSSE2 = false;
internal const bool hasAVX512 = false;
internal const bool hasNEON = false;

internal static void accumAVX2(ref array<u64> acc, ptr data, ptr key, u64 len) { throw panic("unreachable"); }
internal static void accumSSE(ref array<u64> acc, ptr data, ptr key, u64 len) { throw panic("unreachable"); }
internal static void accumBlockAVX2(ref array<u64> acc, ptr data, ptr key) { throw panic("unreachable"); }
internal static void accumBlockSSE(ref array<u64> acc, ptr data, ptr key) { throw panic("unreachable"); }
internal static void accumAVX512(ref array<u64> acc, ptr data, ptr key, u64 len) { throw panic("unreachable"); }
internal static void accumBlockAVX512(ref array<u64> acc, ptr data, ptr key) { throw panic("unreachable"); }
internal static void accumNEON(ref array<u64> acc, ptr data, ptr key, u64 len) { throw panic("unreachable"); }
internal static void accumBlockNEON(ref array<u64> acc, ptr data, ptr key) { throw panic("unreachable"); }

internal static void withAVX512(Action cb) { cb(); }
internal static void withAVX2(Action cb) { cb(); }
internal static void withSSE2(Action cb) { cb(); }
internal static void withGeneric(Action cb) { cb(); }

} // end xxh3_package
