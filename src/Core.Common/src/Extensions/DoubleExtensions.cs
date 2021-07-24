using System;

namespace Application.Core.Common.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="double" />.
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        /// Rounds the specified <see cref="double" /> value to two decimals places using <see cref="MidpointRounding"
        /// /> away from zero.
        /// </summary>
        /// <param name="value">Value to round.</param>
        /// <returns>Rounded value to 2 decimals.</returns>
        public static double ToTwoDecimals(this double value) =>
            Math.Round(value, 2, MidpointRounding.AwayFromZero);

        /// <summary>
        /// Rounds the specified <see cref="double" /> value to the whole number using <see cref="MidpointRounding" />
        /// away from zero.
        /// </summary>
        /// <param name="value">Value to round.</param>
        /// <returns>Whole number.</returns>
        public static double ToZeroDecimals(this double value) =>
            Math.Round(value, 0, MidpointRounding.AwayFromZero);
    }
}