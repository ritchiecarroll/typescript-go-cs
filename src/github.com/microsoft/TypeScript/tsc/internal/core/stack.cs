namespace go.github.com.microsoft.TypeScript.tsc.@internal;

using static core_package;

partial class core_package {

partial struct Stack<T> {
    internal slice<T> data;
}

} partial class coreᴛStack {

public static void Push<T>(this ref Stack<T> s, T item) {
    s.data = append(s.data, item);
}

public static T Pop<T>(this ref Stack<T> s) {
    nint l = len(s.data);
    if (l == 0) {
        throw panic("stack is empty");
    }
    var item = s.data[l - 1];
    T zero = GoZero<T>();
    s.data[l - 1] = zero;
    s.data = s.data.slice(0, l - 1);
    return item;
}

public static T Peek<T>(this ref Stack<T> s) {
    nint l = len(s.data);
    if (l == 0) {
        throw panic("stack is empty");
    }
    return s.data[l - 1];
}

public static nint Len<T>(this ref Stack<T> s) {
    return len(s.data);
}

} // end coreᴛStack
