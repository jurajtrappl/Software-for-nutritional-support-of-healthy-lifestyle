using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Infrastructure.Entities.Base;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Application.Infrastructure.Entities
{
    /// <summary>
    /// Represents database entity of ingredient.
    /// </summary>
    public sealed class Ingredient : BaseEntity, IIngredient
    {
        /// <summary>
        /// Gets or initializes the list of allergens.
        /// </summary>
        [BsonRequired]
        public List<int> Allergens { get; init; }

        /// <summary>
        /// Gets or initializes the name of the ingredient.
        /// </summary>
        [BsonRequired]
        public string Name { get; init; }

        /// <summary>
        /// Gets or initializes macronutrients amounts.
        /// </summary>
        [BsonRequired]
        public Dictionary<MacroNutrient, double> MacroNutrients { get; init; }

        /// <summary>
        /// Initializes a new instance of <see cref="Ingredient" /> with the given allergens, name and macronutrients.
        /// </summary>
        /// <param name="allergens">list of allergens.</param>
        /// <param name="name">ingredient name.</param>
        /// <param name="macroNutrients">macronutrient amounts.</param>
        public Ingredient(List<int> allergens, string name, Dictionary<MacroNutrient, double> macroNutrients)
        {
            Allergens = allergens ?? throw new ArgumentNullException(nameof(allergens));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            MacroNutrients = macroNutrients ?? throw new ArgumentNullException(nameof(macroNutrients));
        }
    }
}