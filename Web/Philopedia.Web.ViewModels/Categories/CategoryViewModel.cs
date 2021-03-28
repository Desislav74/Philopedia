namespace Philopedia.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    using Philopedia.Data.Models;
    using Philopedia.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Post>, IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<PostInCategoryViewModel> Posts { get; set; }
    }
}
