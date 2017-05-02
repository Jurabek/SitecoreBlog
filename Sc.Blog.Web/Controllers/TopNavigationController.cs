using Glass.Mapper.Sc;
using Sc.Blog.Abstractions.Repositories;
using Sc.Blog.Core.Repositories;
using Sc.Blog.Model.Model;
using System;
using System.Web.Mvc;

namespace Sc.Blog.Web.Controllers
{
    public class TopNavigationController : Controller
    {
        private IRepository<TopNavigation, Guid> _repository;

        public TopNavigationController(IRepository<TopNavigation, Guid> repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var model = _repository.GetAll();
            return View(model);
        }
    }
}