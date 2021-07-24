using Application.Core.Common.Entities.Base;
using Application.Core.Common.Enums;
using System.Collections.Generic;

namespace Application.Core.Common.Entities
{
    /// <summary>
    /// An entity describing generated item in the meal plan.
    /// </summary>
    public interface IScheduledMeal : IPlanItem
    {
        /// <summary>
        /// Gets or sets ingredients.
        /// </summary>
        Dictionary<string, double> Ingredients { get; set; }

        /// <summary>
        /// Gets macro nutrients.
        /// </summary>
        Dictionary<MacroNutrient, double> MacroNutrients { get; }

        /// <summary>
        /// Gets meal type.
        /// </summary>
        Meal Type { get; }
    }
}