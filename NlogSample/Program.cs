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
            var logger = LogManager.GetCurrentClassLogger();
            try
            {
                var config = SetupConfiguration(args);

                await using var servicesProvider = SetupServices(config);

                var runner = servicesProvider.GetRequiredService<IRunner>();
                // Perform the tasks
                await runner.DoActionAsync("Action1");

                Console.WriteLine("Press ANY key to exit");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        private static IConfigurationRoot SetupConfiguration(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            return new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
        }

        private static ServiceProvider SetupServices(IConfigurationRoot config)
        {
            return new ServiceCollection()
                .AddTransient<IRunner, Runner>()
                .AddSingleton<IConfiguration>(config)  
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    loggingBuilder.AddNLog(config);
                }).BuildServiceProvider();
        }
    }
}
