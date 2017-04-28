using FluentAssertions;
using Glass.Mapper.Sc;
using Moq;
using NUnit.Framework;
using Sc.Blog.Abstractions.Repositories;
using Sc.Blog.Core.Repositories;
using Sc.Blog.Model.Model;
using Sc.Blog.Model.Model.Folders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sc.Blog.Test.Repositories
{
    [TestFixture]
    public class ArticleRepositoryTest
    {
        private Mock<ISitecoreContext> _context;
        private IRepository<Article, Guid> _repository;

        [OneTimeSetUp]
        public void Init()
        {
            _context = new Mock<ISitecoreContext>();
        }

        [Test]
        public void Delete_with_correct_id_should_delete_item()
        {
            //given
            var articleGuid = Guid.NewGuid();
            _context.Setup(c => c.GetItem<ArticlesFolder>(It.IsAny<string>(), false, false))
                    .Returns(new ArticlesFolder
                    {
                        Children = new List<Article>
                        {
                            new Article{Id = articleGuid}
                        }
                    });

            _repository = new ArticleRepository(_context.Object);

            //when
            var result = _repository.Delete(articleGuid);

            //then
            result.Should().BeTrue();
        }

        [Test]
        public void Delete_with_wrong_id_should_not_delete_item()
        {
            //given
            _context.Setup(c => c.GetItem<ArticlesFolder>(It.IsAny<string>(), false, false))
                    .Returns(new ArticlesFolder());

            _repository = new ArticleRepository(_context.Object);

            //when
            var result = _repository.Delete(Guid.Empty);

            //then
            result.Should().BeFalse();
        }

        [Test]
        public void Get_with_correct_id_should_return_article()
        {
            //given
            var articleGuid = Guid.NewGuid();
            _context.Setup(c => c.GetItem<ArticlesFolder>(It.IsAny<string>(), false, false))
                    .Returns(new ArticlesFolder
                    {
                        Children = new List<Article>
                        {
                            new Article{ Id = articleGuid }
                        }
                    });

            _repository = new ArticleRepository(_context.Object);

            //when
            var result = _repository.Get(articleGuid);

            //then
            result.Id.Should().Be(articleGuid);
        }

        [Test]
        public void GetAll_should_return_articles()
        {
            //given
            _context.Setup(c => c.GetItem<ArticlesFolder>(It.IsAny<string>(), false, false))
                    .Returns(new ArticlesFolder
                    {
                        Children = new List<Article>
                        {
                            new Article()
                        }
                    });

            _repository = new ArticleRepository(_context.Object);

            //when
            var result = _repository.GetAll();

            //then
            result.Should().NotBeEmpty();

            result.Count().Should().Be(1);
        }

        [Test]
        public void Update_should_update_item()
        {
            //given
            _context.Setup(x => x.Save(It.IsAny<Article>(), false, false));

            //when
            _repository = new ArticleRepository(_context.Object);

            //then
            _repository.Update(new Article());
        }
    }
}
