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
    public class ArticleRepository : IRepository<Article, Guid>
    {
        private readonly ISitecoreContext _context;
        private readonly ArticlesFolder _folder;

        public ArticleRepository(ISitecoreContext context)
        {
            _context = context;
            _folder = _context.GetItem<ArticlesFolder>(Folders.Content.Global.Articles);
            RepositoryErrors = new List<Exception>();
        }

        public IList<Exception> RepositoryErrors { get; }

        public bool Create(Article entity)
        {
            try
            {
                using (new SecurityDisabler())
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
                var itemForDelete = _folder.Children.SingleOrDefault(ch => ch.Id == id);
                _context.Delete(itemForDelete);
                return true;
            }
            catch (Exception ex)
            {
                RepositoryErrors.Add(ex);
                return false;
            }
        }

        public Article Get(Guid id)
        {
            return _folder.Children.SingleOrDefault(a => a.Id == id);
        }

        public IEnumerable<Article> GetAll()
        {
            return _folder.Children;
        }

        public bool Update(Article entity)
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
