﻿namespace Philopedia.Data.Models
{
    using Philopedia.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        public string Content { get; set; }

        public int? ParentId { get; set; }

        public virtual Comment Parent { get; set; }
    }
}
