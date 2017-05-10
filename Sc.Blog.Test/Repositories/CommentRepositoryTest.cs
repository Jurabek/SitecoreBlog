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
using System.Text;
using System.Threading.Tasks;

namespace Sc.Blog.Test.Repositories
{

    [TestFixture]
    public class CommentRepositoryTest
    {
        IRepository<Comment, Guid> _repository;
        Mock<ISitecoreContext> _context;
        Guid commentId = Guid.NewGuid();

        [OneTimeSetUp]
        public void Init()
        {
            _context = new Mock<ISitecoreContext>();

            _context.Setup(x => x.GetItem<CommentsFolder>(It.IsAny<string>(), false, false))
                .Returns(new CommentsFolder
                {
                    Children = new List<Comment>
                    {
                        new Comment{ Id = commentId },
                        new Comment(),
                        new Comment(),
                        new Comment()
                    }
                });

            _repository = new CommentRepository(_context.Object);
        }

        [Test]
        public void Create_with_valid_data_should_create_item()
        {
            //given
            _context.Setup(x => x.Create(It.IsAny<CommentsFolder>(), It.IsAny<Comment>(), true, false));

            //when
            var result = _repository.Create(new Comment());

            //then
            result.Should().BeTrue();
        }

        [Test]
        public void Create_with_wrong_data_should_create_error()
        {
            //given
            var errorMessage = "Could not create comment";
            _context.Setup(x => x.Create(It.IsAny<CommentsFolder>(), It.IsAny<Comment>(), true, false))
                .Throws(new Exception(errorMessage));

            //when
            var result = _repository.Create(new Comment());

            //then
            result.Should().BeFalse();

            _repository.RepositoryErrors.LastOrDefault()
                .Message
                .Should().Be(errorMessage);
        }


        [Test]
        public void Delete_wiht_valid_guid_should_delete_item()
        {
            //given
            _context.Setup(x => x.Delete(It.IsAny<Comment>()));

            //when
            var result = _repository.Delete(commentId);

            //then
            result.Should().BeTrue();
        }

        [Test]
        public void Delete_with_invalid_guid_should_create_error()
        {
            //given
            _context.Setup(x => x.Delete(It.IsAny<Comment>()))
                .Throws(new Exception("Could not delete item"));

            //when
            var result = _repository.Delete(Guid.Empty);

            //then
            result.Should().BeFalse();

            _repository.RepositoryErrors
                .LastOrDefault()
                .Message
                .Should()
                .Be("Could not delete item");
        }

        [Test]
        public void Update_with_valid_data_should_update()
        {
            //given
            _context.Setup(x => x.Save(It.IsAny<Comment>(), true, false));

            //when
            var result = _repository.Update(new Comment());

            //then
            result.Should().BeTrue();
        }

        [Test]
        public void Update_with_invalid_data_should_create_errors()
        {
            _context.Setup(x => x.Save(It.IsAny<Comment>(), true, false))
                .Throws(new Exception("Could not update item"));

            var result = _repository.Update(new Comment());

            result.Should().BeFalse();

            _repository.RepositoryErrors.LastOrDefault()
                .Message
                .Should().Be("Could not update item");
        }

        [Test]
        public void GetAll_should_return_all_Items()
        {
            //when
            var result = _repository.GetAll();

            //then
            result.Any().Should().BeTrue();
        }

        [Test]
        public void Get_with_id_should_return_comment()
        {
            //when
            var result = _repository.Get(commentId);

            //then
            result.Should().NotBeNull();
        }
    }
}
