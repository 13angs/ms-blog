using Api.Common.Interfaces;
using Api.Common.Stores;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Api.Common.Services
{
  public class RealtimeService : IRealtime
  {
    private readonly IConfiguration _configuration;
    private readonly ILogger<RealtimeService> _logger;
    public RealtimeService(IConfiguration configuration, ILogger<RealtimeService> logger)
    {
      _configuration = configuration;
      _logger = logger;
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

    public async Task ListenOn(string url, string method)
    {
      var connection = new HubConnectionBuilder()
         .WithUrl(url, options =>
            {
              options.Transports = HttpTransportType.WebSockets;
              options.SkipNegotiation = true;
            })
         .WithAutomaticReconnect()

         .Build();
      connection.On<string, string>(method, (from, message) =>
      {
        _logger.LogInformation($"Receiving message from: {from}");
      });

      await connection.StartAsync();
    }
  }
}