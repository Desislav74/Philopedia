namespace Philopedia.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class CategoriesListViewModel : PagingViewModel
    {
        public IEnumerable<CategoryInListViewModel> Categories { get; set; }
    }
}
