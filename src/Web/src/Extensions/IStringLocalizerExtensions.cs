using Application.Core.Common.Constants;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Web.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="IStringLocalizer{TSource&gt;" />.
    /// </summary>
    public static class IStringLocalizerExtensions
    {
        /// <summary>
        /// Returns splitted resource by the given delimiter for the given key.
        /// </summary>
        /// <param name="localizer">source of resources.</param>
        /// <param name="key">lookup key.</param>
        public static List<string> ListOf(this IStringLocalizer localizer, string key, string delimiter = ",")
        {
            if (localizer is null)
            {
                throw new ArgumentNullException(nameof(localizer));
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.StringIsNullOrEmpty, nameof(key)));
            }

            if (string.IsNullOrEmpty(delimiter))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.StringIsNullOrEmpty, nameof(delimiter)));
            }

            return localizer[key].Value.Split(delimiter).ToList();
        }

        /// <summary>
        /// Translates values of <typeparamref name="TEnum" /> enumeration using localizer values and produces map
        /// between enumeration value and translated value.
        /// </summary>
        /// <typeparam name="TEnum">type of enumeration with values being translated.</typeparam>
        /// <param name="localizer">localizer with resources for the specified culture.</param>
        /// <param name="key">resource key.</param>
        /// <returns>Translated values of <typeparamref name="TEnum" /> enumeration.</returns>
        public static IReadOnlyDictionary<TEnum, string> TranslateEnum<TEnum>(this IStringLocalizer localizer, string key)
            where TEnum : Enum
        {
            if (localizer is null)
            {
                throw new ArgumentNullException(nameof(localizer));
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.StringIsNullOrEmpty, nameof(key)));
            }

            return ((TEnum[])Enum.GetValues(typeof(TEnum)))
                    .Zip(localizer.ListOf(key), (@enum, term) => new { @enum, term })
                    .ToDictionary(x => x.@enum, x => x.term);
        }

        /// <summary>
        /// Gets the enumeration value of type <typeparamref name="TEnum" /> that matches the given <paramref
        /// name="value" />.
        /// </summary>
        /// <typeparam name="TEnum">type of enumeration with values being translated.</typeparam>
        /// <param name="localizer">localizer with resources for the specified culture.</param>
        /// <param name="key">resource key.</param>
        /// <param name="value">neutral language value to translate.</param>
        /// <returns></returns>
        public static TEnum GetTranslatedValue<TEnum>(
            this IStringLocalizer localizer,
            string key,
            string value) where TEnum : Enum
        {
            if (localizer is null)
            {
                throw new ArgumentNullException(nameof(localizer));
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.StringIsNullOrEmpty, nameof(key)));
            }

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.StringIsNullOrEmpty, nameof(value)));
            }

            var translatedEnum = localizer.TranslateEnum<TEnum>(key);
            return translatedEnum.FirstOrDefault(x => x.Value == value).Key;
        }
    }
}