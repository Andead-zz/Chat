using System.ServiceModel;

namespace Andead.Chat.Server.Interfaces
{
    [ServiceContract]
    public interface IChatClient
    {
        [OperationContract(IsOneWay = true)]
        void ReceiveMessage(string message);
    }
}