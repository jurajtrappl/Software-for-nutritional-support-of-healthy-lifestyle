using Application.Web.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Application.Web.Filters
{
    /// <summary>
    /// Attribute that logs what action is about to be executed.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class LogAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ILogger logger = GetLoggerService(context.HttpContext);

            var routeValues = context.HttpContext.Request.RouteValues;
            string httpVerb = context.HttpContext.Request.Method;
            string action = routeValues["action"] as string ?? string.Empty;
            string controller = routeValues["controller"] as string ?? string.Empty;
            string area = routeValues["area"] as string ?? string.Empty;

            logger.LogInformation(
                string.Format(
                    LoggerMessages.ControllerActionEntry,
                    httpVerb,
                    area,
                    controller,
                    action));
        }

        /// <summary>
        /// Returns logger service from <see cref="HttpContext"/>.
        /// </summary>
        /// <param name="context">context to get the logger from.</param>
        private static ILogger GetLoggerService(HttpContext context)
        {
            object loggerService = context.RequestServices
                .GetRequiredService<ILogger<LogAttribute>>();

            return (ILogger)loggerService;
        }
    }
}