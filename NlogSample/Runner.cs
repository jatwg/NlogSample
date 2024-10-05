using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace NLogSample
{
    public class Runner : IRunner
    {
        private readonly ILogger<Runner> _logger;
        private readonly IConfiguration _configuration;

        public Runner(ILogger<Runner> logger, IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task DoActionAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Action name cannot be null or empty", nameof(name));
            }

            _logger.LogInformation("Starting action: {Action}", name);

            try
            {
                // Simulate some async work
                await Task.Delay(1000);

                // Pull value from appsettings.json
                var delayMs = _configuration.GetValue<int>("Runner:DelayMs", 2000);
                _logger.LogDebug("Retrieved DelayMs setting: {DelayMs}", delayMs);

                // Use the retrieved value
                await Task.Delay(delayMs);

                _logger.LogInformation("Completed action: {Action}", name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while executing action: {Action}", name);
                throw;
            }
        }
    }
}
