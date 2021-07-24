using Application.Core.Common.Constants;
using Application.Core.Common.Entities;
using Application.Core.Interfaces;
using Application.Infrastructure.Constants;
using Application.Infrastructure.Entities;
using Application.Infrastructure.Extensions;
using Application.Infrastructure.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Application.Infrastructure.Services
{
    /// <summary>
    /// Implementation of infrastructure ingredients service.
    /// </summary>
    public class IngredientsService : IIngredientsService
    {
        /// <summary>
        /// Ingredients database.
        /// </summary>
        private readonly IMongoDatabase _ingredientsDatabase;

        /// <summary>
        /// Meal component collection name.
        /// </summary>
        private readonly List<string> _mealComponentCollectionsNames;

        /// <summary>
        /// Initializes a new instance of the <see cref="IngredientsService" /> with the given mongo client and settings.
        /// </summary>
        /// <param name="client">client interface to mongoDb.</param>
        /// <param name="mongoSettings">database settings.</param>
        public IngredientsService(IMongoClient client, MongoDbSettings mongoSettings)
        {
            _ingredientsDatabase = client.GetDatabase(mongoSettings.IngredientsDatabaseName);
            if (_ingredientsDatabase is null)
            {
                throw new MongoException(string.Format(MongoExceptionMessages.DatabaseNotFound, _ingredientsDatabase));
            }

            _mealComponentCollectionsNames = mongoSettings.MealComponentCollectionsNames;
        }

        /// <summary>
        /// Returns readonly collection of ingredients from database.
        /// </summary>
        public IReadOnlyDictionary<string, List<IIngredient>> GetAllIngredients(List<int> allergens)
        {
            Dictionary<string, List<IIngredient>> allIngredients = new();
            foreach (var mealComponent in _mealComponentCollectionsNames)
            {
                allIngredients[mealComponent] =
                    GetIngredientCollection(mealComponent)
                        .Find(FilterIngredientsByAllergies(allergens))
                        .Project<IIngredient>(Builders<IIngredient>.Projection.Exclude("_id"))
                        .ToList();
            }

            return allIngredients;
        }

        /// <summary>
        /// Gets list of ingredients and removes those who contain not allowed allergens.
        /// </summary>
        /// <param name="allergens">list of prohibited allergens.</param>
        /// <returns>mongo filter definition that is applied to query builder.</returns>
        private static FilterDefinition<IIngredient> FilterIngredientsByAllergies(IReadOnlyList<int> allergens)
        {
            if (allergens is null)
            {
                throw new ArgumentNullException(nameof(allergens));
            }

            var builder = Builders<IIngredient>.Filter;

            if (allergens.Count == 0)
            {
                return builder.Empty;
            }

            var filters = new FilterDefinition<IIngredient>[allergens.Count];
            for (var i = 0; i < allergens.Count; i++)
            {
                filters[i] = builder.Not(builder.Eq(nameof(Ingredient.Allergens).FirstCharToLowerCase(), allergens[i]));
            }

            return builder.And(filters);
        }

        /// <summary>
        /// Gets a collection by the given collection name.
        /// </summary>
        private IMongoCollection<IIngredient> GetIngredientCollection(string mealComponentCollectionName)
        {
            if (string.IsNullOrEmpty(mealComponentCollectionName))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.StringIsNullOrEmpty, nameof(mealComponentCollectionName)));
            }

            IMongoCollection<IIngredient>? collection = _ingredientsDatabase.GetCollection<IIngredient>(mealComponentCollectionName);
            if (collection is null)
            {
                throw new MongoException(
                    string.Format(MongoExceptionMessages.CollectionNotFound, mealComponentCollectionName));
            }

            return collection;
        }
    }
}