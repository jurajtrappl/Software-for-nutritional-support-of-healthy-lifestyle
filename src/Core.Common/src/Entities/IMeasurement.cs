namespace Application.Core.Common.Entities
{
    /// <summary>
    /// An entity describing the measurement of a human individual.
    /// </summary>
    public interface IMeasurement
    {
        /// <summary>
        /// Gets height of the patient.
        /// Unit: centimeter (cm).
        /// </summary>
        double Height { get; }

        /// <summary>
        /// Gets weight of the patient.
        /// Unit: kilogram (kg).
        /// </summary>
        double Weight { get; }
    }
}