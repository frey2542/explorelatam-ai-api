using ExploreLatamAI.Api.Models.Domain;

namespace ExploreLatamAI.Api.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync();
    }
}
