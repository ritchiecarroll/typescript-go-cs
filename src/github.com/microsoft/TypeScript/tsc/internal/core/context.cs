namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using context = context_package;

partial class core_package {

partial struct key /*num:nint*/;

internal static key requestIDKey => /* iota */ 0;
internal static key checkerLifetimeKey => 1;

public static context.Context WithRequestID(context.Context ctx, @string id) {
    return context.WithValue(ctx, requestIDKey, id);
}

public static @string GetRequestID(context.Context ctx) {
    var (id, ok) = ctx.Value(requestIDKey)._<@string>(ᐧ); if (ok) {
        return id;
    }
    return "";
}

partial struct CheckerLifetime /*num:nint*/;

public static CheckerLifetime CheckerLifetimeTemporary => /* iota */ 0;
public static CheckerLifetime CheckerLifetimeDiagnostics => 1;
public static CheckerLifetime CheckerLifetimeAPI => 2;

public static context.Context WithCheckerLifetime(context.Context ctx, CheckerLifetime lifetime) {
    return context.WithValue(ctx, checkerLifetimeKey, lifetime);
}

public static CheckerLifetime GetCheckerLifetime(context.Context ctx) {
    var (lifetime, ok) = ctx.Value(checkerLifetimeKey)._<CheckerLifetime>(ᐧ); if (ok) {
        return lifetime;
    }
    return CheckerLifetimeTemporary;
}

} // end core_package
