using ExploreLatamAI.Api.Data;
using ExploreLatamAI.Api.Models.Domain;
using ExploreLatamAI.Api.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExploreLatamAI.Api.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {


        private readonly ApplicationDbContext _context;

        public BlogPostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await _context.AddAsync(blogPost);
            await _context.SaveChangesAsync();
            return blogPost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync()
        {
            return await _context.BlogPosts.ToListAsync();
        }
    }
}
