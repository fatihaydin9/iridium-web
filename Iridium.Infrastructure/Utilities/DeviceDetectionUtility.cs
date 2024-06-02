using Iridium.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace Iridium.Infrastructure.Utilities;

public static class DeviceDetectionUtility
{
    public static DeviceType GetDeviceTypeFromContext(HttpContext context)
    {
        var userAgent = context.Request.Headers["Auth-Agent"].ToString().ToLower().Trim();

        if (userAgent.Contains("iphone") || userAgent.Contains("ipad"))
            return DeviceType.AppleDevice;

        if (userAgent.Contains("android") || userAgent.Contains("okhttp"))
            return DeviceType.AndroidDevice;

        if (userAgent.Contains("mozilla") || userAgent.Contains("chrome") || userAgent.Contains("safari"))
            return DeviceType.WebDevice;

        if (userAgent.Contains("postman"))
            return DeviceType.PostmanClient;

        return DeviceType.Unknown;
    }
}