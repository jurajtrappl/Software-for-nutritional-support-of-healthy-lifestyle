using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Common.NutritionalParameters.Base;
using System;
using System.Collections.Generic;

namespace Application.Core.Common.NutritionalParameters
{
    /// <summary>
    /// Represents BMR index.
    /// </summary>
    public sealed class BasalMetabolicRate
    {
        /// <summary>
        /// Contains calculators logic for different sex types.
        /// </summary>
        private readonly static Dictionary<Sex, Func<BasalMetabolicRateCalculator>> _calculators;

        /// <summary>
        /// Gets the value of the index.
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Initializes a static properties of <seealso cref="BasalMetabolicRate" /> class.
        /// </summary>
        static BasalMetabolicRate()
        {
            _calculators = new()
            {
                { Sex.Female, () => new FemaleBasalMetabolicRate() },
                { Sex.Male, () => new MaleBasalMetabolicRate() }
            };
        }

        /// <summary>
        /// Initializes a new instance of the <seealso cref="BasalMetabolicRate" /> class with the value calculated by
        /// the given measurement and profile.
        /// </summary>
        /// <param name="measurement">measurement used to calculate.</param>
        /// <param name="profile">profile used to calculate.</param>
        public BasalMetabolicRate(IMeasurement measurement, IProfile profile)
        {
            if (profile is null)
            {
                throw new ArgumentNullException(nameof(profile));
            }

            Value = _calculators[profile.SexType]().Calculate(measurement, profile);
        }
    }
}