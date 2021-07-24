using Application.Core.Common.Dto;
using Application.Core.Common.Enums;
using Application.Core.Dto;
using Application.Core.Interfaces;
using Application.Infrastructure.Entities;
using Application.Web.Areas.Account.Models.Home;
using Application.Web.Constants;
using Application.Web.Filters;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Application.Web.Areas.Account.Controllers
{
    /// <summary>
    /// Account actions.
    /// </summary>
    [Area(AreaNames.Account)]
    [Log]
    public class HomeController : ExtendedBaseController
    {
        /// <summary>
        /// Localizer of controller resources.
        /// </summary>
        private readonly IHtmlLocalizer _controllerLocalizer;

        /// <summary>
        /// Web application logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Automapper between two objects.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Identity sing in manager.
        /// </summary>
        private readonly SignInManager<ApplicationUser> _signInManager;

        /// <summary>
        /// Localizer for shared resources.
        /// </summary>
        private readonly IStringLocalizer _sharedLocalizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController" /> with DI injected <see
        /// cref="UserManager{ApplicationUser}" /> and <see cref="SignInManager{ApplicationUser}" />.
        /// </summary>
        /// <param name="userManager">user manager of <see cref="ApplicationUser" />.</param>
        /// <param name="signInManager">sign in manager of <see cref="ApplicationUser" />.</param>
        public HomeController(
            IHtmlLocalizer<HomeController> controllerLocalizer,
            ILogger<HomeController> logger,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            IStringLocalizer<SharedResources> sharedLocalizer,
            SignInManager<ApplicationUser> signInManager) : base(userManager)
        {
            (_controllerLocalizer, _logger, _mapper, _signInManager, _sharedLocalizer) =
                (controllerLocalizer, logger, mapper, signInManager, sharedLocalizer);
        }

        /// <summary>
        /// GET: Account/Home/Login.
        /// </summary>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// POST: Account/Home/Login.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            LoginModel modelWithoutPassword = new() { Username = model.Username };

            if (!ModelState.IsValid)
            {
                _logger.LogDebug(string.Format(LoggerMessages.InvalidModelState, nameof(Login)));
                return View(modelWithoutPassword);
            }

            _logger.LogInformation(string.Format(LoggerMessages.Attempt, nameof(Login), model.Username));

            ApplicationUser user = await UserManager.FindByNameAsync(model.Username);
            if (user is not null)
            {
                if (!await UserManager.IsEmailConfirmedAsync(user))
                {
                    _logger.LogDebug(string.Format(LoggerMessages.EmailNotConfirmed, model.Username));
                    ModelState.AddModelError(string.Empty, _controllerLocalizer["EmailNotConfirmed"].Value);
                    return View(modelWithoutPassword);
                }
            }

            var result = await _signInManager.PasswordSignInAsync(
                model.Username,
                model.Password,
                isPersistent: true,
                lockoutOnFailure: true);

            if (result.Succeeded)
            {
                _logger.LogDebug(LoggerMessages.SuccessfulAttempt, nameof(Login), model.Username);
                return RedirectToAction(
                    nameof(App.Controllers.HomeController.Index),
                    ControllerNames.Home,
                    new { area = AreaNames.App });
            }

            if (result.IsLockedOut)
            {
                _logger.LogDebug(string.Format(LoggerMessages.LockedOutLoginAttempt, model.Username));
                ModelState.AddModelError(string.Empty, _controllerLocalizer["LockedOut"].Value);
                return View(modelWithoutPassword);
            }

            _logger.LogWarning(string.Format(LoggerMessages.InvalidAttempt, nameof(Login), model.Username));
            ModelState.AddModelError(string.Empty, _controllerLocalizer["InvalidLoginAttempt"].Value);
            return View(modelWithoutPassword);
        }

        /// <summary>
        /// GET: Account/Home/ForgotPassword.
        /// </summary>
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        /// <summary>
        /// POST: Account/Home/ForgotPassword.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromServices] IMailService mailService, ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogDebug(string.Format(LoggerMessages.InvalidModelState, nameof(ForgotPassword)));
                return View(model);
            }

            _logger.LogInformation(string.Format(LoggerMessages.Attempt, nameof(ForgotPassword), model.RegistrationEmail));

            ApplicationUser user = await UserManager.FindByEmailAsync(model.RegistrationEmail);
            if (user is null || !(await UserManager.IsEmailConfirmedAsync(user)))
            {
                _logger.LogDebug(string.Format(LoggerMessages.AttemptConfirmed, nameof(ForgotPassword), model.RegistrationEmail));
                TempData.Add(TempDataMessages.ForgotPasswordConfirmation, _controllerLocalizer["ForgotPasswordConfirmation"].Value);
                return View();
            }

            await SendResetPasswordEmailConfirmation(mailService, user);
            _logger.LogInformation(string.Format(LoggerMessages.EmailHasBeenSent, nameof(ForgotPassword)));

            TempData.Add(TempDataMessages.ForgotPasswordConfirmation, _controllerLocalizer["ForgotPasswordConfirmation"].Value);
            _logger.LogDebug(string.Format(LoggerMessages.SuccessfulAttempt, nameof(ForgotPassword), model.RegistrationEmail));
            return View();
        }

        /// <summary>
        /// GET: Account/Home/ResetPassword.
        /// </summary>
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            ResetPasswordModel model = new() { Email = email, Token = token };
            return View(model);
        }

        /// <summary>
        /// POST: Account/Home/ResetPassword.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            ResetPasswordModel modelWithoutPassword = new();

            if (!ModelState.IsValid)
            {
                _logger.LogDebug(string.Format(LoggerMessages.InvalidModelState, nameof(ResetPassword)));
                return View(modelWithoutPassword);
            }

            _logger.LogInformation(string.Format(LoggerMessages.Attempt, nameof(ResetPassword), model.Email));

            ApplicationUser user = await UserManager.FindByEmailAsync(model.Email);
            IdentityResult result = await UserManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (user is null || result.Succeeded)
            {
                _logger.LogDebug(string.Format(LoggerMessages.AttemptConfirmed, nameof(ResetPassword), model.Email));
                TempData.Add(TempDataMessages.ResetPasswordConfirmation, _controllerLocalizer["ResetPasswordConfirmation"].Value);
                return View(modelWithoutPassword);
            }

            _logger.LogWarning(string.Format(LoggerMessages.InvalidAttempt, nameof(ResetPassword), model.Email));
            AddErrors(result);
            return View(modelWithoutPassword);
        }

        /// <summary>
        /// GET: Account/Home/Register.
        /// </summary>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// POST: Account/Home/Register.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Register([FromServices] IMailService mailService, RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogDebug(string.Format(LoggerMessages.InvalidModelState, nameof(Register)));
                return View(model);
            }

            _logger.LogInformation(string.Format(LoggerMessages.Attempt, nameof(Register), model.Email));

            ApplicationUser user = _mapper.Map<ApplicationUser>(model);
            user.HoursConfig = TimeConfig.Default.Values;
            ApplicationUser sameUsernameUser = await UserManager.FindByNameAsync(user.UserName);
            if (sameUsernameUser is not null)
            {
                _logger.LogDebug(string.Format(LoggerMessages.AlreadyRegisteredUsername, model.Username));
                ModelState.AddModelError(string.Empty, _sharedLocalizer["AlreadyRegisteredUsername"].Value);
                return View(model);
            }

            ApplicationUser sameEmailAddressUser = await UserManager.FindByEmailAsync(user.Email);
            if (sameEmailAddressUser is not null)
            {
                _logger.LogDebug(string.Format(LoggerMessages.AlreadyRegisteredEmail, model.Email));
                ModelState.AddModelError(string.Empty, _sharedLocalizer["AlreadyRegisteredEmail"].Value);
                return View(model);
            }

            if (Request.Cookies.ContainsKey(CookiesConstants.ChosenPlanInAdvanceName))
            {
                string planName = Request.Cookies[CookiesConstants.ChosenPlanInAdvanceName]!;
                if (Enum.TryParse(planName, out ApplicationPlan applicationPlan))
                {
                    _logger.LogDebug(string.Format(LoggerMessages.AssignedPlanFromCookies, planName, model.Username));
                    user.AppPlan = applicationPlan;
                }
                else
                {
                    _logger.LogCritical(string.Format(LoggerMessages.InvalidDataInView, planName));
                }

                Response.Cookies.Delete(CookiesConstants.ChosenPlanInAdvanceName);
            }

            IdentityResult result = await UserManager.CreateAsync(user, model.NewPassword);
            if (result.Succeeded)
            {
                _logger.LogDebug(string.Format(LoggerMessages.SuccessfulAttempt, nameof(Register), model.Username));

                SendWelcomeEmail(mailService, user);
                _logger.LogInformation(LoggerMessages.EmailHasBeenSent, "Welcome");
                await SendRegisterEmailConfirmation(mailService, user);
                _logger.LogInformation(LoggerMessages.EmailHasBeenSent, nameof(Register));

                return RedirectToAction(
                    nameof(SuccessfulRegistration),
                    ControllerNames.Home,
                    new { area = AreaNames.Account });
            }

            _logger.LogWarning(string.Format(LoggerMessages.InvalidAttempt, nameof(Register), model.Username));
            AddErrors(result);
            return View(model);
        }

        /// <summary>
        /// GET: Account/Home/ConfirmEmail.
        /// </summary>
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            _logger.LogInformation(string.Format(LoggerMessages.Attempt, nameof(ConfirmEmail), email));

            ApplicationUser user = await UserManager.FindByEmailAsync(email);
            if (user is null)
            {
                _logger.LogDebug(string.Format(LoggerMessages.UserWithEmailDoesNotLongerExist, email));
                TempData.Add(TempDataMessages.UserWithEmailDoesNotLongerExist, _controllerLocalizer["UserWithEmailDoesNotLongerExist"].Value);
                return View();
            }

            IdentityResult result = await UserManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                _logger.LogDebug(string.Format(LoggerMessages.SuccessfulAttempt, nameof(ConfirmEmail), email));
                return View(nameof(ConfirmEmail));
            }

            _logger.LogWarning(string.Format(LoggerMessages.InvalidAttempt, nameof(ConfirmEmail), email));
            AddErrors(result);
            return View();
        }

        /// <summary>
        /// POST: Account/Home/Logout.
        /// </summary>
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            await _signInManager.SignOutAsync();
            _logger.LogInformation(string.Format(LoggerMessages.UserLoggedOut, user.UserName));

            return RedirectToAction(
                nameof(Main.Controllers.HomeController.Index),
                ControllerNames.Home,
                new { area = AreaNames.Main });
        }

        /// <summary>
        /// GET: App/Home/SuccessfulRegistration.
        /// </summary>
        public IActionResult SuccessfulRegistration()
        {
            return View();
        }

        /// <summary>
        /// Sends the welcome email to the given users email account.
        /// </summary>
        /// <param name="mailService">infrastructure mail service.</param>
        /// <param name="user">user to whom is the mail being sent.</param>
        private void SendWelcomeEmail(IMailService mailService, ApplicationUser user)
        {
            MailRequest welcomeRequest = new(
                toEmail: user.Email,
                subject: string.Format(_controllerLocalizer["WelcomeSubject"].Value, user.UserName),
                body: _controllerLocalizer["WelcomeBody"].Value);

            _ = mailService.SendEmailAsync(welcomeRequest);
        }

        /// <summary>
        /// Sends the confirmation link of the email account to the given users email account.
        /// </summary>
        /// <param name="mailService">infrastructure mail service.</param>
        /// <param name="user">user to whom is the mail being sent.</param>
        private async Task SendRegisterEmailConfirmation(IMailService mailService, ApplicationUser user)
        {
            string token = await UserManager.GenerateEmailConfirmationTokenAsync(user);
            string callbackUrl = Url.Action(
                nameof(ConfirmEmail),
                ControllerNames.Home,
                new { area = AreaNames.Account, token, email = user.Email },
                protocol: Request.Scheme);

            MailRequest emailRequest = new(
                toEmail: user.Email,
                subject: _controllerLocalizer["EmailConfirmationSubject"].Value,
                body: string.Format(_controllerLocalizer["EmailConfirmationBody"].Value, callbackUrl));

            _ = mailService.SendEmailAsync(emailRequest);
        }

        /// <summary>
        /// Sends the email with password reset token to the given users email account.
        /// </summary>
        /// <param name="mailService">infrastructure mail service.</param>
        /// <param name="user">user to whom is the mail being sent.</param>
        private async Task SendResetPasswordEmailConfirmation(IMailService mailService, ApplicationUser user)
        {
            string token = await UserManager.GeneratePasswordResetTokenAsync(user);
            string callbackUrl = Url.Action(
                nameof(ResetPassword),
                ControllerNames.Home,
                new { token, email = user.Email },
                protocol: Request.Scheme);

            MailRequest confirmationMailRequest = new(
                toEmail: user.Email,
                subject: _controllerLocalizer["ResetPasswordEmailConfirmationSubject"].Value,
                body: string.Format(_controllerLocalizer["ResetPasswordEmailConfirmationBody"].Value, callbackUrl));

            _ = mailService.SendEmailAsync(confirmationMailRequest);
        }
    }
}