namespace Andead.Chat.Resources.Logging
{
    public class NullLogger : ILogger
    {
        public void Info(string message)
        {
        }

        public void Info(string message, InfoCategory category)
        {
        }

        public void Warn(string message)
        {
        }

        public void Trace(string message)
        {
        }
    }
}