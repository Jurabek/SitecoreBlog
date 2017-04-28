using System.Collections.Generic;

namespace Sc.Blog.Model.Model.Folders
{
    public class CommentsFolder
    {
        public virtual IEnumerable<Comment> Children { get; set; }
    }
}
