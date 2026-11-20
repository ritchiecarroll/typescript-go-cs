namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strings = strings_package;
using atomic = go.sync.atomic_package;
using go.sync;
using static ast_package;

partial class ast_package {

partial struct Symbol {
    public SymbolFlags Flags;
    public CheckFlags CheckFlags;
    public @string Name;
    public slice<ж<Node>> Declarations;
    public ж<Node> ValueDeclaration;
    public SymbolTable Members;
    public SymbolTable Exports;
    internal atomic.Uint64 id;
    public ж<Symbol> Parent;
    public ж<Symbol> ExportSymbol;
}

} partial class astᴛSymbol {

public static bool IsExternalModule(this ref Symbol s) {
    return (SymbolFlags)(s.Flags & SymbolFlagsModule) != 0 && IsAmbientModuleSymbolName(s.Name);
}

public static bool IsStatic(this ref Symbol s) {
    if (s.ValueDeclaration == nil) {
        return false;
    }
    var modifierFlags = s.ValueDeclaration.ModifierFlags();
    return (ModifierFlags)(modifierFlags & ModifierFlagsStatic) != 0;
}

public static SymbolFlags CombinedLocalAndExportSymbolFlags(this ref Symbol s) {
    if (s.ExportSymbol != nil) {
        return (SymbolFlags)(s.Flags | s.ExportSymbol.Flags);
    }
    return s.Flags;
}

} partial class ast_package {

partial struct SymbolTable /*map[@string, ж<Symbol>]*/;

public static readonly @string InternalSymbolNamePrefix = ((@string)(new byte[]{0xfe}));

public static readonly @string InternalSymbolNameCall = ((@string)(new byte[]{0xfe, 0x63, 0x61, 0x6c, 0x6c}));
public static readonly @string InternalSymbolNameConstructor = ((@string)(new byte[]{0xfe, 0x63, 0x6f, 0x6e, 0x73, 0x74, 0x72, 0x75, 0x63, 0x74, 0x6f, 0x72}));
public static readonly @string InternalSymbolNameNew = ((@string)(new byte[]{0xfe, 0x6e, 0x65, 0x77}));
public static readonly @string InternalSymbolNameIndex = ((@string)(new byte[]{0xfe, 0x69, 0x6e, 0x64, 0x65, 0x78}));
public static readonly @string InternalSymbolNameExportStar = ((@string)(new byte[]{0xfe, 0x65, 0x78, 0x70, 0x6f, 0x72, 0x74}));
public static readonly @string InternalSymbolNameGlobal = ((@string)(new byte[]{0xfe, 0x67, 0x6c, 0x6f, 0x62, 0x61, 0x6c}));
public static readonly @string InternalSymbolNameMissing = ((@string)(new byte[]{0xfe, 0x6d, 0x69, 0x73, 0x73, 0x69, 0x6e, 0x67}));
public static readonly @string InternalSymbolNameType = ((@string)(new byte[]{0xfe, 0x74, 0x79, 0x70, 0x65}));
public static readonly @string InternalSymbolNameObject = ((@string)(new byte[]{0xfe, 0x6f, 0x62, 0x6a, 0x65, 0x63, 0x74}));
public static readonly @string InternalSymbolNameJSXAttributes = ((@string)(new byte[]{0xfe, 0x6a, 0x73, 0x78, 0x41, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x73}));
public static readonly @string InternalSymbolNameClass = ((@string)(new byte[]{0xfe, 0x63, 0x6c, 0x61, 0x73, 0x73}));
public static readonly @string InternalSymbolNameFunction = ((@string)(new byte[]{0xfe, 0x66, 0x75, 0x6e, 0x63, 0x74, 0x69, 0x6f, 0x6e}));
public static readonly @string InternalSymbolNameComputed = ((@string)(new byte[]{0xfe, 0x63, 0x6f, 0x6d, 0x70, 0x75, 0x74, 0x65, 0x64}));
public static readonly @string InternalSymbolNameAssignmentDeclaration = ((@string)(new byte[]{0xfe, 0x61, 0x73, 0x73, 0x69, 0x67, 0x6e, 0x6d, 0x65, 0x6e, 0x74}));
public static readonly @string InternalSymbolNameInstantiationExpression = ((@string)(new byte[]{0xfe, 0x69, 0x6e, 0x73, 0x74, 0x61, 0x6e, 0x74, 0x69, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x45, 0x78, 0x70, 0x72, 0x65, 0x73, 0x73, 0x69, 0x6f, 0x6e}));
public static readonly @string InternalSymbolNameImportAttributes = ((@string)(new byte[]{0xfe, 0x69, 0x6d, 0x70, 0x6f, 0x72, 0x74, 0x41, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x73}));
public static readonly @string InternalSymbolNameExportEquals = "export="u8;
public static readonly @string InternalSymbolNameDefault = "default"u8;
public static readonly @string InternalSymbolNameThis = "this"u8;
public static readonly @string InternalSymbolNameModuleExports = "module.exports"u8;

public static @string SymbolName(ж<Symbol> Ꮡsymbol) {
    ref var symbol = ref Ꮡsymbol.DerefOrNull();

    if (symbol.ValueDeclaration != nil && IsPrivateIdentifierClassElementDeclaration(symbol.ValueDeclaration)) {
        return symbol.ValueDeclaration.Name().Text();
    }
    return symbol.Name;
}

public static @string EscapeAllInternalSymbolNames(@string name) {
    return strings.ReplaceAll(name, InternalSymbolNamePrefix, "__");
}

public static @string EscapeInternalSymbolName(@string name) {
    var (rest, ok) = strings.CutPrefix(name, InternalSymbolNamePrefix); if (ok) {
        return "__"u8 + rest;
    }
    return name;
}

public static @string EscapeSymbolName(@string name) {
    var (rest, ok) = strings.CutPrefix(name, InternalSymbolNamePrefix); if (ok) {
        return "__"u8 + rest;
    }
    if (len(name) >= 2 && name[0] == '_' && name[1] == '_') {
        return "_"u8 + name;
    }
    return name;
}

} // end ast_package
