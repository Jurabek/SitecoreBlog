using System.Web.Security;

namespace Sc.Blog.Abstractions.Facades
{
    public interface IAuthenticationFacade
    {
        void LogOut();
        bool SignIn(string login, string password, bool rememberMe);
        MembershipUser SignUp(string login, string password, string email);
    }
}