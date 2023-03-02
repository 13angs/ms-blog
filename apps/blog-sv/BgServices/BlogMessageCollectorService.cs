using Api.Common.Interfaces;
using Api.Common.Stores;
using Simple.RabbitMQ;

namespace blog_sv.BgServices
{
  public class BlogMessageCollectorService : IHostedService
  {
    private readonly ILogger<BlogMessageCollectorService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;
    private readonly IRealtime _realTime;
    public BlogMessageCollectorService(IServiceProvider serviceProvider, ILogger<BlogMessageCollectorService> logger, IConfiguration configuration, IRealtime realTime)
    {
      _serviceProvider = serviceProvider;
      _logger = logger;
      _configuration = configuration;
      _realTime = realTime;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
      using (var scope = _serviceProvider.CreateScope())
      {
        IMessageSubscriber subscriber = scope.ServiceProvider.GetRequiredService<IMessageSubscriber>();
        subscriber.Connect(
            _configuration["RabbitMQ:HostName"],
            _configuration["RabbitMQ:ExchangeName"],
            _configuration["RabbitMQ:QueueName"],
            _configuration["RabbitMQ:RouteKey"],
            null
        );
        subscriber.Subscribe(processMessage);
        return Task.CompletedTask;
      }
    }
    public bool processMessage(string message, IDictionary<string, object> headers)
    {
      if(message.Contains(MessageCollectorTypeStore.Blog))
      {
        // _logger.LogInformation(message);
        string url = $"{_configuration["SignalrConfig:HostName"]}{_configuration["SignalrConfig:Endpoints:Blog"]}";
        _realTime.Invoke(url, HubTypeStore.Blog, "Invoke From BlogMessageCollector").GetAwaiter().GetResult();
        return true;
      }
      return false;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }
  }
}