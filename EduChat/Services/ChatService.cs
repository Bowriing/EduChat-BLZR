using Microsoft.AspNetCore.SignalR.Client;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduChat.Services
{
    public class ChatService
    {
        private readonly MySqlConnection _connection;
        private HubConnection _hubConnection;

        public ChatService(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task ConnectAsync(string hubUrl)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .Build();

            await _hubConnection.StartAsync();
        }

        public async Task DisconnectAsync()
        {
            if (_hubConnection != null)
            {
                await _hubConnection.DisposeAsync();
                _hubConnection = null;
            }
        }

        public async Task SendMessageAsync(int groupId, int userId, string userName, string message)
        {
            if (_hubConnection != null)
            {
                await _hubConnection.InvokeAsync("SendMessage", userName, message);

                // Save the message to the database
                await SaveMessageToDatabaseAsync(groupId, userId, userName, message);
            }
        }

        public IDisposable OnMessageReceived(Action<string, string> messageHandler)
        {
            return _hubConnection?.On<string, string>("ReceiveMessage", messageHandler);
        }

        private async Task SaveMessageToDatabaseAsync(int groupId, int userId, string userName, string message)
        {
            using var cmd = new MySqlCommand(@"INSERT INTO GroupChatMessages (GroupId, UserId, UserName, Message) 
                                                VALUES (@groupId, @userId, @userName, @message)", _connection);
            cmd.Parameters.AddWithValue("@groupId", groupId);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@userName", userName);
            cmd.Parameters.AddWithValue("@message", message);

            await _connection.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }

        public async Task<List<(string UserName, string Message)>> GetMessagesForGroupAsync(int groupId)
        {
            var messages = new List<(string, string)>();

            using var cmd = new MySqlCommand("SELECT UserName, Message FROM GroupChatMessages WHERE GroupId = @groupId ORDER BY Timestamp", _connection);
            cmd.Parameters.AddWithValue("@groupId", groupId);

            await _connection.OpenAsync();

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                messages.Add((reader.GetString(0), reader.GetString(1)));
            }

            await _connection.CloseAsync();
            return messages;
        }
    }
}


