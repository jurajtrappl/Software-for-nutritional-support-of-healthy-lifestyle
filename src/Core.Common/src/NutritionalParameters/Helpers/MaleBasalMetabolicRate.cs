using Application.Core.Common.Entities;
using Application.Core.Common.Extensions;
using System;

namespace Application.Core.Common.NutritionalParameters.Base
{
    /// <summary>
    /// Contains logic to calculate male BMR index.
    /// </summary>
    internal sealed class MaleBasalMetabolicRate : BasalMetabolicRateCalculator
    {
        /// <summary>
        /// Calculates BMR index for males using Mifflin-St. Jeor equation.
        /// </summary>
        /// <returns><inheritdoc /></returns>
        internal override double Calculate(IMeasurement measurement, IProfile profile)
        {
            if (measurement is null)
            {
                throw new ArgumentNullException(nameof(measurement));
            }

            return (CaloriesToKj *
                     (10 * measurement.Weight + 6.25 * measurement.Height - 5 * profile.Age + 5))
                    .ToTwoDecimals();
        }
    }
}