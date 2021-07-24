using Application.Core.Common.Entities.Base;
using Application.Core.Common.Enums;
using System.Collections.Generic;

namespace Application.Core.Common.Entities
{
    /// <summary>
    /// An entity describing a substance from which a meal is prepared.
    /// </summary>
    public interface IIngredient : IDatabaseItem
    {
        /// <summary>
        /// Gets the list of allergens (food allergies).
        /// </summary>
        List<int> Allergens { get; }

        /// <summary>
        /// Gets amounts of <see cref="MacroNutrient" /> present in the ingredient. The amount of each <see
        /// cref="MacroNutrient" /> is given per 100g.
        /// </summary>
        Dictionary<MacroNutrient, double> MacroNutrients { get; }

        /// <summary>
        /// Gets the name of the ingredient.
        /// </summary>
        string Name { get; }
    }
}