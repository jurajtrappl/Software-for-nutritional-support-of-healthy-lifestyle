using Application.Core.Common.Interfaces;
using Application.Core.Exercise.Schedulers;
using Application.Core.Interfaces;
using Application.Core.Nutrition.Schedulers;
using Application.Core.Services;
using Application.Core.Services.LogicProviders;
using Application.Infrastructure.Entities;
using Application.Infrastructure.Services;
using Application.Infrastructure.Settings;
using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Application.Web.Extensions
{
    public static class ServiceCollectionInterfaceExtensions
    {
        /// <summary>
        /// Configures and registers Identity service.
        /// </summary>
        /// <param name="services">DI service collection.</param>
        /// <param name="settings">Mongo database settings.</param>
        /// <returns>Modified service collection.</returns>
        public static IServiceCollection AddMongoIdentity(this IServiceCollection services, MongoDbSettings settings)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            services
                .AddIdentity<ApplicationUser, MongoIdentityRole>()
                .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
                .AddMongoDbStores<ApplicationUser, MongoIdentityRole, Guid>(
                    settings.ConnectionString, settings.EatExerciseEnjoyDatabaseName)
                .AddSignInManager()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;

                options.SignIn.RequireConfirmedEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Home/Login";
            });

            return services;
        }

        /// <summary>
        /// Adds custom services from Application.Core.
        /// </summary>
        /// <param name="services">DI service collection.</param>
        /// <returns>Modified service collection.</returns>
        public static IServiceCollection AddBussinessModelServices(this IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services
                .AddSingleton<ApplicationPlansCreator>()
                .AddSingleton<IApplicationPlansSuitabilityService, ApplicationPlansSuitabilityService>()
                .AddSingleton<IPlansSchedulingService, PlansSchedulingService>()
                .AddSingleton<IScheduledItemsDisplayService, ScheduledItemsDisplayService>()
                // nutritional (schedulers)
                .AddSingleton<IMealPlanScheduler, OptimalRatioMealScheduler>()
                .AddSingleton<IDrinkingRegimePlanScheduler, WeightBasedDrinkingRegimeScheduler>()
                .AddSingleton<IExercisePlanScheduler, PercentageReduceExerciseScheduler>()
                // database
                .AddSingleton<IExercisesService, ExercisesService>()
                .AddSingleton<IIngredientsService, IngredientsService>()
                // email
                .AddTransient<IMailService, MailService>()
                // features
                .AddSingleton<ICalendarService, CalendarService>()
                .AddSingleton<IShoppingListService, ShoppingListService>();

            return services;
        }
    }
}