using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;

namespace Sc.Blog.Model.Model.Folders
{
    [SitecoreType(AutoMap = true)]
    public class NavigationItemsFolder
    {
        public Guid Id { get; set; }
        public virtual IEnumerable<TopNavigation> Children { get; set; }
    }
}
