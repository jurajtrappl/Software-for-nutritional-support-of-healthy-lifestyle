using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Application.Web
{
    /// <summary>
    /// The projects start.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Application entry point.
        /// </summary>
        /// <param name="args">Command line args.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Configures and builds a Host using Startup.
        /// </summary>
        /// <param name="args">Host builder args.</param>
        /// <returns>Created and configured <see cref="IHostBuilder" />.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}