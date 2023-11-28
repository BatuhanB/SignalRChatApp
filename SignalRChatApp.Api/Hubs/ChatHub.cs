using Microsoft.AspNetCore.SignalR;

namespace SignalRChatApp.Api.Hubs
{
    public class ChatHub : Hub
    {
        static List<string> clients = new();

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("receiveMessage", user, message);
        }

        public async override Task OnConnectedAsync()
        {
            clients.Add(Context.ConnectionId);
            await Clients.All.SendAsync("clients", clients);
            await Clients.All.SendAsync("userJoined", Context.ConnectionId);
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            clients.Remove(Context.ConnectionId);
            await Clients.All.SendAsync("clients", clients);
            await Clients.All.SendAsync("userLeft", Context.ConnectionId);
        }
    }
}
