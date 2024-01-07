using Iridium.Domain.Enums;
using Newtonsoft.Json;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using NLog;
using System.Diagnostics;
using Iridium.Infrastructure.Models;
using Iridium.Domain.Entities;
using LogLevel = Iridium.Domain.Enums.LogLevel;
using Iridium.Infrastructure.Contexts;

namespace Iridium.Infrastructure.Utilities
{
    [Target("EntityFramework")]
    public class EntityFrameworkTarget : TargetWithLayout
    {
        #region Properties

        [RequiredParameter]
        public ServiceType ServiceType { get; set; }

        [RequiredParameter]
        public Layout MachineName { get; set; }

        [RequiredParameter]
        public Layout ConnString { get; set; }

        #endregion

        #region Methods

        private readonly ApplicationDbContext dbContext;

        protected override void Write(LogEventInfo logEvent)
        {
            using (var context = new ApplicationDbContext(ConnString.ToString()))
            {
                try
                {
                    var param = logEvent.Parameters?.FirstOrDefault(p => p.GetType() == typeof(LogModel));
                    if (param != null)
                    {
                        var logParam = (LogModel)param;
                        dbContext.Log.Add(new Log
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
                        });
                    }
                    else if ((logEvent.Level == NLog.LogLevel.Error) && (logEvent.Exception != null))
                    {
                        int idx = 0;
                        var lst = new List<LogExceptionModel>();
                        var ipAddress = "";
                        var curExc = logEvent.Exception;

                        while (true)
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

                        dbContext.Log.Add(new Log
                        {
                            LogDate = DateTime.Now,
                            LogLevel = GetLogLevel(logEvent),
                            LogType = LogType.Common,
                            ServiceType = ServiceType,
                            InComing = logEvent.Message,
                            OutGoing = exceptionString,
                            ServerName = MachineName.Render(logEvent),
                            UserIp = ipAddress,
                        });
                    }

                    dbContext.SaveChanges();
                }
                catch (Exception exc)
                {
                    EventLog.WriteEntry("Iridium[NLog]:", "İzleme kaydı yazılırken bir hata oluştu.\n" + exc, EventLogEntryType.Error, 50001);
                    throw;
                }
            }
        }

        public LogLevel GetLogLevel(LogEventInfo logEvent)
        {
            return logEvent.Level == NLog.LogLevel.Debug
                ? LogLevel.Debug : logEvent.Level == NLog.LogLevel.Error
                ? LogLevel.Error : logEvent.Level == NLog.LogLevel.Warn ? LogLevel.Warning : LogLevel.Info;
        }
    }

    #endregion
}

