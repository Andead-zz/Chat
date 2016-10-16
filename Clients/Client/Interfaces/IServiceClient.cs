using System;
using System.Threading.Tasks;
using Andead.Chat.Client.Entities;

namespace Andead.Chat.Client.Interfaces
{
    public interface IServiceClient : IDisposable
    {
        bool SignedIn { get; }

        Task<SignInResult> SignInAsync(string name);

        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        Task SignOutAsync();

        Task SendAsync(string message);
    }
}