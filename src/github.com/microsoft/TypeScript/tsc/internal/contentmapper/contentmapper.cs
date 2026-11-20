namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using errors = errors_package;
using reflect = reflect_package;
using strings = strings_package;
using sync = sync_package;
using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using core = go.github.com.microsoft.TypeScript.tsc.@internal.core_package;
using json = go.github.com.microsoft.TypeScript.tsc.@internal.json_package;
using xxh3 = go.github.com.zeebo.xxh3_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using go.github.com.zeebo;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using jsontext = go.github.com.go_json_experiment.json.jsontext_package;
using static contentmapper_package;

partial class contentmapper_package {

public static error ErrProjectUnavailable = errors.New("content mapper project is unavailable");

partial struct Definition {
    public @string Package; /*`json:"package"`*/
    public slice<@string> Extensions; /*`json:"extensions"`*/
    public jsontext.Value Options; /*`json:"options,omitempty"`*/
}

partial struct Manifest {
    public @string Name;
    public @string Version;
    public slice<@string> Exec;
    public slice<@string> CompilerOptions;
    public bool DynamicConfig;
}

partial struct Mapper {
    /*embed*/ public Definition Definition;
    /*embed*/ public Manifest Manifest; /*`json:"-"`*/
    public @string PackageDirectory; /*`json:"-"`*/
    public @string ContributionID; /*`json:"-"`*/
}

internal static ж<collections.Set<@string>> supportedVirtualExtensions = collections.NewSetFromItems(
    (@string)".js", (@string)".jsx", (@string)".mjs", (@string)".cjs", (@string)".ts", (@string)".tsx", (@string)".mts", (@string)".cts", (@string)".json");

public static bool IsSupportedVirtualExtension(@string extension) {
    return supportedVirtualExtensions.Has(extension);
}

} partial class contentmapperᴛMapper {

public static @string DiagnosticName(this ref Mapper m) {
    switch (ᐧ) {
    case {} when m.Name != ""u8: {
        return m.Name;
    }
    case {} when m.Package != ""u8: {
        return m.Package;
    }
    default: {
        return m.ContributionID;
    }}

}

public static @string Identity(this ref Mapper m) {
    if (m.ContributionID != ""u8) {
        return m.ContributionID + " ("u8 + m.manifestIdentity() + ")"u8;
    }
    return m.manifestIdentity();
}

internal static @string manifestIdentity(this ref Mapper m) {
    switch (ᐧ) {
    case {} when m.Name == ""u8: {
        return "";
    }
    case {} when m.Version == ""u8: {
        return m.Name;
    }
    default: {
        return m.Name + "@"u8 + m.Version;
    }}

}

public static xxh3.Uint128 TransformIdentity(this ref Mapper m, ж<core.CompilerOptions> options) {
    var bufᴷ = new stackAppend<byte>();

    var (declared, _) = m.MarshalDeclaredOptions(options);
    var (optionsJSON, _) = json.Marshal(declared.OrTypedNil());
    var buf = new slice<byte>(0, len(m.Identity()) + 2 + len(m.Options) + len(optionsJSON));
    buf = append(buf, m.Identity().ꓸꓸꓸ);
    buf = append(bufᴷ, buf, (byte)0);
    buf = appendꓸꓸꓸ(buf, m.Options);
    buf = append(buf, (byte)0);
    buf = appendꓸꓸꓸ(buf, optionsJSON);
    return xxh3.Hash128(buf);
}

public static (ж<collections.OrderedMap<@string, jsontext.Value>>, error) MarshalDeclaredOptions(this ref Mapper m, ж<core.CompilerOptions> options) {
    var @out = collections.NewOrderedMapWithSizeHint<@string, jsontext.Value>(len(m.CompilerOptions));
    if (options == nil || len(m.CompilerOptions) == 0) {
        return (@out, default);
    }
    var fields = compilerOptionFields();
    var v = reflect.ValueOf(options.OrTypedNil()).Elem();
    foreach (var (_, name) in range(m.CompilerOptions)) {
        var (i, ok) = fields[name, ꟷ];
        if (!ok) {
            continue;
        }
        var field = v.Field(i);
        if (field.IsZero()) {
            continue;
        }
        var (raw, err) = json.Marshal(field.Interface());
        if (err != nil) {
            return (default, err);
        }
        @out.Set(name, (jsontext.Value)raw);
    }
    return (@out, default);
}

} partial class contentmapper_package {

internal static Func<map<@string, nint>> compilerOptionFields = sync.OnceValue(map<@string, nint> () => {
    var t = reflect.TypeFor<core.CompilerOptions>();
    var fields = new map<@string, nint>(t.NumField());
    foreach (var i in range(t.NumField())) {
        var (name, _, _) = strings.Cut(t.Field(i).Tag.Get("json"), ",");
        if (name != ""u8 && name != "-"u8) {
            fields[name] = i;
        }
    }
    return fields;
});

} // end contentmapper_package
