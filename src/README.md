# SerilogUI
A UI provider for serilog to allow users to change log levels without changing appsettings.json or restarting the application.

## Steps   
1. Install **Serilog.Aspnetcore.Configuration.UI** Package in .NET 7 Web API
2. Add below code 
```Csharp
    var logLevelSwitch = new LoggingLevelSwitch();
    
    builder.Logging.ClearProviders();
      builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
      {
        loggerConfiguration.ReadFrom.Configuration(config);
        loggerConfiguration.AddLogLevelManager(logLevelSwitch, config);
      });
```
3. Add below code to register all dependencies
```Csharp
    builder.Services.AddSerilogLogLevelConfigurationUI(logLevelSwitch);
```
4. Add Below code to configure Middleware in pipeline.
```Csharp
    app.UseSerlogConfigurationUI();
```
  
## Samples

Sample folder contains a sample implementation with a test Web API 

## How to change Log levels

1. Run application 
2. Open new browser tab or new browser window
3. Open Url http:<API host: Port>/log/ui e.g http://localhost:4000/log/ui
4. on configuration page choose log level that you want to set and then click on "Set Log Level" button and it will change "Current Log Level"

