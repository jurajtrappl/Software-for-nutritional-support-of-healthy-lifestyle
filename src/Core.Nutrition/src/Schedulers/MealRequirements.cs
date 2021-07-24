using Application.Core.Common.Enums;
using Application.Core.Common.Nutrients;
using Application.Core.Common.NutritionalParameters;
using Application.Core.Nutrition.EatingOccasions.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Core.Nutrition.Schedulers
{
    /// <summary>
    /// Describes how much is it needed to be of each nutrient in each meal and how much calories must a meal have.
    /// </summary>
    internal static class MealRequirements
    {
        /// <summary>
        /// Map between macronutrients and optimal ratio of them in meal nutrients.
        /// </summary>
        private static readonly Dictionary<MacroNutrient, double> _macroNutrientRatio =
            new()
            {
                { MacroNutrient.Carbohydrate, 0.5 },
                { MacroNutrient.Fat, 0.3 },
                { MacroNutrient.Protein, 0.2 }
            };

        /// <summary>
        /// Initializes a new instance of the <see cref="MealRequirements" /> class with specified Total daily energy
        /// expenditure, meals per day and macro nutrient ratio.
        /// </summary>
        /// <param name="tee">total daily energy expenditure of the user.</param>
        /// <param name="mealFrequency">number of meals per day.</param>
        public static Dictionary<Meal, Dictionary<MacroNutrient, double>> GetMealRequirements(
            TotalDailyEnergyExpenditure tee,
            IReadOnlyList<MealOccasion> mealFrequency)
        {
            if (mealFrequency is null)
            {
                throw new ArgumentNullException(nameof(mealFrequency));
            }

            Dictionary<Meal, Dictionary<MacroNutrient, double>> wholeDayMacroNutrientAmounts = new();
            Dictionary<Meal, double> caloriesPerMeal =
                mealFrequency.ToDictionary(food => food.Type, food => tee.Value * food.TeeRatio);
            foreach (var food in mealFrequency)
            {
                Dictionary<MacroNutrient, double> oneMealMacroNutrientAmounts = new();
                foreach (var (macroNutrient, amount) in _macroNutrientRatio)
                {
                    oneMealMacroNutrientAmounts.Add(
                        macroNutrient,
                        MacroNutrientConverterContext.FromKjToGrams(
                            macroNutrient,
                            caloriesPerMeal[food.Type] * amount));
                }
                wholeDayMacroNutrientAmounts.Add(food.Type, oneMealMacroNutrientAmounts);
            }

            return wholeDayMacroNutrientAmounts;
        }
    }
}