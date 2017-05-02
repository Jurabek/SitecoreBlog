using Sc.Blog.Abstractions.Facades;
using Sc.Blog.Abstractions.Providers;
using Sc.Blog.Core.Providers;
using Sc.Blog.Model.ViewModels;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Mvc.Configuration;
using System.Web.Mvc;
using static Sc.Blog.Common.Constants;

namespace Sc.Blog.Web.Controllers
{
    public class AccountController : Controller
    {
        private IAuthenticationProvider _authenticationProvider;
        private IRouteProvider _routeProvider;

        public AccountController(IAuthenticationProvider authenticationProvider,
            IRouteProvider routeProvider)
        {
            _authenticationProvider = authenticationProvider;
            _routeProvider = routeProvider;
        }

        public ActionResult LogOut()
        {
            _authenticationProvider.LogOut();
            return RedirectToHome();
        }       

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(SignInViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (_authenticationProvider.SignIn(viewModel.Login, viewModel.Password, viewModel.RememberMe))
                {
                    return RedirectToHome();
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login or password!");
                }
            }
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(SignUpViewModel viewModel)
        {
            var user = _authenticationProvider.SignUp(viewModel.Login, viewModel.Password, viewModel.Email);
            return RedirectToHome();
        }

        private ActionResult RedirectToHome()
        {
            return _routeProvider.RedirectToItem(Templates.Home.ID, RedirectToRoute);
        }
    }
}