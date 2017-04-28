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
    public class TopNavigationControllerTest
    {
        private Mock<IRepository<TopNavigation, Guid>> _repository;
        private TopNavigationController _controller;

        [OneTimeSetUp]
        public void Init()
        {
            _repository = new Mock<IRepository<TopNavigation, Guid>>();
            _controller = new TopNavigationController(_repository.Object);
        }

        [Test]
        public void Index_should_return_view_and_items()
        {
            //given
            _repository.Setup(x => x.GetAll())
                .Returns(new List<TopNavigation>());

            //when
            var result = _controller.Index() as ViewResult;

            //then
            result.ViewData.Model.Should().NotBeNull();
        }
    }
}
