using Sc.Blog.Abstractions.Providers;
using Sc.Blog.Core.Providers;
using Sc.Blog.Model.ViewModels;
using Sc.Blog.Web.Constants;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Mvc.Configuration;
using System.Web.Mvc;

namespace Sc.Blog.Web.Controllers
{
    public class AccountController : Controller
    {
        private IAuthenticationProvider _authenticationProvider;

        public AccountController() : this(new AuthenticationProvider())
        {
        }

        public AccountController(IAuthenticationProvider authenticationProvider)
        {
            _authenticationProvider = authenticationProvider;
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
                    Item item = Sitecore.Context.Database.GetItem(Sitecore.Data.ID.Parse(TemplateId.HomapPageTemplateId));
                    var pathInfo = LinkManager.GetItemUrl(item, UrlOptions.DefaultOptions);
                    return RedirectToRoute(MvcSettings.SitecoreRouteName, new { pathInfo = pathInfo.TrimStart(new char[] { '/' }) });
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
            return View();
        }
    }
}