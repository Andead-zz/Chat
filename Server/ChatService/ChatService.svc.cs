using System.Collections.Generic;
using System.ServiceModel;

namespace Andead.Chat
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ChatService : IChatService
    {
        private static readonly IDictionary<IChatClient, string> Clients
            = new Dictionary<IChatClient, string>();

        public SignInResponse SignIn(SignInRequest request)
        {
            if (Clients.Values.Contains(request.Name))
            {
                return SignInResponse.Failed("Name has been taken by another client.");
            }

            var client = OperationContext.Current.GetCallbackChannel<IChatClient>();
            Clients[client] = request.Name;

            client.ReceiveMessage($"Welcome to the chat, {request.Name}!");

            return SignInResponse.Successful();
        }

        public void SignOut()
        {
            var client = OperationContext.Current.GetCallbackChannel<IChatClient>();

            if (Clients.ContainsKey(client))
            {
                string name = Clients[client];
                client.ReceiveMessage($"Goodbye, {name}!");

                Clients.Remove(client);
            }
        }

        public void SendMessage(string message)
        {
            var currentClient = OperationContext.Current
                .GetCallbackChannel<IChatClient>();

            if (!Clients.ContainsKey(currentClient))
            {
                return;
            }

            foreach (KeyValuePair<IChatClient, string> pair in Clients)
            {
                IChatClient chatClient = pair.Key;
                string name = pair.Value;

                chatClient.ReceiveMessage($"{name}: {message}");
            }
        }
    }
}