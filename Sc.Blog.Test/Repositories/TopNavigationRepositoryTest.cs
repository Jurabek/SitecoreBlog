using FluentAssertions;
using Glass.Mapper.Sc;
using Moq;
using NUnit.Framework;
using Sc.Blog.Core.Repositories;
using Sc.Blog.Model.Model;
using Sc.Blog.Model.Model.Folders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sc.Blog.Test.Repositories
{
    [TestFixture]
    public class TopNavigationRepositoryTest
    {
        private Mock<ISitecoreContext> _context;

        [OneTimeSetUp]
        public void Init()
        {
            _context = new Mock<ISitecoreContext>();
        }

        [Test]
        public void Delete_with_correct_id_should_delete_item()
        {
            var navigationItemId = Guid.NewGuid();

            //given
            _context.Setup(x => x.GetItem<NavigationItemsFolder>(It.IsAny<string>(),
                false, false))
                .Returns(new NavigationItemsFolder
                {
                    Children = new List<TopNavigation>
                    {
                        new TopNavigation { Id = navigationItemId }
                    }
                });

            var repository = new TopNavigationRepository(_context.Object);

            //when
            var result = repository.Delete(navigationItemId);

            //then
            result.Should().BeTrue();
        }

        [Test]
        public void Get_with_correct_id_should_return_item()
        {
            //given
            var navigationItemId = Guid.NewGuid();
            var navigationItem = new TopNavigation { Id = navigationItemId };

            //given
            _context.Setup(x => x.GetItem<NavigationItemsFolder>(It.IsAny<string>(),
                false, false))
                .Returns(new NavigationItemsFolder
                {
                    Children = new List<TopNavigation>
                    {
                        navigationItem
                    }
                });

            var repository = new TopNavigationRepository(_context.Object);

            //when
            var result = repository.Get(navigationItemId);

            //then

            result.Id.Should().Be(navigationItemId);

            result.Should().BeSameAs(navigationItem);
        }

        [Test]
        public void GetAll_should_return_all_items()
        {
            //given
            _context.Setup(x => x.GetItem<NavigationItemsFolder>(It.IsAny<string>(),
                false, false))
                .Returns(new NavigationItemsFolder
                {
                    Children = new List<TopNavigation>
                    {
                        new TopNavigation(),
                        new TopNavigation(),
                        new TopNavigation(),
                        new TopNavigation(),
                    }
                });

            var repository = new TopNavigationRepository(_context.Object);

            //when
            var result = repository.GetAll();

            //then
            result.Count().Should().Be(4);
        }

        [Test]
        public void Update_should_update_item()
        {
            //given
            var repository = new TopNavigationRepository(_context.Object);

            //when
            repository.Update(new TopNavigation());
        }

        [Test]
        public void Update_with_wrong_data_should_create_errors()
        {
            //given
            _context.Setup(x => x.Save(It.IsAny<TopNavigation>(), true, false))
                .Throws(new Exception("Could not update item"));
            var repository = new TopNavigationRepository(_context.Object);

            //when
            var result = repository.Update(null);

            //then
            result.Should().BeFalse();

            repository.RepositoryErrors
                .LastOrDefault()
                .Message
                .Should()
                .Be("Could not update item");
        }

        [Test]
        public void Create_with_valid_data_shoul_create_item()
        {
            //given
            _context.Setup(x => x.Create(It.IsAny<NavigationItemsFolder>(), It.IsAny<TopNavigation>(), true, false));
            var repository = new TopNavigationRepository(_context.Object);

            //when
            var result = repository.Create(new TopNavigation());

            //then
            result.Should().BeTrue();
        }

        [Test]
        public void Create_with_wrong_data_shoul_create_error()
        {
            //given
            _context.Setup(x => x.Create(It.IsAny<NavigationItemsFolder>(), It.IsAny<TopNavigation>(), true, false))
                .Throws(new Exception("Could not create top navigation"));
            var repository = new TopNavigationRepository(_context.Object);

            //when
            var result = repository.Create(new TopNavigation());

            //then
            result.Should().BeFalse();

            repository.RepositoryErrors.LastOrDefault().Message
                .Should().Be("Could not create top navigation");
        }

    }
}
