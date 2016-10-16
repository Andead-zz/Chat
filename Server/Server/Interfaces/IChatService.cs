using System.Collections.Generic;
using System.ServiceModel;

namespace Andead.Chat.Server
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

        [OperationContract]
        int? GetOnlineCount();

        [OperationContract]
        List<string> GetNamesOnline();
    }
}