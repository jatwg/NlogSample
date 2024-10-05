# NLog Logging in .NET 8

This sample project demonstrates how to configure and use NLog in a .NET 8 application to log messages to the console and a SQL database.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (or any SQL-compliant database)
- Visual Studio 2022

## Getting Started

### Step 1: Install Nuget Packages

```
dotnet add package Microsoft.Data.SqlClient
dotnet add package Microsoft.Extensions.Configuration.Binder
dotnet add package Microsoft.Extensions.Configuration.CommandLine
dotnet add package Microsoft.Extensions.Configuration.EnvironmentVariables
dotnet add package Microsoft.Extensions.Configuration.Json
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package NLog.Database
dotnet add package NLog.Extensions.Logging
```
### Step 2: Ran script

```
execute GenerateTable.sql
```
### Step 3: Run the Project (F5)
