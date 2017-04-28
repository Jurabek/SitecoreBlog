using System.Web.Security;

namespace Sc.Blog.Abstractions.Providers
{
    public interface IAuthenticationProvider
    {
        void LogOut();
        bool SignIn(string login, string password, bool rememberMe);
        MembershipUser SignUp(string login, string password, string email);
    }
}