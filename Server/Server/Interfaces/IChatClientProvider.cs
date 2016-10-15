namespace Andead.Chat.Server.Interfaces
{
    public interface IChatClientProvider
    {
        IChatClient GetCurrent();
    }
}