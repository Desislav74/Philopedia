namespace Philopedia.Services.Data.Posts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Philopedia.Web.ViewModels.Posts;

    public interface IPostsService
    {
        Task<int> CreateAsync(string title, string content, int categoryId, string userId);

        IEnumerable<T> GetByCategoryId<T>(int categoryId, int? take = null, int skip = 0);

        T GetById<T>(int id);

        int GetCountByCategoryId(int categoryId);

        Task UpdateAsync(int id, CreateEditPostInputModel input);

        Task DeleteAsync(int id);

        Task ChangeApproveStatusAsync(int postId);
    }
}
