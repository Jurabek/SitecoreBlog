using AutoMapper;
using Sc.Blog.Abstractions.Facades;
using Sc.Blog.Abstractions.ModelBuilders;
using Sc.Blog.Abstractions.Providers;
using Sc.Blog.Abstractions.Repositories;
using Sc.Blog.Core.ModelBuilders;
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
        private IRouteProvider _routeProvider;
        private IModelBuilder<CommentViewModel> _commenModelBuilder;
        private IArticleModelBuilder<ArticleViewModel> _articleModelBuilder;

        public ArticleController(IRepository<Article, Guid> repository,
            IRouteProvider routerProvider,
            IArticleModelBuilder<ArticleViewModel> articleModelBuilder,
            IModelBuilder<CommentViewModel> commentModelBuilder)
        {
            _repository = repository;
            _routeProvider = routerProvider;
            _commenModelBuilder = commentModelBuilder;
            _articleModelBuilder = articleModelBuilder;
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

        [HttpPost]
        public ActionResult Details(CommentViewModel commentViewModel)
        {
            _commenModelBuilder.Build(commentViewModel);
            var article = _repository.Get(commentViewModel.ArticleId);
            return View(article);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ArticleViewModel viewModel, HttpPostedFileBase file)
        {   
            var result = _articleModelBuilder.Build(viewModel, file);
            if (!result)
            {
                ModelState.AddModelError("", "Could not create article");
                return View(viewModel);
            }
            return _routeProvider.RedirectToItem(Folders.Content.Home, RedirectToRoute);
        }
        
    }
}