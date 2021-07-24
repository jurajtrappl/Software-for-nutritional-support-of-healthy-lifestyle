using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using System;

namespace Application.Infrastructure.Serializers
{
    /// <summary>
    /// Serializes Enum value to its string representation.
    /// </summary>
    /// <typeparam name="TEnum">Enum type.</typeparam>
    public class EnumStringSerializer<TEnum> : EnumSerializer<TEnum>
        where TEnum : struct, Enum
    {
        /// <summary>
        /// Initialize a new instance of <typeparamref name="TEnum" /> as string.
        /// </summary>
        public EnumStringSerializer() : base(BsonType.String)
        {
        }
    }
}