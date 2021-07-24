using Application.Core.Common.Extensions;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Globalization;

namespace Application.Infrastructure.Serializers
{
    /// <summary>
    /// Unifies <see cref="DateTime" /> serialization in the database. The format is the same as datetime invariant
    /// culture string representation.
    /// </summary>
    public sealed class DateTimeInvariantStringSerializer : SerializerBase<DateTime>
    {
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public override DateTime Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            string date = context.Reader.ReadString();
            return DateTime.Parse(date, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateTime value)
            => context.Writer.WriteString(value.ToDatabaseKeyString());
    }
}