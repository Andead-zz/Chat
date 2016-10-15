using System.ServiceModel;
using Andead.Chat.Server.Entities;
using Andead.Chat.Server.Interfaces;

namespace Andead.Chat.Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ChatService : IChatService
    {
        private readonly IChatService _service;

        public ChatService()
        {
            _service = new Services.ChatService(new CurrentOperationContextChatClientProvider());
        }

        public SignInResponse SignIn(SignInRequest request)
        {
            return _service.SignIn(request);
        }

        public void SignOut()
        {
            _service.SignOut();
        }

        public void SendMessage(string message)
        {
            _service.SendMessage(message);
        }
    }
}