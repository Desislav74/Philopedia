﻿namespace Philopedia.Web.Controllers.CategoriesController
{
    using System;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Philopedia.Services.Data.Categories;
    using Philopedia.Services.Data.Posts;
    using Philopedia.Web.ViewModels.Categories;

    public class CategoriesController : Controller
    {
        private const int ItemsPerPage = 5;
        private readonly ICategoriesService categoriesService;
        private readonly IPostsService postsService;

        public CategoriesController(ICategoriesService categoriesService, IPostsService postsService)
        {
            this.categoriesService = categoriesService;
            this.postsService = postsService;
        }

        [Authorize]
        public IActionResult ByName(string name, int page = 1)
        {
            var viewModel =
                this.categoriesService.GetByName<CategoryViewModel>(name);
            if (viewModel == null)
            {
                return this.NotFound();
            }

            viewModel.Posts = this.postsService.GetByCategoryId<PostInCategoryViewModel>(viewModel.Id, ItemsPerPage, (page - 1) * ItemsPerPage);

            var count = this.postsService.GetCountByCategoryId(viewModel.Id);
            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }
    }
}
