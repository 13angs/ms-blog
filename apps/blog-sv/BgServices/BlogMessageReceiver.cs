using Api.Common.Interfaces;
using Api.Common.Stores;

namespace blog_sv.BgServices
{
  public class BlogMessageReceiver: IHostedService
  {
    private readonly ILogger<BlogMessageReceiver> _logger;
    private readonly IRealtime _realtime;
    private readonly IConfiguration _configuration;
    public BlogMessageReceiver(ILogger<BlogMessageReceiver> logger, IRealtime realtime, IConfiguration configuration)
    {
      _logger = logger;
      _realtime = realtime;
      _configuration = configuration;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
      string blogUrl = $"{_configuration["SignalrConfig:HostName"]}{_configuration["SignalrConfig:Endpoints:Blog"]}";
      await _realtime.ListenOn(blogUrl, BlogHubMethodNames.ReceiveAllMessage!);
      //   return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }
  }
}