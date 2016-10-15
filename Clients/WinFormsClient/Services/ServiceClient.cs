using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Andead.Chat.Client.WinForms.ChatService;
using Andead.Chat.Client.WinForms.Entities;
using Andead.Chat.Client.WinForms.Interfaces;

namespace Andead.Chat.Client.WinForms.Services
{
    internal class ServiceClient : IServiceClient, IChatServiceCallback
    {
        private readonly IChatService _service;

        internal ServiceClient()
        {
            _service = new ChatServiceClient(new InstanceContext(this));
        }

        void IChatServiceCallback.ReceiveMessage(string message)
        {
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(message));
        }

        public bool SignedIn { get; private set; }

        public async Task<SignInResult> SignInAsync(string name)
        {
            var request = new SignInRequest {Name = name};

            SignInResponse response = await _service.SignInAsync(request);

            SignedIn = response.Success;

            return new SignInResult(response.Success, response.Message);
        }

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public async Task SignOutAsync()
        {
            await _service.SignOutAsync();

            SignedIn = false;
        }

        public async Task SendAsync(String message)
        {
            await _service.SendMessageAsync(message);
        }
    }
}