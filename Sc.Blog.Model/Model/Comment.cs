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
            Name = DateTime.Now.ToString("yy-MM-ddThh-mm-ss");
            PublishDate = DateTime.Now;
        }

        public virtual Guid Id { get; set; }

        [SitecoreField(Setting = SitecoreFieldSettings.RichTextRaw)]
        public virtual string Text { get; set; }

        public virtual DateTime PublishDate { get; set; }
    }
}
