namespace Philopedia.Web.ViewModels.Posts
{
    using System;

    using Ganss.XSS;
    using Philopedia.Data.Models;
    using Philopedia.Services.Mapping;

    public class PostCommentViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string UserUserName { get; set; }
    }
}