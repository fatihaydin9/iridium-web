using Iridium.Domain.Common;
using Iridium.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Iridium.Domain.Entities;

public class Log : BaseEntity
{
    public DateTime LogDate { get; set; }

    public LogLevel LogLevel { get; set; }

    public LogType LogType { get; set; }

    public ServiceType ServiceType { get; set; }

    public string? Key { get; set; }

    [StringLength(30)]
    public string? KeyName { get; set; }

    public string? InComing { get; set; }

    public string? OutGoing { get; set; }

    [StringLength(20)]
    public string? UserIp { get; set; }

    [StringLength(50)]
    public string? ServerName { get; set; }

    public DateTime? ResponseStart { get; set; }
    public DateTime? ResponseEnd { get; set; }

    public byte? DeviceType { get; set; }
}