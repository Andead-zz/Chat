using System;
using System.Collections.Generic;
using System.Linq;
using Andead.Chat.Resources.Logging;
using Andead.Chat.Resources.Resources.Strings;

namespace Andead.Chat.Server
{
    public class ChatService : IChatService
    {
        private readonly IDictionary<IChatClient, string> _clients
            = new Dictionary<IChatClient, string>();

        private readonly IChatClientProvider _chatClientProvider;
        private readonly ILogger _logger;

        public ChatService(IChatClientProvider chatClientProvider, ILogger logger)
        {
            if (chatClientProvider == null)
            {
                throw new ArgumentNullException(nameof(chatClientProvider));
            }
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            _chatClientProvider = chatClientProvider;
            _logger = logger;
        }

        public SignInResponse SignIn(SignInRequest request)
        {
            IChatClient currentClient = _chatClientProvider.GetCurrent();
            if (currentClient == null)
            {
                _logger.Warn("Sign in failure: wrong callback channel.");
                return SignInResponse.Failed(Errors.CallbackChannelFailure);
            }

            if (_clients.ContainsKey(currentClient))
            {
                _logger.Warn("Sign in failure: already signed in.");
                return SignInResponse.Failed(Errors.AlreadySignedIn);
            }

            string name = request.Name;
            if (string.IsNullOrWhiteSpace(name))
            {
                _logger.Info("Sign in failure: empty name.", InfoCategory.Validation);
                return SignInResponse.Failed(Errors.EmptyNameNotAllowed);
            }

            if (_clients.Values.Contains(name))
            {
                _logger.Info("Sign in failure: name already taken.", InfoCategory.Validation);
                return SignInResponse.Failed(Errors.NameAlreadyTaken);
            }

            _clients[currentClient] = name;

            Broadcast($"{name} has joined the chat.");

            return SignInResponse.Successful();
        }

        public void SignOut()
        {
            IChatClient currentClient = _chatClientProvider.GetCurrent();
            if (!_clients.ContainsKey(currentClient))
            {
                _logger.Warn("Sign out denied for a non-active client.");
                return;
            }

            string name = _clients[currentClient];
            currentClient.ReceiveMessage($"Goodbye, {name}!");

            _clients.Remove(currentClient);

            Broadcast($"{name} has left the chat.");
        }

        public void SendMessage(string message)
        {
            IChatClient currentClient = _chatClientProvider.GetCurrent();
            if (!_clients.ContainsKey(currentClient))
            {
                _logger.Warn("Sending message denied for a non-active client.");
                return;
            }

            string name = _clients[currentClient];

            Broadcast($"{name}: {message}");
        }

        public int? GetOnlineCount()
        {
            IChatClient currentClient = _chatClientProvider.GetCurrent();
            if (!_clients.ContainsKey(currentClient))
            {
                _logger.Warn("Getting online count denied for a non-active client.");
                return null;
            }

            return _clients.Count;
        }

        public List<string> GetNamesOnline()
        {
            IChatClient currentClient = _chatClientProvider.GetCurrent();
            if (!_clients.ContainsKey(currentClient))
            {
                _logger.Warn("Getting names online denied for a non-active client.");
                return null;
            }

            return _clients.Values.ToList();
        }

        private void Broadcast(string message)
        {
            foreach (IChatClient client in _clients.Keys)
            {
                client.ReceiveMessage(message);
            }

            _logger.Info(message, InfoCategory.Broadcast);
        }
    }
}