using System.ServiceModel;

namespace Andead.Chat
{
    [ServiceContract]
    public interface IChatClient
    {
        [OperationContract(IsOneWay = true)]
        void ReceiveMessage(string message);
    }
}