using Application.Core.Common.Entities;
using Application.Infrastructure.Entities.Base;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Application.Infrastructure.Entities
{
    /// <summary>
    /// Represent database entity of exercise activity.
    /// </summary>
    public sealed class Exercise : BaseEntity, IExercise
    {
        /// <summary>
        /// Gets or initializes name.
        /// </summary>
        [BsonRequired]
        public string Name { get; init; }

        /// <summary>
        /// Gets or initializes kj per kg per one minute of practising.
        /// </summary>
        [BsonRequired]
        public double KjPerKgPerMin { get; init; }

        /// <summary>
        /// Initializes a new instance of <see cref="Exercise" /> with the given name and kilojoule per kilogram per
        /// minute exercising.
        /// </summary>
        /// <param name="name">name of the exercise.</param>
        /// <param name="kjPerKgPerMin">how much can an individual burn per minute based on their weight.</param>
        public Exercise(string name, double kjPerKgPerMin)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            KjPerKgPerMin = kjPerKgPerMin;
        }
    }
}