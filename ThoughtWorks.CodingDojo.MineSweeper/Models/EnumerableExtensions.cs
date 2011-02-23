using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThoughtWorks.CodingDojo.MineSweeper.Models
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> collection)
        {
            return collection.OrderBy(x => Guid.NewGuid());
        }
    }
}