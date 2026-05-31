using Microsoft.AspNetCore.SignalR;
using JLoka.ChatService.Models;
using JLoka.ChatService.DataService;

namespace JLoka.ChatService.Hubs;

public class ChatHub: Hub
{

    private readonly SharedDb _shared;

    public ChatHub(SharedDb shared) => _shared = shared;

    public async Task JoinChat(UserConnection conn) 
    {
        await Clients.All.SendAsync("ReceiveMessage", "admin", $"{conn.Username} has joined");
    }

    public async Task JoinSpecificChatRoom(UserConnection conn)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);
        await Clients.Group(conn.ChatRoom).SendAsync("JoinSpecificChatRoom", "admin", $"{conn.Username} has joined {conn.ChatRoom}");
    }
}
