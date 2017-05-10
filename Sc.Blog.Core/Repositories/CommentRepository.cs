using Glass.Mapper.Sc;
using Sc.Blog.Abstractions.Repositories;
using Sc.Blog.Model.Model;
using Sc.Blog.Model.Model.Folders;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.Linq;
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
            RepositoryErrors = new List<Exception>();
        }

        public IList<Exception> RepositoryErrors { get; }

        public bool Create(Comment entity)
        {
            try
            {
                using(new SecurityDisabler())
                {
                    _context.Create(_folder, entity);
                    return true;
                }
            }
            catch (Exception ex)
            {
                RepositoryErrors.Add(ex);
                return false;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                using (new SecurityDisabler())
                {
                    var itemForDelete = _folder.Children.SingleOrDefault(ch => ch.Id == id);
                    _context.Delete(itemForDelete);
                    return true;
                }
            }
            catch (Exception ex)
            {
                RepositoryErrors.Add(ex);
                return false;
            }
        }

        public Comment Get(Guid id)
        {
            return _folder.Children.SingleOrDefault(c => c.Id == id);
        }

        public IEnumerable<Comment> GetAll()
        {
            return _folder.Children;
        }

        public bool Update(Comment entity)
        {
            try
            {
                _context.Save(entity);
                return true;
            }
            catch (Exception ex)
            {
                RepositoryErrors.Add(ex);
                return false;
            }
        }
    }
}
