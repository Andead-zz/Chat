using System.ServiceModel;
using Andead.Chat.Server.Entities;

namespace Andead.Chat.Server.Interfaces
{
    [ServiceContract(CallbackContract = typeof(IChatClient))]
    public interface IChatService
    {
        [OperationContract]
        SignInResponse SignIn(SignInRequest request);

        [OperationContract]
        void SignOut();

        [OperationContract(IsOneWay = true)]
        void SendMessage(string message);
    }
}