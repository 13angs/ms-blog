namespace blog_sv.BgServices
{
  public class BlogMessageCollectorService : IHostedService
  {
    private readonly IServiceProvider _serviceProvider;
    public BlogMessageCollectorService(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {

      return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }
  }
}