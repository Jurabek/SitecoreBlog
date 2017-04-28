using Glass.Mapper.Sc.Fields;
using System;
using System.Collections.Generic;

namespace Sc.Blog.Model.Model
{
    public class Article
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public Image Image { get; set; }

        public DateTime PublishDate { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
