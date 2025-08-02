using Eduegate.Domain;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Eduegate.Hub.Client
{
    public class RealtimeClient
    {
        private static string _url;
        private static HubConnection _connection;

        public RealtimeClient()
        {
            if (_connection == null)
            {
                _url = new Domain.Setting.SettingBL(null).GetSettingValue<string>("RealtimeHubHost");

                if (!string.IsNullOrEmpty(_url))
                {
                    _connection = new HubConnectionBuilder()
                        .WithUrl(_url + "?username=portal&role=system")
                        .Build();
                    
                    _connection.Closed += async (exception) =>
                    {
                        await Task.Delay(new Random().Next(0, 5) * 1000);
                        await StartAsync();
                    };

                    Task.Run(async () =>
                    {
                        await StartAsync();
                    });
                }
            }
        }

        private async Task StartAsync()
        {
            try
            {
                await _connection.StartAsync();
                Console.WriteLine("Connection started.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to start connection: {ex.Message}");
                await Task.Delay(5000); // Retry after 5 seconds
                await StartAsync(); // Retry starting the connection
            }
        }

        public void SendMessage(string message)
        {
            if (_connection != null && _connection.State == HubConnectionState.Connected)
            {
                Task.Run(async () =>
                {
                    try
                    {
                        await _connection.SendAsync("MessageTo", message);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine(ex.Message);
                    }
                });
            }
        }

    }
}