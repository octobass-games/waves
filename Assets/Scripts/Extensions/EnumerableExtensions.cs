using System;
using System.Collections.Generic;
using System.Linq;

namespace Octobass.Waves.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Randomly select a single element from <paramref name="source" />
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source" />
        /// <returns>An element of type <typeparamref name="T"/> from <paramref name="source"/></returns>
        public static T PickRandom<T>(this IEnumerable<T> source)
        {
            return source.PickRandom(1).Single();
        }

        /// <summary>
        /// Randomly select <paramref name="count"/> elements from <paramref name="source"/>.
        /// If <paramref name="count"/> is larger than the number of elements in <paramref name="source"/>
        /// then all elements are returned but in a random order
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="count"></param>
        /// <returns><paramref name="count"/> elements of type <typeparamref name="T"/> from <paramref name="source"/></returns>
        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }

        /// <summary>
        /// Randomly shuffle the elements of <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns>A new enumerable randomly shuffled</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }
    }
}
