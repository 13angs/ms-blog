using Api.Common.DTOs;
using Api.Common.Models;
using blog_sv.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace blog_sv.Controllers
{
  [ApiController]
  [Route("api/v1/blog/blogs")]
  public class BlogController : ControllerBase
  {
    private readonly IBlog _blogSv;
    public BlogController(IBlog blogSv)
    {
      _blogSv = blogSv;
    }

    [HttpPost]
    public async Task<ActionResult> CreateBlog([FromBody] BlogModel model)
    {
      await _blogSv.CreateBlog(model);
      return Ok();
    }

    [HttpGet]
    public ActionResult<IEnumerable<Blog>> GetBlogs()
    {
      return Ok(_blogSv.GetBlogs());
    }
  }
}