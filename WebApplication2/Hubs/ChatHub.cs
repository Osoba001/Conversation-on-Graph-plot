using GroupChatDemo.Services;
using GroupChatDemo.Services.Commands;
using Microsoft.AspNetCore.SignalR;

namespace GroupChatDemo.Hubs
{
    internal class ChatHub : Hub
    {
        private readonly IChatServices _chatServices;

        public ChatHub(IChatServices chatServices)
        {
            _chatServices = chatServices;
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessageToGroup(string groupName, CreateMessageCommand message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", message);
            _ = _chatServices.SendMessage(message);
        }
    }
}


