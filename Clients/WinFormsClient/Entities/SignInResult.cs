namespace Andead.Chat.Client.WinForms.Entities
{
    public class SignInResult
    {
        public SignInResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; }

        public string Message { get; }
    }
}