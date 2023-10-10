using ChatDemo.wpf;
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

        _connection.Closed += async (error) =>
        {
            Console.WriteLine($"Connection closed: {error}");
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await _connection.StartAsync();
        };

        _connection.On<object>("ReceiveMessage", message =>
        {
            Console.WriteLine($"Received message: {message}");
        });

        try
        {
            await _connection.StartAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error starting connection: {ex}");
        }
    }

    public async Task JoinGroupAsync(string groupName)
    {
        try
        {
            await _connection.InvokeAsync("JoinGroup", groupName);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error joining group: {ex}");
        }
    }

    public async Task LeaveGroupAsync(string groupName)
    {
        try
        {
            await _connection.InvokeAsync("LeaveGroup", groupName);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error leaving group: {ex}");
        }
    }

    public async Task SendMessageToGroupAsync(string groupName, CreateMessageCommand message)
    {
        try
        {
            await _connection.InvokeAsync("SendMessageToGroup", groupName, message);
        }
        catch (Exception ex)
        {
            // Handle SendMessageToGroup invocation failure
            Console.WriteLine($"Error sending message to group: {ex}");
        }
    }
}


