namespace go.github.com.microsoft.TypeScript.tsc.@internal;

partial class printer_package {

partial struct EmitFlags /*num:uint32*/;

public static EmitFlags EFSingleLine => /* 1 << iota */ 1;
public static EmitFlags EFMultiLine => 2;
public static EmitFlags EFNoLeadingSourceMap => 4;
public static EmitFlags EFNoTrailingSourceMap => 8;
public static EmitFlags EFNoNestedSourceMaps => 16;
public static EmitFlags EFNoTokenLeadingSourceMaps => 32;
public static EmitFlags EFNoTokenTrailingSourceMaps => 64;
public static EmitFlags EFNoLeadingComments => 128;
public static EmitFlags EFNoTrailingComments => 256;
public static EmitFlags EFNoNestedComments => 512;
public static EmitFlags EFHelperName => 1024;
public static EmitFlags EFExportName => 2048;
public static EmitFlags EFLocalName => 4096;
public static EmitFlags EFIndented => 8192;
public static EmitFlags EFNoIndentation => 16384;
public static EmitFlags EFReuseTempVariableScope => 32768;
public static EmitFlags EFCustomPrologue => 65536;
public static EmitFlags EFNoAsciiEscaping => 131072;
public static EmitFlags EFExternalHelpers => 262144;
public static EmitFlags EFStartOnNewLine => 524288;
public static EmitFlags EFIndirectCall => 1048576;
public static EmitFlags EFAsyncFunctionBody => 2097152;
public static EmitFlags EFNoLexicalArguments => 4194304;
public static EmitFlags EFTransformPrivateStaticElements => 8388608;
public static EmitFlags EFNoLexicalThis => 16777216;

public static EmitFlags EFNone => 0;
public static EmitFlags EFNoSourceMap => /* EFNoLeadingSourceMap | EFNoTrailingSourceMap */ 12;
public static EmitFlags EFNoTokenSourceMaps => /* EFNoTokenLeadingSourceMaps | EFNoTokenTrailingSourceMaps */ 96;
public static EmitFlags EFNoComments => /* EFNoLeadingComments | EFNoTrailingComments */ 384;

} // end printer_package
