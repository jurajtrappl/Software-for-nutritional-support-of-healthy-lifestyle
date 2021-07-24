using Application.Core.Common.Constants;
using System;
using System.Collections.Generic;

namespace Application.Core.Common.NutritionalParameters
{
    /// <summary>
    /// Represents PAL factor.
    /// </summary>
    public sealed class PhysicalActivityLevel
    {
        /// <summary>
        /// Factor for sedentary physical activity level.
        /// </summary>
        private const double SedentaryFactor = 1.2;

        /// <summary>
        /// Factor for light exercise physical activity level. (1-2x per week)
        /// </summary>
        private const double LightExerciseFactor = 1.375;

        /// <summary>
        /// Factor for moderate exercise physical activity level. (3-5x per week)
        /// </summary>
        private const double ModerateExerciseFactor = 1.55;

        /// <summary>
        /// Sedentary singleton.
        /// </summary>
        public static readonly PhysicalActivityLevel Sedentary =
            new(nameof(Sedentary), SedentaryFactor);

        /// <summary>
        /// Light exercise singleton.
        /// </summary>
        public static readonly PhysicalActivityLevel LightExercise =
            new(nameof(LightExercise), LightExerciseFactor);

        /// <summary>
        /// Moderate exercise singleton.
        /// </summary>
        public static readonly PhysicalActivityLevel ModerateExercise =
            new(nameof(ModerateExercise), ModerateExerciseFactor);

        /// <summary>
        /// Name of the activity as string key of the physical activity level.
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Factor as double value of the physical activity level.
        /// </summary>
        private double Value { get; }

        /// <summary>
        /// Initializes a new instance of <seealso cref="PhysicalActivityLevel" /> with the given key and value.
        /// </summary>
        /// <param name="key">name of the pal.</param>
        /// <param name="value">pal factor.</param>
        private PhysicalActivityLevel(string key, double value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.StringIsNullOrEmpty, nameof(key)));
            }

            (Key, Value) = (key, value);
        }

        /// <summary>
        /// Returns list of all pal types as readonly collection.
        /// </summary>
        public static IReadOnlyList<PhysicalActivityLevel> GetValues()
            => new List<PhysicalActivityLevel> { Sedentary, LightExercise, ModerateExercise };

        /// <summary>
        /// Returns a pal factor for the given name.
        /// </summary>
        /// <param name="key">key to search.</param>
        /// <returns>factor as double value if the key is present; otherwise null.</returns>
        public static double? GetValueByKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.StringIsNullOrEmpty, nameof(key)));
            }

            foreach (var enumVal in GetValues())
            {
                if (enumVal.Key == key)
                {
                    return enumVal.Value;
                }
            }

            return null;
        }

        /// <summary>
        /// String representation is the name of the pal.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Key;
    }
}