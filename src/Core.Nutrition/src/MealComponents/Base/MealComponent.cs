using Application.Core.Common.Enums;

namespace Application.Core.Nutrition.MealComponents.Base
{
    /// <summary>
    /// Base part of the meal pattern.
    /// </summary>
    internal abstract class MealComponent
    {
        /// <summary>
        /// The most represented macronutrient.
        /// </summary>
        internal MacroNutrient Majoritarian { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="MealComponent" /> with the given majoritarian macronutrient.
        /// </summary>
        /// <param name="majoritarian">macronutrient with the largest representation.</param>
        protected MealComponent(MacroNutrient majoritarian)
        {
            Majoritarian = majoritarian;
        }
    }
}