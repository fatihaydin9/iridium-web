using Iridium.Domain.Enums;
using Iridium.Infrastructure.Utilities;
using LogLevel = NLog.LogLevel;

namespace Iridium.Infrastructure.Initializers
{
    public static class LogInitializer
    {
        public static void Initialize(ServiceType serviceType, string connString)
        {
            var config = new NLog.Config.LoggingConfiguration();

            var efTarget = new EntityFrameworkTarget
            {
                ServiceType = serviceType,
                MachineName = Environment.MachineName,
                ConnString = connString,
                Layout = "${date:format=HH\\:MM\\:ss} ${logger} ${message}"
            };

            config.AddRule(LogLevel.Info, LogLevel.Fatal, efTarget);
            NLog.LogManager.Configuration = config;
        }
    }
}
