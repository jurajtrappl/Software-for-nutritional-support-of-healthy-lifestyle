using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.Infrastructure.Entities.Base
{
    /// <summary>
    /// Base type for database entities.
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Gets or sets ID.
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }
    }
}