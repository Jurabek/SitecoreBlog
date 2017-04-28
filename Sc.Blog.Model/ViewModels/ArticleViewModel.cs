using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sc.Blog.Model.ViewModels
{
    public class ArticleViewModel
    {
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        public DateTime PublishDate { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

    }
}
