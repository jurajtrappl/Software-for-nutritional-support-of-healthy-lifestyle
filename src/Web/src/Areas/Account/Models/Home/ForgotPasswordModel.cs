using Application.Web.Constants;
using System.ComponentModel.DataAnnotations;

namespace Application.Web.Areas.Account.Models.Home
{
    /// <summary>
    /// Model for the forgot password form.
    /// </summary>
    public sealed class ForgotPasswordModel
    {
        /// <summary>
        /// Gets or initializes email.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.EmailRequired)]
        [EmailAddress(ErrorMessage = ModelDataAnnotations.EmailInvalidFormat)]
        public string RegistrationEmail { get; init; } = string.Empty;
    }
}