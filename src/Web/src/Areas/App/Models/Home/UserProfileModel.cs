using Application.Core.Constants;
using Application.Web.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Web.Areas.App.Models.Home
{
    /// <summary>
    /// Model for enter profile form.
    /// </summary>
    public sealed class UserProfileModel
    {
        /// <summary>
        /// Gets or initializes age.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.AgeRequired)]
        [Range(
            minimum: ProfileConstants.MinimumAge,
            maximum: ProfileConstants.MaximumAge,
            ErrorMessage = ModelDataAnnotations.AgeOutOfRange)]
        public int Age { get; init; }

        /// <summary>
        /// Gets or initializes sex.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.SexTypeRequired)]
        public string SexType { get; init; } = string.Empty;

        /// <summary>
        /// Gets or initializes height.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.HeightRequired)]
        [Range(
            minimum: AnthropometricConstants.MinimumHeight,
            maximum: AnthropometricConstants.MaximumHeight,
            ErrorMessage = ModelDataAnnotations.HeightOutOfRange)]
        public double Height { get; init; }

        /// <summary>
        /// Gets or initializes weight.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.WeightRequired)]
        public double Weight { get; init; }

        /// <summary>
        /// Gets or initializes physical exercise.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.PhysicalActivityLevelRequired)]
        public string PhysicalActivityLevel { get; init; } = string.Empty;

        /// <summary>
        /// Gets or initializes allergens.
        /// </summary>
        public List<int> Allergens { get; init; } = new();
    }
}