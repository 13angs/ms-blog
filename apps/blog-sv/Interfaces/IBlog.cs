using Api.Common.DTOs;
using Api.Common.Models;

namespace blog_sv.Interfaces
{
    public interface IBlog
    {
        public Task<Blog> CreateBlog(BlogModel model);
        public IEnumerable<Blog> GetBlogs();
        public void PublishMessage(string message);
    }
}