using FluentAssertions;
using Moq;
using NUnit.Framework;
using Sc.Blog.Abstractions.Providers;
using Sc.Blog.Abstractions.Repositories;
using Sc.Blog.Core.Mappers;
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
        private Mock<IMediaUploadProvider> _mediaUploadProvider;
        private Mock<IRouteProvider> _routerProvider;

        [OneTimeSetUp]
        public void Init()
        {
            AutoMapperConfiguration.Configure();
            _repository = new Mock<IRepository<Article, Guid>>();
            _mediaUploadProvider = new Mock<IMediaUploadProvider>();
            _routerProvider = new Mock<IRouteProvider>();
            _controller = new ArticleController(_repository.Object, _mediaUploadProvider.Object, _routerProvider.Object);
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
            //given
            _mediaUploadProvider.Setup(x =>
                                x.CreateMedaiItem(It.IsAny<Stream>(),
                                It.IsAny<string>(),
                                It.IsAny<string>()));

            _repository.Setup(x => x.Create(It.IsAny<Article>()))
                .Returns(true);

            var view = new RedirectToRouteResult("Home", new System.Web.Routing.RouteValueDictionary()); 
            Func<string, object, RedirectToRouteResult> func = ((s, y) =>
            {
                return view;
            });

            _routerProvider.Setup(x => x.RedirectToItem(It.IsAny<string>(), It.IsAny<Func<string, object, RedirectToRouteResult>>()))
                .Returns(func);

            //when
            var result = _controller.Create(new ArticleViewModel(),
                new TestPostedFileBase());

            //then
            result.Should().NotBeNull();

            result.Should().BeSameAs(view);
        }

        [Test]
        public void Create_with_invalid_model_should_return_model_error()
        {
            //given
            _mediaUploadProvider.Setup(x =>
                                x.CreateMedaiItem(It.IsAny<Stream>(),
                                It.IsAny<string>(),
                                It.IsAny<string>()));

            _repository.Setup(x => x.Create(It.IsAny<Article>()))
                .Returns(false);

            //when
            var result = _controller.Create(new ArticleViewModel(), new TestPostedFileBase()) as ViewResult;

            //then
            result.Should().NotBeNull();

            result.ViewData.ModelState.Values.First().Errors.First()
                .ErrorMessage.Should().Be("Could not create article");
        }
    }

    class TestPostedFileBase : HttpPostedFileBase
    {
        private Stream _inputStream;
        private string _fileName;

        public TestPostedFileBase()
        {
            _inputStream = null;
            _fileName = null;
        }

        public override Stream InputStream { get { return _inputStream; } }

        public override string FileName { get { return _fileName; } }
    }
}
