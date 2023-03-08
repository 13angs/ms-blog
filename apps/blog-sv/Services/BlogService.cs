using Api.Common.DTOs;
using Api.Common.exceptions;
using Api.Common.Models;
using Api.Common.Stores;
using blog_sv.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Simple.RabbitMQ;

namespace blog_sv.Services
{
  public class BlogService : IBlog
  {
    private readonly IBlogDbContext _dbContext;
    private readonly IMessagePublisher _publisher;
    private readonly IConfiguration _configuration;
    public BlogService(IBlogDbContext dbContext, IMessagePublisher publisher, IConfiguration configuration)
    {
      _dbContext = dbContext;
      _publisher = publisher;
      _configuration = configuration;
      _publisher.Connect(
        _configuration["RabbitMQ:HostName"],
        _configuration["RabbitMQ:ExchangeName"],
        ExchangeType.Fanout
    );
    }

    public async Task<Blog> CreateBlog(BlogModel model)
    {
      string strModel = JsonConvert.SerializeObject(model);
      Blog blog = JsonConvert.DeserializeObject<Blog>(strModel)!;
      blog = await _dbContext.CreateAsync(blog);

      if(blog == null)
        throw new ErrorResponseException(
          StatusCodes.Status500InternalServerError,
          "Failed saving blog",
          new List<Error>()
        );
      
      string strBlog = JsonConvert.SerializeObject(blog);
      BlogMessageCollectorModel msgModel = JsonConvert
                    .DeserializeObject<BlogMessageCollectorModel>(strBlog)!;
      msgModel.MessageType=MessageCollectorTypeStore.Blog;
      string strMsg = JsonConvert.SerializeObject(msgModel);
      PublishMessage(strMsg);
      return blog;
    }

    public IEnumerable<Blog> GetBlogs()
    {
      return _dbContext.GetBlogs();
    }

    public void PublishMessage(string message)
    {
      _publisher.Publish(message, _configuration["RabbitMQ:RouteKey"], null);
    }
  }
}