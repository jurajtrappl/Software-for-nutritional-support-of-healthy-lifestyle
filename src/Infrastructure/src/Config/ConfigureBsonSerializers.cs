using Application.Core.Common.Dto;
using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Infrastructure.Entities;
using Application.Infrastructure.Serializers;
using MongoDB.Bson.Serialization;
using System;

namespace Application.Infrastructure.Config
{
    /// <summary>
    /// Provides methods that modifies the BSON serialization/deserialization process.
    /// </summary>
    public static class ConfigureBsonSerializers
    {
        /// <summary>
        /// Registers BSON serializers and class maps for entities, plans and data transfer objects.
        /// </summary>
        public static void AddBsonSerializers()
        {
            // all dictionaries must have string as key, this is list of all enums that are serialized/deserialized as
            // dictionary key
            BsonSerializer.RegisterSerializer(typeof(DateTime), new DateTimeInvariantStringSerializer());
            BsonSerializer.RegisterSerializer(typeof(Meal), new EnumStringSerializer<Meal>());
            BsonSerializer.RegisterSerializer(typeof(MacroNutrient), new EnumStringSerializer<MacroNutrient>());

            // dto serializers
            BsonClassMap.RegisterClassMap<HourData>();

            // entity serializers
            BsonSerializer.RegisterSerializer(typeof(IMeasurement), new EntitySerializer<IMeasurement, Measurement>());
            BsonSerializer.RegisterSerializer(typeof(IProfile), new EntitySerializer<IProfile, Profile>());
            BsonSerializer.RegisterSerializer(typeof(IExercise), new EntitySerializer<IExercise, Exercise>());
            BsonSerializer.RegisterSerializer(typeof(IIngredient), new EntitySerializer<IIngredient, Ingredient>());
            BsonSerializer.RegisterSerializer(typeof(IPlanResult), new EntitySerializer<IPlanResult, PlanResult>());
            BsonSerializer.RegisterSerializer(
                typeof(IScheduledExercise),
                new EntitySerializer<IScheduledExercise, Core.Exercise.Schedulers.ScheduledExercise>());
            BsonSerializer.RegisterSerializer(
                typeof(IScheduledMeal),
                new EntitySerializer<IScheduledMeal, Core.Nutrition.Schedulers.ScheduledMeal>());
            BsonSerializer.RegisterSerializer(
                typeof(IScheduledDrink),
                new EntitySerializer<IScheduledDrink, Core.Nutrition.Schedulers.ScheduledDrink>());
        }
    }
}