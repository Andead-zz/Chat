using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Andead.Chat.Resources.Resources.Strings;

namespace Andead.Chat.Server
{
    public class ChatService : IChatService
    {
        private static readonly IDictionary<IChatClient, string> Clients
            = new Dictionary<IChatClient, string>();

        private readonly IChatClientProvider _chatClientProvider;

        public ChatService(IChatClientProvider chatClientProvider)
        {
            if (chatClientProvider == null)
            {
                throw new ArgumentNullException(nameof(chatClientProvider));
            }

            _chatClientProvider = chatClientProvider;
        }

        public SignInResponse SignIn(SignInRequest request)
        {
            IChatClient currentClient = _chatClientProvider.GetCurrent();
            if (currentClient == null)
            {
                return SignInResponse.Failed(Errors.CallbackChannelFailure);
            }

            if (Clients.ContainsKey(currentClient))
            {
                return SignInResponse.Failed(Errors.AlreadySignedIn);
            }

            string name = request.Name;
            if (string.IsNullOrWhiteSpace(name))
            {
                return SignInResponse.Failed(Errors.EmptyNameNotAllowed);
            }

            if (Clients.Values.Contains(name))
            {
                return SignInResponse.Failed(Errors.NameAlreadyTaken);
            }

            Clients[currentClient] = name;

            Broadcast($"{name} has joined the chat.");

            return SignInResponse.Successful();
        }

        public void SignOut()
        {
            IChatClient currentClient = _chatClientProvider.GetCurrent();
            if (!Clients.ContainsKey(currentClient))
            {
                return;
            }

            string name = Clients[currentClient];
            currentClient.ReceiveMessage($"Goodbye, {name}!");

            Clients.Remove(currentClient);

            Broadcast($"{name} has left the chat.");
        }

        public void SendMessage(string message)
        {
            IChatClient currentClient = _chatClientProvider.GetCurrent();
            if (!Clients.ContainsKey(currentClient))
            {
                return;
            }

            string name = Clients[currentClient];

            Broadcast($"{name}: {message}");
        }

        public int? GetOnlineCount()
        {
            IChatClient currentClient = _chatClientProvider.GetCurrent();
            if (!Clients.ContainsKey(currentClient))
            {
                return null;
            }

            return Clients.Count;
        }

        public List<string> GetNamesOnline()
        {
            IChatClient currentClient = _chatClientProvider.GetCurrent();
            if (!Clients.ContainsKey(currentClient))
            {
                return null;
            }

            return Clients.Values.ToList();
        }

        private static void Broadcast(string message)
        {
            foreach (IChatClient client in Clients.Keys)
            {
                client.ReceiveMessage(message);
            }
        }
    }
}