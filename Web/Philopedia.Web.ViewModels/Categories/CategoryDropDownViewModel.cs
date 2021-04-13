namespace Philopedia.Web.ViewModels.Posts
{
    using Philopedia.Data.Models;
    using Philopedia.Services.Mapping;

    public class CategoryDropDownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
