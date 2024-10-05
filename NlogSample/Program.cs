using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;

namespace NLogSample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Get the logger for the current class
            var logger = LogManager.GetCurrentClassLogger();
            try
            {
                // Set up the configuration
                var config = SetupConfiguration(args);

                // Set up the dependency injection container and create a service provider
                await using var servicesProvider = SetupServices(config);

                // Resolve the IRunner instance from the service provider
                var runner = servicesProvider.GetRequiredService<IRunner>();
                
                // Perform the tasks
                await runner.DoActionAsync("Action1");

                // Wait for user input before exiting
                Console.WriteLine("Press ANY key to exit");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                // Log any unhandled exceptions
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure NLog shuts down properly
                LogManager.Shutdown();
            }
        }

        /// <summary>
        /// Sets up the configuration for the application
        /// </summary>
        /// <param name="args">Command line arguments</param>
        /// <returns>The configured IConfigurationRoot object</returns>
        private static IConfigurationRoot SetupConfiguration(string[] args)
        {
            // Determine the current environment (defaults to "Production" if not set)
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            // Build and return the configuration
            return new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
        }

        /// <summary>
        /// Sets up the dependency injection container
        /// </summary>
        /// <param name="config">The application configuration</param>
        /// <returns>A configured ServiceProvider</returns>
        private static ServiceProvider SetupServices(IConfigurationRoot config)
        {
            return new ServiceCollection()
                // Register IRunner as a transient service
                .AddTransient<IRunner, Runner>()
                // Register IConfiguration as a singleton
                .AddSingleton<IConfiguration>(config)  
                // Configure logging
                .AddLogging(loggingBuilder =>
                {
                    // Remove any existing logging providers
                    loggingBuilder.ClearProviders();
                    // Set the minimum log level
                    loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    // Add NLog as a logging provider
                    loggingBuilder.AddNLog(config);
                }).BuildServiceProvider();
        }
    }
}
