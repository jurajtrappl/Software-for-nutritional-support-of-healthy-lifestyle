using Application.Core.Constants;
using Application.Web.Constants;
using System.ComponentModel.DataAnnotations;

namespace Application.Web.Areas.App.Models.Settings
{
    /// <summary>
    /// Model for the settings form that changes profile information.
    /// </summary>
    public sealed class ChangeProfileInformationModel
    {
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
        public string Username { get; init; } = string.Empty;
    }
}