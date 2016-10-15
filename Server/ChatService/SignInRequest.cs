using System.Runtime.Serialization;

namespace Andead.Chat
{
    [DataContract]
    public class SignInRequest
    {
        [DataMember]
        public string Name { get; set; }
    }
}