namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using strings = strings_package;

partial class modulespecifiers_package {

public static nint CountPathComponents(@string path) {
    nint initial = 0;
    if (strings.HasPrefix(path, "./")) {
        initial = 2;
    }
    return strings.Count(path.slice(initial), "/");
}

} // end modulespecifiers_package
