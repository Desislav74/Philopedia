namespace Philopedia.Web.ViewModels.Categories
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CategoryInputModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }
    }
}
