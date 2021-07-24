using Application.Core.Constants;
using Application.Web.Constants;
using System.ComponentModel.DataAnnotations;

namespace Application.Web.Areas.App.Models.Settings
{
    /// <summary>
    /// Model for settings form that changes password.
    /// </summary>
    public sealed class ChangePasswordModel
    {
        /// <summary>
        /// Gets or initializes current password.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.CurrentPasswordRequired)]
        public string CurrentPassword { get; init; } = string.Empty;

        /// <summary>
        /// Gets or initializes password.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.NewPasswordRequired)]
        [StringLength(
            maximumLength: UserConstraintConstants.PasswordMaximumSize,
            ErrorMessage = ModelDataAnnotations.NotAppropriateLength,
            MinimumLength = UserConstraintConstants.PasswordMinimumSize)]
        public string NewPassword { get; init; } = string.Empty;

        /// <summary>
        /// Gets or initializes confirm password.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.ConfirmPasswordRequired)]
        [StringLength(
            maximumLength: UserConstraintConstants.PasswordMaximumSize,
            ErrorMessage = ModelDataAnnotations.NotAppropriateLength,
            MinimumLength = UserConstraintConstants.PasswordMinimumSize)]
        [Compare(nameof(NewPassword), ErrorMessage = ModelDataAnnotations.PasswordsDoNotMatch)]
        public string ConfirmNewPassword { get; init; } = string.Empty;
    }
}