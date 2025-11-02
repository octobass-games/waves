using System.Collections.Generic;

namespace Octobass.Waves.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Allows forward cyclic access to elements of a list. Once the index is incremented
        /// beyond the size of the list, it loops back to zero. If <paramref name="currentIndex"/>
        /// is less than zero then zero is returned
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="currentIndex"></param>
        /// <returns>The next index to access for foward cyclic access</returns>
        public static int NextIndex<T>(this List<T> source, int currentIndex)
        {
            if (currentIndex < 0) return 0;

            return (currentIndex + 1) % (source.Count);
        }

        /// <summary>
        /// Allows backward cyclic access to elements of a list. Once the index is decremented
        /// below zero then it loops back to the end of the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="currentIndex"></param>
        /// <returns>The next index to access for backward cyclic access</returns>
        public static int PrevIndex<T>(this List<T> source, int currentIndex)
        {
            if (currentIndex <= 0) return source.Count - 1;

            return currentIndex - 1;
        }
    }
}
