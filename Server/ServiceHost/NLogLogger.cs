using System;
using Andead.Chat.Resources.Logging;

namespace ServiceHost
{
    public class NLogLogger : ILogger
    {
        private readonly NLog.ILogger _logger;

        public NLogLogger(NLog.ILogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            _logger = logger;
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Info(string message, InfoCategory category)
        {
            Info($"{category}: {message}");
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Trace(string message)
        {
            _logger.Trace(message);
        }
    }
}