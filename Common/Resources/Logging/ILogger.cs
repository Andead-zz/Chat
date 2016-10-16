namespace Andead.Chat.Resources.Logging
{
    public interface ILogger
    {
        void Info(string message);

        void Info(string message, InfoCategory category);

        void Warn(string message);

        void Trace(string message);
    }
}