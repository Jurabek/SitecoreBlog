using FluentAssertions;
using Moq;
using NUnit.Framework;
using Sc.Blog.Abstractions.Facades;
using Sc.Blog.Abstractions.Repositories;
using Sc.Blog.Core.Mappers;
using Sc.Blog.Core.ModelBuilders;
using Sc.Blog.Model.Model;
using Sc.Blog.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sc.Blog.Test.ModelBuilders
{
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

    [TestFixture]
    public class ArticleModelBuilderTest
    {
        private ArticleModelBuilder _modelBuider;
        private Mock<IRepository<Article, Guid>> _repository;
        private Mock<IMediaUploadFacade> _mediaUploadFacade;

        [OneTimeSetUp]
        public void Init()
        {
            AutoMapperConfiguration.Configure();

            _repository = new Mock<IRepository<Article, Guid>>();
            _mediaUploadFacade = new Mock<IMediaUploadFacade>();
            _modelBuider = new ArticleModelBuilder(_repository.Object, _mediaUploadFacade.Object);

            _mediaUploadFacade.Setup(x =>
                               x.CreateMedaiItem(It.IsAny<Stream>(),
                               It.IsAny<string>(),
                               It.IsAny<string>()));
        }

        [Test]
        public void Build_with_valid_data_should_create_article()
        {
            //given
            _repository.Setup(x => x.Create(It.IsAny<Article>()))
                .Returns(true);

            //when
            var result = _modelBuider.Build(new ArticleViewModel(), new TestPostedFileBase());
            //then

            result.Should().BeTrue();
        }

        [Test]
        public void Build_with_wrong_data_should_create_error()
        {
            string errorMessage = "Article should be null!";
            //given
            _repository.Setup(x => x.Create(null)).Returns(false);
            _repository.SetupGet(x => x.RepositoryErrors)
                .Returns(new List<Exception> { new Exception(errorMessage) });

            //when
            var result = _modelBuider.Build(null, null);

            //then
            result.Should().BeFalse();

            _modelBuider.ModelErrors.FirstOrDefault().Message.Should().Be(errorMessage);
        }

    }
}
