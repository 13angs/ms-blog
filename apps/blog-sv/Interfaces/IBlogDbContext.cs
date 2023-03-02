using Api.Common.Models;

namespace blog_sv.Interfaces
{
    public interface IBlogDbContext
    {
        public Task<Blog> CreateAsync(Blog blog);
        public IEnumerable<Blog> GetBlogs();
    }
}