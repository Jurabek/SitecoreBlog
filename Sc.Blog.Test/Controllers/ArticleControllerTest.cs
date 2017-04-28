using FluentAssertions;
using Moq;
using NUnit.Framework;
using Sc.Blog.Abstractions.Repositories;
using Sc.Blog.Model.Model;
using Sc.Blog.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Sc.Blog.Test.Controllers
{
    [TestFixture]
    public class ArticleControllerTest
    {
        private ArticleController _controller;
        private Mock<IRepository<Article, Guid>> _repository;

        [OneTimeSetUp]
        public void Init()
        {
            _repository = new Mock<IRepository<Article, Guid>>();
            _controller = new ArticleController(_repository.Object, null);
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
    }
}
