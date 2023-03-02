using Simple.RabbitMQ;

namespace hub_sv.BgServices
{
  public class HubMessageCollector : IHostedService
  {
    private readonly ILogger<HubMessageCollector> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;
    public HubMessageCollector(ILogger<HubMessageCollector> logger, IServiceProvider serviceProvider, IConfiguration configuration)
    {
      _logger = logger;
      _serviceProvider = serviceProvider;
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
        _logger.LogInformation(message);
        return true;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }
  }
}