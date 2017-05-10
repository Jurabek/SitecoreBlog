using FluentAssertions;
using Moq;
using NUnit.Framework;
using Sc.Blog.Abstractions.Facades;
using Sc.Blog.Abstractions.Providers;
using Sc.Blog.Model.ViewModels;
using Sc.Blog.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sc.Blog.Test.Controllers
{
    [TestFixture]
    public class AccountControllerTest
    {
        private Mock<IAuthenticationFacade> _authenticationProvider;
        private Mock<IRouteProvider> _routerProvider;
        private AccountController _controller; 

        [OneTimeSetUp]
        public void Init()
        {
            _authenticationProvider = new Mock<IAuthenticationFacade>();
            _routerProvider = new Mock<IRouteProvider>();
            _controller = new AccountController(_authenticationProvider.Object, _routerProvider.Object);
            
            Func<string, object, RedirectToRouteResult> func = (routeName, routeValues) =>
            {
                return new RedirectToRouteResult("Home", new System.Web.Routing.RouteValueDictionary());
            };

            //given
            _routerProvider.Setup(x => x.RedirectToItem(It.IsAny<string>(),
                It.IsAny<Func<string, object, RedirectToRouteResult>>())).
                Returns(func);
        }

        [Test]
        public void LogOut_should_log_out_and_redirect_into_home()
        {
            //when
            var result = _controller.LogOut() as RedirectToRouteResult;

            //then
            result.Should().NotBeNull();

            result.RouteName.Should().Be("Home");
        }

        [Test]
        public void SignId_should_return_view()
        {
            //when
            var result = _controller.SignIn();

            //then
            result.Should().BeAssignableTo(typeof(ViewResult));
        }

        [Test]
        public void SignIn_with_wrong_data_should_return_model_error()
        {
            ClearModelState();

            //given
            string errorMessage = "Login required!";
            _controller.ModelState.AddModelError("error", errorMessage);

            //when
            var result = _controller.SignIn(null) as ViewResult;

            //then
            result.Should().NotBeNull();

            result.ViewData.ModelState
                .Values
                .First()
                .Errors
                .First()
                .ErrorMessage
                .Should()
                .Be(errorMessage);
        }

        [Test]
        public void Sign_with_data_should_sing_in_and_redirect_to_home()
        {
            //given
            ClearModelState();
            _authenticationProvider.Setup(x => x.SignIn(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>())).Returns(true);

            //when
            var result = _controller.SignIn(new SignInViewModel()) as RedirectToRouteResult;

            //then
            result.Should().NotBeNull();

            result.RouteName.Should().Be("Home");
        }

        [Test]
        public void SignIn_with_wrong_login_should_return_model_error()
        {
            //given
            ClearModelState();
            _authenticationProvider.Setup(x => x.SignIn(It.IsAny<string>(),
               It.IsAny<string>(),
               It.IsAny<bool>())).Returns(false);

            //when
            var result = _controller.SignIn(new SignInViewModel()) as ViewResult;

            //then
            result.ViewData.ModelState
                .Values
                .First()
                .Errors
                .First()
                .ErrorMessage
                .Should()
                .Be("Invalid login or password!");
        }
        [Test]
        public void SignUp_should_return_view()
        {
            //when
            ClearModelState();
            var result = _controller.SignUp() as ViewResult;

            //then
            result.Should().NotBeNull();
        }

        [Test]
        public void SignUp_with_valid_data_should_sign_up()
        {
            //given
            ClearModelState();
            _authenticationProvider.Setup(x => x.SignUp(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()));


            //when
            var result = _controller.SignUp(new SignUpViewModel()) as RedirectToRouteResult;

            //then
            result.RouteName.Should().Be("Home");
        }

        [Test]
        public void SignUp_with_wrong_data_model_errors()
        {
            //given
            string errorMessage = "Password not matched!";
            ClearModelState();
            _controller.ModelState.AddModelError("", errorMessage);
            
            //when
            var model = new SignUpViewModel();
            var result = _controller.SignUp(model) as ViewResult;

            //then
             result.ViewData.ModelState
                .Values
                .First()
                .Errors
                .First()
                .ErrorMessage
                .Should()
                .Be(errorMessage);

            result.ViewData.Model.Should().Be(model);
        }

        [Test]
        public void SignUp_with_wrong_data_should_return_model_error()
        {
            //given
            ClearModelState();
            string errorMessage = "Password not matched!";
            _controller.ModelState.AddModelError("error", errorMessage);

            //when
            var model = new SignInViewModel();
            var result = _controller.SignIn(model) as ViewResult;

            //then
            result.ViewData.ModelState
                .Values
                .First()
                .Errors
                .First()
                .ErrorMessage
                .Should()
                .Be(errorMessage);

            result.ViewData.Model.Should().Be(model);
        }

        protected void ClearModelState()
        {
            _controller.ModelState.Clear();
        }
    }
}
