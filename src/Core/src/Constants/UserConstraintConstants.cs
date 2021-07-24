namespace Application.Core.Constants
{
    /// <summary>
    /// Defines the length limits of values describing user.
    /// </summary>
    public static class UserConstraintConstants
    {
        /// <summary>
        /// The maximum possible size of the user's first name.
        /// </summary>
        public const int FirstNameMaximumSize = 20;

        /// <summary>
        /// The minimum possible size of the user's first name.
        /// </summary>
        public const int FirstNameMinimumSize = 2;

        /// <summary>
        /// The maximum possible size of the user's last name.
        /// </summary>
        public const int LastNameMaximumSize = 20;

        /// <summary>
        /// The minimum possible size of the user's last name.
        /// </summary>
        public const int LastNameMinimumSize = 2;

        /// <summary>
        /// The maximum possible size of the user's password.
        /// </summary>
        public const int PasswordMaximumSize = 20;

        /// <summary>
        /// The minimum possible size of the user's password.
        /// </summary>
        public const int PasswordMinimumSize = 6;

        /// <summary>
        /// The maximum possible size of the user's username.
        /// </summary>
        public const int UsernameMaximumSize = 20;

        /// <summary>
        /// The minimum possible size of the user's username.
        /// </summary>
        public const int UsernameMinimumSize = 1;
    }
}