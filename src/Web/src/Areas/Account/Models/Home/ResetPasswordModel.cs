using Application.Core.Constants;
using Application.Web.Constants;
using System.ComponentModel.DataAnnotations;

namespace Application.Web.Areas.Account.Models.Home
{
    /// <summary>
    /// Model for the reset password form.
    /// </summary>
    public sealed class ResetPasswordModel
    {
        /// <summary>
        /// Gets or initializes new password.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.PasswordRequired)]
        [StringLength(
            maximumLength: UserConstraintConstants.PasswordMaximumSize,
            ErrorMessage = ModelDataAnnotations.NotAppropriateLength,
            MinimumLength = UserConstraintConstants.PasswordMinimumSize)]
        [DataType(DataType.Password)]
        public string NewPassword { get; init; } = string.Empty;

        /// <summary>
        /// Gets or initializes confirm password.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.ConfirmPasswordRequired)]
        [StringLength(
            maximumLength: UserConstraintConstants.PasswordMaximumSize,
            ErrorMessage = ModelDataAnnotations.NotAppropriateLength,
            MinimumLength = UserConstraintConstants.PasswordMinimumSize)]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = ModelDataAnnotations.PasswordsDoNotMatch)]
        public string ConfirmPassword { get; init; } = string.Empty;

        /// <summary>
        /// Gets or initializes email address.
        /// </summary>
        public string Email { get; init; } = string.Empty;

        /// <summary>
        /// Gets or initializes a token to confirm email adress.
        /// </summary>
        public string Token { get; init; } = string.Empty;
    }
}