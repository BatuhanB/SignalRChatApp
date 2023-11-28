namespace SignalRChatApp.Api.Interfaces
{
    public interface IMessageClient
    {
        Task SendMessage(string user,string message);
        Task Clients(List<string> clients);
        Task UserConnected(string connectionId);
        Task UserDisconnected(string connectionId);
    }
}
