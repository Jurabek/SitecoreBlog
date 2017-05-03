using Sitecore.Data;
using System;
using System.Web.Mvc;

namespace Sc.Blog.Abstractions.Providers
{
    public interface IRouteProvider
    {
        ActionResult RedirectToItem(string itemPath, Func<string, object, RedirectToRouteResult> redirectToRoute);
    }
}