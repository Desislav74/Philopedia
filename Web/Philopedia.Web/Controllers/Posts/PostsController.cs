using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Philopedia.Data.Models;
using Philopedia.Services.Data.Categories;
using Philopedia.Services.Data.Posts;
using Philopedia.Web.ViewModels.Posts;

namespace Philopedia.Web.Controllers.Posts
{
    public class PostsController : Controller
    {
        private readonly IPostsService postsService;
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public PostsController(IPostsService postsService, ICategoriesService categoriesService, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            this.postsService = postsService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
            this.environment = environment;
        }

        public IActionResult Create()
        {
            var viewModel = new PostCreateInputModel()
            {
                Categories = this.categoriesService.GetAll<CategoryDropDownViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostCreateInputModel input, string image)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var postId = await this.postsService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/images");
            this.TempData["InfoMessage"] = "Forum post created!";
            return this.RedirectToAction(nameof(this.Create), new { id = postId });
        }
    }
}
