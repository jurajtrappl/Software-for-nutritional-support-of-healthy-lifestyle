using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.Infrastructure.Entities
{
    /// <summary>
    /// Represents database entity of user profile information.
    /// </summary>
    public sealed class Profile : IProfile
    {
        /// <summary>
        /// Gets or initializes age.
        /// </summary>
        [BsonRequired]
        public int Age { get; init; }

        /// <summary>
        /// Gets or initializes sex.
        /// </summary>
        [BsonRequired]
        [PersonalData]
        public Sex SexType { get; init; }

        /// <summary>
        /// Gets or initializes physical activity level type.
        /// </summary>
        [BsonRequired]
        public PhysicalActivityLevel Pal { get; init; }
    }
}