using Sc.Blog.Abstractions.Providers;
using Sitecore.Security.Authentication;
using Sitecore.Security.Domains;
using System.Web.Security;

namespace Sc.Blog.Core.Providers
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        private readonly Domain domain = Sitecore.Context.Domain;

        public MembershipUser SignUp(string login, string password, string email)
        {
            return Membership.CreateUser($"{domain.Name}\\{login}", password, email);
        }

        public bool SignIn(string login, string password, bool rememberMe)
        {
            return AuthenticationManager.Login($"{domain.Name}\\{login}", password, rememberMe);
        }

        public void LogOut()
        {
            AuthenticationManager.Logout();
        }
    }
}
