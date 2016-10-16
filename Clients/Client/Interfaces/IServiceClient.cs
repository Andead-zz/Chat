using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Andead.Chat.Client.Entities;
using System.Collections.ObjectModel;

namespace Andead.Chat.Client.Interfaces
{
    public interface IServiceClient : IDisposable
    {
        bool SignedIn { get; }

        Task<int?> GetOnlineCountAsync();

        Task<SignInResult> SignInAsync(string name);

        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        Task SignOutAsync();

        Task SendAsync(string message);

        Task<string[]> GetNamesOnlineAsync();
    }
}