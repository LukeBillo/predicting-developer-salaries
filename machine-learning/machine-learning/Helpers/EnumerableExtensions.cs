using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benchmarking
{
    internal static class EnumerableExtensions
    {
        // https://stackoverflow.com/questions/18986129/c-splitting-an-array-into-n-parts
        // This code is not mine.
        public static IEnumerable<IEnumerable<T>> Split<T>(this T[] array, int size)
        {
            for (var i = 0; i < (float)array.Length / size; i++)
            {
                yield return array.Skip(i * size).Take(size);
            }
        }
    }
}
