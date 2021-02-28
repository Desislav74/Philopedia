using Philopedia.Data.Models;
using Philopedia.Services.Mapping;

namespace Philopedia.Web.ViewModels.Posts
{
    public class CategoryDropDownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
