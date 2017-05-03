using AutoMapper;
using Sc.Blog.Abstractions.Providers;
using Sc.Blog.Abstractions.Repositories;
using Sc.Blog.Model.Model;
using Sc.Blog.Model.ViewModels;
using System;
using System.Web;
using System.Web.Mvc;
using static Sc.Blog.Common.Constants;

namespace Sc.Blog.Web.Controllers
{
    public class ArticleController : Controller
    {
        private IRepository<Article, Guid> _repository;
        private IMediaUploadProvider _mediaUploadProvider;
        private IRouteProvider _routeProvider;

        public ArticleController(IRepository<Article, Guid> repository, 
            IMediaUploadProvider mediaUploaderProvider,
            IRouteProvider routerProvider)
        {
            _repository = repository;
            _mediaUploadProvider = mediaUploaderProvider;
            _routeProvider = routerProvider;
        }

        public ActionResult Index()
        {
            var articles = _repository.GetAll();
            return View(articles);
        }

        public ActionResult Details(Guid id)
        {
            var article = _repository.Get(id);
            return View(article);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ArticleViewModel viewModel, HttpPostedFileBase file)
        {
            if (file != null)
            {
                viewModel.Image = _mediaUploadProvider.CreateMedaiItem(file.InputStream,
                    file.FileName, Folders.MediaLibrary.Images.Blog);
            }

            var model = Mapper.Map<Article>(viewModel);
            var result = _repository.Create(model);
            if(!result)
            {
                ModelState.AddModelError("", "Could not create article");
                return View();
            }
            return _routeProvider.RedirectToItem(Folders.Content.Home, RedirectToRoute);
        }

        [HttpPost]
        public ActionResult AddComment(CommentViewModel commentViewModel)
        {
            return View("Details");
        }
    }
}