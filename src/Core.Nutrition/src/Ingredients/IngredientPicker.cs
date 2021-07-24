using Application.Core.Common.Constants;
using Application.Core.Common.Entities;
using Application.Core.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Core.Nutrition.Ingredients
{
    /// <summary>
    /// Picks a random <see cref="Ingredient" /> from the given ingredients. The randomization is controlled in a way
    /// that there cant be three same ingredients for the same food component.
    /// </summary>
    public sealed class IngredientPicker
    {
        /// <summary>
        /// The minimal count of different ingredients when randomly selecting.
        /// </summary>
        private const int RememberFactor = 2;

        /// <summary>
        /// All ingredients that can be used to prepare a food grouped by <see cref="MealComponent" />.
        /// </summary>
        private readonly IReadOnlyDictionary<string, List<IIngredient>> _ingredients;

        /// <summary>
        /// Remembers what was selected for the same food component (RememberFactor times items each).
        /// </summary>
        private readonly Dictionary<string, List<Ingredient>> _selectedIngredients = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="IngredientPicker" /> class with specified ingredients.
        /// </summary>
        /// <param name="ingredients">
        /// The ingredients from which <see cref="IngredientPicker" /> will choose a random <see cref="Ingredient" />.
        /// </param>
        internal IngredientPicker(IReadOnlyDictionary<string, List<IIngredient>> ingredients)
        {
            _ingredients = ingredients ?? throw new ArgumentNullException(nameof(ingredients));
            InitSelectedIngredients();
        }

        private void InitSelectedIngredients()
        {
            foreach (var mealComponentCollectionName in _ingredients.Keys)
            {
                _selectedIngredients.Add(mealComponentCollectionName, new List<Ingredient>());
            }
        }

        /// <summary>
        /// Selects a random <see cref="IIngredient" /> from the given ingredients for the given collection.
        /// </summary>
        internal Ingredient SelectIngredient(string mealComponentCollectionName)
        {
            Ingredient ingredient =
                FilterRecentlySelectedIngredients(mealComponentCollectionName)
                .SelectRandom();

            if (_selectedIngredients[mealComponentCollectionName].Count == RememberFactor)
            {
                _selectedIngredients[mealComponentCollectionName].Clear();
            }

            _selectedIngredients[mealComponentCollectionName].Add(ingredient);

            return ingredient;
        }

        /// <summary>
        /// Segregates previously selected ingredients of the given <see cref="MealComponent" /> from all ingredients.
        /// </summary>
        private IEnumerable<Ingredient> FilterRecentlySelectedIngredients(string mealComponentCollectionName)
        {
            if (string.IsNullOrEmpty(mealComponentCollectionName))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.StringIsNullOrEmpty, nameof(mealComponentCollectionName)));
            }

            return
                from ingredient in _ingredients[mealComponentCollectionName].Except(_selectedIngredients[mealComponentCollectionName])
                select new Ingredient(ingredient.Name, ingredient.Allergens, ingredient.MacroNutrients);
        }
    }
}