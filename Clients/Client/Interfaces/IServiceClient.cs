using System;
using System.Threading.Tasks;
using Andead.Chat.Client.Entities;

namespace Andead.Chat.Client.Interfaces
{
    public interface IServiceClient : IDisposable
    {
        bool SignedIn { get; }

        string ServerName { get; }

        Task<int?> GetOnlineCountAsync();

        Task<SignInResult> SignInAsync(string name);

        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        Task SignOutAsync();

        Task<SendMessageResult> SendAsync(string message);

        Task<string[]> GetNamesOnlineAsync();
    }
}