using Iridium.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Iridium.Infrastructure.Utilities
{
    public static class DeviceDetectionUtility
    {
        public static DeviceType GetDeviceTypeFromContext(HttpContext context)
        {
            var userAgent = context.Request.Headers["User-Agent"].ToString().ToLower().Trim();

            if (userAgent.Contains("iphone") || userAgent.Contains("ipad"))
                return DeviceType.AppleDevice;

            else if (userAgent.Contains("android") || userAgent.Contains("okhttp"))
                return DeviceType.AndroidDevice;

            else if (userAgent.Contains("mozilla") || userAgent.Contains("chrome") || userAgent.Contains("safari"))
                return DeviceType.WebDevice;

            else if (userAgent.Contains("postman"))
                return DeviceType.PostmanClient;

            else
                return DeviceType.Unknown;
        }
    }
}
