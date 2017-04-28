using System.Collections.Generic;

namespace Sc.Blog.Model.Model.Folders
{
    public class NavigationItemsFolder
    {
        public virtual IEnumerable<TopNavigation> Children { get; set; }
    }
}
