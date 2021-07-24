using Application.Web.Constants;
using System.ComponentModel.DataAnnotations;

namespace Application.Web.Areas.Account.Models.Home
{
    /// <summary>
    /// Model for the login form.
    /// </summary>
    public sealed class LoginModel
    {
        /// <summary>
        /// Gets or initializes username.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.UsernameRequired)]
        [DataType(DataType.Text)]
        public string Username { get; init; } = string.Empty;

        /// <summary>
        /// Gets or initializes password.
        /// </summary>
        [Required(ErrorMessage = ModelDataAnnotations.PasswordRequired)]
        [DataType(DataType.Password)]
        public string Password { get; init; } = string.Empty;
    }
}