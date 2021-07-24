using Application.Core.Common.Entities;
using Application.Core.Common.Extensions;
using System;

namespace Application.Core.Common.NutritionalParameters
{
    /// <summary>
    /// Represent BMI index.
    /// </summary>
    public readonly struct BodyMassIndex
    {
        /// <summary>
        /// The maximum possible value for underweight category.
        /// </summary>
        private const double Underweight = 19;

        /// <summary>
        /// The maximum possible value for normal category.
        /// </summary>
        private const double Normal = 26;

        /// <summary>
        /// The maximum possible value for overweight category.
        /// </summary>
        private const double Overweight = 30;

        /// <summary>
        /// Cm to m conversion rate.
        /// </summary>
        private const int CentimetersInMeter = 100;

        /// <summary>
        /// Gets value of the BMI index.
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Initializes a new instance of <seealso cref="BodyMassIndex" /> class with the value calculated by the given measurement.
        /// </summary>
        /// <param name="measurement">measurement used to calculate.</param>
        public BodyMassIndex(IMeasurement measurement)
        {
            if (measurement is null)
            {
                throw new ArgumentNullException(nameof(measurement));
            }

            Value = (measurement.Weight / Math.Pow(measurement.Height / CentimetersInMeter, 2))
                .ToTwoDecimals();
        }

        /// <summary>
        /// Inidicates whether the BMI index falls into the underweight category.
        /// </summary>
        /// <returns>True if the BMI index is in underweight category; otherwise False.</returns>
        public bool IsUnderweight() => Value <= Underweight;

        /// <summary>
        /// Inidicates whether the BMI index falls into the normal category.
        /// </summary>
        /// <returns>True if the BMI index is in normal category; otherwise False.</returns>
        public bool IsNormalWeight() => Value is > Underweight and <= Normal;

        /// <summary>
        /// Inidicates whether the BMI index falls into the overweight category.
        /// </summary>
        /// <returns>True if the BMI index is in overweight category; otherwise False.</returns>
        public bool IsOverweight() => Value is > Normal and <= Overweight;
    }
}