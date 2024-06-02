using Iridium.Core.Enums;
using Iridium.Infrastructure.Utilities;
using NLog;
using NLog.Config;
using LogLevel = NLog.LogLevel;

namespace Iridium.Infrastructure.Initializers;

public static class LogInitializer
{
    public static void Initialize(ServiceType serviceType, string connString)
    {
        var config = new LoggingConfiguration();

        var efTarget = new EntityFrameworkTarget
        {
            ServiceType = serviceType,
            MachineName = Environment.MachineName,
            ConnString = connString,
            Layout = "${date:format=HH\\:MM\\:ss} ${logger} ${message}"
        };

        config.AddRule(LogLevel.Info, LogLevel.Fatal, efTarget);
        LogManager.Configuration = config;
    }
}