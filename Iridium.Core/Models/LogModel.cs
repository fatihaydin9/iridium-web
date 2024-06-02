using Iridium.Core.Enums;

namespace Iridium.Core.Models
{
    public class LogModel
    {
        public string? InComing { get; set; }
        public string? OutGoing { get; set; }
        public string IpAddress { get; set; }
        public LogType LogType { get; set; }
        public string KeyName { get; set; }
        public string? Key { get; set; }
        public DateTime? ResponseStart { get; set; }
        public DateTime? ResponseEnd { get; set; }
        public byte? DeviceType { get; set; }
    }
}
