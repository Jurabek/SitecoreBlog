using Sc.Blog.Abstractions.Facades;
using Sc.Blog.Abstractions.Providers;
using Sc.Blog.Model.ViewModels;
using System.Web.Mvc;
using static Sc.Blog.Common.Constants;

namespace Sc.Blog.Web.Controllers
{
    public class AccountController : Controller
    {
        private IAuthenticationFacade _authenticationProvider;
        private IRouteProvider _routeProvider;

        public AccountController(IAuthenticationFacade authenticationProvider,
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
            return View(viewModel);
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(SignUpViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _authenticationProvider.SignUp(viewModel.Login, viewModel.Password, viewModel.Email);
                return RedirectToHome();
            }
            return View(viewModel);
        }

        private ActionResult RedirectToHome()
        {
            return _routeProvider.RedirectToItem(Folders.Content.Home, RedirectToRoute);
        }
    }
}