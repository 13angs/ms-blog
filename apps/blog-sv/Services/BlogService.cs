using Api.Common.DTOs;
using Api.Common.Models;
using blog_sv.Interfaces;
using Newtonsoft.Json;

namespace blog_sv.Services
{
  public class BlogService : IBlog
  {
    private readonly IBlogDbContext _dbContext;
    public BlogService(IBlogDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<Blog> CreateBlog(BlogModel model)
    {
      string strModel = JsonConvert.SerializeObject(model);
      Blog blog = JsonConvert.DeserializeObject<Blog>(strModel)!;
      return await _dbContext.CreateAsync(blog);
    }
  }
}