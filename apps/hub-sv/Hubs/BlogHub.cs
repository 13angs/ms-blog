using Api.Common.Stores;
using Microsoft.AspNetCore.SignalR;

namespace hub_sv.Hubs
{
  public class BlogHub : Hub
  {
    private readonly ILogger<BlogHub> _logger;
    public BlogHub(ILogger<BlogHub> logger)
    {
      _logger = logger;
    }
    public async Task SendAllMessage(string from, string message)
    {
        _logger.LogInformation($"Invoking SendAllMessage method from: {from}");
      await Clients.All.SendAsync(BlogHubMethodNames.ReceiveAllMessage!, from, message);
    }
  }
}