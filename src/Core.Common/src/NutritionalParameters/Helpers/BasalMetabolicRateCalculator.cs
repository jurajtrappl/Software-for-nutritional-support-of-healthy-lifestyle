using Application.Core.Common.Entities;

namespace Application.Core.Common.NutritionalParameters.Base
{
    /// <summary>
    /// Base class for BMR index.
    /// </summary>
    internal abstract class BasalMetabolicRateCalculator
    {
        /// <summary>
        /// Conversion rate between 1 kcal to 1kJ.
        /// </summary>
        protected const double CaloriesToKj = 4.2;

        /// <summary>
        /// Calculates BMR index from the given measurement and profile.
        /// </summary>
        /// <param name="measurement">measurement used to calculate.</param>
        /// <param name="profile">profile used to calculate.</param>
        /// <returns>double value of the BMR index round to two decimal places.</returns>
        internal abstract double Calculate(IMeasurement measurement, IProfile profile);
    }
}