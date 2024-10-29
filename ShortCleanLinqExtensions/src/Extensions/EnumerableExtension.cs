
namespace ShortCleanLinqExtensions.src.Extensions;

public static class EnumerableExtension
{
        public static IEnumerable<T> WhereNull<T>(this IEnumerable<T?> source, Func<T, bool> func)
        {
            if (source is not null)
            {
                return source!.Where(func);
            }
            return source;
        }
}
