namespace Andead.Chat.Client.WinForms.Entities
{
    public class ConnectionConfiguration
    {
        public static ConnectionConfiguration Default = new ConnectionConfiguration
        {
            ServerName = "localhost",
            TimeOut = 1000
        };

        public string ServerName { get; set; }

        public int TimeOut { get; set; }
    }
}