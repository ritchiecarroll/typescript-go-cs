namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using static printer_package;

partial class printer_package {

partial struct GeneratedIdentifierFlags /*num:nint*/;

public static UntypedInt GeneratedIdentifierFlagsNone => iota;
public static UntypedInt GeneratedIdentifierFlagsAuto => 1;
public static UntypedInt GeneratedIdentifierFlagsLoop => 2;
public static UntypedInt GeneratedIdentifierFlagsUnique => 3;
public static UntypedInt GeneratedIdentifierFlagsNode => 4;
public static UntypedInt GeneratedIdentifierFlagsKindMask => 7;
public static UntypedInt GeneratedIdentifierFlagsReservedInNestedScopes => /* 1 << 3 */ 8;
public static UntypedInt GeneratedIdentifierFlagsOptimistic => /* 1 << 4 */ 16;
public static UntypedInt GeneratedIdentifierFlagsFileLevel => /* 1 << 5 */ 32;
public static UntypedInt GeneratedIdentifierFlagsAllowNameSubstitution => /* 1 << 6 */ 64;

} partial class printerᴛGeneratedIdentifierFlags {

public static GeneratedIdentifierFlags Kind(this GeneratedIdentifierFlags f) {
    return (GeneratedIdentifierFlags)(f & (nint)GeneratedIdentifierFlagsKindMask);
}

public static bool IsAuto(this GeneratedIdentifierFlags f) {
    return f.Kind() == GeneratedIdentifierFlagsAuto;
}

public static bool IsLoop(this GeneratedIdentifierFlags f) {
    return f.Kind() == GeneratedIdentifierFlagsLoop;
}

public static bool IsUnique(this GeneratedIdentifierFlags f) {
    return f.Kind() == GeneratedIdentifierFlagsUnique;
}

public static bool IsNode(this GeneratedIdentifierFlags f) {
    return f.Kind() == GeneratedIdentifierFlagsNode;
}

public static bool IsReservedInNestedScopes(this GeneratedIdentifierFlags f) {
    return (GeneratedIdentifierFlags)(f & (nint)GeneratedIdentifierFlagsReservedInNestedScopes) != 0;
}

public static bool IsOptimistic(this GeneratedIdentifierFlags f) {
    return (GeneratedIdentifierFlags)(f & (nint)GeneratedIdentifierFlagsOptimistic) != 0;
}

public static bool IsFileLevel(this GeneratedIdentifierFlags f) {
    return (GeneratedIdentifierFlags)(f & (nint)GeneratedIdentifierFlagsFileLevel) != 0;
}

public static bool HasAllowNameSubstitution(this GeneratedIdentifierFlags f) {
    return (GeneratedIdentifierFlags)(f & (nint)GeneratedIdentifierFlagsAllowNameSubstitution) != 0;
}

} // end printerᴛGeneratedIdentifierFlags
