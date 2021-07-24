using Application.Web.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Web.Areas.App.Models.ShoppingList
{
    /// <summary>
    /// Model for shopping list form.
    /// </summary>
    public sealed class PlanIntervalModel
    {
        /// <summary>
        /// Gets or initializes starting date.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.StartingDateRequired)]
        public DateTime StartingDate { get; init; }

        /// <summary>
        /// Gets or initializes starting meal.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.StartingMealRequired)]
        public string StartingMeal { get; init; }

        /// <summary>
        /// Gets or initializes ending date.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.EndingDateRequired)]
        public DateTime EndingDate { get; init; }

        /// <summary>
        /// Gets or initializes ending meal.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.EndingMealRequired)]
        public string EndingMeal { get; init; }

        public PlanIntervalModel()
        {
            StartingMeal = string.Empty;
            EndingMeal = string.Empty;
        }
    }
}