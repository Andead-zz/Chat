using Andead.Chat.Client.WinForms.Entities;
using Andead.Chat.Client.WinForms.Interfaces;
using Andead.Chat.Client.WinForms.Properties;

namespace Andead.Chat.Client.WinForms.Services
{
    public class ApplicationSettingsConnectionConfigurationProvider : IConnectionConfigurationProvider
    {
        private static ConnectionConfiguration _connectionConfiguration;

        public ConnectionConfiguration GetConfiguration()
        {
            return _connectionConfiguration ??
                   (_connectionConfiguration = new ConnectionConfiguration
                   {
                       ServerName = Settings.Default.ServerName,
                       TimeOut = Settings.Default.Timeout
                   });
        }
    }
}