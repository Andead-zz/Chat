using System.ServiceModel;
using Andead.Chat.Server.Interfaces;

namespace Andead.Chat.Server
{
    public class CurrentOperationContextChatClientProvider : IChatClientProvider
    {
        public IChatClient GetCurrent()
        {
            return OperationContext.Current.GetCallbackChannel<IChatClient>();
        }
    }
}