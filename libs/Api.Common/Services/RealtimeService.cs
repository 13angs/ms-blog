using Api.Common.Interfaces;
using Api.Common.Stores;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

namespace Api.Common.Services
{
  public class RealtimeService : IRealtime
  {
    private readonly IConfiguration _configuration;
    public RealtimeService(IConfiguration configuration)
    {
      _configuration = configuration;
    }
    public async Task Invoke(string url, string from, string message)
    {
      var connection = new HubConnectionBuilder()
           .WithUrl(url)
           .WithAutomaticReconnect()
           .Build();
        await connection.StartAsync();
        await connection.InvokeAsync(BlogHubMethodNames.SendAllMessage!, from, message);
    }
  }
}