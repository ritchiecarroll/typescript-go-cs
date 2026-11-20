namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using collections = go.github.com.microsoft.TypeScript.tsc.@internal.collections_package;
using json = go.github.com.microsoft.TypeScript.tsc.@internal.json_package;
using @internal = go.github.com.go_json_experiment.json.internal_package;
using go.github.com.microsoft.TypeScript.tsc.@internal;
using jsonopts = go.github.com.go_json_experiment.json.@internal.jsonopts_package;
using static packagejson_package;

partial class packagejson_package {

partial struct HeaderFields {
    public Expected<@string> Name; /*`json:"name"`*/
    public Expected<@string> Version; /*`json:"version"`*/
    public Expected<@string> Type; /*`json:"type"`*/
}

partial struct PathFields {
    public Expected<@string> TSConfig; /*`json:"tsconfig"`*/
    public Expected<@string> Main; /*`json:"main"`*/
    public Expected<@string> Types; /*`json:"types"`*/
    public Expected<@string> Typings; /*`json:"typings"`*/
    public JSONValue TypesVersions; /*`json:"typesVersions"`*/
    public ExportsOrImports Imports; /*`json:"imports"`*/
    public ExportsOrImports Exports; /*`json:"exports"`*/
}

partial struct DependencyFields {
    public Expected<map<@string, @string>> Dependencies; /*`json:"dependencies"`*/
    public Expected<map<@string, @string>> DevDependencies; /*`json:"devDependencies"`*/
    public Expected<map<@string, @string>> PeerDependencies; /*`json:"peerDependencies"`*/
    public Expected<map<@string, @string>> OptionalDependencies; /*`json:"optionalDependencies"`*/
}

} partial class packagejsonᴛDependencyFields {

public static bool HasDependency(this ref DependencyFields df, @string name) {
    {
        var (deps, ok) = df.Dependencies.GetValue(); if (ok) {
            var (_, okΔ1) = deps[name, ꟷ]; if (okΔ1) {
                return true;
            }
        }
    }
    {
        var (devDeps, ok) = df.DevDependencies.GetValue(); if (ok) {
            var (_, okΔ1) = devDeps[name, ꟷ]; if (okΔ1) {
                return true;
            }
        }
    }
    {
        var (peerDeps, ok) = df.PeerDependencies.GetValue(); if (ok) {
            var (_, okΔ1) = peerDeps[name, ꟷ]; if (okΔ1) {
                return true;
            }
        }
    }
    {
        var (optDeps, ok) = df.OptionalDependencies.GetValue(); if (ok) {
            var (_, okΔ1) = optDeps[name, ꟷ]; if (okΔ1) {
                return true;
            }
        }
    }
    return false;
}

public static void RangeDependencies(this ref DependencyFields df, Func<@string, @string, @string, bool> f) {
    {
        var (deps, ok) = df.Dependencies.GetValue(); if (ok) {
            foreach (var (name, version) in deps) {
                if (!f(name, version, "dependencies")) {
                    return;
                }
            }
        }
    }
    {
        var (devDeps, ok) = df.DevDependencies.GetValue(); if (ok) {
            foreach (var (name, version) in devDeps) {
                if (!f(name, version, "devDependencies")) {
                    return;
                }
            }
        }
    }
    {
        var (peerDeps, ok) = df.PeerDependencies.GetValue(); if (ok) {
            foreach (var (name, version) in peerDeps) {
                if (!f(name, version, "peerDependencies")) {
                    return;
                }
            }
        }
    }
    {
        var (optDeps, ok) = df.OptionalDependencies.GetValue(); if (ok) {
            foreach (var (name, version) in optDeps) {
                if (!f(name, version, "optionalDependencies")) {
                    return;
                }
            }
        }
    }
}

public static ж<collections.Set<@string>> GetRuntimeDependencyNames(this ref DependencyFields df) {
    nint count = default;
    var (deps, _) = df.Dependencies.GetValue();
    count += len(deps);
    var (peerDeps, _) = df.PeerDependencies.GetValue();
    count += len(peerDeps);
    var (optDeps, _) = df.OptionalDependencies.GetValue();
    count += len(optDeps);
    var names = collections.NewSetWithSizeHint<@string>(count);
    foreach (var (name, _) in deps) {
        names.Add(name);
    }
    foreach (var (name, _) in peerDeps) {
        names.Add(name);
    }
    foreach (var (name, _) in optDeps) {
        names.Add(name);
    }
    return names;
}

} partial class packagejson_package {

partial struct Fields {
    /*embed*/ public HeaderFields HeaderFields;
    /*embed*/ public PathFields PathFields;
    /*embed*/ public DependencyFields DependencyFields;
    public Expected<ContentMapperFields> ContentMapper; /*`json:"-"`*/
}

partial struct ContentMapperFields {
    public Expected<slice<@string>> Exec; /*`json:"exec"`*/
    public Expected<slice<@string>> CompilerOptions; /*`json:"compilerOptions"`*/
    public Expected<bool> DynamicConfig; /*`json:"dynamicConfig"`*/
}

partial struct typeScriptFields {
    public Expected<ContentMapperFields> ContentMapper; /*`json:"contentMapper"`*/
}

internal partial struct Parse_parsed /*dyn*/ {
    /*embed*/ public HeaderFields HeaderFields;
    /*embed*/ public PathFields PathFields;
    /*embed*/ public DependencyFields DependencyFields;
    public Expected<typeScriptFields> TypeScript; /*`json:"typescript"`*/
}

public static (Fields, error) Parse(slice<byte> data) {
    ref var parsed = ref heap(new Parse_parsed(), out var Ꮡparsed);
    var err = json.Unmarshal(data, Ꮡparsed, json.AllowDuplicateNames(true)); if (err != nil) {
        return (new Fields(), err);
    }
    var (typeScript, _) = parsed.TypeScript.GetValue();
    return (new Fields(
        HeaderFields: parsed.HeaderFields,
        PathFields: parsed.PathFields,
        DependencyFields: parsed.DependencyFields,
        ContentMapper: typeScript.ContentMapper
    ), default);
}

} // end packagejson_package
