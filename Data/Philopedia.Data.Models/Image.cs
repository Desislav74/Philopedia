namespace Philopedia.Data.Models
{
    using System;

    using Philopedia.Data.Common.Models;

    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Extension { get; set; }
    }
}
