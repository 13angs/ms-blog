using Api.Common.Stores;
using Simple.RabbitMQ;

namespace blog_sv.BgServices
{
  public class BlogMessageCollectorService : IHostedService
  {
    private readonly ILogger<BlogMessageCollectorService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;
    public BlogMessageCollectorService(IServiceProvider serviceProvider, ILogger<BlogMessageCollectorService> logger, IConfiguration configuration)
    {
      _serviceProvider = serviceProvider;
      _logger = logger;
      _configuration = configuration;
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
        _logger.LogInformation(message);
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