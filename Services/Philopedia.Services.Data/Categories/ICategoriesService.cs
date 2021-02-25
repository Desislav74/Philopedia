namespace Philopedia.Services.Data.Categories
{
    using System.Threading.Tasks;

    using Philopedia.Web.ViewModels.Categories;

    public interface ICategoriesService
    {
        Task CreateAsync(CategoryInputModel input, string imagePath);
    }
}
