using Andead.Chat.Client.WinForms.Entities;

namespace Andead.Chat.Client.WinForms.Interfaces
{
    public interface IConnectionConfigurationProvider
    {
        ConnectionConfiguration GetConfiguration();
    }
}