using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;

namespace Application.Infrastructure.Serializers
{
    /// <summary>
    /// Serialization of entities in bussiness model.
    /// </summary>
    /// <typeparam name="TBase">type being serialized.</typeparam>
    /// <typeparam name="TSerializedBy">cast type of the rsult.</typeparam>
    public sealed class EntitySerializer<TBase, TSerializedBy> : SerializerBase<TBase> where TSerializedBy : TBase
    {
        /// <summary>
        /// Deserializes value.
        /// </summary>
        public override TBase Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
            => BsonSerializer.Deserialize<TSerializedBy>(context.Reader);

        /// <summary>
        /// Serializes the given value.
        /// </summary>
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TBase value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            BsonSerializer.Serialize(context.Writer, (TSerializedBy)value);
        }
    }
}