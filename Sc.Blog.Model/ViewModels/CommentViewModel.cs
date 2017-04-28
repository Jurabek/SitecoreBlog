using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sc.Blog.Model.ViewModels
{
    public class CommentViewModel
    {
        public Guid ArticleId { get; set; }

        public string Text { get; set; }

        public string Author { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
