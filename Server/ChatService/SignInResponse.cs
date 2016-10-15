﻿using System.Runtime.Serialization;

namespace Andead.Chat
{
    [DataContract]
    public class SignInResponse
    {
        private SignInResponse()
        {
        }

        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        public string Message { get; set; }

        public static SignInResponse Successful(string message = "Welcome!")
        {
            return new SignInResponse
            {
                Success = true,
                Message = message
            };
        }

        public static SignInResponse Failed(string errorMessage = null)
        {
            return new SignInResponse
            {
                Message = errorMessage,
                Success = false
            };
        }
    }
}