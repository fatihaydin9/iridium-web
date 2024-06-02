using Iridium.Core.Auth;
using Iridium.Core.Enums;
using Iridium.Core.Models;
using Iridium.Domain.Entities;
using Iridium.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using LogLevel = Iridium.Core.Enums.LogLevel;

namespace Iridium.Infrastructure.Utilities;

[Target("EntityFramework")]
public class EntityFrameworkTarget : TargetWithLayout
{
    #region Properties

    [RequiredParameter] public ServiceType ServiceType { get; set; }

    [RequiredParameter] public Layout MachineName { get; set; }

    [RequiredParameter] public Layout ConnString { get; set; }

    #endregion

    #region Methods

    protected override void Write(LogEventInfo logEvent)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(ConnString.ToString());

        using (var context = new ApplicationDbContext(optionsBuilder.Options, new MockUserService()))
        {
            var param = logEvent.Parameters?.FirstOrDefault(p => p.GetType() == typeof(LogModel));
            if (param != null)
            {
                var logParam = (LogModel)param;
                var log = new Log
                {
                    LogDate = DateTime.Now,
                    ServiceType = ServiceType,
                    LogType = logParam.LogType,
                    Key = logParam.Key,
                    KeyName = logParam.KeyName,
                    LogLevel = GetLogLevel(logEvent),
                    InComing = logParam.InComing,
                    OutGoing = logParam.OutGoing,
                    ServerName = MachineName.Render(logEvent),
                    UserIp = logParam.IpAddress,
                    ResponseStart = logParam.ResponseStart,
                    ResponseEnd = logParam.ResponseEnd,
                    DeviceType = logParam.DeviceType,
                    CreatedDate = DateTime.Now,
                    CreatedBy = 1
                };
                context.Log.Add(log);
            }
            else if (logEvent.Level == NLog.LogLevel.Error && logEvent.Exception != null)
            {
                var idx = 0;
                var lst = new List<LogExceptionModel>();
                var ipAddress = "";
                var curExc = logEvent.Exception;

                while (true)
                    if (curExc != null)
                    {
                        lst.Add(new LogExceptionModel
                        {
                            Tag = $"[{idx++} Index Exception]",
                            Message = curExc.Message,
                            StackTrace = curExc.StackTrace
                        });

                        if (curExc.InnerException == null)
                            break;

                        curExc = curExc.InnerException;
                    }

                var exceptionString = JsonConvert.SerializeObject(lst, Formatting.None);

                var log = new Log
                {
                    LogDate = DateTime.Now,
                    LogLevel = GetLogLevel(logEvent),
                    LogType = LogType.Common,
                    ServiceType = ServiceType,
                    InComing = logEvent.Message,
                    OutGoing = exceptionString,
                    ServerName = MachineName.Render(logEvent),
                    UserIp = ipAddress,
                    CreatedDate = DateTime.Now,
                    CreatedBy = 1
                };
                context.Log.Add(log);
            }

            context.SaveChanges();
        }
    }

    private LogLevel GetLogLevel(LogEventInfo logEvent)
    {
        return logEvent.Level == NLog.LogLevel.Debug
            ? LogLevel.Debug
            : logEvent.Level == NLog.LogLevel.Error
                ? LogLevel.Error
                : logEvent.Level == NLog.LogLevel.Warn
                    ? LogLevel.Warning
                    : LogLevel.Info;
    }
}

#endregion