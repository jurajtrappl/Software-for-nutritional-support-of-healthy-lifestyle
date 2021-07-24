namespace Application.Infrastructure.Extensions
{
    /// <summary>
    /// Provides extension methods for string.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns same string but with lowercased first letter.
        /// </summary>
        /// <param name="value">string to modify.</param>
        public static string FirstCharToLowerCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return char.ToLower(value[0]) + value[1..];
        }
    }
}