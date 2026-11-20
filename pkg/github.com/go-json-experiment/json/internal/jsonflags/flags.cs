// Copyright 2023 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.github.com.go_json_experiment.json.@internal;

using @internal = go.github.com.go_json_experiment.json.internal_package;
using go.github.com.go_json_experiment.json;
using static jsonflags_package;

partial class jsonflags_package {

partial struct Bools /*num:uint64*/;

} partial class jsonflagsᴛBools {

public static void JSONOptions(this Bools _Δp0, @internal.NotForPublicUse _Δp1) { }

} partial class jsonflags_package {

public static Bools AllFlags => /* AllCoderFlags | AllArshalV2Flags | AllArshalV1Flags */ unchecked((Bools)8796093022206);
public static Bools AllCoderFlags => /* (maxCoderFlag - 1) - initFlag */ 262142;
public static Bools AllArshalV2Flags => /* (maxArshalV2Flag - 1) - (maxCoderFlag - 1) */ 1073479680;
public static Bools AllArshalV1Flags => /* (maxArshalV1Flag - 1) - (maxArshalV2Flag - 1) */ unchecked((Bools)8795019280384);
public static Bools NonBooleanFlags => /* 0 |
	Indent |
	IndentPrefix |
	ByteLimit |
	DepthLimit |
	Marshalers |
	Unmarshalers |
	FormatTag */ 369344512;
public static Bools DefaultV1Flags => /* 0 |
	AllowDuplicateNames |
	AllowInvalidUTF8 |
	EscapeForHTML |
	EscapeForJS |
	PreserveRawStrings |
	Deterministic |
	FormatNilMapAsNull |
	FormatNilSliceAsNull |
	MatchCaseInsensitiveNames |
	CallMethodsWithLegacySemantics |
	FormatByteArrayAsArray |
	FormatBytesWithLegacySemantics |
	FormatDurationAsNano |
	MatchCaseSensitiveDelimiter |
	MergeWithLegacySemantics |
	OmitEmptyWithLegacySemantics |
	ParseBytesWithLooseRFC4648 |
	ParseTimeWithLooseRFC3339 |
	ReportErrorsWithLegacySemantics |
	StringifyWithLegacySemantics |
	UnmarshalArrayFromAnyLength */ unchecked((Bools)6596008085030);
public static Bools AnyWhitespace => /* Multiline | SpaceAfterColon | SpaceAfterComma */ 14336;
public static Bools WhitespaceFlags => /* AnyWhitespace | Indent | IndentPrefix */ 63488;
public static Bools AnyEscape => /* EscapeForHTML | EscapeForJS */ 1536;
public static Bools CanonicalizeNumbers => /* CanonicalizeRawInts | CanonicalizeRawFloats */ 192;
public static Bools TagFlags => /* StringTag | FormatTag */ 402653184;

internal static Bools initFlag => /* 1 << iota */ 1;
public static Bools AllowDuplicateNames => 2;
public static Bools AllowInvalidUTF8 => 4;
public static Bools WithinArshalCall => 8;
public static Bools OmitTopLevelNewline => 16;
public static Bools PreserveRawStrings => 32;
public static Bools CanonicalizeRawInts => 64;
public static Bools CanonicalizeRawFloats => 128;
public static Bools ReorderRawObjects => 256;
public static Bools EscapeForHTML => 512;
public static Bools EscapeForJS => 1024;
public static Bools Multiline => 2048;
public static Bools SpaceAfterColon => 4096;
public static Bools SpaceAfterComma => 8192;
public static Bools Indent => 16384;
public static Bools IndentPrefix => 32768;
public static Bools ByteLimit => 65536;
public static Bools DepthLimit => 131072;
internal static Bools maxCoderFlag => 262144;

internal static Bools _ᴛ1ʗ => /* (maxCoderFlag >> 1) << iota */ 131072;
public static Bools StringifyNumbers => 262144;
public static Bools Deterministic => 524288;
public static Bools FormatNilMapAsNull => 1048576;
public static Bools FormatNilSliceAsNull => 2097152;
public static Bools OmitZeroStructFields => 4194304;
public static Bools MatchCaseInsensitiveNames => 8388608;
public static Bools RejectUnknownMembers => 16777216;
public static Bools Marshalers => 33554432;
public static Bools Unmarshalers => 67108864;
public static Bools StringTag => 134217728;
public static Bools FormatTag => 268435456;
public static Bools FormatTagSupported => 536870912;
internal static Bools maxArshalV2Flag => 1073741824;

internal static Bools _ᴛ2ʗ => /* (maxArshalV2Flag >> 1) << iota */ 536870912;
public static Bools CallMethodsWithLegacySemantics => 1073741824;
public static Bools FormatByteArrayAsArray => unchecked((Bools)2147483648);
public static Bools FormatBytesWithLegacySemantics => unchecked((Bools)4294967296);
public static Bools FormatDurationAsNano => unchecked((Bools)8589934592);
public static Bools MatchCaseSensitiveDelimiter => unchecked((Bools)17179869184);
public static Bools MergeWithLegacySemantics => unchecked((Bools)34359738368);
public static Bools OmitEmptyWithLegacySemantics => unchecked((Bools)68719476736);
public static Bools ParseBytesWithLooseRFC4648 => unchecked((Bools)137438953472);
public static Bools ParseTimeWithLooseRFC3339 => unchecked((Bools)274877906944);
public static Bools ReportErrorsWithLegacySemantics => unchecked((Bools)549755813888);
public static Bools StringifyWithLegacySemantics => unchecked((Bools)1099511627776);
public static Bools UnmarshalAnyWithRawNumber => unchecked((Bools)2199023255552);
public static Bools UnmarshalArrayFromAnyLength => unchecked((Bools)4398046511104);
internal static Bools maxArshalV1Flag => unchecked((Bools)8796093022208);

internal static UntypedInt bitsUsed => 43;

internal const uint64 _ᴛ3ʗ = /* uint64((1<<bitsUsed)-maxArshalV1Flag) + uint64(maxArshalV1Flag-(1<<bitsUsed)) */ 0;

partial struct Flags {
    public uint64 Presence, Values;
}

} partial class jsonflagsᴛFlags {

public static void Join(this ref Flags dst, Flags src) {
    dst.Presence |= (uint64)(src.Presence);
    dst.Values &= (uint64)(~src.Presence);
    dst.Values |= (uint64)(src.Values);
}

public static void Set(this ref Flags fs, Bools f) {
    var id = (uint64)f & ~(uint64)1;
    fs.Presence |= (uint64)(id);
    fs.Values &= (uint64)(~id);
    fs.Values |= (uint64)((uint64)((Bools)(f & 1)) * id);
}

public static bool Get(this Flags fs, Bools f) {
    return (fs.Values & (uint64)f) > 0;
}

public static bool Has(this Flags fs, Bools f) {
    return (fs.Presence & (uint64)f) > 0;
}

public static void Clear(this ref Flags fs, Bools f) {
    var mask = (uint64)(~f);
    fs.Presence &= (uint64)(mask);
    fs.Values &= (uint64)(mask);
}

} // end jsonflagsᴛFlags
