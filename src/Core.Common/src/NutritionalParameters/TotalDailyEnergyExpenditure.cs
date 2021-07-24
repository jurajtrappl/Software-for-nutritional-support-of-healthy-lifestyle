using Application.Core.Common.Entities;
using Application.Core.Common.Extensions;
using System;

namespace Application.Core.Common.NutritionalParameters
{
    /// <summary>
    /// Represents TDEE index.
    /// </summary>
    public struct TotalDailyEnergyExpenditure
    {
        /// <summary>
        /// Describes average TDEE index value given by population statistics.
        /// Unit: kilojoule.
        /// </summary>
        private const double AverageTdee = 8400;

        /// <summary>
        /// Desribe above average TDEE index value given by dietetary system.
        /// Unit: kilojoule.
        /// </summary>
        private const double AboveAverageTdee = 12000;

        /// <summary>
        /// Gets value of the TDEE index.
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Initializes a new instance of the <seealso cref="TotalDailyEnergyExpenditure" /> with the value calculated by the
        /// given measurement and profile.
        /// </summary>
        /// <param name="measurement">measurement used to calculate.</param>
        /// <param name="profile">profile used to calculate.</param>
        public TotalDailyEnergyExpenditure(IMeasurement measurement, IProfile profile)
        {
            if (profile is null)
            {
                throw new ArgumentNullException(nameof(profile));
            }

            BasalMetabolicRate bmr = new(measurement, profile);
            double? pal = PhysicalActivityLevel.GetValueByKey(profile.Pal.ToString());
            if (pal is null)
            {
                throw new NullReferenceException(nameof(pal));
            }

            Value = (bmr.Value * pal.Value).ToTwoDecimals();
        }

        /// <summary>
        /// Indicates whether the TDEE index is below average TDEE.
        /// </summary>
        /// <returns>True if the TDEE index is below average; otherwise False.</returns>
        public bool IsBelowAverage() => Value <= AverageTdee;

        /// <summary>
        /// Indicates whether the TDEE index is average TDEE.
        /// </summary>
        /// <returns>True if the TDEE index is average; otherwise False.</returns>
        public bool IsAverage() => Value is > AverageTdee and <= AboveAverageTdee;

        /// <summary>
        /// Indicates whether the TDEE index is above average TDEE.
        /// </summary>
        /// <returns>True if the TDEE index is above average; otherwise False.</returns>
        public bool IsAboveAverage() => Value is > AboveAverageTdee;
    }
}