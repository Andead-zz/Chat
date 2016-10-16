using Andead.Chat.Client.WinForms.ChatService;
using Andead.Chat.Client.WinForms.Entities;

namespace Andead.Chat.Client.WinForms.Interfaces
{
    public interface IChatServiceFactory
    {
        IChatService Create(ConnectionConfiguration configuration, IChatServiceCallback callbackClient);

        void Dispose(IChatService chatService, ConnectionConfiguration configuration);
    }
}