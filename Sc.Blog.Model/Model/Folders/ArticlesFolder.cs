using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sc.Blog.Model.Model.Folders
{
    [SitecoreType(AutoMap = true)]
    public class ArticlesFolder
    {
        public Guid Id { get; set; }

        public virtual IEnumerable<Article> Children { get; set; }
    }
}
