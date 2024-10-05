using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLogSample
{
    public class Runner
    {
        private readonly ILogger<Runner> _logger;

        public Runner(ILogger<Runner> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void DoAction(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Action name cannot be null or empty", nameof(name));
            }

            _logger.LogDebug(20, "Doing hard work! {Action}", name);
        }
    }
}
