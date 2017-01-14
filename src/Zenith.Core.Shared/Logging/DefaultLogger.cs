using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Shared.Logging
{
    public class DefaultLogger : IDefaultLogger
    {
        private Logger _logger = null;

        public void Create(string name)
        {
            _logger = LogManager.GetLogger(name);
        }

        public void Log(LogType type, string message)
        {
            LogLevel level = LogLevel.Off;

            if (type == LogType.Error)
                level = LogLevel.Error;
            else
                level = LogLevel.Info;

            _logger.Log(level, message);
        }
    }

    public interface IDefaultLogger
    {
        void Create(string name);
        void Log(LogType type, string message);
    }

    public enum LogType
    {
        Info,
        Error
    }
}
