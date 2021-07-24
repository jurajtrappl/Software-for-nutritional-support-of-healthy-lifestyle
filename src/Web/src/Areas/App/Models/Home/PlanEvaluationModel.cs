using Application.Core.Constants;
using Application.Web.Constants;
using System.ComponentModel.DataAnnotations;

namespace Application.Web.Areas.App.Models.Home
{
    /// <summary>
    /// Model for data in plan evaluation form.
    /// </summary>
    public sealed class PlanEvaluationModel
    {
        /// <summary>
        /// Gets or sets age.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.AgeRequired)]
        [Range(
            minimum: ProfileConstants.MinimumAge,
            maximum: ProfileConstants.MaximumAge,
            ErrorMessage = ModelDataAnnotations.AgeOutOfRange)]
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets weight.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.WeightRequired)]
        public double Weight { get; set; }
    }
}