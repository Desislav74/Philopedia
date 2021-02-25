namespace Philopedia.Data.Models
{
    using System.Collections.Generic;

    using Philopedia.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Posts = new HashSet<Post>();
            this.Images = new HashSet<Image>();
        }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
