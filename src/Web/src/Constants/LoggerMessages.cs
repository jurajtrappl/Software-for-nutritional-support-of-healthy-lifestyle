namespace Application.Web.Constants
{
    /// <summary>
    /// Defines messages for web application logger.
    /// </summary>
    internal static class LoggerMessages
    {
        internal const string AddedToCookies = "{0}: {1} has been added to the cookies.";

        internal const string AlreadyRegisteredEmail = "User with the same email address as: {0} is already registered.";

        internal const string AlreadyRegisteredUsername = "User with the same name as: {0} is already registered.";

        internal const string AssignedPlanFromCookies = "Assigned {0} plan from the cookies for: {1}.";

        internal const string Attempt = "{0} attempt for {1}.";

        internal const string ControllerActionEntry = "{0}: {1}/{2}/{3}.";

        internal const string CultureDoesNotExist = "Culture: {0} does not exist.";

        internal const string EmailHasBeenSent = "{0} email has been sent.";

        internal const string EmailNotConfirmed = "Loggin attempt to the account with the username: {0} that has not confirmed email yet.";

        internal const string ApplicationPlanEnd = "{0} has ended application plan: {1}.";

        internal const string SuitablePlansFound = "Suitable plans for the user with {0} kg, {1} cm and indicator whether is frequently exercising: {2}; are: {3}.";

        internal const string AttemptConfirmed = "{0} attempt for the email address: {1} ends up being confirmed.";

        internal const string InvalidAttempt = "Attempt to {0} was invalid for {1}.";

        internal const string InvalidDataInResources = "Invalid data: {0} in the resources.";

        internal const string InvalidDataInView = "Invalid data: {0} in the view.";

        internal const string InvalidModelState = "{0} invalid model state. Re-displaying the view.";

        internal const string LockedOutLoginAttempt = "Login attempt to locked out account with the username: {0}.";

        internal const string NotSuitablePlanChosen = "{0} has chosen not suitable plan.";

        internal const string PasswordEmailConfirmationSent = "Password recovery email verification email has been sent.";

        internal const string RegisterEmailConfirmationSent = "Register confirmation email has been sent.";

        internal const string SameApplicationPlan = "{0} has chosen the same application plan.";

        internal const string ScheduledPlans = "Plans were scheduled for {0}.";

        internal const string SuccessfulAttempt = "Attempt to {0} was successful for {1}.";

        internal const string UserLoggedOut = "User with the username: {0} has been logged out.";

        internal const string UserWithEmailDoesNotLongerExist = "User with the email address: {0} does not longer exist.";

        internal const string WelcomeEmailSent = "Welcome email was sent.";

        internal const string WrongPassword = "Wrong password for user: {0}.";
    }
}