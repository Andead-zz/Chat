using System.Runtime.Serialization;

namespace Andead.Chat.Server.Entities
{
    [DataContract]
    public class SignInRequest
    {
        [DataMember]
        public string Name { get; set; }
    }
}