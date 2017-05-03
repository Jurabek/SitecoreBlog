using System;
using System.ComponentModel.DataAnnotations;

namespace Sc.Blog.Model.ViewModels
{
    public class CommentViewModel
    {
        public Guid ArticleId { get; set; }

        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        public string Author { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
