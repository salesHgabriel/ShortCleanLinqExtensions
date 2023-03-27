using System.Text.Json;

namespace ShortCleanLinqExtensions.src.Extensions
{
    public static class ListExtension
    {
        /// <summary>
        /// The method serializer object to json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns>string</returns>
        public static string ToJson<T>(this List<T> source)
        {
            if (source is null)
            {
                throw new Exception("You need list");
            }

            return JsonSerializer.Serialize(source);
        }

        /// <summary>
        /// The collapse method collapses a collection of List into a single, flat collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="anotherList"></param>
        /// <returns>List</returns>
        public static List<T> Collapse<T>(this List<List<T>> source)
        {
            if (source is null)
            {
                throw new Exception("You need list");
            }
            return source
                .Aggregate(new List<T>(), (x, y) => x.Concat(y)
                .ToList());
        }

        /// <summary>
        /// The diff method compares the List against another List. This method will return the values in the original List that are not present in the given List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="anotherList"></param>
        /// <returns>List</returns>
        public static List<T> Diff<T>(this List<T> source, List<T> anotherList)
        {
            if (source is null || anotherList is null)
            {
                throw new Exception("You need list");
            }

            return source.Except(anotherList).ToList();
        }
    }
}