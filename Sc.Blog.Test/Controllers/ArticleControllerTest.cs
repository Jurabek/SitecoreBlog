using FluentAssertions;
using Moq;
using NUnit.Framework;
using Sc.Blog.Abstractions.Facades;
using Sc.Blog.Abstractions.ModelBuilders;
using Sc.Blog.Abstractions.Providers;
using Sc.Blog.Abstractions.Repositories;
using Sc.Blog.Core.Mappers;
using Sc.Blog.Core.ModelBuilders;
using Sc.Blog.Model.Model;
using Sc.Blog.Model.ViewModels;
using Sc.Blog.Web.Controllers;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.FakeDb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sc.Blog.Test.Controllers
{
    [TestFixture]
    public class ArticleControllerTest
    {
        private ArticleController _controller;
        private Mock<IRepository<Article, Guid>> _repository;
        private Mock<IMediaUploadFacade> _mediaUploadProvider;
        private Mock<IRouteProvider> _routerProvider;
        private Mock<IModelBuilder<CommentViewModel>> _commentModelBuilder;
        private Mock<IArticleModelBuilder<ArticleViewModel>> _articleModelBuilder;

        [OneTimeSetUp]
        public void Init()
        {
            _repository = new Mock<IRepository<Article, Guid>>();
            _mediaUploadProvider = new Mock<IMediaUploadFacade>();
            _routerProvider = new Mock<IRouteProvider>();
            _commentModelBuilder = new Mock<IModelBuilder<CommentViewModel>>();
            _articleModelBuilder = new Mock<IArticleModelBuilder<ArticleViewModel>>();

            _controller = new ArticleController(_repository.Object,
                _routerProvider.Object,
                _articleModelBuilder.Object, 
                _commentModelBuilder.Object);
        }

        [Test]
        public void Index_should_return_view_and_all_articles()
        {
            //given
            _repository.Setup(x => x.GetAll()).Returns(new List<Article>());

            //when
            var result = _controller.Index() as ViewResult;

            //then
            result.ViewData.Model.Should().NotBeNull();

        }

        [Test]
        public void Details_with_article_id_should_return_article()
        {
            var article = new Article();
            //given
            _repository.Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(article);

            //when
            var result = _controller.Details(Guid.NewGuid()) as ViewResult;

            //then
            result.ViewData.Model.Should().BeSameAs(article);
        }

        [Test]
        public void Create_should_return_view()
        {
            //when
            var result = _controller.Create();

            //then
            result.Should().BeAssignableTo(typeof(ActionResult));
        }

        [Test]
        public void Create_with_model_and_file_should_create_article_and_redirect_to_home()
        {            
            var view = new RedirectToRouteResult("Home", new System.Web.Routing.RouteValueDictionary()); 
            Func<string, object, RedirectToRouteResult> func = ((s, y) =>
            {
                return view;
            });

            _routerProvider.Setup(x => x.RedirectToItem(It.IsAny<string>(), It.IsAny<Func<string, object, RedirectToRouteResult>>()))
                .Returns(func);

            _articleModelBuilder.Setup(x => x.Build(It.IsAny<ArticleViewModel>(), It.IsAny<HttpPostedFileBase>())).Returns(true);

            //when
            var result = _controller.Create(new ArticleViewModel(), null);

            //then
            result.Should().NotBeNull();

            result.Should().BeSameAs(view);
        }

        [Test]
        public void Create_with_invalid_model_should_return_model_error()
        {
            //given
            _articleModelBuilder.Setup(x => x.Build(null, null)).Returns(false);

            //when
            var result = _controller.Create(new ArticleViewModel(), null) as ViewResult;

            //then
            result.Should().NotBeNull();

            result.ViewData.ModelState.Values.First().Errors.First()
                .ErrorMessage.Should().Be("Could not create article");
        }

        [Test]
        public void Detailes_with_view_model_should_creta_comment()
        {
            var article = new Article();
            //given
            _commentModelBuilder.Setup(x => x.Build(It.IsAny<CommentViewModel>()));
            _repository.Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(article);
            //when
            var result = _controller.Details(new CommentViewModel()) as ViewResult;

            //then
            result.ViewData.Model.Should().Be(article);
        }
    }

    
}
