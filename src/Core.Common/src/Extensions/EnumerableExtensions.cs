using Application.Core.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Core.Common.Extensions
{
    /// <summary>
    /// Defines extension methods for <see cref="IEnumerable{T}" />.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Represents pseudo-random number generator.
        /// </summary>
        private static readonly Random _random = new();

        /// <summary>
        /// Selects a random element from <see cref="IEnumerable{T}" />.
        /// </summary>
        /// <typeparam name="T">The type stored by the <see cref="IEnumerable{T}" />.</typeparam>
        /// <param name="enumerable">A <see cref="IEnumerable{T}" /> from which a random element is returned.</param>
        /// <returns>Random element from <see cref="IEnumerable{T}" />.</returns>
        public static T SelectRandom<T>(this IEnumerable<T> enumerable)
        {
            IList<T> list = enumerable as IList<T> ?? enumerable.ToList();
            if (list.Count == 0)
            {
                throw new InvalidOperationException(
                    string.Format(ExceptionMessages.EmptyCollection, enumerable));
            }

            return list.ElementAt(_random.Next(0, list.Count));
        }
    }
}