using Application.Web.Constants;
using Application.Web.Extensions;
using Application.Web.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Application.Web.Areas.Management.Controllers
{
    /// <summary>
    /// Management actions.
    /// </summary>
    [Area(AreaNames.Management)]
    [Log]
    public sealed class CultureController : Controller
    {
        /// <summary>
        /// Web application logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// List of supported cultures that web application is translated to.
        /// </summary>
        private readonly LinkedList<CultureInfo> _supportedCultures;

        public CultureController(IOptions<RequestLocalizationOptions> localizerOptions, ILogger<CultureController> logger)
        {
            _supportedCultures = new(localizerOptions.Value.SupportedCultures);
            _logger = logger;
        }

        /// <summary>
        /// GET: Management/Home/Index.
        /// </summary>
        public IActionResult SetCulture(string currentCultureName, string returnUrl)
        {
            LinkedListNode<CultureInfo>? currentCulture = _supportedCultures.GetNodes()
                .FirstOrDefault(c => c.Value.Name == currentCultureName);
            if (currentCulture is null)
            {
                _logger.LogCritical(string.Format(LoggerMessages.CultureDoesNotExist, currentCulture));
                throw new NullReferenceException(nameof(currentCulture));
            }

            LinkedListNode<CultureInfo>? nextCulture = currentCulture.Next();
            if (nextCulture is null)
            {
                _logger.LogCritical(string.Format(LoggerMessages.CultureDoesNotExist, nextCulture));
                throw new NullReferenceException(nameof(nextCulture));
            }

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(nextCulture.Value.Name)),
                new CookieOptions { 
                    Expires = DateTimeOffset.UtcNow.AddYears(CookiesConstants.CultureDuration),
                    Secure = true,
                    HttpOnly = true,
                    IsEssential = true
                }
            );
            _logger.LogDebug(string.Format(LoggerMessages.AddedToCookies, "Culture", nextCulture));

            return LocalRedirect(returnUrl);
        }
    }
}