using Application.Core.Common.Enums;

namespace Application.Core.Common.Entities
{
    /// <summary>
    /// An entity describing additional user data needed for the calculation.
    /// </summary>
    public interface IProfile
    {
        /// <summary>
        /// Gets age in years.
        /// Unit: year.
        /// </summary>
        int Age { get; }

        /// <summary>
        /// Gets physical activity level factor.
        /// </summary>
        PhysicalActivityLevel Pal { get; }

        /// <summary>
        /// Gets biological sex.
        /// </summary>
        Sex SexType { get; }
    }
}