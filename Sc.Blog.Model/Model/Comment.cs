using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sc.Blog.Model.Model
{
    public class Comment : BaseEntity
    {
        public Comment()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        public string Text { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
