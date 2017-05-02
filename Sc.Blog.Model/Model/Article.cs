using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using System;
using System.Collections.Generic;
using static Sc.Blog.Common.Constants;

namespace Sc.Blog.Model.Model
{

    [SitecoreType(AutoMap = true, TemplateId = Templates.Article.TemplateId)]
    public class Article : BaseEntity
    {
        public Article()
        {
            Id = Guid.NewGuid();
            Name = DateTime.Now.ToString("yy-MM-ddThh-mm-ss");
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        [SitecoreField(Setting = SitecoreFieldSettings.RichTextRaw)]
        public string Body { get; set; }

        public Image Image { get; set; }

        public DateTime PublishDate { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
