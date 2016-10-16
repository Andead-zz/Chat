using Andead.Chat.Client.WinForms.Entities;
using Andead.Chat.Client.WinForms.Interfaces;

namespace Andead.Chat.Client.WinForms.Services
{
    public class DefaultConnectionConfigurationProvider : IConnectionConfigurationProvider
    {
        public ConnectionConfiguration GetConfiguration()
        {
            return ConnectionConfiguration.Default;
        }
    }
}