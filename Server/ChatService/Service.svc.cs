using System.Collections.Generic;
using System.ServiceModel;

namespace Andead.Chat.Server.Wcf
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service : IChatService
    {
        private readonly IChatService _service;

        public Service()
        {
            _service = new ChatService(new CurrentOperationContextChatClientProvider());
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

        public int? GetOnlineCount()
        {
            return _service.GetOnlineCount();
        }

        public List<string> GetNamesOnline()
        {
            return _service.GetNamesOnline();
        }
    }
}