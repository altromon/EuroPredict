using System;
using System.Collections.Generic;
using System.Linq;

namespace EuroPredict.Core.Shared
{
    public static class IEnumerableExtensions
    {
        public static string ToSortedString(this IEnumerable<int> enumerable)
        {
            return string.Join(",", enumerable.OrderBy(value => value));
        }
    }
}
