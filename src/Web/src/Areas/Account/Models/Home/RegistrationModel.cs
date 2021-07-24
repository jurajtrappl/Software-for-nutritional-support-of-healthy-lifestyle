using Application.Core.Constants;
using Application.Web.Constants;
using System.ComponentModel.DataAnnotations;

namespace Application.Web.Areas.Account.Models.Home
{
    /// <summary>
    /// Model for the registration form.
    /// </summary>
    public sealed class RegistrationModel
    {
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
        /// Gets or initializes email.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.EmailRequired)]
        [EmailAddress(ErrorMessage = ModelDataAnnotations.EmailInvalidFormat)]
        public string Email { get; init; } = string.Empty;

        /// <summary>
        /// Gets or initializes first name.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.FirstNameRequired)]
        [StringLength(
            maximumLength: UserConstraintConstants.FirstNameMaximumSize,
            ErrorMessage = ModelDataAnnotations.NotAppropriateLength,
            MinimumLength = UserConstraintConstants.FirstNameMinimumSize)]
        public string FirstName { get; init; } = string.Empty;

        /// <summary>
        /// Gets or initializes last name.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.LastNameRequired)]
        [StringLength(
            maximumLength: UserConstraintConstants.LastNameMaximumSize,
            ErrorMessage = ModelDataAnnotations.NotAppropriateLength,
            MinimumLength = UserConstraintConstants.LastNameMinimumSize)]
        public string LastName { get; init; } = string.Empty;

        /// <summary>
        /// Gets or initializes username.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.UsernameRequired)]
        [StringLength(
            maximumLength: UserConstraintConstants.UsernameMaximumSize,
            ErrorMessage = ModelDataAnnotations.NotAppropriateLength,
            MinimumLength = UserConstraintConstants.UsernameMinimumSize)]
        [DataType(DataType.Text)]
        public string Username { get; init; } = string.Empty;

        /// <summary>
        /// Gets or initializes password.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.PasswordRequired)]
        [StringLength(
            maximumLength: UserConstraintConstants.PasswordMaximumSize,
            ErrorMessage = ModelDataAnnotations.NotAppropriateLength,
            MinimumLength = UserConstraintConstants.PasswordMinimumSize)]
        [DataType(DataType.Password)]
        public string NewPassword { get; init; } = string.Empty;
    }
}