using Glass.Mapper.Sc;
using Sc.Blog.Abstractions.Repositories;
using Sc.Blog.Model.Model;
using Sc.Blog.Model.Model.Folders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sc.Blog.Common.Constants;

namespace Sc.Blog.Core.Repositories
{
    public class CommentRepository : IRepository<Comment, Guid>
    {
        private ISitecoreContext _context;
        private CommentsFolder _folder;

        public CommentRepository(ISitecoreContext context)
        {
            _context = context;
            _folder = _context.GetItem<CommentsFolder>(Folders.Content.Global.Comments);
        }

        public bool Create(Comment entity)
        {
            try
            {
                _context.Create(_folder, entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Comment Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comment> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Comment entity)
        {
            throw new NotImplementedException();
        }
    }
}
