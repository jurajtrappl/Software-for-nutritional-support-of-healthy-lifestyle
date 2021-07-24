using Application.Core.Common.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.Infrastructure.Entities
{
    /// <summary>
    /// Represents database entity of anthropometric human measurement.
    /// </summary>
    public sealed class Measurement : IMeasurement
    {
        /// <summary>
        /// Gets or initializes height.
        /// Unit: centimeter.
        /// </summary>
        [BsonRequired]
        public double Height { get; init; }

        /// <summary>
        /// Gets or initializes weight.
        /// Unit: kilogram.
        /// </summary>
        [BsonRequired]
        public double Weight { get; init; }
    }
}