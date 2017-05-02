using Glass.Mapper.Sc;
using Sc.Blog.Abstractions.Repositories;
using Sc.Blog.Model.Model;
using Sc.Blog.Model.Model.Folders;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sc.Blog.Core.Repositories
{
    public class ArticleRepository : IRepository<Article, Guid>
    {
        private readonly ISitecoreContext _context;
        private readonly ArticlesFolder _folder;

        public ArticleRepository(ISitecoreContext context)
        {
            _context = context;
            _folder = _context.GetItem<ArticlesFolder>("/sitecore/content/global/articles");
        }

        public bool Create(Article entity)
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
            catch (Exception)
            {
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

        public void Update(Article entity)
        {
            _context.Save(entity);
        }
    }
}
