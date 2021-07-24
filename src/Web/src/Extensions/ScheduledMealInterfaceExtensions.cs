using Application.Core.Common.Entities;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace Application.Web.Extensions
{
    /// <summary>
    /// Provides extension methods defined on UI layer for <see cref="IScheduledMeal" />.
    /// </summary>
    public static class ScheduledMealInterfaceExtensions
    {
        /// <summary>
        /// Returns the same scheduled meal but with translated names of ingredients using the given ingredients localizer.
        /// </summary>
        /// <param name="meal">meal to translate.</param>
        /// <param name="ingredientsLocalizer">translated ingredients as resources.</param>
        public static IScheduledMeal TranslateIngredients(
            this IScheduledMeal meal,
            IStringLocalizer ingredientsLocalizer)
        {
            Dictionary<string, double> translatedIngredients = new();
            foreach (var (name, amount) in meal.Ingredients)
            {
                translatedIngredients.Add(ingredientsLocalizer[name].Value, amount);
            }
            meal.Ingredients = translatedIngredients;
            return meal;
        }
    }
}