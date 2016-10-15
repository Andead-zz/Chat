using System;
using System.Collections.Generic;
using Andead.Chat.Server.Entities;
using Andead.Chat.Server.Interfaces;

namespace Andead.Chat.Server.Services
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
                return SignInResponse.Failed("The callback channel provided was not correct.");
            }

            if (Clients.ContainsKey(currentClient))
            {
                string currentName = Clients[currentClient];
                return SignInResponse.Failed($"You are already in the chat as {currentName}.");
            }

            string name = request.Name;
            if (string.IsNullOrWhiteSpace(name))
            {
                return SignInResponse.Failed("You must have a name to sign in.");
            }

            if (Clients.Values.Contains(name))
            {
                return SignInResponse.Failed($"The name {name} has been taken by someone else.");
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

        private static void Broadcast(string message)
        {
            foreach (IChatClient client in Clients.Keys)
            {
                client.ReceiveMessage(message);
            }
        }
    }
}