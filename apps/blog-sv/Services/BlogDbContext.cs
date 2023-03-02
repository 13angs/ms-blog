using Api.Common.Models;
using blog_sv.Interfaces;
using MongoDB.Driver;

namespace blog_sv.Services
{
  public class BlogDbContext : IBlogDbContext
  {
    private readonly IConfiguration _configuration;
    private readonly IMongoCollection<Blog> _mongoCols;
    public BlogDbContext(IConfiguration configuration)
    {
      _configuration = configuration;
      IMongoClient client = new MongoClient(_configuration["MongoConfig:ConnectionString"]);
      IMongoDatabase mongodb = client.GetDatabase(_configuration["MongoConfig:DatabaseName"]);
      _mongoCols = mongodb.GetCollection<Blog>(_configuration["MongoConfig:Collections:Blog"]);
    }
    public async Task<Blog> CreateAsync(Blog blog)
    {
      await _mongoCols.InsertOneAsync(blog);
      return blog;
    }

    public IEnumerable<Blog> GetBlogs()
    {
      IEnumerable<Blog> blogs = _mongoCols.Find(b => true).ToList();
      return blogs;
    }
  }
}