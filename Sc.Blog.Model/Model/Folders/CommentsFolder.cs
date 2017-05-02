using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;

namespace Sc.Blog.Model.Model.Folders
{
    [SitecoreType(AutoMap = true)]
    public class CommentsFolder
    {
        public Guid Id { get; set; }
        public virtual IEnumerable<Comment> Children { get; set; }
    }
}
