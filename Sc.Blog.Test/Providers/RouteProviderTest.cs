using FluentAssertions;
using Moq;
using NUnit.Framework;
using Sc.Blog.Abstractions.Facades;
using Sc.Blog.Abstractions.Providers;
using Sc.Blog.Core.Providers;
using Sitecore.Data.Items;
using Sitecore.FakeDb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sc.Blog.Test.Providers
{
    [TestFixture]
    public class RouteProviderTest
    {
        IRouteProvider _routeProvider;
        Mock<ISitecoreSettingsFacade> _sitecoreSettingsFacade;
        Mock<ISitecoreDatabaseFacade> _sitecoreDatbaseFacade;
        Mock<ILinkManagerFacade> _linkManagerFacade;

        [OneTimeSetUp]
        public void Init()
        {
            _sitecoreSettingsFacade = new Mock<ISitecoreSettingsFacade>();
            _sitecoreDatbaseFacade = new Mock<ISitecoreDatabaseFacade>();
            _linkManagerFacade = new Mock<ILinkManagerFacade>();

            _routeProvider = new RouteProvider(_sitecoreSettingsFacade.Object,
                _sitecoreDatbaseFacade.Object,
                _linkManagerFacade.Object);
        }

        [Test]
        public void RedirectToItem_with_parameters_should_redirect_to_route()
        {
            //given
            Db db = new Db()
            {
                new DbItem("Home")
            };

            var item = db.GetItem("sitecore/content/home");

            _sitecoreDatbaseFacade.Setup(x => x.GetItem(It.IsAny<string>()))
                .Returns(item);

            _linkManagerFacade.Setup(x => x.GetItemUrl(It.IsAny<Item>()))
                .Returns("Home");

            _sitecoreSettingsFacade.SetupGet(x => x.SitecoreRouteName)
                .Returns("sitecore");

            Func<string, object, RedirectToRouteResult> redirectToRoute = (s, o) =>
            {
                var routes = AnonymousObjectToDictionary(o);
                return new RedirectToRouteResult(s, new RouteValueDictionary(routes));
            };

            //when
            var result = _routeProvider.RedirectToItem("home", redirectToRoute) as RedirectToRouteResult;

            //then
            result.RouteName.Should().Be("sitecore");
            result.RouteValues.FirstOrDefault().Value.Should().Be("Home");
        }

        public static IDictionary<string, object> AnonymousObjectToDictionary(object obj)
        {
            return TypeDescriptor.GetProperties(obj)
                .OfType<PropertyDescriptor>()
                .ToDictionary(
                    prop => prop.Name,
                    prop => prop.GetValue(obj)
                );
        }
    }
}
