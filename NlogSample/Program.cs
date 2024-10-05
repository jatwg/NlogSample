using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using System.Text;

namespace NLogSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            try
            {
                var config = SetupConfiguration();

                using var servicesProvider = SetupServices(config);

                var runner = servicesProvider.GetRequiredService<Runner>();
                runner.DoAction("Action1");

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

        private static IConfigurationRoot SetupConfiguration()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            return new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                //.AddEnvironmentVariables()
                .Build();
        }

        // Good: Services setup method
        private static ServiceProvider SetupServices(IConfigurationRoot config)
        {
            return new ServiceCollection()
                .AddTransient<Runner>()
                // Add more services here
                //.AddSingleton<IMyService, MyServiceImplementation>()
                //.AddScoped<IAnotherService, AnotherServiceImplementation>()
                //.AddTransient<IThirdService, ThirdServiceImplementation>()
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    //loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    loggingBuilder.AddNLog(config);
                }).BuildServiceProvider();
        }
    }
}
