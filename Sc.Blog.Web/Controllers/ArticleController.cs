using AutoMapper;
using Glass.Mapper.Sc;
using Sc.Blog.Abstractions.Providers;
using Sc.Blog.Abstractions.Repositories;
using Sc.Blog.Core.Providers;
using Sc.Blog.Core.Repositories;
using Sc.Blog.Model.Model;
using Sc.Blog.Model.ViewModels;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Sc.Blog.Web.Controllers
{
    public class ArticleController : Controller
    {
        private IRepository<Article, Guid> _repository;
        private IMediaUploadProvider _mediaUploadProvider;

        public ArticleController(IRepository<Article, Guid> repository, IMediaUploadProvider mediaUploaderProvider)
        {
            _repository = repository;
            _mediaUploadProvider = mediaUploaderProvider;
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
                viewModel.ImageId = _mediaUploadProvider.CreateMedaiItem(file.InputStream,
                    file.FileName, "/sitecore/media library/images/blog");
            }

            var model = Mapper.Map<Article>(viewModel);
            var result = _repository.Create(model);
            return View();
        }

        [HttpPost]
        public ActionResult AddComment(CommentViewModel commentViewModel)
        {
            return View("Details");
        }
    }
}