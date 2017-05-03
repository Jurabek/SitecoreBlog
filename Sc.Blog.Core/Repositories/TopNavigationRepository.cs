using Glass.Mapper.Sc;
using Sc.Blog.Abstractions.Repositories;
using Sc.Blog.Model.Model;
using Sc.Blog.Model.Model.Folders;
using System;
using System.Collections.Generic;
using System.Linq;
using static Sc.Blog.Common.Constants;

namespace Sc.Blog.Core.Repositories
{
    public class TopNavigationRepository : IRepository<TopNavigation, Guid>
    {
        private ISitecoreContext _context;
        private NavigationItemsFolder _folder;

        public TopNavigationRepository(ISitecoreContext context)
        {
            _context = context;
            _folder = _context.GetItem<NavigationItemsFolder>(Folders.Content.Global.NavigationItems);
        }

        public bool Create(TopNavigation entity)
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
            var childItem = _folder.Children.SingleOrDefault(i => i.Id == id);
            _context.Delete(childItem);
            return true;
        }

        public TopNavigation Get(Guid id)
        {
            return _folder.Children.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<TopNavigation> GetAll()
        {
            return _folder.Children;
        }

        public void Update(TopNavigation entity)
        {
            _context.Save(entity);
        }
    }
}
