using Application.Web.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Web.Filters
{
    /// <summary>
    /// Takes care about raised exceptions and routes the context further to general exception view.
    /// </summary>
    public sealed class ErrorFilter : IExceptionFilter
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void OnException(ExceptionContext context)
        {
            ILogger logger = GetLoggerService(context.HttpContext);
            logger.LogError(context.Exception.Message);
            context.ExceptionHandled = true;

            RedirectToActionResult result = new(
                actionName: nameof(Areas.Errors.Controllers.HomeController.ApplicationError),
                controllerName: ControllerNames.Home,
                routeValues: new { area = AreaNames.Errors });
            context.Result = result;
        }

        /// <summary>
        /// Returns logger service from <see cref="HttpContext"/>.
        /// </summary>
        /// <param name="context">context to get the logger from.</param>
        private static ILogger GetLoggerService(HttpContext context)
        {
            object loggerService = context.RequestServices
                .GetRequiredService<ILogger<ErrorFilter>>();

            return (ILogger)loggerService;
        }
    }
}