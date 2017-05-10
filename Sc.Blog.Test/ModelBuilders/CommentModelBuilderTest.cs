using FluentAssertions;
using Glass.Mapper.Sc;
using Moq;
using NUnit.Framework;
using Sc.Blog.Abstractions.ModelBuilders;
using Sc.Blog.Abstractions.Repositories;
using Sc.Blog.Core.Mappers;
using Sc.Blog.Core.ModelBuilders;
using Sc.Blog.Model.Model;
using Sc.Blog.Model.ViewModels;
using Sitecore.Data.Items;
using Sitecore.FakeDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sc.Blog.Test.ModelBuilders
{
    [TestFixture]
    public class CommentModelBuilderTest
    {
        IModelBuilder<CommentViewModel> _commentModelBuilder;

        Mock<IRepository<Comment, Guid>> _commentRepository;
        Mock<ISitecoreContext> _context;

        [OneTimeSetUp]
        public void Init()
        {
            _commentRepository = new Mock<IRepository<Comment, Guid>>();
            _context = new Mock<ISitecoreContext>();
            _commentModelBuilder = new CommentModelBuilder(_commentRepository.Object, _context.Object);
            AutoMapperConfiguration.Configure();
        }

        [Test]
        public void Build_with_valid_model_should_create_comment()
        {
            //given
            _commentRepository.Setup(x => x.Create(It.IsAny<Comment>()))
                .Returns(true);

            var dbItem = new DbItem("Article") { };
            dbItem.Add(new DbField("Comments"));
            Db db = new Db() { dbItem };
            Item article = db.GetItem("/sitecore/content/article");

            _context.Setup(x => x.GetItem<Item>(It.IsAny<Guid>(), false, false))
                .Returns(article);
            //when
            var result = _commentModelBuilder.Build(new CommentViewModel());

            //then
            result.Should().BeTrue();
        }

        [Test]
        public void Build_with_wrong_model_should_create_errors()
        {
            //given
            var errorMessage = "Comment text should not be empty!";
            _commentRepository.Setup(x => x.Create(It.IsAny<Comment>()))
                .Returns(false);
            _commentRepository.SetupGet(x => x.RepositoryErrors)
                .Returns(new List<Exception> { new Exception(errorMessage) });
            //when
            var result = _commentModelBuilder.Build(null);

            //then
            result.Should().BeFalse();

            _commentModelBuilder.ModelErrors.LastOrDefault()
                .Message
                .Should()
                .Be(errorMessage);
        }

        [Test]
        public void Build_with_wrong_article_id_should_create_could_not_find_item_exception()
        {
            var errorMessage = "Could not find item!";
            //given
            _commentRepository.Setup(x => x.Create(It.IsAny<Comment>()))
                .Returns(true);

            _context.Setup(x => x.GetItem<Item>(It.IsAny<Guid>(), false, false))
                .Throws(new Exception(errorMessage));

            //when
            var result = _commentModelBuilder.Build(new CommentViewModel());

            //then
            result.Should().BeFalse();

            _commentModelBuilder
                .ModelErrors
                .LastOrDefault()
                .Message
                .Should()
                .Be(errorMessage);
        }
    }
}
