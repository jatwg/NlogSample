# NLogSample - Logging with NLog in .NET

This project demonstrates the use of NLog for logging in a .NET application with dependency injection and configuration management.

## Project Structure

The project consists of the following main files:

1. `Program.cs`: Contains the main entry point and setup logic.
2. `Runner.cs`: Implements the `IRunner` interface to perform asynchronous actions.
3. `IRunner.cs`: Defines the interface for the Runner.
4. `appsettings.json`: Configuration file for the application.
5. `Nlog.config`: Configuration file for NLog.

## Configuration

### appsettings.json

The `appsettings.json` file contains the following configurations:

- Logging levels for different namespaces
- Runner configuration (e.g., delay in milliseconds)

```json
{
    "Logging": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft": "Warning",
        "Microsoft.Hosting.Lifetime": "Information"
      }
    },
    "Runner": {
      "DelayMs": 3000
    }
}
```

### Nlog.config

The `Nlog.config` file configures NLog with the following targets:

- Database: Logs are stored in a SQL Server database
- File: Logs are written to a file
- Console: Logs are output to the console

## Program.cs

The `Program.cs` file contains the main logic for the application:

### Main Method

- Sets up logging using NLog
- Configures the application
- Sets up dependency injection
- Resolves and runs an instance of `IRunner`
- Handles exceptions and ensures proper shutdown of NLog

### SetupConfiguration Method

- Determines the current environment
- Builds and returns an `IConfigurationRoot` object
- Loads settings from JSON files, environment variables, and command-line arguments

### SetupServices Method

- Sets up the dependency injection container
- Registers `IRunner` and `IConfiguration`
- Configures logging with NLog

## Runner.cs

The `Runner.cs` file implements the `IRunner` interface:

### Runner Class

- Initializes with `ILogger<Runner>` and `IConfiguration` dependencies
- Implements the `DoActionAsync` method

### DoActionAsync Method

- Validates the input
- Logs the start and completion of the action
- Simulates work with delays
- Retrieves a delay value from the configuration
- Handles and logs any errors that occur during execution

## IRunner.cs

The `IRunner.cs` file defines the contract for the Runner:

```csharp
public interface IRunner
{
    Task DoActionAsync(string name);
}
```

## Usage

To use this application:

1. Ensure proper configuration in `appsettings.json` and `Nlog.config`.
2. Set the `ASPNETCORE_ENVIRONMENT` variable if needed (defaults to "Production").
3. Run the application.
4. The application will perform the configured action and wait for user input before exiting.

## Dependencies

- Microsoft.Extensions.Configuration
- Microsoft.Extensions.DependencyInjection
- Microsoft.Extensions.Logging
- NLog
- NLog.Extensions.Logging

## Error Handling

The application uses NLog to log any unhandled exceptions. In case of an error, check the logs for detailed information.

## Logging

Logs are written to:
- SQL Server database
- File (`c:\nlog\nlog-details.log`)
- Console

The log level can be configured in `appsettings.json` and `Nlog.config`.

## Environment-specific Configuration

The application supports environment-specific configuration files (e.g., `appsettings.Development.json`). The environment is determined by the `ASPNETCORE_ENVIRONMENT` variable.

## Command-line Arguments

The application supports configuration through command-line arguments, which can override settings from other sources.
