namespace go.github.com.microsoft.TypeScript.tsc.@internal;

partial class core_package {

public static (nint, bool) BinarySearchUniqueFunc<S, E>(S x, Func<nint, E, nint> cmp)
    where S : /* ~[]E */ ISlice<E>, ISupportMake<S>, ISliceWrap<S, E>, new()
{
    nint n = len(x);
    if (n == 0) {
        return (0, false);
    }
    nint low = 0;
    nint high = n - 1;
    while (low <= high) {
        nint middle = low + ((high - low) >> 1);
        nint value = cmp(middle, x[middle]);
        if (value < 0) {
            low = middle + 1;
        } else if (value > 0) {
            high = middle - 1;
        } else {
            return (middle, true);
        }
    }
    return (low, false);
}

} // end core_package
