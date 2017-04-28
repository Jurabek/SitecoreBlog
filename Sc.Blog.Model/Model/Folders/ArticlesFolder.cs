using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sc.Blog.Model.Model.Folders
{
    public class ArticlesFolder
    {
        public virtual IEnumerable<Article> Children { get; set; }
    }
}
