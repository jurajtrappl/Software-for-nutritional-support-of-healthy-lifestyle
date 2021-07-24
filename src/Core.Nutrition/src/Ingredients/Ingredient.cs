using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using System;
using System.Collections.Generic;

namespace Application.Core.Nutrition.Ingredients
{
    /// <summary>
    /// Represents a substance from which the meal is prepared.
    /// </summary>
    internal sealed class Ingredient : IIngredient
    {
        /// <summary>
        /// Gets or initializes the name of the ingredient.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Gets or initializes the list of allergens.
        /// </summary>
        public List<int> Allergens { get; init; }

        /// <summary>
        /// Gets or initializes <see cref="MacroNutrient" /> present in the ingredient. The amount of each <see
        /// cref="MacroNutrient" /> is given per 100g.
        /// </summary>
        public Dictionary<MacroNutrient, double> MacroNutrients { get; init; }

        /// <summary>
        /// Initializes a new instance of <see cref="Ingredient" /> with the given name, list of allergens and macronutrients.
        /// </summary>
        /// <param name="name">ingredient name.</param>
        /// <param name="allergens">list of allergens.</param>
        /// <param name="macroNutrients">macronutrient amounts.</param>
        public Ingredient(string name, List<int> allergens, Dictionary<MacroNutrient, double> macroNutrients)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Allergens = allergens ?? throw new ArgumentNullException(nameof(allergens));
            MacroNutrients = macroNutrients ?? throw new ArgumentNullException(nameof(macroNutrients));
        }
    }
}