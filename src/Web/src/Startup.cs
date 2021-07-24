using Application.Core.Common.Constants;
using Application.Infrastructure.Config;
using Application.Infrastructure.Settings;
using Application.Web.Constants;
using Application.Web.Extensions;
using Application.Web.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Application.Web
{
    /// <summary>
    /// Configures services and the apps request pipeline.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="configuration">Configuration properties.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Gets configuration properties.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Collection of service descriptors.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            #region settings

            services.Configure<MongoDbSettings>(Configuration.GetSection(nameof(MongoDbSettings)));
            services.Configure<MailSettings>(Configuration.GetSection(nameof(MailSettings)));

            var mongoDbSettings = Configuration.GetSection(nameof(MongoDbSettings))
                .Get<MongoDbSettings>();
            services.AddSingleton(mongoDbSettings);

            #endregion settings

            #region services

            // own services that support web application logic (scheduling, planning, displaying, querying etc.)
            services.AddBussinessModelServices();

            // nosql database provider
            services.AddSingleton<IMongoClient>(mc => new MongoClient(mongoDbSettings.ConnectionString));

            // supports UI login functionality, manages users, passwords, etc.
            services.AddMongoIdentity(mongoDbSettings);

            // automaps between two objects
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // supports customization for specific cultures or regions
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services
                .AddMvc(options =>
                {
                    // global filters
                    options.Filters.Insert(0, new ErrorFilter());
                    options.Filters.Insert(1, new AutoValidateAntiforgeryTokenAttribute());
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                })
                .AddRazorOptions(options =>
                {
                    // 1. /Areas/{2}/Views/{1}/{0}.cshtml
                    // 2. /Areas/{2}/Views/Shared/{0}.cshtml
                    // 3. /Views/Shared/{0}.cshtml
                    options.AreaViewLocationFormats.Add("/SharedGlobal/Areas/{2}.cshtml");
                })
                .AddMvcLocalization(
                    // culture is part of the name
                    format: LanguageViewLocationExpanderFormat.Suffix,
                    localizationOptionsSetupAction: options =>
                    {
                        options.ResourcesPath = "Resources";
                    }
                );

            // csrf (xsrf) attacks
            services.AddAntiforgery(options =>
            {
                options.FormFieldName = "__RequestVerificationToken";
            });

            // endpoint routing configuration
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            services.AddControllersWithViews();

            #endregion services

            #region configuration

            // modifies default behaviour for bson serialization/deserialization process
            ConfigureConventions.AddConventionsPacks();

            // adds serializers for entities and types of Application
            ConfigureBsonSerializers.AddBsonSerializers();

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(2);
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                // range of cultures and languages to support
                var supportedCultures = new[] {
                    new CultureInfo(CultureDescriptors.Slovak),
                    new CultureInfo(CultureDescriptors.Czech)
                };

                options.DefaultRequestCulture = new(supportedCultures.First().Name, supportedCultures.First().Name);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            // forcing https
            services.Configure<MvcOptions>(options =>
            {
                options.SslPort = 443;
                options.Filters.Add(new RequireHttpsAttribute());
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = (context) => true;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
                options.HttpOnly = HttpOnlyPolicy.Always;
                options.Secure = CookieSecurePolicy.SameAsRequest;
            });

            #endregion configuration
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Mechanism to configure application's request pipeline.</param>
        /// <param name="env">Information about the web hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Areas/Errors/Home/ApplicationError");

            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            // glob & loc
            var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizationOptions?.Value);

            // creates the routing table directs requests to controller actions
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "area",
                    "{area=Main}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapFallbackToAreaController(
                    nameof(Areas.Errors.Controllers.HomeController.ApplicationError),
                    ControllerNames.Home,
                    AreaNames.Errors);
            });

            // when exportning a calendar, we use unsupported encoding set
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
    }
}