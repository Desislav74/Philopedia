using Philopedia.Data.Common.Models;

namespace Philopedia.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Vote : BaseModel<int>
    {
        [Required]
        public int UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        public VoteType Type { get; set; }
    }
}
