using System.ServiceModel;

namespace Andead.Chat
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