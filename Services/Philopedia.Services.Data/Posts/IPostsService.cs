using System.Threading.Tasks;
using Philopedia.Web.ViewModels.Posts;

namespace Philopedia.Services.Data.Posts
{
    public interface IPostsService
    {
        Task<int> CreateAsync(PostCreateInputModel input, string userId, string imagePath);
    }
}
