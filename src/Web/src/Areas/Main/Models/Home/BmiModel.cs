using Application.Core.Constants;
using Application.Web.Constants;
using System.ComponentModel.DataAnnotations;

namespace Application.Web.Areas.Main.Models.Home
{
    /// <summary>
    /// Body mass index model for bmi calculator form.
    /// </summary>
    public sealed class BmiModel
    {
        /// <summary>
        /// Gets or initializes height.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.HeightRequired)]
        [Range(
            minimum: AnthropometricConstants.MinimumHeight,
            maximum: AnthropometricConstants.MaximumHeight,
            ErrorMessage = ModelDataAnnotations.HeightOutOfRange)]
        public double? Height { get; init; }

        /// <summary>
        /// Gets or initializes weight.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.WeightRequired)]
        public double? Weight { get; init; }

        /// <summary>
        /// Gets or initializes is frequently exercising.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.IsFrequentlyExercisingRequired)]
        public bool IsFrequentlyExercising { get; init; }
    }
}