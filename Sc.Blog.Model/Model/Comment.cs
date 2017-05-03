using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sc.Blog.Common.Constants;

namespace Sc.Blog.Model.Model
{
    [SitecoreType(AutoMap = true, TemplateId = Templates.Comment.TemplateId)]
    public class Comment : BaseEntity
    {
        public Comment()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        [SitecoreField(Setting = SitecoreFieldSettings.RichTextRaw)]
        public string Text { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
