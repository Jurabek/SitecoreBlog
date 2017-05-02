using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sc.Blog.Model.Model
{
    public class BaseEntity
    {
        [SitecoreInfo(Glass.Mapper.Sc.Configuration.SitecoreInfoType.Name)]
        public string Name { get; set; }
    }
}
