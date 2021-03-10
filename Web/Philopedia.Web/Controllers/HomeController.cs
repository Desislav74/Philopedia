namespace Philopedia.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using Philopedia.Services.Data.Categories;
    using Philopedia.Web.ViewModels;
    using Philopedia.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        public HomeController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Index(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 6;
            var viewModel = new CategoriesListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Categories = this.categoriesService.GetAllWhitePaging<CategoryInListViewModel>(id, ItemsPerPage),
            };
            return this.View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult Privacy()
        {
            return this.View();
        }
    }
}
