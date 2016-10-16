using System;
using System.ServiceModel;
using Andead.Chat.Client.WinForms.ChatService;
using Andead.Chat.Client.WinForms.Entities;

namespace Andead.Chat.Client.WinForms.Interfaces
{
    public class WcfChatServiceFactory : IChatServiceFactory
    {
        public IChatService Create(ConnectionConfiguration configuration, IChatServiceCallback callbackClient)
        {
            IChatService channel = DuplexChannelFactory<IChatService>.CreateChannel(
                new InstanceContext(callbackClient),
                new NetTcpBinding(SecurityMode.None),
                new EndpointAddress($"net.tcp://{configuration.ServerName}/Service.svc"));

            return channel;
        }

        public void Dispose(IChatService chatService, ConnectionConfiguration configuration)
        {
            TimeSpan timeout = TimeSpan.FromMilliseconds(configuration.TimeOut);

            (chatService as ICommunicationObject)?.Close(timeout);
        }
    }
}