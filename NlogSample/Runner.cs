using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace NLogSample
{
    /// <summary>
    /// Implements the IRunner interface to perform asynchronous actions
    /// </summary>
    public class Runner : IRunner
    {
        private readonly ILogger<Runner> _logger;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the Runner class
        /// </summary>
        /// <param name="logger">The logger instance for this class</param>
        /// <param name="configuration">The configuration instance to access app settings</param>
        /// <exception cref="ArgumentNullException">Thrown if logger or configuration is null</exception>
        public Runner(ILogger<Runner> logger, IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Performs an asynchronous action with the given name
        /// </summary>
        /// <param name="name">The name of the action to perform</param>
        /// <returns>A Task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentException">Thrown if the action name is null or empty</exception>
        public async Task DoActionAsync(string name)
        {
            // Validate input
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Action name cannot be null or empty", nameof(name));
            }

            // Log the start of the action
            _logger.LogInformation("Starting action: {Action}", name);

            try
            {
                // Simulate some initial async work
                await Task.Delay(1000);

                // Retrieve the delay value from configuration
                var delayMs = _configuration.GetValue<int>("Runner:DelayMs", 2000);
                _logger.LogDebug("Retrieved DelayMs setting: {DelayMs}", delayMs);

                // Use the retrieved value for an additional delay
                await Task.Delay(delayMs);

                // Log the completion of the action
                _logger.LogInformation("Completed action: {Action}", name);
            }
            catch (Exception ex)
            {
                // Log any errors that occur during the action
                _logger.LogError(ex, "Error occurred while executing action: {Action}", name);
                // Re-throw the exception to be handled by the caller
                throw;
            }
        }
    }
}
