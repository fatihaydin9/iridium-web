using Iridium.Domain.Enums;
using Iridium.Infrastructure.Models;
using ILogger = NLog.ILogger;

namespace Iridium.Infrastructure.Utilities;

public static class NLogUtils
{
    public static void Bilgi(this ILogger logger, string message)
    {
        logger.Info(message);
    }

    public static void Bilgi(this ILogger logger, string inComing, string outGoing, string ipAddress, LogType logType, string keyName = null, string? key = null, DateTime? responseStart = null, DateTime? responseEnd = null, byte? deviceType = (byte)DeviceType.Unknown)
    {
        logger.Info(string.Empty, new LogModel
        {
            InComing = inComing,
            OutGoing = outGoing,
            IpAddress = ipAddress,
            LogType = logType,
            KeyName = keyName,
            Key = key,
            ResponseStart = responseStart,
            ResponseEnd = responseEnd,
            DeviceType = deviceType
        });
    }

    public static void Hata(this ILogger logger, string inComing, string outGoing, string ipAddress, LogType logType, string keyName = null, string? key = null, DateTime? responseStart = null, DateTime? responseEnd = null, byte? deviceType = (byte)DeviceType.Unknown)
    {
        logger.Error(string.Empty, new LogModel
        {
            InComing = inComing,
            OutGoing = outGoing,
            IpAddress = ipAddress,
            LogType = logType,
            KeyName = keyName,
            Key = key,
            ResponseStart = responseStart,
            ResponseEnd = responseEnd,
            DeviceType = deviceType
        });
    }

    public static void Hata(this ILogger logger, string message)
    {
        logger.Error(message);
    }

    public static void Hata(this ILogger logger, string inComing, string outGoing)
    {
        logger.Error(inComing, outGoing);
    }
}