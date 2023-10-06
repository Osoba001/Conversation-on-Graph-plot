using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

public class ChatClient
{
    private HubConnection _connection;

    public async Task StartConnectionAsync()
    {
        _connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7254/group-notification")
            .Build();

        await _connection.StartAsync();

        _connection.On<object>("ReceiveMessage", message =>
        {
            // Handle received messages here
            Console.WriteLine($"Received message: {message}");
        });
    }

    public async Task JoinGroupAsync(string groupName)
    {
        await _connection.InvokeAsync("JoinGroup", groupName);
    }

    public async Task LeaveGroupAsync(string groupName)
    {
        await _connection.InvokeAsync("LeaveGroup", groupName);
    }

    public async Task SendMessageToGroupAsync(string groupName, string message)
    {
        await _connection.InvokeAsync("SendMessageToGroup", groupName, message);
    }
}
