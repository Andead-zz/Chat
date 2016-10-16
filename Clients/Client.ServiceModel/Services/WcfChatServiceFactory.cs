using System;
using System.ServiceModel;
using Andead.Chat.Client.ServiceModel.ChatService;
using Andead.Chat.Client.ServiceModel.Entities;
using Andead.Chat.Client.ServiceModel.Interfaces;

namespace Andead.Chat.Client.ServiceModel.Services
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