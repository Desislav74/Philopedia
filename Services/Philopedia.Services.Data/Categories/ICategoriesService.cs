using System.Collections.Generic;

namespace Philopedia.Services.Data.Categories
{
    using System.Threading.Tasks;

    using Philopedia.Web.ViewModels.Categories;

    public interface ICategoriesService
    {
        Task CreateAsync(CategoryInputModel input, string imagePath);

        IEnumerable<T> GetAllWhitePaging<T>(int page, int itemsPerPage = 6);

        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);
    }
}
